namespace AdvanceCRM.Common {

    /** Options shared by the upload (import) and download (export) progress helpers. */
    export interface TransferProgressOptions {
        /** Heading on the progress panel, e.g. "Importing Specification". */
        title?: string;
        /** Called when the user cancels; the transfer is aborted before this runs. */
        onCancel?: () => void;
        /** Called with a human readable message when the transfer fails (not on cancel). */
        onError?: (message: string) => void;
    }

    export interface UploadProgressOptions extends TransferProgressOptions {
        /** Endpoint the file is posted to ("~/" prefixed or absolute). */
        url: string;
        /** Body of the POST — the file plus any extra fields. */
        formData: FormData;
        /** Status text while the server works on the file the browser already sent. */
        processingText?: string;
        /** Receives the server's response body once the import finished. */
        onSuccess?: (responseText: string) => void;
    }

    export interface DownloadProgressOptions extends TransferProgressOptions {
        /** Endpoint the file is fetched from ("~/" prefixed or absolute). */
        url: string;
        /** Posted as ordinary form fields; without both this and `request` the file is fetched with GET. */
        fields?: { [key: string]: any };
        /** Forces the verb — needed for an endpoint that is POST-only but takes no fields. */
        method?: string;
        /** A Serenity service request — posted as the "request" field, exactly like Q.postToService. */
        request?: any;
        /** Name used to save the file when the response carries no Content-Disposition. */
        fileName?: string;
        /** Status text while the server builds the file, before any bytes arrive. */
        preparingText?: string;
        /** Called once the file has been handed to the browser to save. */
        onSuccess?: () => void;
    }

    /**
     * Import and export both move files that can run into hundreds of MB, and until they finish the
     * user has no idea whether anything is happening. These helpers run the transfer over XMLHttpRequest
     * (instead of a plain form post / fetch, which report nothing) and show a modal panel with the live
     * percentage, so the user can see progress and knows to wait.
     */
    export namespace TransferProgress {

        /** Bytes as "12.4 MB" — the panel's secondary line, so the percentage has some scale to it. */
        function formatBytes(bytes: number): string {
            if (bytes == null || isNaN(bytes))
                return '';
            if (bytes < 1024)
                return bytes + ' B';
            if (bytes < 1024 * 1024)
                return (bytes / 1024).toFixed(1) + ' KB';
            if (bytes < 1024 * 1024 * 1024)
                return (bytes / (1024 * 1024)).toFixed(1) + ' MB';
            return (bytes / (1024 * 1024 * 1024)).toFixed(2) + ' GB';
        }

        /** The panel is used from every module, so its CSS is injected once from here. */
        function ensureStyles() {
            if (document.getElementById('tp-progress-style'))
                return;

            var css =
                '.tp-overlay{position:fixed;inset:0;top:0;left:0;right:0;bottom:0;z-index:100000;' +
                'background:rgba(20,32,52,.45);display:flex;align-items:center;justify-content:center;}' +
                '.tp-panel{background:#fff;border-radius:12px;box-shadow:0 12px 40px rgba(10,25,60,.28);' +
                'padding:22px 26px;min-width:360px;max-width:90vw;font-size:13px;color:#34425a;}' +
                '.tp-title{font-size:15px;font-weight:700;color:#1b62d6;margin-bottom:14px;}' +
                '.tp-bar{position:relative;height:12px;border-radius:8px;background:#eaeef6;overflow:hidden;}' +
                '.tp-fill{height:100%;width:0;background:#1b62d6;border-radius:8px;transition:width .15s linear;}' +
                '.tp-bar.tp-indeterminate .tp-fill{width:35%;background:#1b62d6;' +
                'animation:tp-slide 1.1s ease-in-out infinite;}' +
                '@keyframes tp-slide{0%{margin-left:-35%}100%{margin-left:100%}}' +
                '.tp-meta{display:flex;justify-content:space-between;align-items:center;margin-top:10px;gap:14px;}' +
                '.tp-status{color:#55637a;}' +
                '.tp-percent{font-weight:700;font-size:16px;color:#1b62d6;white-space:nowrap;}' +
                '.tp-actions{margin-top:16px;text-align:right;}' +
                '.tp-cancel{background:#fff;color:#c0392b;border:1px solid #f0c8c2;border-radius:6px;' +
                'padding:5px 14px;font-size:12px;font-weight:600;cursor:pointer;}' +
                '.tp-cancel:hover{background:#fdf3f2;}' +
                '.tp-note{margin-top:10px;color:#8a95a6;font-size:12px;}';

            var style = document.createElement('style');
            style.id = 'tp-progress-style';
            style.appendChild(document.createTextNode(css));
            document.getElementsByTagName('head')[0].appendChild(style);
        }

        interface Panel {
            setTitle(text: string): void;
            setStatus(text: string): void;
            /** A percentage of 0..100; anything negative switches the bar to "unknown / working". */
            setPercent(percent: number): void;
            /** Hides the Cancel button once cancelling no longer makes sense. */
            hideCancel(): void;
            close(): void;
        }

        /** Builds the modal panel. The dim backdrop is what stops the user from clicking on. */
        function openPanel(title: string, onCancel: () => void): Panel {
            ensureStyles();

            var overlay = $('<div class="tp-overlay"></div>').appendTo(document.body);
            var panel = $('<div class="tp-panel"></div>').appendTo(overlay);
            var titleEl = $('<div class="tp-title"></div>').text(title || 'Please wait…').appendTo(panel);
            var bar = $('<div class="tp-bar"></div>').appendTo(panel);
            var fill = $('<div class="tp-fill"></div>').appendTo(bar);
            var meta = $('<div class="tp-meta"></div>').appendTo(panel);
            var statusEl = $('<span class="tp-status"></span>').appendTo(meta);
            var percentEl = $('<span class="tp-percent"></span>').appendTo(meta);
            $('<div class="tp-note">Please keep this page open until it finishes.</div>').appendTo(panel);
            var actions = $('<div class="tp-actions"></div>').appendTo(panel);
            var cancelBtn = $('<button type="button" class="tp-cancel">Cancel</button>')
                .appendTo(actions)
                .on('click', () => onCancel());

            return {
                setTitle: text => titleEl.text(text),
                setStatus: text => statusEl.text(text || ''),
                setPercent: percent => {
                    if (percent == null || percent < 0 || isNaN(percent)) {
                        bar.addClass('tp-indeterminate');
                        fill.css('width', '');
                        percentEl.text('');
                        return;
                    }
                    var pct = Math.max(0, Math.min(100, Math.round(percent)));
                    bar.removeClass('tp-indeterminate');
                    fill.css('width', pct + '%');
                    percentEl.text(pct + '%');
                },
                hideCancel: () => cancelBtn.hide(),
                close: () => overlay.remove()
            };
        }

        /** Serenity guards its POST endpoints with the CSRF cookie; mirror what Q.serviceCall sends. */
        function csrfToken(): string {
            var token = Q.getCookie('CSRF-TOKEN');
            return token ? String(token) : null;
        }

        /**
         * Uploads a file and reports the sent percentage, then switches to "processing" while the
         * server imports it (that phase has no percentage — the browser is only waiting for a reply).
         */
        export function upload(options: UploadProgressOptions) {
            var cancelled = false;
            var xhr = new XMLHttpRequest();
            var panel = openPanel(options.title || 'Uploading file…', () => {
                cancelled = true;
                xhr.abort();
                panel.close();
                if (options.onCancel)
                    options.onCancel();
            });
            panel.setPercent(0);
            panel.setStatus('Starting upload…');

            xhr.open('POST', Q.resolveUrl(options.url), true);
            var token = csrfToken();
            if (token)
                xhr.setRequestHeader('X-CSRF-TOKEN', token);

            xhr.upload.onprogress = e => {
                if (!e.lengthComputable) {
                    panel.setPercent(-1);
                    panel.setStatus('Uploading ' + formatBytes(e.loaded) + '…');
                    return;
                }
                panel.setPercent(e.loaded * 100 / e.total);
                panel.setStatus('Uploading ' + formatBytes(e.loaded) + ' of ' + formatBytes(e.total));
            };

            // Everything is on the wire — from here the server is reading the sheet and writing rows,
            // which reports no progress, so the bar goes indeterminate and cancelling is pointless.
            xhr.upload.onload = () => {
                panel.setPercent(-1);
                panel.setStatus(options.processingText || 'Uploaded. Processing on the server…');
                panel.hideCancel();
            };

            xhr.onload = () => {
                panel.close();
                if (cancelled)
                    return;
                if (xhr.status >= 200 && xhr.status < 300) {
                    if (options.onSuccess)
                        options.onSuccess(xhr.responseText);
                    return;
                }
                var message = Q.isEmptyOrNull(xhr.responseText)
                    ? 'Upload failed (HTTP ' + xhr.status + ').'
                    : xhr.responseText;
                if (options.onError)
                    options.onError(message);
                else
                    Q.notifyError(message);
            };

            xhr.onerror = () => {
                panel.close();
                if (cancelled)
                    return;
                var message = 'Upload failed — the connection to the server was lost.';
                if (options.onError)
                    options.onError(message);
                else
                    Q.notifyError(message);
            };

            xhr.send(options.formData);
        }

        /**
         * Downloads a file and reports the received percentage, then saves it under the name the
         * server sent. A plain link / form post gives the user no feedback at all on a big export.
         */
        export function download(options: DownloadProgressOptions) {
            var cancelled = false;
            var xhr = new XMLHttpRequest();
            var panel = openPanel(options.title || 'Downloading file…', () => {
                cancelled = true;
                xhr.abort();
                panel.close();
                if (options.onCancel)
                    options.onCancel();
            });
            // The server builds the whole workbook before the first byte arrives, so start on the
            // "working" bar rather than a 0% that would sit there looking stuck.
            panel.setPercent(-1);
            panel.setStatus(options.preparingText || 'Preparing the file on the server…');

            // Anything carrying fields is posted as a form; an explicit method wins, so a POST-only
            // endpoint that takes no fields (a template download) can still be asked for.
            var body: string = null;
            var method = (options.method || '').toUpperCase();
            var isPost = method ? method === 'POST' : (options.fields != null || options.request != null);
            if (isPost) {
                var parts: string[] = [];
                var add = (name: string, value: any) => {
                    if (value != null)
                        parts.push(encodeURIComponent(name) + '=' + encodeURIComponent(String(value)));
                };
                if (options.fields) {
                    for (var key in options.fields) {
                        if (Object.prototype.hasOwnProperty.call(options.fields, key))
                            add(key, options.fields[key]);
                    }
                }
                if (options.request != null)
                    add('request', $.toJSON(options.request));
                var formToken = csrfToken();
                if (formToken)
                    add('__RequestVerificationToken', formToken);
                body = parts.join('&');
            }

            xhr.open(isPost ? 'POST' : 'GET', Q.resolveUrl(options.url), true);
            xhr.responseType = 'blob';
            var token = csrfToken();
            if (token)
                xhr.setRequestHeader('X-CSRF-TOKEN', token);
            if (isPost)
                xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset=UTF-8');

            xhr.onprogress = e => {
                // Without a Content-Length (chunked or compressed responses) only the received size
                // is known, so show that instead of a percentage that would be a guess.
                if (!e.lengthComputable) {
                    panel.setPercent(-1);
                    panel.setStatus('Downloaded ' + formatBytes(e.loaded) + '…');
                    return;
                }
                panel.setPercent(e.loaded * 100 / e.total);
                panel.setStatus('Downloaded ' + formatBytes(e.loaded) + ' of ' + formatBytes(e.total));
            };

            xhr.onload = () => {
                if (cancelled) {
                    panel.close();
                    return;
                }
                if (xhr.status < 200 || xhr.status >= 300) {
                    panel.close();
                    var message = 'Export failed (HTTP ' + xhr.status + ').';
                    if (options.onError)
                        options.onError(message);
                    else
                        Q.notifyError(message);
                    return;
                }

                panel.setPercent(100);
                panel.setStatus('Saving file…');
                saveBlob(xhr.response, fileNameOf(xhr, options.fileName));
                panel.close();
                if (options.onSuccess)
                    options.onSuccess();
            };

            xhr.onerror = () => {
                panel.close();
                if (cancelled)
                    return;
                var message = 'Download failed — the connection to the server was lost.';
                if (options.onError)
                    options.onError(message);
                else
                    Q.notifyError(message);
            };

            xhr.send(body);
        }

        /** Reads the download's file name out of Content-Disposition, falling back to the caller's. */
        function fileNameOf(xhr: XMLHttpRequest, fallback: string): string {
            var disposition = xhr.getResponseHeader('Content-Disposition');
            if (disposition) {
                // filename*=UTF-8''name.xlsx wins over the plain filename="name.xlsx" when both are sent.
                var utf8 = /filename\*=UTF-8''([^;]+)/i.exec(disposition);
                if (utf8 && utf8[1]) {
                    try {
                        return decodeURIComponent(utf8[1].replace(/"/g, ''));
                    }
                    catch (e) { /* fall through to the plain filename */ }
                }
                var plain = /filename="?([^";]+)"?/i.exec(disposition);
                if (plain && plain[1])
                    return plain[1];
            }
            return fallback || 'download';
        }

        /** Hands the downloaded bytes to the browser's save dialog. */
        function saveBlob(blob: Blob, fileName: string) {
            var nav = window.navigator as any;
            if (nav && nav.msSaveBlob) {
                nav.msSaveBlob(blob, fileName);
                return;
            }
            var url = URL.createObjectURL(blob);
            var link = document.createElement('a');
            link.href = url;
            link.download = fileName;
            link.style.display = 'none';
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
            // Revoking straight away can cut the save short in some browsers, so give it a moment.
            window.setTimeout(() => URL.revokeObjectURL(url), 30000);
        }
    }
}
