var datePickerSelector = ".date-picker";
var hijriFormat = 'hYYYY-hMM-hDD';
var gregFormat = 'DD-MM-YYYY';
var timeFormat = 'HH:mm a';
var currentLangugae = 'ar-sa';


function setupDate(language) {

    if (language) {
        currentLangugae = language;

    }

    $(datePickerSelector).each(function () {

        moment.locale(currentLangugae, {
            week: { dow: 7 } // Monday is the first day of the week
        });

        initGregDate(this);

    });

    $.validator.addMethod('date', function (value, element) {

        var isValid = moment(value, 'iYYYY-iMM-iDD').isValid();

        if (!isValid) {
            isValid = /\d{1,2}-\d{4}$/.test(value);
        }

        return isValid;
    });


}

function switchDate(btn, otherDateElement) {
    var datePicker = $(btn).closest(datePickerSelector).data("DateTimePicker");
    var item = $(btn).closest(datePickerSelector)[0];

    var otherDatePicker;
    var otherDateItem;

    var form = $(btn).closest('form');
    if (form) {
        var otherDatePicker = $($(form).find(otherDateElement)).closest(datePickerSelector).data("DateTimePicker");
        var otherDateItem = $($(form).find(otherDateElement)).closest(datePickerSelector)[0];
    }
    else {
        otherDatePicker = $(otherDateElement).closest(datePickerSelector).data("DateTimePicker");
        otherDateItem = $(otherDateElement).closest(datePickerSelector)[0];
    }

    var isHijri = datePicker.hijri() == true;

    if (!isHijri) {
        setHijriDate(datePicker, item);

        if (otherDatePicker) {
            setHijriDate(otherDatePicker, otherDateItem);
        }

    } else {
        setGregDate(datePicker, item);

        if (otherDatePicker) {
            setGregDate(otherDatePicker, otherDateItem);
        }
    }

}

function setHijriDate(datePicker, item) {

    datePicker.format(getHijriFormat(item));
    datePicker.dayViewHeaderFormat("hMMMM hYYYY");
    datePicker.hijri(true);
}

function setGregDate(datePicker, item) {

    datePicker.format(getGregFromat(item));
    datePicker.dayViewHeaderFormat("MMMM YYYY");
    datePicker.hijri(false);
}

function initGregDate(item) {


    $(item).datetimepicker({
        locale: currentLangugae,
        format: getGregFromat(item),
        date: getDefaultDate(item),
        dayViewHeaderFormat: "MMMM YYYY",
        allowInputToggle: true,
        showTodayButton: true,
        sideBySide: isSideBySide(item),
        viewMode: getViewMode(item),
        minDate: getMinDate(item),
        maxDate: getMaxDate(item),
        useCurrent: false,
        showClear: true,

        keepOpen: false,
        debug: false,
    });
}

function initHijriDate(item) {


    $(item).datetimepicker({
        locale: currentLangugae,
        format: getHijriFormat(item),
        dayViewHeaderFormat: "hMMMM hYYYY",
        hijri: true,
        allowInputToggle: true,
        showTodayButton: false,
    });
}

function getViewMode(item) {

    var viewMode = $(item).attr("data-view-mode");

    if (viewMode)
        return viewMode;

    return "days";
}

function isMonthOnly(item) {
    return $(item).attr("data-month-only");
}

function getDefaultDate(item) {
    var defaultDate = $(item).attr('data-default-date');

    var inputVal = $(item).find('input').val();

    if (inputVal != '')
        defaultDate = $(item).find('input').val();

    if (defaultDate === undefined)
        return '';

    return moment(defaultDate, getGregFromat(item));
}

function getGregFromat(item) {

    var selectType = $(item).attr("data-select-mode");

    var format = gregFormat;

    if (selectType == 'month') {
        if (isMonthOnly(item))
            format = "M";
        else
            format = "MM-YYYY";
    }

    if (selectType == 'year') {
        format = "YYYY";
    }

    if (selectType == 'date_time') {
        format = gregFormat + " " + timeFormat;
    }

    return format;

}

function getHijriFormat(item) {

    var selectType = $(item).attr("data-select-mode");

    var format = hijriFormat;

    if (selectType == 'month')
        if (isMonthOnly(item))
            format = "hM";
        else
            format = "hYYYY-hMM";

    if (selectType == 'year') {
        format = "hYYYY";
    }

    if (selectType == 'date_time') {
        format = hijriFormat + " " + timeFormat;
    }

    return format;

}

function isSideBySide(item) {
    var isSideBySide = $(item).attr("data-side-by-side");

    if (isSideBySide)
        return true;

    return false;
}


function getMinDate(item) {

    var minDate = $(item).attr("data-min-date");

    if (minDate)
        return moment(minDate, 'iDD-iMM-iYYYY');

    return false;

}

function getMaxDate(item) {

    var maxDate = $(item).attr("data-max-date");

    if (maxDate)
        return moment(maxDate, 'iDD-iMM-iYYYY');


    return false;
}