﻿var $fileInput = $("#txtFileUpload");

$(document).ready(function () {

    if ($("#txt_SourceId").val() > 0) // Edit
        initializeImagePreview();
    else
        initializeFileInput([], []); // Add
});

function initializeFileInput(files, filesConfig) {
    $fileInput.fileinput({
        uploadUrl: uploadLinks.save,
        uploadExtraData: { sourceId: $("#txt_SourceId").val(), sourceType: $("#txt_SourceType").val() },
        deleteUrl: uploadLinks.delete,
        uploadAsync: true,
        showUpload: false,
        showRemove: false,
        showUploadedThumbs: false,
        allowedFileExtensions: ["jpg", "png", "gif"],
        minFileCount: 1,
        maxFileCount: 30,
        maxFileSize: 2000,
        overwriteInitial: false,
        validateInitialCount: true,
        initialPreview: files,
        initialPreviewConfig: filesConfig
    });
}

function initializeImagePreview() {

    var files = [];
    var filesConfig = [];

    $.ajax({
        type: "GET",
        url: uploadLinks.getImagePreview,
        data: { sourceId: $("#txt_SourceId").val(), sourceType: $("#txt_SourceType").val() },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $.each(data, function (index, item) {
                files.push("<img src=" + item.Url.replace(/ /g, "%20") + " class='kv-preview-data krajee-init-preview file-preview-image' />");
                filesConfig.push({ caption: item.Caption, size: item.Size, key: item.ID });
            });

            initializeFileInput(files, filesConfig);
        }
    });
}

// using the click event on the submit button
$(document).on("click", "#btn_Save", function (e) {
    $("form").valid(); // to show normal form validation
    var valid = checkFileUpload();
    if (!valid)
        e.preventDefault();
});

function checkFileUpload() {
    var count = $fileInput.fileinput("getFilesCount");
    if (count > 0) // check if there are images
        return true;

    // trigger upload
    $fileInput.fileinput("upload");
    return false;
}

function onSuccess(response) {
    if (response.Status === resultStatus.Success) {
        var files = $fileInput.fileinput("getFileStack");
        if (files.length > 0) // There are new images
        {
            // update source id before upload
            $fileInput.on("filebatchpreupload", function (event, data) {
                data.extra.sourceId = response.ID;
            });

            // trigger upload
            $fileInput.fileinput("upload");

            // show sucess message after file upload complete
            $fileInput.on("filebatchuploadcomplete", function () {
                showSuccess(response.RedirectToUrl, response.Message);
            });
        }
        else
            showSuccess(response.RedirectToUrl, response.Message);
    } else
        openPopupDialog(response);
}