$(function () {
    // Value is the element to be validated, params is the array of name/value pairs of the parameters extracted from the HTML, element is the HTML element that the validator is attached to
    $.validator.unobtrusive.adapters.add("greaterthan", ["otherfieldname", "allowequal", "allowmeempty", "allowotherempty"], function (options) {
        options.rules['greaterthan'] = {
            'allowequal': options.params['allowequal'],
            'otherfieldname': "#" + options.params.otherfieldname,
            'allowmeempty': options.params.allowmeempty,
            'allowotherempty': options.params.allowotherempty

        };
        options.messages["greaterthan"] = options.message;

        var $form = $(options.form);
        var otherFieldId = options.params.otherfieldname;

        if (otherFieldId.indexOf('#') != 0)
            otherFieldId = '#' + otherFieldId;

        $form.find(otherFieldId).closest('.date-picker').on("dp.change", function (e) {
            var validator = $form.validate();
            validator.element(options.element);
        });
    });

    // Value is the element to be validated, params is the array of name/value pairs of the parameters extracted from the HTML, element is the HTML element that the validator is attached to
    $.validator.addMethod("greaterthan", function (value, element, params) {

        var $form = $($(this)[0].currentForm);
        var validator = $form.validate();

        var $input = $form.find("#" + element.id);

        if (params.otherfieldname.indexOf('#') != 0)
            params.otherfieldname = '#' + params.otherfieldname;

        var $other = $form.find(params.otherfieldname);

        var other = $other.val();

        if (other == "" || other == undefined) {
            params.otherfieldname = params.otherfieldname.replace("#", "");
            other = $(document).find("input[id*= " + params.otherfieldname + "]").val();
        }

        if ((value == "" || value == undefined) && (other == "" || other == undefined)) {
            return true;
        }


        if ($input.parents('.date-picker').length > 0) {
            value = getDate(value, $input);
            other = getDate(other, $other);
        }


        if (isNaN(value) || isNaN(other)) {
            return true;
        }

        if (params.allowmeempty == "True" && params.allowotherempty == "False") {

            if (($input.val() == "") && (other != "" && other != undefined))
                return true;
        }

        if (params.allowmeempty == "False" && params.allowotherempty == "True") {

            if (($other.val() == "") && (value != "" && value != undefined))
                return true;
        }


        return params.allowequal == "True" ? (value >= other) : value > other;
    });

    function getDate(value, element) {
        var datePickerInstance = element.closest('.date-picker').data("DateTimePicker");

        if (typeof datePickerInstance != 'undefined' && (typeof datePickerInstance.hijri === 'function' && datePickerInstance.hijri())) {
            return moment(value, 'hYYYY-hMM-hDD');
        } else {
            return moment(value, 'iDD-iMM-iYYYY');
        }
    }
});