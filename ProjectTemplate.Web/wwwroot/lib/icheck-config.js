
function initICheck() {

    if (jQuery().iCheck) {

        $('.icheck').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue',
            increaseArea: '20%' // optional
        });
    }


}

function clearICheck() {

    if (jQuery().iCheck) {
        $(".icheck").iCheck('uncheck');
    }
    
}