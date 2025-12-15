
namespace AdvanceCRM.Common {
    export class QuickMailOptions {
        Contacts?: { email: string }[];
    }

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class QuickMailDialog extends Serenity.PropertyDialog<QuickMailOptions, any> {

        private form: QuickMailForm;
        private contactsQueue: { email: string }[] = [];

        constructor(opt?: QuickMailOptions) {
            super(opt);

            this.form = new QuickMailForm(this.idPrefix);
            this.contactsQueue = opt?.Contacts || [];

            if (this.contactsQueue.length > 0) {
                let allEmails = this.contactsQueue.map(c => c.email.trim()).join(", ");
                this.form.EmailTo.value = allEmails; // Use set_value() for form fields
            }

            this.form.TemplateId.changeSelect2(e => {
                let templateId = this.form.TemplateId.value;
                if (templateId) {
                    let lookup = Q.getLookup("Template.QuickMailTemplate"); // ✅ Get Lookup Data
                    let selectedTemplate = lookup.itemById[templateId];

                    console.log("Selected Template:", selectedTemplate); // 🐞 Debugging

                    if (selectedTemplate) {
                        // ✅ Set Subject and Message
                        this.form.Subject.element.val(selectedTemplate.Subject || "").trigger("change");
                        this.form.Message.set_value(selectedTemplate.Message || "");
                        if (selectedTemplate.Attachments) {
                            try {
                                let attachments = JSON.parse(selectedTemplate.Attachments); // ✅ Parse JSON
                                this.form.Attachments.set_value(attachments); // ✅ Set to form
                            } catch (e) {
                                console.error("Error parsing attachments:", e);
                                this.form.Attachments.set_value([]); // If error, set empty
                            }
                        } else {
                            this.form.Attachments.set_value([]); // ✅ Handle empty case
                        }

                    }
                }
            });


        }

        protected getDialogTitle(): string {
            return "QuickMail Sender";
        }
        protected getDialogOptions() {
            let opt = super.getDialogOptions();
            opt.width = "800px";  // Set your desired width here
            return opt;
        }

        protected getDialogButtons(): Serenity.DialogButton[] {
            return [
                {
                    text: 'Send',
                    click: () => {
                        //if (!this.validateBeforeSave()) {
                        //    return;
                        //}

                        let formData = new FormData();
                        formData.append("subject", this.form.Subject.value);
                        formData.append("tomail", this.form.EmailTo.value);
                        formData.append("body", encodeURIComponent(this.form.Message.value));

                        let attachments = this.form.Attachments.value as Serenity.UploadedFile[]; // ✅ Ensure correct type

                        if (attachments && attachments.length > 0) {
                            for (let file of attachments) {
                                formData.append("attachments[]", file.Filename); // ✅ Use 'Filename' from UploadedFile object
                            }
                        }

                        $.ajax({
                            url: Q.resolveUrl('~/Dashboard/SendMail'),
                            type: "POST",
                            data: formData,
                            processData: false,
                            contentType: false,
                            success: function (response) {
                                Q.notifyInfo(response);
                            },
                            error: function (xhr, status, error) {
                                Q.notifyError("Error sending email: " + xhr.responseText);
                            }
                        });

                        this.dialogClose();
                    }
                }
            ];
        }

    }
}
