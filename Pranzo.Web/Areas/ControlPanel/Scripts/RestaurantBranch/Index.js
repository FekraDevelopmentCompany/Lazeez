﻿// ======== Search Parameter ==============
var param = function (aoData) {
    aoData.push({ name: "RestaurantID", value: $("#txt_RestaurantId").val() });
    aoData.push({ name: "Name", value: $("#txt_Name").val() });
    aoData.push({ name: "DistrictID", value: $("#ddl_DistrictNames").val() });
    aoData.push({ name: "Address", value: $("#txt_Address").val() });
}

$(document).ready(function () {

    // datatable columns rendering
    var renderEdit = function (data, type, row) {
        return "<img id='" + row["ID"] + "' src='../Content/images/grid-icons/edit.png' class='edit datatable-icons' title='" + tooltip.edit + "'/>";
    }

    var renderDelete = function (data, type, row) {
        return "<img id='" + row["ID"] + "' src='../Content/images/grid-icons/delete.png' class='delete datatable-icons' title='" + tooltip.delete + "'/>";
    }

    var renderIsMain = function (data) {
        if (data === true)
            return "<img src='../Content/Images/grid-icons/yes.png' />";

        return "<img src='../Content/Images/grid-icons/no.png' />";
    }

    // initialize datatable columns
    var columns = [
        { data: "ID", sName: "ID" },
        { data: "Name", sName: "Name" },
        { data: "District", sName: "District" },
        { data: "Phone", sName: "Phone" },
        { data: "Address", sName: "Address" },
        { data: "IsMain", sName: "IsMain", mRender: renderIsMain },
        { mRender: renderEdit, "bSortable": false },
        { mRender: renderDelete, "bSortable": false }
    ];

    // initialize datatable grid
    initializeGrid("#grid", links.search, columns, false, param, 0, true, true);
});


// search button
$(document).on("click", "#btn_Search", function () {
    refreshGrid();
});

// cancel button
$(document).on("click", "#btn_Cancel", function () {
    $("select").val("");
    $("input:text").val("");
    refreshGrid();
});

// add button
$(document).on("click", "#btn_Add", function () {
    window.location.href = links.addEdit + "?id=-1&restaurantId=" + $("#txt_RestaurantId").val();
});

// edit grid icon
$(document).on("click", ".edit", function () {
    window.location.href = links.addEdit + "?id=" + this.id + "&restaurantId=" + $("#txt_RestaurantId").val();
});

// delete grid icon
$(document).on("click", ".delete", function () {
    $.ajax({
        url: links.delete,
        type: "GET",
        data: { id: this.id },
        success: function (data) {
            openPopupDialog(data);
        }
    });
});