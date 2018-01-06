﻿var btnSubmit;
var resultStatus =
    {
        Success: 1
    };

$.ajaxSetup({
    cache: false,
    error: function (response) {
        if (response.status === 500) { // InternalServerError 
            openPopupDialog(response.responseText);
        }
    }
});

$(function () {
    //Set date format && Date format Validation
    //$.validator.addMethod("date",
    //function (value, element) {
    //    if (this.optional(element)) {
    //        return true;
    //    }
    //    var valid = true;
    //    try {
    //        $.datepicker.parseDate("dd/mm/yy", value);
    //    }
    //    catch (err) {
    //        valid = false;
    //    }
    //    return valid;
    //});

    // Disable all submit buttons within 
    // the form that is being submitted
    $(document).on("submit", "form", function () {
        btnSubmit = $(this).find('button[type="submit"],[type="button"]');
        btnSubmit.attr("disabled", "disabled");
    });
});

function ReEnableFormSubmit() {
    // Re-enable button after some time in case something went wrong
    if (typeof btnSubmit != "undefined")
        btnSubmit.removeAttr("disabled");
}

function isArabicUI() {

    var name = "CurrentCulture";
    var parts = document.cookie.split(name + "=");
    var lang = "En";
    if (parts.length === 2)
        lang = parts.pop().split(";").shift();

    if (lang.indexOf("En") === 0)
        return false;
    else
        return true;
}

function openPopupDialog(data) {
    $("#modal").html(data);
    $("#modal").modal("show");

    // Re-enable button after some time in case something went wrong
    ReEnableFormSubmit();
}

function showSuccess(redirectToUrl, message) {
    Lobibox.notify(
        "success",
        {
            msg: message,
            delay: 3000,
            iconSource: "fontAwesome",
            soundPath: "../../Content/plugins/lobibox/sounds/",
            redirectToUrl: redirectToUrl
        }
    );
}

function OnSuccess(response) {
    if (response.Status === resultStatus.Success)
        showSuccess(response.RedirectToUrl, response.Message);
    else
        openPopupDialog(response);
}

function OnFailure(response) {
    openPopupDialog(response.responseText);
}

function OnModalSuccess(response) {
    ReEnableFormSubmit();
}

function OnModalFailure(response) {
    openPopupDialog(response.responseText);
}

function revalidateForm() {
    $("Form").removeData("validator") /* added by the raw jquery.validate plugin */
                      .removeData("unobtrusiveValidation");  /* added by the jquery unobtrusive plugin */
    $.validator.unobtrusive.parse(document);
}