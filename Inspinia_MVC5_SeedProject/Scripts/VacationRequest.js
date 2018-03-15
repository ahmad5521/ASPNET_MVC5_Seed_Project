$(document).ready(function () {
    //Validate();

    //i-check
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });    

    $(".touchspin1").TouchSpin({
        buttondown_class: 'btn btn-white',
        buttonup_class: 'btn btn-white'
    });

    loadBalance();
})

function loadBalance() {
    $('#ibox1').children('.ibox-content').toggleClass('sk-loading'); $('#ibox1').addClass('bounce');
    $.ajax({
        url: "/VacationRequest/GetBalance/63012460",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#annualH').text(result.Annual);
            $('#privH').text(result.Priv);
            $('#holydaysH').text(result.Holyday);
            $('#overtimeH').text(result.Overtime);
            $('#urgentH').text(result.Urgent);
            $('#mngH').text(result.Mng);
            //$('#mngH').text(0);
            $('#ibox1').children('.ibox-content').toggleClass('sk-loading'); $('#ibox1').addClass('bounce');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}



