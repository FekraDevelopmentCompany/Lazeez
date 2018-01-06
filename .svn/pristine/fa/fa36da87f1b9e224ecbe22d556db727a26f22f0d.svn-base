var param = function (aoData) {
    aoData.push({ name: "Name", value: $("#txt_UserName").val() });
    aoData.push({ name: "UserType", value: $("#ddl_UserType").val() });
    aoData.push({ name: "Phone", value: $("#txt_Phone").val() });
}


$(document).ready(function () {

    // datatable columns rendering
    var renderEdit = function (data, type, row) {
        return "<img id='" + row["ID"] + "' src='../Content/images/grid-icons/edit.png' class='edit datatable-icons' title='" + tooltip.edit + "'/>";
    }

    var renderDelete = function (data, type, row) {
        if (row["ID"] == 1)
            return "<img id='" + row["ID"] + "' src='../Content/images/grid-icons/PreventDelete.png' class='delete datatable-icons' />";
        else
            return "<img id='" + row["ID"] + "' src='../Content/images/grid-icons/delete.png' class='delete datatable-icons' title='" + tooltip.delete + "'/>";
    }

    // initialize datatable columns
    var columns = [
        { data: "ID", sName: "ID" },
        { data: "Name", sName: "Name" },
        { data: "UserType", sName: "UserType" },
        { data: "NumberOfRestaurant", sName: "NumberOfRestaurant" },
        { data: "BranchName", sName: "BranchName" },
        { data: "Phone", sName: "Phone" },
        { data: "Email", sName: "Email" },
        { mRender: renderEdit, "bSortable": false },
        { mRender: renderDelete, "bSortable": false }
    ];

    // initialize datatable grid
    initializeGrid("#grid", links.search, columns, false, param, 0, true, true);
    // Customize grid Columns
    custoColumnGrid()
});


// search button
$(document).on("click", "#btn_Search", function () {
    refreshGrid();
});

// cancel button
$(document).on("click", "#btn_Cancel", function () {
    $("#txt_UserName").val("");
    $("#txt_Phone").val("");
    //$("#ddl_UserType").val(1);
    refreshGrid();
});

// add button
$(document).on("click", "#btn_Add", function () {
    window.location.href = links.addEdit + "?id=-1"
});

// edit grid icon
$(document).on("click", ".edit", function () {
    window.location.href = links.addEdit + "?id=" + this.id
});

// delete grid icon
$(document).on("click", ".delete", function () {
    if (this.id != 1) {
        $.ajax({
            url: links.delete,
            type: "GET",
            data: { id: this.id },
            success: function (data) {
                openPopupDialog(data);
            }
        });
    }
});