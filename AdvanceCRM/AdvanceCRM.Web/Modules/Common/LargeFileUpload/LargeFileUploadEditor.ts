namespace AdvanceCRM {

    import Element = Serenity.Decorators.element;

    export interface LargeFileUploadEditorOptions {
        //urlPrefix?: string;
    }

    @Serenity.Decorators.registerEditor([Serenity.IReadOnly, Serenity.IGetEditValue, Serenity.ISetEditValue])
    @Element('<div/>')
    export class LargeFileUploadEditor extends Serenity.Widget<LargeFileUploadEditorOptions> {

        private items: Serenity.UploadedFile[] = [];
        private toolbar: Serenity.Toolbar;
        private fileSymbols: JQuery;
        private uploadInput: JQuery;
        public fileAdded: (file: Serenity.UploadedFile) => void;

        constructor(div: JQuery, opt: LargeFileUploadEditorOptions) {
            super(div, opt);

            this.items = [];
            div.addClass('s-MultipleImageUploadEditor');
            var self = this;
            this.toolbar = new Serenity.Toolbar($('<div/>').appendTo(this.element), {
                buttons: this.getToolButtons()
            });
            var progress = $('<div><div></div></div>')
                .addClass('upload-progress').prependTo(this.toolbar.element);

            var addFileButton = this.toolbar.findButton('add-file-button');

            this.uploadInput = Serenity.UploadHelper.addLargeFileUploadInput({
                container: addFileButton,
                zone: this.element,
                inputName: this.uniqueName,
                progress: progress,
                fileDone: (response, name, data) => {
                    var newEntity = { OriginalName: name, Filename: response.TemporaryFile };
                    self.items.push(newEntity);
                    self.fileAdded && self.fileAdded(newEntity);
                    self.populate();
                    self.updateInterface();
                }
            });

            this.fileSymbols = $('<ul/>').appendTo(this.element);
            this.updateInterface();
        }

        protected addFileButtonText(): string {
            return Q.tryGetText('Controls.ImageUpload.AddFileButton') || 'Add File';
        }

        protected getToolButtons(): Serenity.ToolButton[] {
            return [{
                title: this.addFileButtonText(),
                cssClass: 'add-file-button',
                onClick: function () {
                }
            }];
        }

        protected populate(): void {
            Serenity.UploadHelper.populateFileSymbols(this.fileSymbols, this.items,
                true, undefined);

            this.fileSymbols.children().each((i, e) => {
                var x = i;
                $("<a class='delete'></a>").appendTo($(e).children('.filename'))
                    .click(ev => {
                        ev.preventDefault();
                        this.items.splice(x, 1);
                        this.populate();
                    });
            });
        }

        protected updateInterface(): void {
            var addButton = this.toolbar.findButton('add-file-button');
            addButton.toggleClass('disabled', this.get_readOnly());
            this.fileSymbols.find('a.delete').toggle(!this.get_readOnly());
        }

        get_readOnly(): boolean {
            return this.uploadInput.attr('disabled') != null;
        }

        set_readOnly(value: boolean): void {
            if (this.get_readOnly() !== value) {
                if (value) {
                    (this.uploadInput.attr('disabled', 'disabled') as any).fileupload('disable');
                }
                else {
                    (this.uploadInput.removeAttr('disabled') as any).fileupload('enable');
                }
                this.updateInterface();
            }
        }

        get_value(): Serenity.UploadedFile[] {
            return this.items.map(x => $.extend({}, x));
        }

        get value(): Serenity.UploadedFile[] {
            return this.get_value();
        }

        set_value(value: Serenity.UploadedFile[]) {
            this.items = (value || []).map(x => $.extend({}, x));
            this.populate();
            this.updateInterface();
        }

        set value(v: Serenity.UploadedFile[]) {
            this.set_value(v);
        }

        getEditValue(property: Serenity.PropertyItem, target: any) {
            target[property.name] = $.toJSON(this.get_value());
        }

        setEditValue(source: any, property: Serenity.PropertyItem) {
            var val = source[property.name];
            if (typeof val === 'string') {
                var json = Q.coalesce(Q.trimToNull(val), '[]');
                if (Q.startsWith(json, '[') && Q.endsWith(json, ']')) {
                    this.set_value($.parseJSON(json));
                }
                else {
                    this.set_value([{
                        Filename: json,
                        OriginalName: 'UnknownFile'
                    }]);
                }
            }
            else {
                this.set_value(val as any);
            }
        }
    }
}