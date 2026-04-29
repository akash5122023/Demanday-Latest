
namespace AdvanceCRM.Demanday {

    export class DemandayAudioAttachment {
        static render(root: JQuery, idPrefix: string, fieldName: string, value: string) {
            if (!root || !root.length) return;
            const $input = root.find('#' + idPrefix + fieldName);
            const $field = $input.closest('.field');
            if (!$field.length) return;

            $field.find('.demanday-audio-preview').remove();

            const files = (value || '').split('|').map(f => f.trim()).filter(f => f.length > 0);
            if (!files.length) return;

            const baseUrl = Q.resolveUrl('~/upload/');
            let html = '<div class="demanday-audio-preview" style="margin-top:8px;">';
            files.forEach(file => {
                const filename = file.split('/').pop();
                const url = baseUrl + file;
                html += '<div class="audio-item" style="margin:5px 0; display:flex; align-items:center; gap:6px;">' +
                    '<audio controls preload="none" style="height:32px; max-width:260px;">' +
                    '<source src="' + url + '" type="audio/mpeg">' +
                    'Your browser does not support audio.' +
                    '</audio>' +
                    '<a href="' + url + '" download="' + filename + '" class="btn btn-sm btn-info" style="margin-left:5px;" title="Download">' +
                    '<i class="fa fa-download"></i>' +
                    '</a>' +
                    '</div>';
            });
            html += '</div>';
            $field.append(html);
        }
    }
}
