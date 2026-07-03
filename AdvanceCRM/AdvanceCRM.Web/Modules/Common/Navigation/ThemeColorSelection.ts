namespace AdvanceCRM.Common {
    // Theme color picker — additive, sets a `theme-color-<name>` class on body.
    // CSS overrides in ExtSkins.css are scoped to body.skin-modernize* so other
    // skins (Azure, Cosmos, etc.) are unaffected.
    export class ThemeColorSelection extends Serenity.Widget<any> {
        constructor(select: JQuery) {
            super(select);

            this.change(e => {
                $.cookie('ThemeColorPreference', select.val(), {
                    path: Q.Config.applicationPath,
                    expires: 365
                });
                $('body').removeClass('theme-color-' + this.getCurrentColor());
                $('body').addClass('theme-color-' + select.val());
            });

            Q.addOption(select, 'blue',   Q.text('Site.Layout.ThemeColorBlue'));
            Q.addOption(select, 'aqua',   Q.text('Site.Layout.ThemeColorAqua'));
            Q.addOption(select, 'purple', Q.text('Site.Layout.ThemeColorPurple'));
            Q.addOption(select, 'teal',   Q.text('Site.Layout.ThemeColorTeal'));
            Q.addOption(select, 'cyan',   Q.text('Site.Layout.ThemeColorCyan'));
            Q.addOption(select, 'orange', Q.text('Site.Layout.ThemeColorOrange'));
            Q.addOption(select, 'white',  Q.text('Site.Layout.ThemeColorWhite'));

            select.val(this.getCurrentColor());
        }

        protected getCurrentColor() {
            var cls = Q.first(($('body').attr('class') || '').split(' '),
                x => Q.startsWith(x, 'theme-color-'));
            if (cls) {
                return cls.substr('theme-color-'.length);
            }
            return 'blue';
        }
    }
}
