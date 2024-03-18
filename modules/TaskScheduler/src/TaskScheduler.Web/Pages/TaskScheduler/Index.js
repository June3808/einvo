$(function () {

    //$("#InvoiceJournalsFilter :input").on('input', function () {
    //    dataTable.ajax.reload();
    //});

    //After abp v7.2 use dynamicForm 'column-size' instead of the following settings
    //$('#InvoiceJournalsCollapse div').addClass('col-sm-3').parent().addClass('row');

    //$('.date-picker').datetimepicker({
    //    //locale: abp.localization.currentLanguage.name,
    //    format: 'L'
    //})

    var getFilter = function () {
        var input = {};
        $("#InvoiceJournalsFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/InvoiceJournalsFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    //$('.datepicker').datepicker({
    //    format: 'dd/MM/yyyy',
    //    endDate: '+0d',
    //    autoclose: true,
    //    "showTodayButton": true,
    //});

    var l = abp.localization.getResource('TaskScheduler');

    var service = taskScheduler.taskScheduler;

    var dataTable = $('#TaskSchedulerTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,//disable default searchbox
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getAll, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Start Job'),
                                visible: abp.auth.isGranted('TaskScheduler.TaskScheduler.Update'),
                                action: function (data) {
                                    service.start(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyStartScheduleJob'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            },
                            {
                                text: l('Stop Job'),
                                visible: abp.auth.isGranted('TaskScheduler.TaskScheduler.Update'),
                                action: function (data) {
                                    service.stop(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyStopScheduleJob'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            },
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('TaskScheduler.TaskScheduler.Update'),
                                action: function (data) {
                                    location.href = abp.appPath + 'TaskScheduler/Create?Id=' + data.record.id;
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('TaskScheduler.TaskScheduler.Delete'),
                                confirmMessage: function (data) {
                                    return l('TaskSchedulerDeletionConfirmationMessage', data.record.id);
                                },
                                action: function (data) {
                                    service.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
            {
                title: l('JobStatus'),
                data: "jobStatus",
                render: function (data) {
                    return jobStatusFormatter(data);
                }
            },
            {
                title: l('JobRunStatus'),
                data: "jobRunStatus",
                render: function (data) {
                    return jobRunStatusFormatter(data);
                }
            },
            {
                title: l('JobName'),
                data: "jobName"
            },
            {
                title: l('StartDateTime'),
                data: "startDateTime"
            },
            {
                title: l('LastExecuteTime'),
                data: "lastExecuteTime"
            },
            {
                title: l('EndDateTime'),
                data: "endDateTime"
            }
        ]
    }));


    var jobRunStatusFormatter = function (value) {
        console.log(value);
        switch (value) {
            case 1:
                return 'Executing';
            case 2:
                return 'Error';
            case 3:
                return 'Finished';
            case 4:
                return 'Aborted';
            case 0:
            default:
                return 'Waiting';
        }
    }

    var jobStatusFormatter = function (value) {
        console.log(value);
        switch (value) {
            case 1:
                return 'Ready';
            case 0:
            default:
                return 'Hold';
        }
    }
});
