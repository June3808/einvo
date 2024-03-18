$(function () {
    var service = taskScheduler.taskScheduler;

    $.validator.setDefaults({
        errorClass: 'errorField',
        errorElement: 'div',
        errorPlacement: function (error, element) {
            error.addClass("ui red pointing above ui label error").appendTo(element.closest('div.field'));
        },
        highlight: function (element, errorClass, validClass) {
            $(element).closest("div.field").addClass("error").removeClass("success");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).closest(".error").removeClass("error").addClass("success");
        }
    });

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


        rebuildMisfireInstructionDropdown(checkedInput);
        
        
    }

    function rebuildMisfireInstructionDropdown(checkedInput) {
        let misfireOpt = misfireInstrForDropdown(checkedInput.val());
        console.log(misfireOpt);

        var $el = $('#misfireInstructions');
        $el.empty(); // remove old options

        $.each(misfireOpt, function (key, value) {
            $el.append($("<option></option>")
                .attr("value", value.value).text(value.name));
        });

        $el.on('change', function () {
            $("#ScheduleJob_ScheduleJob_MisfireInstruction").val($(this).val());
        });

        $el.val('0'); // reset misfire instruction to default value
    }

    triggerTypeChanged();

    $('#misfireInstructions').val(MisfireInstruction);

    $('form').on("submit",function (event) {

        var _form = $('#form');
        if (!_form.valid()) {

            event.preventDefault();
            return;
        }



        //var formData = new FormData(_form[0]);
        //formData.append("mustStartNow", true);

        //console.log(formData);
        //$.ajax({
        //    type: 'POST', enctype: 'multipart/form-data', url: '/TaskScheduler/Create',
        //    data: formData, processData: false, contentType: false, dataType: "json", cache: false,
        //    success: function (data) {
        //        window.location = '/TaskScheduler/Index';
        //    },
        //    error: function (e) {
        //        console.log(e);
        //        //abp.message.error(e);
        //        $('#dimmer').dimmer('hide');
        //        prependErrorMessage(e, $('#trigger-properties'));
        //    }
        //});
    });


    $.validator.addMethod('notequalsimple', function (e1, element, param) {

        let triggerType = $('input[name="ScheduleJob.TriggerType"]:checked').val();
        let e2 = $("#Trigger_Simple_IntervalMinutes").val();
        console.log(triggerType);

        if (triggerType == "simple") {
            if (isNaN(e1) && isNaN(e2))
                return false;

            var num1 = Number(e1);
            var num2 = Number(e2);

            //console.log(num1 == 0 && num2 == 0);
            return (num1 != 0 || num2 != 0);
        }
        return true;

    }, 'Interval cannot be 0');

    $.validator.addMethod('croncronexpression', function (e1, element, param) {

        let triggerType = $('input[name="ScheduleJob.TriggerType"]:checked').val();
        let e2 = $("#Trigger_Cron_CronExpression").val();

        if (triggerType == "cron")
            return e2 != "";
        else return true;

    }, 'Cron expression cannot be empty');


    $.validator.addMethod('notequaldaily', function (e1, element, param) {

        let triggerType = $('input[name="ScheduleJob.TriggerType"]:checked').val();
        let e2 = $("#Trigger_Daily_IntervalMinutes").val();

        if (triggerType == "daily") {
            if (isNaN(e1) && isNaN(e2))
                return false;

            var num1 = Number(e1);
            var num2 = Number(e2);

            //console.log(e1 + "+" + e2 );
            //console.log(num1 != 0 || num2 != 0);
            return (num1 != 0 || num2 != 0);
        }
        return true;

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

        $('#Trigger_Cron_CronExpression').val(c.cron("value"));
    })
});
