$(function () {
    var service = Sys.taskSchedulers;

    function misfireInstrForDropdown(triggerType) {

        const map = $misfireInstructionsMap[triggerType.toLowerCase()];
        const keys = Object.keys(map);

        const result = [];
        for (let i = 0; i < keys.length; i++) { // prepare suitable object for dropdown
            result.push({ name: map[keys[i]], value: keys[i] });
        }

        return result;
    }

    if (!$('#trigger-type-radios input:checked').val()) {
        $('#trigger-type-radios input:first').attr('checked', true);
        triggerTypeChanged();
    }

    $('#trigger-type-radios input').change(function () { triggerTypeChanged(); })

    $('#DeleteJobButton').click(function () {
        deleteJob();
    })

    function deleteJob() {
        let id = $("#ScheduleJobId").val();

        abp.message.confirm(
            'Are you sure you want to delete this job?',
            function (isConfirmed) {
                if (isConfirmed) {
                    service.delete(id)
                        .then(function () {
                            location.href = abp.appPath + 'TaskScheduler/';
                        });
                }
            }
        );
    }

    function triggerTypeChanged() {

        const checkedInput = $('#trigger-type-radios').find(':checked');
        $('.trigger-type').hide();

        let triggerType = checkedInput.val().toLowerCase();
        console.log(triggerType);
        $('.' + triggerType + '.trigger-type').show();

        $('#specificTriggerHeader').text(checkedInput.closest('.radio').find('label').text() + ' Trigger Properties');

        let misfireOpt = misfireInstrForDropdown(checkedInput.val());
        console.log(misfireOpt);

        var $el = $('#misfireInstructions');
        $el.empty(); // remove old options

        $.each(misfireOpt, function (key, value) {
            $el.append($("<option></option>")
                .attr("value", value.value).text(value.name));
        });

        //$('#misfireInstructions').dropdown('change values', misfireOpt);
        //$('#misfireInstructions').dropdown('set selected', '0'); // reset misfire instruction to default value
    }

    //$('#misfireInstructions').dropdown('setting', 'onChange', function (value) {
    //    $('#selectedMisfireInstruction').val(value); // value is mirrored to hidden input because dynamically changed dropdown's value is not propagated properly during form submit
    //});


    //$('#misfireInstructions').dropdown('set selected', MisfireInstruction); // because misfire dropdown values were changed in triggerTypeChanged
    //$('#triggerPriority').dropdown('set selected', $("#triggerPriority").val()); // in case priority is out of range [1-10]


    //$('.submit-buttons .btn-submit').click(function (event) {
 

    //    var _form = $('#form');
    //    if (!_form.valid()) {
    //        event.preventDefault();
    //        return;
    //    }


    //    //var formData = new FormData(_form[0]);
    //    //formData.append("mustStartNow", true);

    //    //console.log(formData);
    //    //$.ajax({
    //    //    type: 'POST', enctype: 'multipart/form-data', url: '/TaskScheduler/Create',
    //    //    data: formData, processData: false, contentType: false, dataType: "json", cache: false,
    //    //    success: function (data) {
    //    //        window.location = '/TaskScheduler/Index';
    //    //    },
    //    //    error: function (e) {
    //    //        console.log(e);
    //    //        //abp.message.error(e);
    //    //        //$('#dimmer').dimmer('hide');
    //    //        //prependErrorMessage(e, $('#trigger-properties'));
    //    //    }
    //    //});
    //});

    $.validator.addMethod('greaterDateThan', function (datefrom, element, dateto) {

        var e1 = $("#EndDateTime").val();
        var e2 = $("#StartDateTime").val();

        if (e1 == "" && e2 == "")
            return true;

        if (e1 == "" && e2 != "")
            return true;

        console.log({ e1: e1, e2: e2, d1: moment(e1, dateFmt), d2: moment(e2, dateFmt) });

        if (!/Invalid|NaN/.test(moment(e1, dateFmt))) {
            return moment(e1, dateFmt).isAfter(moment(e2, dateFmt));
        }

        return isNaN(e1) && isNaN($(e2).val())
            || (Number(e1) > Number($(e2).val()));
        return false;

    }, 'End Date cannot be more than Start Date');

    $.validator.addMethod('notEqualSimple', function (e1, element, param) {

        var e2 = $("#simple_IntervalMinutes").val();

        if (isNaN(e1) && isNaN(e2))
            return false;

        var num1 = Number(e1);
        var num2 = Number(e2);


        return num1 != num2;

    }, 'Interval cannot be 0');

    $.validator.addMethod('notEqualDaily', function (e1, element, param) {

        var e2 = $("#daily_IntervalMinutes").val();

        if (isNaN(e1) && isNaN(e2))
            return false;

        var num1 = Number(e1);
        var num2 = Number(e2);


        return num1 != num2;

    }, 'Interval cannot be 0');

    function prependErrorMessage(e, parent) {
        $(".ui.negative.message").remove();
        parent.prepend(initErrorMessage.call($(getErrorMessage(e))));
    }

    // Initialize DOM with cron builder

    var initCronVal = $('#cron-expression').val();
    var c;

    if (initCronVal) {
        c = $('#selector').cron({
            "initial": $('#cron-expression').val()
        });
    }
    else {
        c = $('#selector').cron();
    }


    // Add event handler to button
    $('#cronGenerator').click(function () {

        $('#ScheduleJob_Trigger_Cron_CronExpression').val(c.cron("value"));
    })
});
