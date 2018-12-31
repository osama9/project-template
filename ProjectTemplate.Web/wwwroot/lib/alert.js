
toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-full-width",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

function showAlert(alert,alertDiv) {
    
    if (!alert)
        return;
    
    if (alert.isAutoHide) {

        switch (alert.alertType) {
            case 1: //Warning

                toastr.warning(alert.message);
                break;

            case 2: //Info

                toastr.info(alert.message);
                break;

            case 3: //Success

                toastr.success(alert.message);
                break;

            case 4: //Error

                toastr.error(alert.message);
                break;

            case 5: //Hide
                break;

            default:
        }
    }
    else {

        var option = {
            container: "",
            message: alert.message,
            type: "success",
            icon: "",
        }

        if (alertDiv)
            option.container = alertDiv;

        switch (alert.alertType) {
            case 1: //Warning

                option.icon = "exclamation-triangle";
                option.type = "warning";

                break;

            case 2: //Info

                option.icon = "question-circle";
                option.type = "info";

                break;

            case 3: //Success

                option.icon = "check";
                option.type = "success";

                break;

            case 4: //Error
                option.icon = "exclamation";
                option.type = "danger";

                break;

            case 5: //Hide
                break;

            default:
        }



        setAlert(option);
    }
}


function setAlert(options) {

    options = $.extend(true, {
        container: "", // alerts parent container(by default placed after the page breadcrumbs)
        place: "append", // "append" or "prepend" in container 
        type: 'success', // alert's type
        message: "", // alert's message
        close: true, // make alert closable
        reset: true, // close all previouse alerts first
        focus: true, // auto scroll to the alert after shown
        closeInSeconds: 0, // auto close after defined seconds
        icon: "" // put icon before the message
    }, options);

    var id = getUniqueID("App_alert");

    var html = '<div id="' + id + '" class="custom-alerts alert alert-' + options.type + ' fade show">' + (options.close ? '<button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>' : '') + (options.icon !== "" ? '<i class="fa-lg fa fa-' + options.icon + '"></i>  ' : '') + options.message + '</div>';

    if (options.reset) {
        $('.custom-alerts').remove();
    }

    if (!options.container) {

        if ($('.page-fixed-main-content').size() === 1) {
            $('.page-fixed-main-content').prepend(html);
        } else if (($('body').hasClass("page-container-bg-solid") || $('body').hasClass("page-content-white")) && $('.page-head').size() === 0) {
            $('.page-title').after(html);
        } else {
            if ($('.page-bar').size() > 0) {
                $('.page-bar').after(html);
            } else if ($('.page-breadcrumb, .breadcrumbs').size() !== 0) {
                $('.page-breadcrumb, .breadcrumbs').after(html);
            }
            else {
                $('#alert').html(html);
            }
        }
    } else {
        if (options.place == "append") {
            $(options.container).append(html);
        } else {
            $(options.container).prepend(html);
        }
    }

    if (options.closeInSeconds > 0) {
        setTimeout(function () {
            $('#' + id).remove();
        }, options.closeInSeconds * 1000);
    }

    return id;
}


function getUniqueID(prefix) {
    return 'prefix_' + Math.floor(Math.random() * (new Date()).getTime());
}