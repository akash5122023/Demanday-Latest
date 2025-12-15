namespace AdvanceCRM.Membership {

    interface RazorpayClientConfig {
        enabled?: boolean;
        key?: string;
        currency?: string;
        plans?: { [plan: string]: number };
    }

    interface RazorpayCreateOrderResponse {
        Key: string;
        OrderId: string;
        Amount: number;
        Currency: string;
        Success?: boolean;
    }

    declare var Razorpay: any;

    @Serenity.Decorators.registerClass()
    export class SignUpPanel extends Serenity.PropertyPanel<SignUpRequest, any> {

        protected getFormKey() { return SignUpForm.formKey; }

        private form: SignUpForm;
        private paymentConfig: RazorpayClientConfig = {};
        private paymentCompleted = false;

        constructor(container: JQuery) {
            super(container);

            this.form = new SignUpForm(this.idPrefix);

            this.form.PaymentOrderId.value = '';
            this.form.PaymentId.value = '';
            this.form.PaymentSignature.value = '';
            this.form.PaymentAmount.value = '';
            this.form.PaymentCurrency.value = '';

            const razorpayConfig = (window as any).SignUpRazorpay as RazorpayClientConfig;
            this.paymentConfig = razorpayConfig || {};
            this.paymentCompleted = !(this.paymentConfig?.enabled);

            const paymentSection = this.byId('PaymentSection');
            const paymentButton = this.byId('PaymentButton');
            const paymentStatus = this.byId('PaymentStatus');
            const paymentPlanLabel = this.byId('PaymentPlanLabel');
            const paymentAmountLabel = this.byId('PaymentAmountLabel');

            const paymentStatusIcon = paymentStatus.find('i');
            const paymentStatusText = paymentStatus.find('span');

            const setPaymentStatus = (status: 'pending' | 'success' | 'error', message: string) => {
                if (!paymentStatus.length)
                    return;

                paymentStatus.removeClass('payment-status--pending payment-status--success payment-status--error');
                paymentStatus.addClass(`payment-status--${status}`);

                const icon = status === 'success' ? 'fa-check-circle' : status === 'error' ? 'fa-times-circle' : 'fa-clock-o';
                if (paymentStatusIcon.length)
                    paymentStatusIcon.attr('class', `fa ${icon}`);
                if (paymentStatusText.length)
                    paymentStatusText.text(message);
            };

            const getPlanAmount = () => {
                const planName = (this.form.Plan.value || '').toString();
                const plans = this.paymentConfig?.plans;
                if (!planName || !plans)
                    return 0;

                if (Object.prototype.hasOwnProperty.call(plans, planName))
                    return plans[planName];

                for (const key in plans) {
                    if (!Object.prototype.hasOwnProperty.call(plans, key))
                        continue;

                    if (key.toLowerCase() === planName.toLowerCase())
                        return plans[key];
                }

                return 0;
            };

            const formatAmount = (amount: number) => {
                const currency = this.paymentConfig?.currency || 'INR';
                return `${currency} ${(amount / 100).toFixed(2)}`;
            };

            const updatePaymentSummary = () => {
                const planName = (this.form.Plan.value || '').toString();
                const amount = getPlanAmount();

                if (paymentPlanLabel.length)
                    paymentPlanLabel.text(`Plan: ${planName || '--'}`);

                if (paymentAmountLabel.length)
                    paymentAmountLabel.text(amount > 0 ? `Amount: ${formatAmount(amount)}` : 'Amount: --');

                if (this.paymentConfig?.enabled) {
                    if (amount > 0) {
                        if (!this.paymentCompleted)
                            setPaymentStatus('pending', 'Payment pending');
                        if (paymentButton.length)
                            paymentButton.prop('disabled', false).text('Pay with Razorpay');
                    } else {
                        setPaymentStatus('error', 'Payment amount not configured for this plan.');
                        if (paymentButton.length)
                            paymentButton.prop('disabled', true).text('Pay with Razorpay');
                    }
                }
            };

            if (paymentSection.length && this.paymentConfig?.enabled) {
                paymentSection.addClass('is-visible');
                this.paymentCompleted = false;
                updatePaymentSummary();

                if (paymentButton.length) {
                    paymentButton.text('Pay with Razorpay');
                    paymentButton.on('click', () => {
                        if (this.paymentCompleted)
                            return;

                        const planName = (this.form.Plan.value || '').toString();
                        const amount = getPlanAmount();

                        if (!planName) {
                            Q.alert('Please select a subscription plan before attempting payment.');
                            return;
                        }

                        if (amount <= 0) {
                            Q.alert('Payment amount is not configured for the selected plan.');
                            return;
                        }

                        if (typeof Razorpay === 'undefined') {
                            setPaymentStatus('error', 'Unable to load Razorpay checkout. Please refresh and try again.');
                            return;
                        }

                        this.paymentCompleted = false;
                        this.form.PaymentOrderId.value = '';
                        this.form.PaymentId.value = '';
                        this.form.PaymentSignature.value = '';
                        this.form.PaymentAmount.value = '';
                        this.form.PaymentCurrency.value = '';

                        setPaymentStatus('pending', 'Creating secure payment session...');
                        paymentButton.prop('disabled', true);
                        paymentButton.text('Processing payment...');

                        Q.serviceCall<RazorpayCreateOrderResponse>({
                            url: Q.resolveUrl('~/Account/SignUp/CreateOrder'),
                            request: {
                                Plan: planName
                            },
                            onSuccess: response => {
                                if (!response || !response.OrderId) {
                                    setPaymentStatus('error', 'Unable to initiate payment. Please try again.');
                                    paymentButton.prop('disabled', false);
                                    paymentButton.text('Pay with Razorpay');
                                    return;
                                }

                                const amountValue = response.Amount || amount;
                                const currency = response.Currency || (this.paymentConfig.currency || 'INR');

                                const options: any = {
                                    key: response.Key || this.paymentConfig.key,
                                    amount: amountValue,
                                    currency: currency,
                                    name: this.form.Company.value || 'BizPlusERP',
                                    description: `Subscription for ${planName}`,
                                    order_id: response.OrderId,
                                    prefill: {
                                        name: this.form.DisplayName.value || this.form.Company.value || '',
                                        email: this.form.Email.value || ''
                                    },
                                    handler: (paymentResponse: any) => {
                                        this.form.PaymentOrderId.value = paymentResponse?.razorpay_order_id || response.OrderId || '';
                                        this.form.PaymentId.value = paymentResponse?.razorpay_payment_id || '';
                                        this.form.PaymentSignature.value = paymentResponse?.razorpay_signature || '';
                                        this.form.PaymentAmount.value = amountValue.toString();
                                        this.form.PaymentCurrency.value = currency;
                                        this.paymentCompleted = true;
                                        setPaymentStatus('success', 'Payment received successfully.');
                                        paymentButton.text('Payment completed');
                                        paymentButton.prop('disabled', true);
                                    }
                                };

                                options.modal = {
                                    ondismiss: () => {
                                        if (!this.paymentCompleted) {
                                            setPaymentStatus('pending', 'Payment pending');
                                            paymentButton.prop('disabled', false);
                                            paymentButton.text('Pay with Razorpay');
                                        }
                                    }
                                };

                                const razorpay = new Razorpay(options);
                                razorpay.open();
                            },
                            onError: response => {
                                setPaymentStatus('error', response?.Error?.Message || 'Unable to create a payment order.');
                                paymentButton.prop('disabled', false);
                                paymentButton.text('Pay with Razorpay');
                            }
                        });
                    });
                }
            } else {
                this.paymentCompleted = true;
                if (paymentSection.length)
                    paymentSection.hide();
            }

            const plan = Q.parseQueryString()['plan'];
            if (plan)
                this.form.Plan.value = plan as string;

            this.form.ConfirmEmail.addValidationRule(this.uniqueName, e => {
                if (this.form.ConfirmEmail.value !== this.form.Email.value) {
                    return Q.text('Validation.EmailConfirm');
                }
            });

            this.form.ConfirmPassword.addValidationRule(this.uniqueName, e => {
                if (this.form.ConfirmPassword.value !== this.form.Password.value) {
                    return Q.text('Validation.PasswordConfirm');
                }
            });

            const trialInfo = this.byId('TrialInfo');
            const trialInfoText = this.byId('TrialInfoText');
            let trialSettingsRequested = false;
            let defaultTrialDays = 0;
            let planTrialDays: { [plan: string]: number } = {};

            const host = (window.location.hostname || '').toLowerCase();
            const suppressTrialInfo = host.indexOf('demo.') === 0 || host.indexOf('test.') === 0;

            if (suppressTrialInfo && trialInfo.length)
                trialInfo.remove();

            const resolveTrialDays = (planName: string) => {
                if (!planName)
                    return defaultTrialDays;

                if (!planTrialDays)
                    return defaultTrialDays;

                const direct = planTrialDays[planName];
                if (typeof direct === 'number')
                    return direct;

                const lowered = planName.toLowerCase();
                for (const key in planTrialDays) {
                    if (!Object.prototype.hasOwnProperty.call(planTrialDays, key))
                        continue;

                    if (key.toLowerCase() === lowered)
                        return planTrialDays[key];
                }

                return defaultTrialDays;
            };

            const updateTrialInfo = () => {
                if (suppressTrialInfo || !trialInfo.length)
                    return;

                const planName = (this.form.Plan.value || '').toString();
                const days = resolveTrialDays(planName);

                if (days > 0) {
                    trialInfo.addClass('is-visible');
                    if (trialInfoText.length)
                        trialInfoText.text(`${days} day${days === 1 ? '' : 's'} remaining in trial`);
                } else {
                    trialInfo.removeClass('is-visible');
                }
            };

            const loadTrialSettings = () => {
                if (suppressTrialInfo || trialSettingsRequested)
                    return;

                trialSettingsRequested = true;

                fetch(Q.resolveUrl('~/api/public/trial-settings'), { cache: 'no-cache' })
                    .then(response => response.ok ? response.json() : null)
                    .then(data => {
                        if (!data)
                            return;

                        const parsedDefault = parseInt(data.defaultDays, 10);
                        if (!isNaN(parsedDefault) && parsedDefault > 0)
                            defaultTrialDays = parsedDefault;

                        if (data.plans && typeof data.plans === 'object')
                            planTrialDays = data.plans as { [plan: string]: number };

                        updateTrialInfo();
                    })
                    .catch(() => {
                        // ignore network issues, keep defaults hidden
                    });
            };

            const handlePlanChange = () => {
                if (this.paymentConfig?.enabled) {
                    this.paymentCompleted = false;
                    updatePaymentSummary();
                }

                if (!suppressTrialInfo) {
                    updateTrialInfo();
                    loadTrialSettings();
                }
            };

            this.form.Plan.element.on('change', handlePlanChange);

            if (this.paymentConfig?.enabled) {
                updatePaymentSummary();
            }

            if (!suppressTrialInfo) {
                loadTrialSettings();
                updateTrialInfo();
            }

            this.byId('SubmitButton').click(e => {
                e.preventDefault();

                if (!this.validateForm()) {
                    return;
                }

                if (this.paymentConfig?.enabled && !this.paymentCompleted) {
                    Q.alert('Please complete the payment before submitting the sign up form.');
                    return;
                }

                Q.serviceCall({
                    url: Q.resolveUrl('~/Account/SignUp'),
                    request: {
                        Plan: this.form.Plan.value,
                        Company: this.form.Company.value,
                        DisplayName: this.form.DisplayName.value,
                        Email: this.form.Email.value,
                        MobileNumber: this.form.MobileNumber.value,
                        Password: this.form.Password.value,
                        PaymentOrderId: this.form.PaymentOrderId.value,
                        PaymentId: this.form.PaymentId.value,
                        PaymentSignature: this.form.PaymentSignature.value,
                        PaymentAmount: this.form.PaymentAmount.value,
                        PaymentCurrency: this.form.PaymentCurrency.value
                    },
                    onSuccess: response => {
                        Q.information(Q.text('Forms.Membership.SignUp.Success'), () => {
                            window.location.href = Q.resolveUrl('~/');
                        });
                    }
                });

            });
        }
    }
}
