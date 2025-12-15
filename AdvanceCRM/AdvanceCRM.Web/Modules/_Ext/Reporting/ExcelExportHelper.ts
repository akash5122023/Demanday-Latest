namespace _Ext {

    export interface ExcelExportOptions {
        grid: Serenity.DataGrid<any, any>;
        service: string;
        onViewSubmit: () => boolean;
        editRequest?: (request: Serenity.ListRequest) => Serenity.ListRequest;
        title?: string;
        hint?: string;
        separator?: boolean;
    }

    export namespace ExcelExportHelper {

        export function createToolButton(options: ExcelExportOptions): Serenity.ToolButton {

            return {
                title: Q.coalesce(options.title, ''),
                hint: Q.coalesce(options.hint, 'Excel'),
                cssClass: 'export-xlsx-button',
                onClick: function () {
                    if (!options.onViewSubmit()) {
                        return;
                    }

                    let grid = options.grid;

                    var request = Q.deepClone(grid.getView().params) as Serenity.ListRequest;
                    request.Take = 0;
                    request.Skip = 0;
                    var sortBy = grid.getView().sortBy;
                    if (sortBy) {
                        request.Sort = sortBy;
                    }

                    request.ExportColumns = [];
                    (request as any).IncludeColumns = [];
                    let columns = grid.getGrid().getColumns();
                    for (let column of columns) {
                        const columnKey = column.id || column.field;
                        if (!columnKey) {
                            continue;
                        }

                        if (columnKey.length > 1 && columnKey.charAt(0) === '_' && columnKey.charAt(1) === '_') {
                            continue;
                        }

                        request.ExportColumns.push(columnKey);
                        (request as any).IncludeColumns.push(columnKey);
                    }

                    if (options.editRequest)
                        request = options.editRequest(request);

                    Q.postToService({ service: options.service, request: request, target: '_blank' });
                },
                separator: options.separator
            };
        }
    }
}