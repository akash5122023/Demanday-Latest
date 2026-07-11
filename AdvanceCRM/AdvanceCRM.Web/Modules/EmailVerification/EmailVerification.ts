namespace AdvanceCRM.EmailVerification {

    interface EmailVerificationResult {
        Success: boolean;
        Email?: string;
        Status?: string;
        Message?: string;
        FromCache?: boolean;
        LimitReached?: boolean;
        VerifiedDate?: string;
        Allowed?: number;
        Used?: number;
        Remaining?: number;
    }

    interface BulkVerificationResult {
        Success: boolean;
        FileId?: string;
        Status?: string;
        Percentage?: string;
        Message?: string;
    }

    var BulkPollMs = 4000;

    interface ContactSearchItem {
        Source?: string;
        Id?: number;
        CampaignId?: string;
        FirstName?: string;
        LastName?: string;
        Email?: string;
        CompanyName?: string;
        Title?: string;
        WorkPhone?: string;
        Country?: string;
    }

    interface ContactSearchResult {
        Success: boolean;
        Message?: string;
        Items?: ContactSearchItem[];
        Truncated?: boolean;
        CacheHit?: boolean;
        CachedEmail?: string;
        CachedStatus?: string;
        CachedMessage?: string;
        CachedVerifiedDate?: string;
    }

    interface QuotaStatusResult {
        Success: boolean;
        Allowed?: number;
        Used?: number;
        Remaining?: number;
        CanManageQuota?: boolean;
    }

    interface QuotaAdminItem {
        UserId: number;
        Username?: string;
        DisplayName?: string;
        AllowedCount: number;
        UsedCount: number;
    }

    interface QuotaListResult {
        Success: boolean;
        Message?: string;
        CanManageQuota?: boolean;
        Items?: QuotaAdminItem[];
    }

    /** Keep in sync with MinSearchLength in EmailVerificationPage.cs. */
    var MinSearchLength = 2;
    var SearchDebounceMs = 300;

    /**
     * Email Verification tool page: enter an email and Verify / Trace it, or start a
     * Bulk verification. A per-user search quota limits how many fresh verifications a
     * user may run; results are cached and shared, so a previously-verified email is shown
     * for free (and surfaced as the user types). Admins can set each user's quota inline.
     */
    export class EmailVerificationPage {

        private element: JQuery;
        private emailInput: JQuery;
        private resultBox: JQuery;
        private gridBox: JQuery;
        private quotaBox: JQuery;
        private cachedBox: JQuery;
        private adminBox: JQuery;

        private searchTimer: any;
        // Incremented per search; a response whose token is stale gets dropped, so a slow
        // reply for "jo" can never overwrite the newer results for "john".
        private searchToken = 0;

        private adminItems: QuotaAdminItem[] = [];

        constructor(element: JQuery) {
            this.element = element;
            this.render();
            this.loadQuotaStatus();
        }

        private render() {
            var el = this.element;
            el.addClass('email-verification-page');

            $('<div class="ev-heading"></div>').text('Continue with email').appendTo(el);

            // Populated asynchronously by loadQuotaStatus().
            this.quotaBox = $('<div class="ev-quota" style="display:none"></div>').appendTo(el);
            this.adminBox = $('<div class="ev-admin"></div>').appendTo(el);

            this.emailInput = $('<input type="email" class="ev-input" placeholder="Email address">')
                .appendTo(el)
                .on('input', () => this.queueContactSearch());
            $('<div class="ev-hint"></div>')
                .text('Verify any Email Address at our free Email Checker to see if it exists')
                .appendTo(el);

            // "Already verified" note shown as the user types a full email.
            this.cachedBox = $('<div class="ev-cached" style="display:none"></div>').appendTo(el);

            var actions = $('<div class="ev-actions"></div>').appendTo(el);
            $('<button type="button" class="ev-btn"></button>')
                .text('Verify Email')
                .appendTo(actions)
                .on('click', () => this.verify());
            var bulkRow = $('<div class="ev-bulk-row"></div>').appendTo(actions);
            $('<button type="button" class="ev-btn"></button>')
                .text('Start Bulk Email Verification')
                .appendTo(bulkRow)
                .on('click', () => this.bulkVerify());
            $('<button type="button" class="ev-btn ev-btn-light"></button>')
                .text('Bulk Template')
                .appendTo(bulkRow)
                .on('click', () => {
                    window.location.href = Q.resolveUrl('~/EmailVerification/DownloadBulkTemplate');
                });
            $('<button type="button" class="ev-btn"></button>')
                .text('Trace Email')
                .appendTo(actions)
                .on('click', () => this.trace());

            this.resultBox = $('<div class="ev-result"></div>').appendTo(el);
            this.gridBox = $('<div class="ev-grid-box"></div>').appendTo(el);
        }

        // ---- Quota banner ----

        private loadQuotaStatus() {
            fetch(Q.resolveUrl('~/EmailVerification/QuotaStatus'), { method: 'POST' })
                .then(r => r.json())
                .then((res: QuotaStatusResult) => {
                    if (!res || !res.Success)
                        return;
                    this.renderQuota(res.Allowed || 0, res.Used || 0, res.Remaining || 0, !!res.CanManageQuota);
                })
                .catch(() => { /* leave the banner hidden if the status can't be fetched */ });
        }

        /** Renders (and shows) the quota banner. Called on load and after each verify. */
        private renderQuota(allowed: number, used: number, remaining: number, canManage: boolean) {
            this.quotaBox.empty().css('display', 'flex');

            $('<span class="ev-quota-label"></span>').text('Search quota').appendTo(this.quotaBox);

            var pillClass = 'ev-quota-pill';
            if (remaining <= 0) pillClass += ' ev-quota-empty';
            else if (remaining <= Math.max(1, Math.round(allowed * 0.1))) pillClass += ' ev-quota-low';

            $('<span></span>').addClass(pillClass)
                .text('Used ' + used + ' / ' + allowed + '  •  ' + remaining + ' left')
                .appendTo(this.quotaBox);

            if (canManage) {
                $('<button type="button" class="ev-quota-manage"></button>')
                    .text('Manage Quotas')
                    .appendTo(this.quotaBox)
                    .on('click', () => this.toggleAdmin());
            }
        }

        /** After a verify, the response carries the fresh counts; keep the banner in sync. */
        private updateQuotaFromResult(res: EmailVerificationResult) {
            if (res == null || res.Allowed == null)
                return;
            // Preserve the Manage button by re-reading its presence from the current banner.
            var canManage = this.quotaBox.find('.ev-quota-manage').length > 0;
            this.renderQuota(res.Allowed || 0, res.Used || 0, res.Remaining || 0, canManage);
        }

        // ---- As-you-type contact lookup + cached verification note ----

        /** Debounce the keystrokes so we hit the server once the user pauses, not per key. */
        private queueContactSearch() {
            if (this.searchTimer)
                clearTimeout(this.searchTimer);

            var term = this.getEmail();
            if (term.length < MinSearchLength) {
                // Below the threshold there is nothing to show – drop any in-flight response too.
                this.searchToken++;
                this.gridBox.empty();
                this.hideCached();
                return;
            }

            this.searchTimer = setTimeout(() => this.searchContacts(term), SearchDebounceMs);
        }

        private searchContacts(term: string) {
            var token = ++this.searchToken;

            var fd = new FormData();
            fd.append('email', term);

            fetch(Q.resolveUrl('~/EmailVerification/SearchContacts'), { method: 'POST', body: fd })
                .then(r => r.json())
                .then((res: ContactSearchResult) => {
                    if (token !== this.searchToken)
                        return; // a newer search already went out
                    this.renderCached(res);
                    this.renderGrid(res);
                })
                .catch(() => {
                    if (token !== this.searchToken)
                        return;
                    this.hideCached();
                    this.gridBox.html('<div class="ev-grid-empty">Contact search failed.</div>');
                });
        }

        private renderCached(res: ContactSearchResult) {
            if (!res || !res.CacheHit) {
                this.hideCached();
                return;
            }

            var status = (res.CachedStatus || 'unknown').toLowerCase();
            var cls = 'ev-cached ev-cached-other';
            var tag = res.CachedStatus || 'Unknown';
            if (status === 'valid') { cls = 'ev-cached ev-cached-valid'; tag = 'Valid ✓'; }
            else if (status === 'invalid') { cls = 'ev-cached ev-cached-invalid'; tag = 'Invalid ✕'; }

            var html = '<span class="ev-cached-tag">Already verified: ' + Q.htmlEncode(tag) + '</span>';
            if (res.CachedMessage)
                html += ' <span>' + Q.htmlEncode(res.CachedMessage) + '</span>';
            if (res.CachedVerifiedDate)
                html += '<div class="ev-cached-meta">Verified on ' + Q.htmlEncode(res.CachedVerifiedDate) +
                    ' — shown from shared results, no quota used.</div>';

            this.cachedBox.attr('class', cls).html(html).show();
        }

        private hideCached() {
            this.cachedBox.hide().empty();
        }

        private renderGrid(res: ContactSearchResult) {
            this.gridBox.empty();

            if (!res || !res.Success) {
                this.gridBox.html('<div class="ev-grid-empty">' +
                    Q.htmlEncode(res && res.Message ? res.Message : 'Contact search failed.') + '</div>');
                return;
            }

            var items = res.Items || [];
            if (items.length === 0) {
                this.gridBox.html('<div class="ev-grid-empty">No matching contacts.</div>');
                return;
            }

            var html = '<div class="ev-grid-title">Matching contacts (' + items.length + ')</div>';
            if (res.Truncated && res.Message)
                html += '<div class="ev-grid-note">' + Q.htmlEncode(res.Message) + '</div>';

            html += '<div class="ev-grid-scroll"><table class="ev-grid"><thead><tr>' +
                '<th>Module</th><th>Campaign</th><th>First Name</th><th>Last Name</th>' +
                '<th>Email</th><th>Company</th><th>Title</th><th>Work Phone</th><th>Country</th>' +
                '</tr></thead><tbody>';

            for (var i = 0; i < items.length; i++) {
                var it = items[i];
                var badge = it.Source === 'TM ETContact' ? 'ev-badge-tm' : 'ev-badge-dd';
                html += '<tr>' +
                    '<td><span class="ev-badge ' + badge + '">' + Q.htmlEncode(it.Source || '') + '</span></td>' +
                    '<td>' + Q.htmlEncode(it.CampaignId || '') + '</td>' +
                    '<td>' + Q.htmlEncode(it.FirstName || '') + '</td>' +
                    '<td>' + Q.htmlEncode(it.LastName || '') + '</td>' +
                    '<td>' + Q.htmlEncode(it.Email || '') + '</td>' +
                    '<td>' + Q.htmlEncode(it.CompanyName || '') + '</td>' +
                    '<td>' + Q.htmlEncode(it.Title || '') + '</td>' +
                    '<td>' + Q.htmlEncode(it.WorkPhone || '') + '</td>' +
                    '<td>' + Q.htmlEncode(it.Country || '') + '</td>' +
                    '</tr>';
            }

            html += '</tbody></table></div>';
            this.gridBox.html(html);
        }

        private getEmail(): string {
            return $.trim(String(this.emailInput.val() || ''));
        }

        private verify() {
            var email = this.getEmail();
            if (!email) {
                Q.notifyError('Please enter an email address.');
                return;
            }
            this.post('~/EmailVerification/Verify', { email: email });
        }

        private trace() {
            var email = this.getEmail();
            if (!email) {
                Q.notifyError('Please enter an email address.');
                return;
            }
            this.post('~/EmailVerification/Trace', { email: email });
        }

        // ---- Admin: manage per-user quotas ----

        private toggleAdmin() {
            if (this.adminBox.is(':visible')) {
                this.adminBox.hide();
                return;
            }
            this.adminBox.show();
            this.loadQuotaList();
        }

        private loadQuotaList() {
            this.adminBox.html('<div class="ev-admin-title">User search quotas</div>' +
                '<div class="ev-grid-empty">Loading…</div>');

            fetch(Q.resolveUrl('~/EmailVerification/ListQuota'), { method: 'POST' })
                .then(r => r.json())
                .then((res: QuotaListResult) => {
                    if (!res || !res.Success) {
                        this.adminBox.html('<div class="ev-admin-title">User search quotas</div>' +
                            '<div class="ev-grid-empty">' +
                            Q.htmlEncode(res && res.Message ? res.Message : 'Could not load quotas.') + '</div>');
                        return;
                    }
                    this.adminItems = res.Items || [];
                    this.renderAdmin('');
                })
                .catch(() => {
                    this.adminBox.html('<div class="ev-admin-title">User search quotas</div>' +
                        '<div class="ev-grid-empty">Could not load quotas.</div>');
                });
        }

        private renderAdmin(filter: string) {
            var lower = (filter || '').toLowerCase();
            var items = this.adminItems.filter(it => {
                if (!lower) return true;
                return (it.Username || '').toLowerCase().indexOf(lower) >= 0 ||
                    (it.DisplayName || '').toLowerCase().indexOf(lower) >= 0;
            });

            this.adminBox.empty();
            $('<div class="ev-admin-title"></div>').text('User search quotas').appendTo(this.adminBox);

            var search = $('<input type="text" class="ev-admin-search" placeholder="Filter users…">')
                .val(filter)
                .appendTo(this.adminBox)
                .on('input', (e) => this.renderAdmin(String($(e.target).val() || '')));

            var scroll = $('<div class="ev-admin-scroll"></div>').appendTo(this.adminBox);
            var table = $('<table class="ev-admin-table"><thead><tr>' +
                '<th>User</th><th>Used</th><th>Allowed</th><th>Reset</th><th></th>' +
                '</tr></thead><tbody></tbody></table>').appendTo(scroll);
            var tbody = table.find('tbody');

            if (items.length === 0) {
                tbody.append('<tr><td colspan="5" class="ev-grid-empty">No users.</td></tr>');
            }

            items.forEach(it => {
                var name = it.DisplayName || it.Username || ('User #' + it.UserId);
                var tr = $('<tr></tr>').appendTo(tbody);
                var userCell = $('<td></td>').appendTo(tr);
                $('<div></div>').css('font-weight', '600').text(name).appendTo(userCell);
                if (it.Username && it.DisplayName)
                    $('<div class="ev-cached-meta"></div>').text(it.Username).appendTo(userCell);

                $('<td class="ev-admin-used"></td>').text(String(it.UsedCount)).appendTo(tr);

                var allowedInput = $('<input type="number" min="0" class="ev-admin-allowed">')
                    .val(String(it.AllowedCount));
                $('<td></td>').append(allowedInput).appendTo(tr);

                var resetCb = $('<input type="checkbox">');
                $('<td></td>').append(resetCb).appendTo(tr);

                var saveBtn = $('<button type="button" class="ev-admin-save"></button>').text('Save');
                $('<td></td>').append(saveBtn).appendTo(tr);

                saveBtn.on('click', () => {
                    var allowed = parseInt(String(allowedInput.val()), 10);
                    if (isNaN(allowed) || allowed < 0) {
                        Q.notifyError('Enter a valid allowed count.');
                        return;
                    }
                    this.saveQuota(it, allowed, resetCb.is(':checked'), resetCb, saveBtn);
                });
            });
        }

        private saveQuota(item: QuotaAdminItem, allowed: number, resetUsed: boolean, resetCb: JQuery, saveBtn: JQuery) {
            saveBtn.prop('disabled', true);

            var fd = new FormData();
            fd.append('userId', String(item.UserId));
            fd.append('allowedCount', String(allowed));
            fd.append('resetUsed', resetUsed ? 'true' : 'false');

            fetch(Q.resolveUrl('~/EmailVerification/SetQuota'), { method: 'POST', body: fd })
                .then(r => r.json())
                .then((res: { Success: boolean; Message?: string }) => {
                    saveBtn.prop('disabled', false);
                    if (!res || !res.Success) {
                        Q.notifyError(res && res.Message ? res.Message : 'Could not save quota.');
                        return;
                    }
                    item.AllowedCount = allowed;
                    if (resetUsed) {
                        item.UsedCount = 0;
                        resetCb.prop('checked', false);
                        saveBtn.closest('tr').find('.ev-admin-used').text('0');
                    }
                    Q.notifySuccess('Quota updated for ' + (item.DisplayName || item.Username || ('User #' + item.UserId)) + '.');
                    // If the admin changed their own quota, refresh the top banner too.
                    this.loadQuotaStatus();
                })
                .catch(() => {
                    saveBtn.prop('disabled', false);
                    Q.notifyError('Could not save quota.');
                });
        }

        private bulkVerify() {
            var fileInput = document.createElement('input');
            fileInput.type = 'file';
            fileInput.accept = '.csv,.xlsx,.xls,.txt';
            fileInput.style.display = 'none';
            document.body.appendChild(fileInput);
            fileInput.onchange = () => {
                if (fileInput.files && fileInput.files.length > 0) {
                    var fd = new FormData();
                    fd.append('file', fileInput.files[0]);
                    this.showBulkInfo('Uploading “' + fileInput.files[0].name + '” …');
                    fetch(Q.resolveUrl('~/EmailVerification/BulkVerify'), { method: 'POST', body: fd })
                        .then(r => r.json())
                        .then((res: BulkVerificationResult) => {
                            if (!res || !res.Success || !res.FileId) {
                                this.showError(res && res.Message ? res.Message : 'Bulk upload failed.');
                                return;
                            }
                            this.showBulkInfo('File accepted. Verifying…');
                            this.pollBulk(res.FileId);
                        })
                        .catch(err => this.showError('Bulk verification failed: ' + err));
                }
                if (fileInput.parentNode) document.body.removeChild(fileInput);
            };
            fileInput.click();
        }

        /** Polls the server until ZeroBounce reports the file Complete, then shows a download link. */
        private pollBulk(fileId: string) {
            var fd = new FormData();
            fd.append('fileId', fileId);
            fetch(Q.resolveUrl('~/EmailVerification/BulkStatus'), { method: 'POST', body: fd })
                .then(r => r.json())
                .then((res: BulkVerificationResult) => {
                    if (!res || !res.Success) {
                        this.showError(res && res.Message ? res.Message : 'Status check failed.');
                        return;
                    }
                    var status = (res.Status || '').toLowerCase();
                    if (status === 'complete') {
                        this.showBulkComplete(fileId);
                    }
                    else if (status === 'error' || res.Message) {
                        this.showError(res.Message || 'The file could not be processed.');
                    }
                    else {
                        this.showBulkInfo('Verifying… ' + (res.Percentage || '') +
                            ' (' + (res.Status || 'processing') + ')');
                        setTimeout(() => this.pollBulk(fileId), BulkPollMs);
                    }
                })
                .catch(err => this.showError('Status check failed: ' + err));
        }

        private showBulkInfo(message: string) {
            this.resultBox.removeClass('ev-error')
                .html('<div class="ev-result-title">Bulk verification</div><div>' +
                    Q.htmlEncode(message) + '</div>')
                .show();
        }

        private showBulkComplete(fileId: string) {
            var link = Q.resolveUrl('~/EmailVerification/BulkResult?fileId=' + encodeURIComponent(fileId));
            this.resultBox.removeClass('ev-error')
                .html('<div class="ev-result-title">Bulk verification complete</div>' +
                    '<div>Your results are ready.</div>' +
                    '<div style="margin-top:10px"><a class="ev-download" href="' + link + '">Download results (CSV)</a></div>')
                .show();
        }

        private post(url: string, data: any) {
            var fd = new FormData();
            for (var key in data)
                if (data.hasOwnProperty(key)) fd.append(key, data[key]);

            fetch(Q.resolveUrl(url), { method: 'POST', body: fd })
                .then(r => r.json())
                .then((res: EmailVerificationResult) => this.showResult(res))
                .catch(err => this.showError('Request failed: ' + err));
        }

        private showResult(res: EmailVerificationResult) {
            if (!res || !res.Success) {
                // The quota may have been reported even on a refusal – keep the banner accurate.
                this.updateQuotaFromResult(res);
                this.showError(res && res.Message ? res.Message : 'Something went wrong.');
                return;
            }
            var html = '<div class="ev-result-title">Result</div>';
            if (res.Email) html += '<div>Email: ' + Q.htmlEncode(res.Email) + '</div>';
            if (res.Status) html += '<div>Status: ' + Q.htmlEncode(res.Status) + '</div>';
            if (res.Message) html += '<div>' + Q.htmlEncode(res.Message) + '</div>';
            if (res.FromCache) {
                html += '<div class="ev-cached-meta">Shown from shared results' +
                    (res.VerifiedDate ? ' (verified on ' + Q.htmlEncode(res.VerifiedDate) + ')' : '') +
                    ' — no quota used.</div>';
            }
            this.resultBox.removeClass('ev-error').html(html).show();
            this.updateQuotaFromResult(res);
        }

        private showError(message: string) {
            this.resultBox.addClass('ev-error')
                .html('<div class="ev-result-title">Error</div><div>' + Q.htmlEncode(message) + '</div>')
                .show();
        }
    }
}
