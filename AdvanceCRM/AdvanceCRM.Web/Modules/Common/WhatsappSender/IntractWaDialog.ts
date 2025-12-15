
namespace AdvanceCRM.Common {
    export class IntractWaOptions {
        Contacts: { number: string; name: string }[];
    }

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class IntractWaDialog extends Serenity.PropertyDialog<IntractWaOptions, any> {
        private form: IntractWaForm;
        private contactsQueue: { number: string; name: string }[] = [];
        private isProcessing: boolean = false;
        ImageUrl: any;

        constructor(opt?: IntractWaOptions) {
            super(opt);
            this.form = new IntractWaForm(this.idPrefix);
            this.contactsQueue = opt.Contacts || [];
            if (this.contactsQueue.length > 0) {
                let allNumbers = this.contactsQueue.map(c => c.number).join(", ");
                let allNames = this.contactsQueue.map(c => c.name).join(", ");

                this.form.Number.value = allNumbers; // Show all numbers in one field
                this.form.Variable.value = allNames; // Show all names in one field
            }
            this.form.Image.change(e => this.uploadImage(e));
            // Listen for Template selection change
            this.form.Template.changeSelect2(e => this.onTemplateChange());

            //this.form.Image.change(e => this.uploadImage((e.target as HTMLInputElement).files[0]));


        }

        //// Override the getToolbarButtons method to add custom buttons
        getToolbarButtons(): Serenity.ToolButton[] {
            let buttons: Serenity.ToolButton[] = super.getToolbarButtons() || [];

            // Add your custom "Set Image" button
            buttons.push({
                title: "Preview", // Button title
                cssClass: "set-image-btn", // Optional CSS class for styling
                onClick: () => this.onPreviewButtonClick() // Action when button is clicked
            });

            return buttons;
        }

        private onPreviewButtonClick() {
            if (this.ImageUrl) {
                // Display the image URL or take action
                console.log("Preview image:", this.ImageUrl);

                // Optionally, you can open the image in a modal or use a UI element to show it
                // For example:
                window.open(this.ImageUrl, "_blank");
            } else {
                console.log("No image URL available to preview.");
            }
        }
        protected getDialogTitle(): string {
            return "BizplusWhatsApp Sender";
        }
        private onTemplateChange() {
            let selectedTemplate = this.form.Template.value;
            if (!selectedTemplate) return;

            Q.serviceRequest('Common/GetTemplateImage', { templateName: selectedTemplate })
                .done((response) => {
                    console.log('Image saved successfully:', response);
                    if (response.ImageUrl) {
                        this.ImageUrl = response.ImageUrl; // Store the image URL received from the response

                        // Optionally, trigger an update or show the image right away
                        // You can also update the button title or perform other actions if needed
                        console.log("Image URL set:", this.ImageUrl);
                    } else {
                        console.error("No image URL found in the response.");
                    }
                })
                .fail((jqXHR, textStatus, errorThrown) => {
                    console.error('Error occurred while saving image:', errorThrown);
                });
        }


        private uploadImage(event: JQueryEventObject) {
            let fileInput = event.target as HTMLInputElement;
            let file = fileInput.files[0];

            if (file) {

                let reader = new FileReader();
                reader.onload = (event: any) => {
                    let dataUrl = event.target.result;
                    let base64String = dataUrl.split(',')[1];

                    let request = {
                        ImagePath: base64String,
                        FileName: file.name
                    };

                    Q.serviceRequest('Common/UploadImage', request)
                        .done((response) => {
                            console.log('Image saved successfully:', response);
                        })
                        .fail((jqXHR, textStatus, errorThrown) => {
                            console.error('Error occurred while saving image:', errorThrown);
                        });
                };
                reader.readAsDataURL(file);
            }
        }

        private processQueue() {
            if (this.contactsQueue.length === 0) {
                Q.notifySuccess("All messages have been sent.");
                this.dialogClose();
                return;
            }

            let contact = this.contactsQueue.shift(); // Get the next contact

            let domainUrl = `${window.location.protocol}//${window.location.host}`;
            let Img = this.form.Image.value;
            let Image = Img ? Img.OriginalName : "";
            let filePath = Image ? `${domainUrl}/Common/intractIMg/${Image}` : "";

            if (filePath == "") {
                filePath = this.ImageUrl;
            }
            let requestData = {
                Phone: contact.number,
                Template: this.form.Template.text,
                Variable: contact.name,
                ImageUrl: filePath
            };

            CommonService.SendIntractWa(requestData)
                .done(response => {
                    /*Q.notifyInfo(`Message sent to ${contact.number}: ${response.Status}`);*/
                    Q.notifyInfo(`Message ${response.Status}`);
                    this.processQueue(); // Automatically move to the next contact
                })
                .fail(() => {
                    Q.notifyError(`Failed to send message to ${contact.number}. Skipping...`);
                    this.processQueue(); // Continue even if one fails
                });

        }

        protected getDialogButtons(): Serenity.DialogButton[] {
            return [
                {
                    text: 'Preview Image',
                    click: () => {
                        this.onPreviewButtonClick()
                    },

                },
                {
                    text: 'Send',
                    click: () => {
                        if (!this.validateBeforeSave()) {
                            return;
                        }

                        if (this.isProcessing) return; // Prevent multiple clicks

                        this.isProcessing = true;
                        this.processQueue(); // Start automatic sending
                    }
                }

            ];
        }
    }
}
