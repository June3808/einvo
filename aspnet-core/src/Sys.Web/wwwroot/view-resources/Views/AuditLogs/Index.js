$(function () {
    var l = abp.localization.getResource('SysResource');

    var service = Sys.auditLogs.auditLog;
    var detailModal = new abp.ModalManager(abp.appPath + 'AuditLogs/DetailModal');

    var dataTable = $('#SystemLogTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList, function () {
            let includeException = false;

            if (hasException == "true") {
                includeException = true;
            }
            else if (hasException == "") {
                includeException = null;
            }

            return {
                queryString: queryString,
                startTime: moment.utc(startTime),
                endTime: moment.utc(endTime),
                userName: userName,
                hasException: hasException,
                applicationName: applicationName,
                //serviceName: serviceName
                //ApplicationName: MethodName,
                //Browser: Browser,
            }
        }),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Detail'),
                                action: function (data) {
                                    for (var rec in data.record) {
                                        currentDetailModalData[rec] = data.record[rec];
                                    }

                                    console.log("currentDetailModalData");
                                    console.log(currentDetailModalData);
                                    detailModal.open();
                                }
                            }
                        ]
                }
            },
            //{ data: "executionTime" },
            {
                title: l('executionTime'),
                data: "executionTime",
                render: function (data) {
                    return luxon
                        .DateTime
                        .fromISO(data, {
                            locale: abp.localization.currentCulture.name
                        }).toFormat("yyyy-MM-dd HH:mm:ss");
                }
            },
            {
                title: l('httpMethod'),
                data: "httpMethod",
                render: function (data,type,row) {
                    console.log(row);
                    let html = '<span class="badge bg-warning">' + row.httpStatusCode + '</span>&nbsp' + row.httpMethod;
                    return html;
                }
            },
/*            { data: "httpStatusCode" },*/
            { data: "userName" },
            { data: "applicationName" },
        ]
    }));

    $('#search-button').click(function (e) {
        queryString = $('#QueryModel_QueryString').val();
        startTime = $('#QueryModel_StartTime').val();
        endTime = $('#QueryModel_EndTime').val();
        hasException = $('#QueryModel_HasException').val();
        applicationName = $('#QueryModel_ApplicationName').val();
        userName = $('#QueryModel_UserName').val();
        //serviceName = $('#QueryModel_ServiceName').val();

        dataTable.ajax.reload()
    })
});