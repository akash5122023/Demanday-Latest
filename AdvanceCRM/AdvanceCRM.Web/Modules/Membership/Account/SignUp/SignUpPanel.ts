namespace AdvanceCRM.Membership {

    @Serenity.Decorators.registerClass()
    export class SignUpPanel extends Serenity.PropertyPanel<SignUpRequest, any> {

        protected getFormKey() { return SignUpForm.formKey; }

        private form: SignUpForm;

        constructor(container: JQuery) {
            super(container);

            this.form = new SignUpForm(this.idPrefix);

            const queryParams = Q.parseQueryString();
            const planParam = queryParams['plan'];
            if (planParam)
                this.form.Plan.value = planParam as string;

            const usersParam = queryParams['users'];
            let parsedUsers = 1;
            if (usersParam) {
                const asNumber = parseInt(usersParam as string, 10);
                if (!isNaN(asNumber) && asNumber > 0)
                    parsedUsers = asNumber;
            }

            if (this.form.Users)
                (this.form.Users.value as any) = parsedUsers;

            const paymentOrderParam = queryParams['razorpay_order_id'];
            if (paymentOrderParam && this.form.PaymentOrderId)
                this.form.PaymentOrderId.value = paymentOrderParam as string;

            const paymentIdParam = queryParams['razorpay_payment_id'];
            if (paymentIdParam && this.form.PaymentId)
                this.form.PaymentId.value = paymentIdParam as string;

            const paymentSignatureParam = queryParams['razorpay_signature'];
            if (paymentSignatureParam && this.form.PaymentSignature)
                this.form.PaymentSignature.value = paymentSignatureParam as string;

            const paymentAmountParam = queryParams['amount'];
            if (paymentAmountParam && this.form.PaymentAmount)
                this.form.PaymentAmount.value = paymentAmountParam as string;

            const paymentCurrencyParam = queryParams['currency'];
            if (paymentCurrencyParam && this.form.PaymentCurrency)
                this.form.PaymentCurrency.value = (paymentCurrencyParam as string).toUpperCase();

            const couponParam = queryParams['coupon'];
            if (couponParam && this.form.CouponCode)
                this.form.CouponCode.value = couponParam as string;

            const trialInfo = this.byId('TrialInfo');
            const trialInfoText = this.byId('TrialInfoText');
            if (trialInfo.length) {
                const trialMessage = this.element.data('trialMessage') as string;
                const trialVisibleRaw = this.element.data('trialVisible');
                const shouldShowTrial = trialVisibleRaw === true || trialVisibleRaw === 'true';

                if (shouldShowTrial && !Q.isEmptyOrNull(trialMessage) && trialInfoText.length) {
                    trialInfoText.text(trialMessage);
                    trialInfo.show();
                } else {
                    trialInfo.hide();
                }
            }

            this.form.ConfirmEmail.addValidationRule(this.uniqueName, e => {
                if (this.form.ConfirmEmail.value !== this.form.Email.value) {
                    return Q.text('Validation.EmailConfirm');
                }
            });

            const submitButton = this.byId('SubmitButton');
            const acceptTermsCheckbox = this.byId('AcceptTerms');

            if (acceptTermsCheckbox.length && submitButton.length) {
                const updateSubmitState = () => {
                    const isChecked = acceptTermsCheckbox.is(':checked');
                    submitButton.prop('disabled', !isChecked);
                };

                updateSubmitState();
                acceptTermsCheckbox.on('change', updateSubmitState);
                acceptTermsCheckbox.on('ifChanged', updateSubmitState);
            }

            const registerContainer = this.element.closest('.register-box-body');
            let loadingOverlay = registerContainer.find('.signup-loading-overlay');

            if (!loadingOverlay.length) {
                loadingOverlay = $(
                    '<div class="signup-loading-overlay" role="status" aria-live="assertive">' +
                    '  <div class="overlay-content">' +
                    '    <div class="spinner" aria-hidden="true"></div>' +
                    '    <div class="message">Setting up your workspace. This can take up to a minute.</div>' +
                    '  </div>' +
                    '</div>'
                ).appendTo(registerContainer);
            }

            const showLoading = (message?: string) => {
                if (!loadingOverlay.length)
                    return;

                const messageElement = loadingOverlay.find('.message');
                if (message && messageElement.length)
                    messageElement.text(message);

                loadingOverlay.attr('aria-busy', 'true');
                loadingOverlay.fadeIn(150);
            };

            const hideLoading = () => {
                if (!loadingOverlay.length)
                    return;

                loadingOverlay.attr('aria-busy', 'false');
                loadingOverlay.fadeOut(150);
            };

            submitButton.click(e => {
                e.preventDefault();

                if (acceptTermsCheckbox.length && !acceptTermsCheckbox.is(':checked')) {
                    Q.alert('Please accept the terms to continue.');
                    return;
                }

                if (!this.validateForm()) {
                    return;
                }

                const selectedPlan = this.form.Plan.value;
                const selectedUsersRaw: any = this.form.Users ? this.form.Users.value : null;
                let selectedUsers = parsedUsers;
                if (selectedUsersRaw != null) {
                    if (typeof selectedUsersRaw === 'number')
                        selectedUsers = selectedUsersRaw > 0 ? selectedUsersRaw : parsedUsers;
                    else {
                        const parsed = parseInt(selectedUsersRaw.toString(), 10);
                        if (!isNaN(parsed) && parsed > 0)
                            selectedUsers = parsed;
                    }
                }

                parsedUsers = selectedUsers;

                // Combine area code + mobile if area code widget present
                let mobileRaw = this.form.MobileNumber.value || "";
                const areaSelect = this.element.closest('.register-box-body').find('#AreaCodeSelect');
                if (areaSelect.length) {
                    const acVal = areaSelect.val() as string;
                    if (acVal && mobileRaw && mobileRaw.indexOf(acVal) !== 0) {
                        mobileRaw = acVal + mobileRaw.replace(/^0+/, '');
                    }
                }

                showLoading();

                Q.serviceCall({
                    url: Q.resolveUrl('~/Account/SignUp'),
                    request: {
                        Plan: selectedPlan,
                        Company: this.form.Company.value,
                        Subdomain: this.form.Subdomain.value,
                        DisplayName: this.form.DisplayName.value,
                        Email: this.form.Email.value,
                        MobileNumber: mobileRaw,
                        PaymentOrderId: this.form.PaymentOrderId ? (this.form.PaymentOrderId.value || undefined) : undefined,
                        PaymentId: this.form.PaymentId ? (this.form.PaymentId.value || undefined) : undefined,
                        PaymentSignature: this.form.PaymentSignature ? (this.form.PaymentSignature.value || undefined) : undefined,
                        PaymentAmount: this.form.PaymentAmount ? (this.form.PaymentAmount.value || undefined) : undefined,
                        PaymentCurrency: this.form.PaymentCurrency ? (this.form.PaymentCurrency.value || undefined) : undefined,
                        Users: selectedUsers,
                        CouponCode: this.form.CouponCode ? (this.form.CouponCode.value || undefined) : undefined
                    },
                    onSuccess: response => {
                        Q.information(Q.text('Forms.Membership.SignUp.Success'), () => {
                            window.location.href = 'https://bizpluserp.com/';
                        });
                    },
                    onCleanup: () => hideLoading()
                });

            });
        }
    }
}
