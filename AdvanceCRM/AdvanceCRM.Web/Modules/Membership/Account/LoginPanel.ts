namespace AdvanceCRM.Membership{

    interface TenantLicenseStatusResponse {
        hasLicense?: boolean;
        plan?: string;
        licenseStartDate?: string;
        licenseEndDate?: string;
        totalDays?: number;
        remainingDays?: number;
        isExpired?: boolean;
        isTrial?: boolean;
        serverDateUtc?: string;
    }

    @Serenity.Decorators.registerClass()
    export class LoginPanel extends Serenity.PropertyPanel<LoginRequest, any> {

        private planButton?: JQuery;
        private planLink?: JQuery;
        private showPlanDetails: boolean = true;
        private allowPlanPurchaseCtas: boolean = false;

        protected getFormKey() { return LoginForm.formKey; }

        constructor(container: JQuery) {
            super(container);

            this.resetPlanDisplay();

            const host = (window.location.hostname || '').toLowerCase();
            const plansUrl = Q.resolveUrl('~/plans.html');
            const isDemoHost = host.indexOf('demo.') === 0;
            const isTestHost = host.indexOf('test.') === 0;
            const isDemoOrTestHost = isDemoHost || isTestHost;
            this.allowPlanPurchaseCtas = !isDemoOrTestHost;
            this.showPlanDetails = !isDemoOrTestHost;

            if (this.showPlanDetails) {
                if (!this.allowPlanPurchaseCtas)
                    this.hidePlanCtas();

                this.loadTenantLicense(plansUrl);
            } else {
                this.hidePlanBadge();

                if (this.allowPlanPurchaseCtas) {
                    this.ensurePlanLink(plansUrl, '<i class="fa fa-angle-right"></i>&nbsp;Purchase Plan');
                } else {
                    this.hidePlanCtas();
                }
            }

            $.fn['vegas'] && $('body')['vegas']({
                delay: 30000,
                cover: true,
                overlay: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAIAAAACAQMAAABIeJ9nAAAAA3NCSVQICAjb4U" +
                    "/gAAAABlBMVEX///8AAABVwtN+AAAAAnRSTlMA/1uRIrUAAAAJcEhZcwAAAsQAAALEAVuRnQsAAAAWdEVYdENyZWF0" +
                    "aW9uIFRpbWUAMDQvMTMvMTGrW0T6AAAAHHRFWHRTb2Z0d2FyZQBBZG9iZSBGaXJld29ya3MgQ1M1cbXjNgAAAAxJREFUCJljaGBgAAABhACBrONIPgAAAABJRU5ErkJggg==",
                slides: [
                    //{ src: Q.resolveUrl('~/Content/site/slides/BlueBackground.jpg')},
                    { src: Q.resolveUrl('~/Content/site/slides/slide1.jpg'), transition: 'fade' },
                    { src: Q.resolveUrl('~/Content/site/slides/slide2.jpg'), transition: 'zoomIn' },
                    { src: Q.resolveUrl('~/Content/site/slides/slide3.jpg'), transition: 'zoomOut' },
                    { src: Q.resolveUrl('~/Content/site/slides/slide4.jpg'), transition: 'blur' },
                    { src: Q.resolveUrl('~/Content/site/slides/slide5.jpg'), transition: 'swirlLeft' },
                    { src: Q.resolveUrl('~/Content/site/slides/slide6.jpg'), transition: 'swirlRight' }
                ]
            });

            this.byId('LoginButton').click(e => {
                e.preventDefault();

                if (!this.validateForm()) {
                    return;
                }

                var request = this.getSaveEntity();

                Q.serviceCall({
                    url: Q.resolveUrl('~/Account/Login'),
                    request: request,
                    onSuccess: response => {
                        this.redirectToReturnUrl();
                    },
                    onError: (response: Serenity.ServiceResponse) => {
                        if (response != null && response.Error != null && response.Error.Code == "RedirectUserTo") {
                            window.location.href = response.Error.Arguments;
                            return;
                        }

                        if (response != null && response.Error != null && !Q.isEmptyOrNull(response.Error.Message)) {
                            Q.notifyError(response.Error.Message);
                            $('#Password').focus();

                            return;
                        }

                        Q.ErrorHandling.showServiceError(response.Error);
                    }
                });
            });
        }

        private ensurePlanButton(plansUrl: string, text: string): void {
            const loginButton = this.byId('LoginButton');
            if (!loginButton.length)
                return;

            const buttons = loginButton.closest('.buttons');
            if (!buttons.length)
                return;

            if (!this.planButton || !this.planButton.length) {
                this.planButton = $('<a/>', {
                    href: plansUrl,
                    target: '_blank',
                    class: 'btn btn-warning btn-plan'
                }).insertBefore(loginButton);
            } else {
                this.planButton.insertBefore(loginButton);
            }

            if (!Q.isEmptyOrNull(text))
                this.planButton.text(text);
        }

        private ensurePlanLink(plansUrl: string, html: string): void {
            const actions = this.element.find('.actions');
            if (!actions.length)
                return;

            const signUpLink = actions.find('a').filter((_, el) => {
                const href = (el as HTMLAnchorElement).getAttribute('href') || '';
                return href.indexOf('/Account/SignUp') >= 0;
            }).first();

            if (signUpLink.length) {
                if (!signUpLink.data('original-href')) {
                    signUpLink.data('original-href', signUpLink.attr('href') || '');
                    signUpLink.data('original-html', signUpLink.html());
                    const originalTarget = signUpLink.attr('target');
                    if (originalTarget)
                        signUpLink.data('original-target', originalTarget);
                }

                signUpLink.attr('href', plansUrl);
                signUpLink.attr('target', '_blank');
                signUpLink.addClass('purchase-plan-link');
                if (!Q.isEmptyOrNull(html))
                    signUpLink.html(html);

                this.planLink = signUpLink;
                return;
            }

            if (!this.planLink || !this.planLink.length) {
                this.planLink = $('<a/>', {
                    href: plansUrl,
                    target: '_blank',
                    class: 'purchase-plan-link'
                }).prependTo(actions);
            }

            if (!Q.isEmptyOrNull(html))
                this.planLink.html(html);
        }

        private hidePlanCtas(): void {
            if (this.planButton && this.planButton.length) {
                this.planButton.remove();
                this.planButton = undefined;
            }

            if (this.planLink && this.planLink.length) {
                const originalHref = this.planLink.data('original-href');
                const originalHtml = this.planLink.data('original-html');
                const originalTarget = this.planLink.data('original-target');

                if (originalHref != null) {
                    this.planLink.attr('href', originalHref as string);
                    if (originalTarget)
                        this.planLink.attr('target', originalTarget as string);
                    else
                        this.planLink.removeAttr('target');

                    this.planLink.removeClass('purchase-plan-link');

                    if (originalHtml != null)
                        this.planLink.html(originalHtml as string);

                    this.planLink.removeData('original-href')
                        .removeData('original-html')
                        .removeData('original-target');
                } else {
                    this.planLink.remove();
                }

                this.planLink = undefined;
            }
        }

        private hidePlanBadge(): void {
            const planContainer = this.byId('LicensePlanContainer');
            if (planContainer.length)
                planContainer.addClass('hidden');

            const planLabel = this.byId('LicensePlanLabel');
            if (planLabel.length)
                planLabel.text('');

            const planRemainingLabel = this.byId('LicensePlanRemainingLabel');
            if (planRemainingLabel.length) {
                planRemainingLabel.html('');
                planRemainingLabel.addClass('hidden');
            }
        }

        private loadTenantLicense(plansUrl: string): void {
            if (!this.showPlanDetails)
                return;

            this.resetPlanDisplay();

            const planLabel = this.byId('LicensePlanLabel');
            if (!planLabel.length)
                return;

            Q.serviceCall({
                url: Q.resolveUrl('~/Account/LicenseStatus'),
                method: 'GET',
                onSuccess: (response: TenantLicenseStatusResponse) => {
                    const responseAny = response as any;
                    const hasLicense = response?.hasLicense ?? responseAny?.HasLicense;
                    if (!response || hasLicense === false)
                        return;

                    this.renderTenantLicense(response, plansUrl);
                },
                onError: () => { /* silently ignore */ }
            });
        }

        private renderTenantLicense(status: TenantLicenseStatusResponse, plansUrl: string): void {
            if (!this.showPlanDetails) {
                this.hidePlanCtas();
                this.hidePlanBadge();
                return;
            }

            const planLabel = this.byId('LicensePlanLabel');
            if (!planLabel.length)
                return;

            const planContainer = this.byId('LicensePlanContainer');
            const planRemainingLabel = this.byId('LicensePlanRemainingLabel');
            const statusAny = status as any;

            const planRaw = status.plan ?? statusAny?.Plan;
            const planText = !Q.isEmptyOrNull(planRaw) ? planRaw! : 'Free Trial';

            let licenseEnd: any = status.licenseEndDate ?? statusAny?.LicenseEndDate;
            if (licenseEnd instanceof Date)
                licenseEnd = licenseEnd.toISOString();

            const hasEndDate = !Q.isEmptyOrNull(licenseEnd);
            const endDateText = hasEndDate
                ? Q.formatDate(licenseEnd, 'yyyy-MM-dd')
                : null;

            let remaining: number | null | undefined = status.remainingDays ?? statusAny?.RemainingDays;
            if (remaining == null && hasEndDate && licenseEnd) {
                const endDate = Q.parseISODateTime(licenseEnd);
                if (endDate) {
                    let now = new Date();
                    const serverDate = status.serverDateUtc ?? statusAny?.ServerDateUtc;
                    if (!Q.isEmptyOrNull(serverDate)) {
                        const parsedServer = Q.parseISODateTime(serverDate);
                        if (parsedServer)
                            now = parsedServer;
                    }
                    const diffMs = endDate.getTime() - now.getTime();
                    remaining = Math.ceil(diffMs / (24 * 60 * 60 * 1000));
                }
            }

            const isExpired = (status.isExpired ?? statusAny?.IsExpired) === true || (remaining != null && remaining < 0);

            const metaLines: string[] = [];

            if (isExpired) {
                metaLines.push(endDateText
                    ? `Expired on ${endDateText}`
                    : 'Expired');
                remaining = 0;
            } else {
                if (remaining != null) {
                    if (remaining > 1) {
                        metaLines.push(`${remaining} days remaining`);
                    } else if (remaining === 1) {
                        metaLines.push('1 day remaining');
                    } else if (remaining === 0) {
                        metaLines.push('Expires today');
                    }
                }

                if (endDateText)
                    metaLines.push(`Expiry Date: ${endDateText}`);
            }

            if (!metaLines.length && hasEndDate && !isExpired && endDateText)
                metaLines.push(`Expiry Date: ${endDateText}`);

            const purchasedUsersRaw = status.purchasedUsers ?? statusAny?.PurchasedUsers;
            let purchasedUsers: number | null = null;
            if (typeof purchasedUsersRaw === 'number')
                purchasedUsers = purchasedUsersRaw;
            else if (typeof purchasedUsersRaw === 'string') {
                const parsedUsers = parseInt(purchasedUsersRaw, 10);
                if (!isNaN(parsedUsers))
                    purchasedUsers = parsedUsers;
            }

            if (purchasedUsers != null && purchasedUsers > 0) {
                const userLabel = purchasedUsers === 1 ? 'user' : 'users';
                metaLines.push(`${purchasedUsers} licensed ${userLabel}`);
            }

            const purchaseAmountRaw = status.purchaseAmount ?? statusAny?.PurchaseAmount;
            let purchaseAmount: number | null = null;
            if (typeof purchaseAmountRaw === 'number')
                purchaseAmount = purchaseAmountRaw;
            else if (typeof purchaseAmountRaw === 'string') {
                const parsedAmount = parseFloat(purchaseAmountRaw);
                if (!isNaN(parsedAmount))
                    purchaseAmount = parsedAmount;
            }

            const purchaseCurrencyRaw = status.purchaseCurrency ?? statusAny?.PurchaseCurrency;
            const purchaseCurrency = typeof purchaseCurrencyRaw === 'string'
                ? purchaseCurrencyRaw.trim().toUpperCase()
                : '';

            if (purchaseAmount != null && purchaseAmount > 0) {
                const formattedAmount = purchaseAmount.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                const paidLine = purchaseCurrency
                    ? `Paid: ${purchaseCurrency} ${formattedAmount}`
                    : `Paid: ${formattedAmount}`;
                metaLines.push(paidLine);
            }

            planLabel.text(planText);

            if (planRemainingLabel.length) {
                if (metaLines.length) {
                    const html = metaLines.map(line => Q.htmlEncode(line)).join('<br/>');
                    planRemainingLabel.html(html);
                    planRemainingLabel.removeClass('hidden');
                } else {
                    planRemainingLabel.html('');
                    planRemainingLabel.addClass('hidden');
                }
            }

            if (planContainer.length)
                planContainer.addClass('hidden');

            const isTrial = (status.isTrial ?? statusAny?.IsTrial) === true;
            if (this.allowPlanPurchaseCtas && (isTrial || isExpired)) {
                this.ensurePlanButton(plansUrl, isExpired ? 'Renew Plan' : 'Upgrade Plan');
                this.ensurePlanLink(plansUrl, '<i class="fa fa-angle-right"></i>&nbsp;Purchase Plan');
            }
        }

        private resetPlanDisplay(): void {
            const planLabel = this.byId('LicensePlanLabel');
            if (planLabel.length)
                planLabel.text('');

            const planRemainingLabel = this.byId('LicensePlanRemainingLabel');
            if (planRemainingLabel.length) {
                planRemainingLabel.html('');
                planRemainingLabel.addClass('hidden');
            }

            const planContainer = this.byId('LicensePlanContainer');
            if (planContainer.length)
                planContainer.addClass('hidden');
        }

        protected redirectToReturnUrl() {
            var q = Q.parseQueryString();
            var returnUrl = q['returnUrl'] || q['ReturnUrl'];
            if (returnUrl) {
                var hash = window.location.hash;
                if (hash != null && hash != '#')
                    returnUrl += hash;
                window.location.href = returnUrl;
            }
            else {
                window.location.href = Q.resolveUrl('~/');
            }
        }

        protected getTemplate() {

            const containerWelcome = Q.trimToNull((this.element?.data('default-welcome') as string) ?? '');
            const defaultWelcome = containerWelcome || Q.text('Forms.Membership.Login.FormTitle') || 'Welcome to Bizpluserp System';

            // Always return the unified platform welcome text regardless of subdomain or company
            // (previous logic attempted to customize per tenant producing 'Welcome to ABIT', etc.)
            const resolveWelcomeHeading = () => defaultWelcome;

            const welcomeHeading = resolveWelcomeHeading();

            return `
            <div class="flex-layout">
                <div class="login-header">
                    <div class="logo"></div>
                </div>
                <h3>${Q.htmlEncode(welcomeHeading)}</h3>
                <form id="~_Form" action="">
                    <div class="s-Form">
                        <div class="fieldset ui-widget ui-widget-content ui-corner-all">
                            <div id="~_PropertyGrid"></div>
                            <div class="clear"></div>
                        </div>
                        <div class="buttons">
                            <div class="plan-badge" id="~_LicensePlanContainer">
                                <span class="label">Plan</span>
                                <span class="value" id="~_LicensePlanLabel">Free Trial</span>
                                <span class="meta hidden" id="~_LicensePlanRemainingLabel"></span>
                            </div>
                            <button id="~_LoginButton" type="submit" class="btn btn-primary">
                                ${Q.text('Forms.Membership.Login.SignInButton')}
                            </button>
                        </div>
                        <div class="actions">
                            <a href="${Q.resolveUrl('~/Account/SignUp')}"><i class="fa fa-angle-right"></i>&nbsp;${Q.text('Forms.Membership.Login.SignUpButton')}</a>
                            <a href="${Q.resolveUrl('~/Account/ForgotPassword')}"><i class="fa fa-angle-right"></i>&nbsp;${Q.text('Forms.Membership.Login.ForgotPassword')}</a>
                            <div class="clear"></div>
                        </div>
<div style="text-align:center">
                                <br>
                                <a href="` + Q.text('Site.Layout.WhiteLabelURL') + `" target="_blank"><i style="color:burlywood">&nbsp;<strong>Powered by:</strong> ` + Q.text('Site.Layout.WhiteLabel') + `</i></a>
                            </div>
                       
                  
                    </div>
                </form>
            </div>
            `;
        }
    }
}
