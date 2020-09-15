function SubmitPayment() {
    debugger;
    var paymentDetailsRequestModel = {
        Name: $('#txtFullName').val(),
        ProductName: $('#txtProductName').val(),
        Email: $('#txtEmail').val(),
        Price: $('#totalAmount').text(),
        ContactNumber: $('#txtContactNumber').val(),
        Country: $('#txtCountry').val(),
        State: $('#txtState').val(),
        City: $('#txtCity').val(),
        ZipCode: $('#txtZipCode').val(),
        Address: $('#txtAddress').val()
    }    
    $.ajax({
        type: "Post",
        url: "/PayKun/ProcessPayment",
        data: paymentDetailsRequestModel,
        cache: false,
        success: function (data) {
            $("#payment_form").html(data);
        }
    });

    //alert('payment done');
}