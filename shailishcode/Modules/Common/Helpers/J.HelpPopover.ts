/* Modules/Common/Helpers/J.HelpPopover.ts */

//===================================================
//  Copyright @ 2020
//  Author : Hung Vo (it.minhhung@gmail.com)
//  Time : 2020, August 06
//  Description : HelpPopover
//===================================================

namespace J {

    export class HelpPopover {

        public buttons: HelpPopoverItem[] = [];

        constructor(
            public form: Serenity.PrefixedContext,
            public propertyItems: Serenity.PropertyItem[],
            public klassName?: string,
            public bootstrapPopoverOption?: any) {

            this.klassName = Q.coalesce(klassName, "");
        }

        public init() {
            if (this.form && this.propertyItems) {

                this.propertyItems.forEach((editor, idx) => {
                    let popoverEditorOptionKey = "j_custom_help_popover";

                    if (editor.editorParams && editor.editorParams[popoverEditorOptionKey]) {
                        let helpData = JSON.parse(editor.editorParams[popoverEditorOptionKey]);
                        let button = this.createHelpPopoverButton(this.form[editor.name], helpData, this.bootstrapPopoverOption);
                        if (button) {
                            this.buttons.push({
                                editor: this.form[editor.name],
                                helpButton: button
                            });
                        }
                    }
                });
            }
        }

        public createHelpPopoverButton(editorField: Serenity.Widget<any> | string, opt: HelpPopoverOption, bootstrapPopoverOption?: any): JQuery {

            //console.log(this.form);
            let editor: any;

            if (typeof editorField == "string") {
                editor = this.form[editorField];
            }
            else {
                editor = editorField;
            }

            if (!editor) {
                return;
            }

            if (editor && editor.element) {

                let fieldName = editor.element.attr("id").split("_").pop();

                let trigger: string = Q.coalesce(opt?.trigger, "hover");
                let position: "caption" | "editor" = Q.coalesce(opt?.position, "editor");
                let html: boolean = Q.coalesce(opt?.html, false);

                bootstrapPopoverOption = Q.extend({ delay: { "show": 10, "hide": 100 } }, bootstrapPopoverOption || {});

                //let enumEditor: Serenity.EnumEditor = (Q as any).safeCast(editor, Serenity.EnumEditor);
                //if (enumEditor) {
                //    console.log(enumEditor.items);
                //}
                //else {
                //    let lookup: Serenity.LookupEditor = (Q as any).safeCast(editor, Serenity.LookupEditor);
                //    if (lookup) {
                //        //console.log(lookup.items);
                //    }
                //}

                let helpButton: JQuery;

                let uniqueId: string = J.createGuid();

                let helpKlass = "j-custom-inplace-help-button";
                let helpPopoverKlass = "j-custom-help-popover";

                if (position == "caption") {
                    helpButton = $(`<a tabindex="0" role="button" class="${helpKlass}" data-toggle="popover" style="padding-left: 5px"><i class="fa fa-question-circle" style="color: #3c8dbc;"></i></a>`);
                }
                else {
                    helpButton = $(`<a tabindex="0" role="button" class="inplace-button ${helpKlass}" data-toggle="popover"><b><i class="fa fa-question-circle" style="color: #3c8dbc"></i></b></a>`);
                }

                helpButton.attr("j-popover-id", uniqueId);
                helpButton.attr("j-popover-field-name", fieldName);
                helpButton.addClass(this.klassName);

                // https://bootstrapdocs.com/v3.3.6/docs/javascript/#popovers
                if (opt?.content) {
                    helpButton.attr('data-content', opt?.content);
                }

                if (opt?.title) {
                    helpButton.attr('title', `${Q.text(opt?.title)}`);
                }

                helpButton.attr('data-placement', Q.coalesce(opt?.placement, "auto"))
                    .attr("data-trigger", trigger)
                    .attr('data-html', `${html}`)
                    .attr('data-container', "body")
                    .click(e => {
                        let target = $(e.target);

                        if (!target.hasClass(helpKlass)) {
                            target = target.closest(`.${helpKlass}`);
                        }
                        //if (target.parent().hasClass("inplace-button")) {
                        //    target = target.parent();
                        //}
                        //else {
                        //    if (target.parent().parent().hasClass('inplace-button')) {
                        //        target = target.parent().parent();
                        //    }
                        //}

                        (target as any).popover(bootstrapPopoverOption)
                            .data('bs.popover')
                            .tip()
                            .addClass(helpPopoverKlass)
                            .addClass(`${this.klassName}`)
                            .addClass(`j-popover-id-${uniqueId}`)
                            .addClass(`${fieldName}`);

                        (target as any).popover("toggle");
                    });


                //registryEvents && registryEvents(helpButton);

                //helpButton.on('hidden.bs.popover', function () {
                //    Q.notifyInfo("hidden.bs.popover");
                //});
                //helpButton.on('show.bs.popover', function () {
                //    Q.notifyInfo("show.bs.popover");
                //});

                if (position == "caption") {
                    helpButton.appendTo(editor.getGridField().find(".caption"));
                }
                else {
                    helpButton.insertBefore(editor.getGridField().find(".vx"));
                }

                (helpButton as any).popover(bootstrapPopoverOption)
                    .data('bs.popover')
                    .tip()
                    .addClass(helpPopoverKlass)
                    .addClass(`${this.klassName}`)
                    .addClass(`j-popover-id-${uniqueId}`)
                    .addClass(`${fieldName}`);

                (helpButton as any).popover();

                return helpButton;
            }

            return null;
        }
    }

    export function createGuid() {
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
                .toString(16)
                .substring(1);
        }
        return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
    }

    export class HelpPopoverItem {
        editor: Serenity.Widget<any>;
        helpButton: JQuery;
    }

    export class HelpPopoverOption {
        content: string;
        title?: string;
        trigger?: string // "click" | "hover" | "focus" | "manual";
        placement?: "top" | "bottom" | "left" | "right" | "auto";
        position?: "caption" | "editor";
        html?: boolean;
    }
}