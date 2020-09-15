$(document).ready(function () {
    var cartValue = $('#hdnCartValue').val();
    $('#cartValues').text(cartValue);
});

function isEmail(email) {
    debugger;
    //var regex = /^([a-zA-Z0-9_.+-])+\@@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var regex = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
    //return regex.test(email);
}
$("#txtUserName").focusout(function () { callLoginMethod("txtUserName", "Please enter username or email"); });
$("#txtPassword").focusout(function () { callLoginMethod("txtPassword", "Please enter password"); });
function callLoginMethod(elementPrefix, elementComment) {
    debugger;
    if ($('#' + elementPrefix).val() == '') {
        $('#' + elementPrefix).css("border-color", "#FF0000");
        $('#error_' + elementPrefix).text(elementComment);
    }
    else {
        //$('#' + elementPrefix).css("border-color", "#2eb82e");  
        $('#' + elementPrefix).css("border-color", "");
        $('#error_' + elementPrefix).text("");
    }
}
$("#btnRegister").click(function () {
    $('#loginModal').modal('hide');
    location.href = "/Account/SignIn";
    //$.ajax({
    //    type: "GET",
    //    url: '@Url.Action("SignIn","Account")',
    //    success: function () {

    //    }
    //})
    })
$("#btnLogin").click(function () {

    if ($("#txtUserName").val() == "" || $("#txtPassword").val() == "") {
        callLoginMethod("txtUserName", "Please enter username or email");
        callLoginMethod("txtPassword", "Please enter password");
        return false;
    }
    debugger;
    // $("#loader").fadeOut(1000); 
    //$("#loader").show(); 
    //$(this).after("<img src='~/assets/img/loader.gif' alt='loading' />").fadeIn(); 
    var username = $("#txtUserName").val();
    var password = $("#txtPassword").val();

    var valdata = {
        Username: username,
        Password: password
    };

    $.ajax({
        type: "POST",
        url: '/Account/Login',
        data: valdata,
        dataType: "json",
        success: function (data) {
            if (data.success) {
                $("#invalidUser").text("");
                toastr.success(data.message, 'Success');
                //$("#loader").hide();
                location.href = "/Home/Index";
            }
            else {
                //alert("Invalid Username and Password!");
                toastr.error(data.message, 'Error');
                $("#invalidUser").text("Invalid Username and Password!");
            }
        },
        error: function () {
            //alert("Error while registration.");
        }
    });

});
$("#btnSendEmail").click(function () {
    debugger;
    if ($("#txtEmail").val() == "") {
        callLoginMethod("txtEmail", "Please enter email");
        return false;
    }
    if (!isEmail($("#txtEmail").val())) {
        $("#txtEmail").css("border-color", "");
        $("#error_txtEmail").text("Please enter a valid email.");
        return false;
    }
    var email = $("#txtEmail").val();

    var valdata = {
        Email: email
    };

    $.ajax({
        type: "POST",
        //url: '@Url.Action("SendEmail", "Account")',
        url: '/Account/SendEmail',
        data: valdata,
        dataType: "json",
        success: function (data) {
            if (data) {
                $('#forgetPasswordModal').modal('hide');
                $("#error_txtNewPassword").text("");
                $("#error_txtConfirmPassword").text("");
                $("#error_txtOTP").text("");
                $("#error_txtEmail").text("");
                $("#txtNewPassword").css("border-color", "");
                $("#txtConfirmPassword").css("border-color", "");
                $("#txtOTP").css("border-color", "");
                $("#txtEmail").css("border-color", "");
                $('#newPasswordModal').modal('show');
            }
            else {
                $("#error_txtEmail").text("Invalid email");
            }
        },
        error: function () {
            alert("Error while registration.");
        }
    });

});
$("#btnCreatePassword").click(function () {
    debugger;
    if ($("#txtNewPassword").val() == "" || $("#txtConfirmPassword").val() == "" || $("#txtOTP").val() == "") {
        callLoginMethod("txtNewPassword", "Please enter password");
        callLoginMethod("txtConfirmPassword", "Please enter confirm password");
        callLoginMethod("txtOTP", "Please enter OTP");
        return false;
    }
    if ($("#txtNewPassword").val() != $("#txtConfirmPassword").val()) {
        $("#txtNewPassword").css("border-color", "#FF0000");
        $("#txtConfirmPassword").css("border-color", "#FF0000");
        $("#error_txtNewPassword").text("Password and confirm password should be matched.");
        $("#error_txtConfirmPassword").text("Password and confirm password should be matched.");
        return false;
    }
    var newPassword = $("#txtNewPassword").val();
    var confirmPassword = $("#txtConfirmPassword").val();
    var otp = $("#txtOTP").val();


    var valdata = {
        Password: newPassword,
        OTP: otp,
    };

    $.ajax({
        type: "POST",
        //url: '@Url.Action("CreatePassword", "Account")',
        url: '/Account/CreatePassword',
        data: valdata,
        dataType: "json",
        success: function (data) {
            if (data) {
                //alert("Password created successfully!");
                toastr.success("Password created successfully", 'Success');
                $('#newPasswordModal').modal('hide');
                //$('#btnLogin').modal('show');
                $('#loginModal').modal('show');

            }
            else {
                $("#error_txtOTP").text("Invalid OTP or OTP time expire.")
            }

        },
        error: function () {
            alert("Error while registration.");
        }
    });

});
$("#btnloginModal").click(function () {
    $("#error_txtUserName").text("");
    $("#error_txtPassword").text("");
    $("#txtUserName").css("border-color", "");
    $("#txtPassword").css("border-color", "");
    $("#invalidUser").text("");
    $('#loginModal').modal('show');
})
$("#btnForgetPassword").click(function () {
    //    debugger;
    $('#loginModal').modal('hide');
    $("#error_txtEmail").text("");
    $("#txtEmail").css("border-color", "");
    $('#forgetPasswordModal').modal('show');
    //    return false;

});

function RemoveCartItemList(symbol) {

    var valdata = {
        id: symbol
    };
    debugger;
    var removeItemCount = $("#productCount_" + symbol).text().split(":")[1];
    var exitingCartValue = $("#cartValues").text();
    var newCartValue = parseInt(exitingCartValue) - parseInt(removeItemCount);
    $("#cartValues").text(newCartValue);
    if (newCartValue == 0) {
        $("#mini_cart").remove();
        $("#divSubTotal").remove();
        $("#divCartFooter").remove();
    }

    $.ajax({
        type: "POST",
        //url: '@Url.Action("RemoveFromCart", "Shopping")',
        url: '/Shopping/RemoveFromCart',
        data: valdata,
        dataType: "json",
        success: function () {
            debugger;
            //alert("added product to cart");
            toastr.success("Product remove from cart", 'Success');
            $("#cartItem_" + symbol).remove();

            //location.href = "/Shopping/CartDetails";
        },
        error: function () {
            //alert("Error while registration.");
            toastr.error("Product added to cart", 'Error');
        }
    });
}

function IsLogin() {
    debugger;
    $.ajax({
        type: "POST",
        url: "/Account/IsUserLogin",
        data: '',
        dataType: "json",
        success: function (data) {
            debugger;
            if (data == true) {
                location.href = "/PayKun/ProcessPayment";
            }
            else {
                $("#error_txtUserName").text("");
                $("#error_txtPassword").text("");
                $("#txtUserName").css("border-color", "");
                $("#txtPassword").css("border-color", "");
                $("#invalidUser").text("");
                $('#loginModal').modal('show');
            }
        },
        error: function () {
            //alert("Error while registration.");
            toastr.error("Error", 'Error');
        }
    });
}

function SearchProduct() {
    debugger;
   var id= $("#txtSearchProduct").val();
    //var valdata = {
    //    id: $("#txtSearchProduct").val()
    //};
    location.href = "/Shopping/Search/" + '?id=' + id;
    //$.ajax({
    //    type: "GET",
    //    url: "/Shopping/Search",
    //    data: valdata,
    //    success: function (data) {
    //        debugger;
           
    //    },
    //    error: function () {
    //        //alert("Error while registration.");
    //        toastr.error("Error", 'Error');
    //    }
    //});
}