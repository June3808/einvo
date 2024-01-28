(function ($) {
    abp.modals.AuditlogDetailModal = function () {

        let l = abp.localization.getResource('SysResource');

        this.init = function (modalManager) {
            _modalManager = modalManager;


            let actions = $(".actions.hidden");
            console.log(actions);

            actions.each(function (i) {
                let input = $('pre.actions_' + (i+1));
                let text = $(this).val();

                let formattedtext = getFormattedParameters(text);

                input.html(formattedtext);
            });
        };

        function getFormattedParameters(parameters) {
            try {
                let json = JSON.parse(parameters);
                return JSON.stringify(json, null, 4);
            } catch (e) {
                return parameters;
            }
        }
    }
})(jQuery);