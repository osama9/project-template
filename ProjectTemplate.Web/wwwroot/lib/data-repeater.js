var FormRepeater = function () {

    return {
        //main function to initiate the module
        init: function () {
            $('.mt-repeater').each(function () {

                var currentForm = $(this).parents("form")[0];

                $(this).repeater({
                    errorMessage: true,
                    show: function () {
                        $(this).slideDown();

                        refreshUnobtrusiveValidation(currentForm);
                    },

                    hide: function (deleteElement) {
                        if (confirm('Are you sure you want to delete this element?')) {
                            $(this).slideUp(deleteElement);
                        }
                    },

                    ready: function (setIndexes) {

                        refreshUnobtrusiveValidation(currentForm);
                    }

                });
            });
        }
    };

}();

jQuery(document).ready(function () {
    FormRepeater.init();
});