var param = function (aoData) {
    aoData.push({ name: "MasterDataId", value: $("#ddl_lkpNames").val() });
    aoData.push({ name: "Name", value: $("#txt_Name").val() });
}

$(document).ready(function () {

    // datatable columns rendering
    var renderEdit = function (data, type, row) {
        return "<img id='" + row["ID"] + "' src='../Content/images/grid-icons/edit.png' class='edit datatable-icons' title='" + tooltip.edit + "'/>";
    }

    var renderDelete = function (data, type, row) {
        return "<img id='" + row["ID"] + "' src='../Content/images/grid-icons/delete.png' class='delete datatable-icons' title='" + tooltip.delete + "'/>";
    }
   
    //var hideDescription = $("#txt_DisplayDescriptionHeader").val();

    // initialize datatable columns
    var columns = [
        { data: "ID", sName: "ID" },
        { data: "Name", sName: "Name" },
        { data: "Description", sName: "Description" },
        { mRender: renderEdit, "bSortable": false },
        { mRender: renderDelete, "bSortable": false }
    ];

    // initialize datatable grid
    initializeGrid("#grid", links.search, columns, false, param, 0, true, true);
    // Customize grid Columns
    custoColumnGrid($("#ddl_lkpNames").val());
});


// search button
$(document).on("click", "#btn_Search", function () {
    refreshGrid();
});

$(document).on("change", "#ddl_lkpNames", function () {
    refreshGrid();
    custoColumnGrid(this.value);
});

// cancel button
$(document).on("click", "#btn_Cancel", function () {
    $("#txt_Name").val("");
    refreshGrid();
});

// add button
$(document).on("click", "#btn_Add", function () {
    window.location.href = links.addEdit + "?id=-1&masterDataId=" + $("#ddl_lkpNames").val();
});

// edit grid icon
$(document).on("click", ".edit", function () {
    window.location.href = links.addEdit + "?id=" + this.id + "&masterDataId=" + $("#ddl_lkpNames").val();
});

// delete grid icon
$(document).on("click", ".delete", function () {
    $.ajax({
        url: links.delete,
        type: "GET",
        data: { id: this.id, masterDataId: $("#ddl_lkpNames").val() },
        success: function (data) {
            openPopupDialog(data);
        }
    });
});