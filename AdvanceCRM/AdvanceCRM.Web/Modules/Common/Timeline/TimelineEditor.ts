namespace AdvanceCRM.Common {

    @Serenity.Decorators.registerEditor([Serenity.IGetEditValue, Serenity.ISetEditValue])
    @Serenity.Decorators.element("<div/>")
    export class TimelineEditor extends Serenity.TemplatedWidget<any>
        implements Serenity.IGetEditValue, Serenity.ISetEditValue {

        private isDirty: boolean;
        private items: TimelineRow[];

        constructor(div: JQuery) {
            super(div);
        }

        protected getTemplate() {
            return "<div><div id='~_Toolbar'></div><ul id='~_TimelineList' class='timeline'></ul></div>";
        }

        protected updateContent() {
            var TimelineList = this.byId('TimelineList');
            TimelineList.children().remove();
            if (this.items.length) {
                var startli = $('<li/>');
                startli.addClass('time-label');
                $('<span/>').addClass('bg-green').text(Q.formatDate(this.items[0].InsertDate, 'g')).appendTo(startli);
                startli.appendTo(TimelineList);

                for (var t1 = 0; t1 < this.items.length; t1++) {
                    var item = this.items[t1];
                    var li = $('<li/>');
                    if (item.Type == 2) {
                        $('<i/>').addClass('fa fa-sliders bg-purple').appendTo(li);
                    }
                    else if (item.Type == 3) {
                        $('<i/>').addClass('fa fa-user bg-blue').appendTo(li);
                    }
                    else if (item.Type == 4) {
                        $('<i/>').addClass('fa fa-handshake-o bg-yellow').appendTo(li);
                    }
                    else if (item.Type == 5) {
                        $('<i/>').addClass('fa fa-calendar bg-orange').appendTo(li);
                    }
                    else {
                        $('<i/>').addClass('fa fa-circle-o-notch bg-maroon').appendTo(li);
                    }
                    var div = $('<div/>');
                    div.addClass('timeline-item');
                    var span = $('<span/>');
                    span.addClass('time');
                    $('<i/>').addClass('fa fa-clock-o').appendTo(span);
                    span.text(Q.formatDate(item.InsertDate, 'g'));
                    span.appendTo(div);
                    var h3 = $('<h3/>');
                    h3.addClass('timeline-header');
                    $('<a/>').text(item.InsertUserDisplayName).appendTo(h3);
                    h3.appendTo(div);

                    $('<div/>').addClass('timeline-body').html(Q.coalesce(item.Text, '')).appendTo(div);

                    div.appendTo(li);

                    li.appendTo(TimelineList);
                }

                var endli = $('<li/>');
                $('<i/>').addClass('fa fa-clock-o bg-gray').appendTo(endli);
                endli.appendTo(TimelineList);
            }
        }

        public get value() {
            return this.items;
        }

        public set value(value: TimelineRow[]) {
            this.items = value || [];
            this.set_isDirty(false);
            this.updateContent();
        }

        public getEditValue(prop: Serenity.PropertyItem, target) {
            target[prop.name] = this.value;
        }

        public setEditValue(source, prop: Serenity.PropertyItem) {
            this.value = source[prop.name] || [];
        }

        public get_isDirty(): boolean {
            return this.isDirty;
        }

        public set_isDirty(value): void {
            this.isDirty = value;
        }

        public onChange: () => void;
    }
}