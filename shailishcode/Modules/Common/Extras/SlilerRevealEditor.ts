namespace AdvanceCRM {
    export class SlilerRevealEditorOptions {
        initDialog: () => Serenity.EntityDialog<any, any>;
        onDataChangeCallback?: () => void;
        sliderWidth?: any;
        entityOrId?: any;

        // if we want to show next/previous buttons on slider
        // then we also need to include grid data
        grid?: Serenity.DataGrid<any, any>;
        showNextPreviousButtons?: boolean;
    }

    @Serenity.Decorators.registerClass()
    export class SlilerRevealEditor extends Serenity.TemplatedWidget<SlilerRevealEditorOptions> {

        private slider: JQuery;
        private overlayDiv: JQuery = $(`<div class="custom-slide-reveal-overlay ui-dialog" style="position: fixed; top: 0px; left: 0px; height: 100%; width: 100%; background-color: rgba(0, 0, 0, 0.5);"></div>`);
        private dlg: Serenity.EntityDialog<any, any>;

        private btnPreviousRecord: JQuery = $(`<div class="tool-button btn-previous-record icon-tool-button" style="display: none"><div class="button-outer"><span class="button-inner"><i class="fa fa-step-backward text-purple"></i> Previous</span></div></div>`);
        private btnNextRecord: JQuery = $(`<div class="tool-button btn-next-record icon-tool-button" style="display: none"><div class="button-outer"><span class="button-inner"><i class="fa fa-step-forward text-purple"></i> Next</span></div></div>`);

        constructor(container: JQuery, opt?: SlilerRevealEditorOptions) {
            super(container, opt);

            this.dlg = this.options.initDialog();

            // #region pre/next toolbar
            let dlgUniqueId = this.dlg.element.attr("id");
            let dlgToolbar = this.dlg.element.find(`#${dlgUniqueId}_Toolbar`).find(`.tool-buttons`);

            let nextPreOuter = $(`<div class="buttons-inner"></div>`);

            this.btnPreviousRecord.click(e => {
                let self = this;

                (function back(loadedNewPage?: boolean) {
                    let listIds = self.options.grid.view.getItems().map(x => x[(self.options.grid as any).getIdProperty()]);

                    if (self.options.grid !== null && typeof self.options.grid !== 'undefined') {
                        if (loadedNewPage) {
                            let preId = listIds[listIds.length - 1];
                            self.options.entityOrId = preId;
                            self.loadByIdAndOpenSlider(preId);
                            (self.slider as any).slideReveal('show');
                        }
                        else {
                            let currentEntityId = (self.dlg as any).entityId;
                            if (currentEntityId !== null && typeof currentEntityId !== 'undefined') {

                                let currentIdIndex = listIds.indexOf(currentEntityId);
                                if (currentIdIndex <= 0) {
                                    self.backToPreviousPage(() => back(true), listIds[0]);
                                }
                                else {
                                    let preId = listIds[currentIdIndex - 1];
                                    self.options.entityOrId = preId;
                                    self.loadByIdAndOpenSlider(preId);
                                    (self.slider as any).slideReveal('show');
                                }
                            }
                        }
                    }
                })();
            });

            this.btnNextRecord.click(e => {
                let self = this;
                (function next(loadedNewPage?: boolean): void {
                    let listIds = self.options.grid.view.getItems().map(x => x[(self.options.grid as any).getIdProperty()]);

                    if (self.options.grid !== null && typeof self.options.grid !== 'undefined') {
                        if (loadedNewPage) {
                            let nextId = listIds[0];
                            self.options.entityOrId = nextId;
                            self.loadByIdAndOpenSlider(nextId);
                            (self.slider as any).slideReveal('show');
                        }
                        else {
                            let currentEntityId = (self.dlg as any).entityId;
                            if (currentEntityId !== null && typeof currentEntityId !== 'undefined') {
                                let listIds = self.options.grid.view.getItems().map(x => x[(self.options.grid as any).getIdProperty()]);

                                let currentIdIndex = listIds.indexOf(currentEntityId);
                                if (currentIdIndex >= listIds.length - 1) {
                                    self.goToNextPage(() => next(true), listIds[0]);
                                }
                                else {
                                    let nextId = listIds[currentIdIndex + 1];
                                    self.options.entityOrId = nextId;
                                    self.loadByIdAndOpenSlider(nextId);
                                    (self.slider as any).slideReveal('show');
                                }
                            }
                        }
                    }
                })();
            });

            nextPreOuter.append(this.btnPreviousRecord);
            nextPreOuter.append(this.btnNextRecord);

            dlgToolbar.find(".buttons-outer").append(nextPreOuter);

            // #endregion

            //container.appendTo(target);
            container.appendTo($("body"));

            if (this.options.showNextPreviousButtons && this.options.grid !== null && typeof this.options.grid !== "undefined") {
                this.btnPreviousRecord.show();
                this.btnNextRecord.show();
            }

            if ($(".custom-slide-reveal-overlay").length > 0) {
                this.overlayDiv.css("opacity", 0.3);
            }

            this.overlayDiv.insertBefore(this.element);

            this.overlayDiv.click(e => {
                try {
                    (this.slider as any).slideReveal('hide');
                } catch{ }
            });


            let targetIndex = 0;
            try {
                targetIndex = Q.parseInteger($(".s-Dialog").css("z-Index"));
            } catch{ }

            targetIndex = targetIndex.toString() == "NaN" ? 0 : targetIndex;
            targetIndex = targetIndex <= 1100 ? 1100 : targetIndex;

            this.overlayDiv.css("z-index", targetIndex + 1);
            this.element.css("z-index", targetIndex + 1);
            this.byId("sliderCloseButton").css("z-index", targetIndex + 1);

            this.element.css("position", "relative");

            this.dlg.element.addClass("flex-layout");

            let nbrExistedSlider = $(".slider-container").length;

            //let topHeight = $('header.main-header').height();
            this.slider = (this.byId("sliderContainer") as any).slideReveal({
                push: false,
                position: 'right',
                zIndex: 1100,
                overlay: false,
                autoEscape: true,
                //top: topHeight,
                width: Q.coalesce(this.options.sliderWidth, `calc(50% - ${nbrExistedSlider * 40}px)`),
                trigger: this.byId("sliderCloseButton"),
                show: (slider, trigger) => {
                    (this.dlg as any).handleResponsive();
                },
                shown: (slider, trigger) => {
                    $(slider).css("right", "0px");
                },
                hide: (slider, trigger) => {

                },
                hidden: (slider, trigger) => {

                    try {
                        this.overlayDiv.remove();
                    } catch { }

                    try {
                        this.element.remove();
                    } catch{ }

                    try {
                        this.dlg.dialogClose();
                    } catch { }

                    $(".slider-full-height-width").last().remove();
                    Q.Router.replaceLast("");
                }
            });

            this.dlg.element.on("ondatachange", (x, data) => {
                this.options.onDataChangeCallback && this.options.onDataChangeCallback();
            });

            this.dlg.element.one('dialogclose panelclose', () => {
                (this.slider as any).slideReveal('hide');
                this.options.onDataChangeCallback && this.options.onDataChangeCallback();
                Q.Router.replaceLast("");
            });
        }

        private backToPreviousPage(callback?: () => void, firstIdOfOldPage?: any) {
            let currentPage = parseInt($(".slick-pg-current").val());
            let prePage = currentPage - 1;

            let isFirstPage = false;
            if (prePage < 1) {
                prePage = 1;
                isFirstPage = true;
            }

            this.options.grid.view.seekToPage = prePage;
            this.options.grid.refresh();

            if (!isFirstPage) {
                let self = this;
                (function execCallback() {
                    setTimeout(function () {
                        if (self.options.grid.view.getItems()[0] !== firstIdOfOldPage) {
                            callback && callback();
                        }
                        else {
                            execCallback();
                        }
                    }, 500);
                })();
            }
        }

        private goToNextPage(callback?: () => void, firstIdOfOldPage?: any) {
            let currentTotalPage = parseInt($(".slick-pg-total").text());
            let currentPage = parseInt($(".slick-pg-current").val());
            let nextPage = currentPage + 1;

            let isLastPage = false;
            if (nextPage > currentTotalPage) {
                nextPage = currentTotalPage;
                isLastPage = true;
            }

            this.options.grid.view.seekToPage = nextPage;
            this.options.grid.refresh();

            if (!isLastPage) {
                let self = this;
                (function execCallback() {
                    setTimeout(function () {
                        if (self.options.grid.view.getItems()[0] !== firstIdOfOldPage) {
                            callback && callback();
                        }
                        else {
                            execCallback();
                        }
                    }, 500);
                })();
            }
        }

        public handleEditItem() {
            if (this.options.entityOrId !== null && typeof this.options.entityOrId == "string") {
                this.loadByIdAndOpenSlider(this.options.entityOrId);
                Q.Router.dialog(this.element, this.dlg.element, () => 'edit/' + this.options.entityOrId.toString());
            }
            else {
                Q.Router.dialog(this.element, this.dlg.element, () => "new");
                this.loadNewAndOpenSlider();
            }
        }

        public loadByIdAndOpenSlider(id: any) {

            this.dlg.loadById(id,
                response => window.setTimeout(() => {

                    this.dlg.dialogOpen(false);

                    this.convertJqueryDialogToSlider();

                }, 10),
                () => {
                    try {
                        (this.slider as any).slideReveal('hide');
                    } catch{

                    }

                    if (!this.element.is(':visible')) {
                        this.element.remove();
                    }
                });

            this.injectDialogIntoPanel(id);
        }

        public loadNewAndOpenSlider() {

            this.btnPreviousRecord.hide();
            this.btnNextRecord.hide();

            this.dlg.loadNewAndOpenDialog(false);

            this.convertJqueryDialogToSlider();

            this.injectDialogIntoPanel();
        }

        private convertJqueryDialogToSlider() {
            if (!this.dlg.element.parent().is(this.byId("panelContainer"))) {
                let orgDlgParent = this.dlg.element.parent();
                this.dlg.element.appendTo(this.byId("panelContainer"));

                orgDlgParent.remove();

                this.dlg.element.addClass("ui-dialog");
                this.dlg.element.addClass("flex-layout");
                this.dlg.element.addClass("slider-full-height-width");
            }
        }

        private injectDialogIntoPanel(id?: any) {
            var nbrRetry = 0;

            var self = this;
            (function appendDialogIntoPanel() {
                setTimeout(function () {
                    nbrRetry++;

                    if (nbrRetry >= 100) {
                        return;
                    }

                    if (self.slider.find(".s-TemplatedDialog").length === 1) {

                        (self.slider as any).slideReveal('show');

                        //self.options.grid.element.removeClass('hidden').removeClass('panel-hidden');
                    }
                    else {
                        appendDialogIntoPanel();
                    }

                }, 100);
            })();

        }

        getTemplate() {
            return `<div id="~_sliderContainer" class="slider-container ui-dialog" style="background-color:#fff; position:relative; border-left: solid 1px #c9c9c9; overflow: auto;">
                        <div id="~_sliderCloseButton" style="width:25px; height: 25px; position: absolute; top: 10px; right: 13px;color: red; cursor: pointer">
                            <i class="fa fa-lg fa-times"></i>
                        </div>
                        <div id="~_panelContainer">
                        </div>
                    </div>`;
        }
    }
}