namespace _Ext {
    @Serenity.Decorators.registerClass([Serenity.IGetEditValue, Serenity.ISetEditValue])
    @Serenity.Decorators.editor()
    @Serenity.Decorators.element("<div/>")
    export class StarRatingEditor extends Serenity.TemplatedWidget<any>
        implements Serenity.IGetEditValue, Serenity.ISetEditValue {
        private $stars: JQuery;

        constructor(container: JQuery) {
            super(container);

            // Create the five stars using a template string
            const starsHtml = `
                <span class="star">&#9734;</span>
                <span class="star">&#9734;</span>
                <span class="star">&#9734;</span>
                <span class="star">&#9734;</span>
                <span class="star">&#9734;</span>
            `;

            // Add the stars to the container element
            this.element.append(starsHtml);

            // Get the stars as a jQuery collection
            this.$stars = this.element.find('.star');

            // Add click event listener to stars
            this.$stars.click((e) => {
                const index = this.$stars.index(e.target);
                this.updateStars(index + 1);
                this.element.trigger('change');
            });
        }

        public getTemplate() {
            return `<div class="star-rating-editor"></div>`;
        }

        public getEditValue(property: Serenity.PropertyItem, target: any) {
            target[property.name] = this.$stars.filter('.gold').length;
        }

        public setEditValue(source: any, property: Serenity.PropertyItem) {
            const value = source[property.name];
            this.updateStars(value != null ? value : 0);
        }

        private updateStars(value: number) {
            this.$stars.removeClass('gold').text('☆');
            this.$stars.slice(0, value).addClass('gold').text('★');
        }
    }
}

//in css
//.star - rating - editor.star {
//    color: black;
//    cursor: pointer;
//    font - size: 1.5em;
//    margin - right: 5px;
//}

//.star - rating - editor.star.gold {
//    color: gold;
//}