var emptyTable = "No data available in table";
var info = "Showing _START_ to _END_ of _TOTAL_ entries";
var processing = "Loading...";
var next = "Next";
var previous = "Previous";
var val_MustAddMsg = "Must add at least one record";

var arabic = isArabicUI();

if (arabic) {
    emptyTable = "عفواً .. لا يوجد بيانات متاحة";
    info = "مشاهدة _START_ الى _END_ من _TOTAL_ البيانات";
    processing = "...جارى تحميل البيانات";
    next = "التالى";
    previous = "السابق";
    val_MustAddMsg = "يجب إضافة سجل واحد على الأقل";
}

// Grid intialization
function initializeGrid(gridId, url, columns, checkboxColumn, parameters, sortColumnIndex, scrollX, paging, rowCallback) {

    if (checkboxColumn)
        columns.unshift({ data: "Checkbox", sName: "Checkbox", "mRender": renderCheckbox, "bSortable": false });

    var table = $(gridId).dataTable({
        "scrollX": scrollX,
        "bPaginate": paging,
        "processing": true,
        "serverSide": true,
        "bResetDisplay": false,
        "sAjaxSource": url,
        'bScrollInfinite': true,
        "jQueryUI": true,
        "lengthChange": false,
        "bStateSave": false,
        "filter": false,
        "order": [[sortColumnIndex, "desc"]],
        "columns": columns,
        "fnServerParams": parameters,
        "fnRowCallback": rowCallback,
        "oLanguage": {
            "sEmptyTable": emptyTable,
            "sInfo": info,
            "sProcessing": processing,
            "oPaginate": {
                "sNext": next,
                "sPrevious": previous
            }
        },
        select: {
            style: "os"
        }
    });

    rowSelection();
    return table;
}

function generateGrid(gridId, columns, sortColumnIndex, isRowRequired) {

    var table = $(gridId).dataTable({
        "isRowRequired": isRowRequired,
        "bPaginate": false,
        "bResetDisplay": false,
        'bScrollInfinite': true,
        "jQueryUI": true,
        "lengthChange": false,
        "bStateSave": false,
        "columns": columns,
        "filter": false,
        "order": [[sortColumnIndex, "desc"]],
        "oLanguage": {
            "sVal_MustAddMsg": val_MustAddMsg,
            "sEmptyTable": emptyTable,
            "sInfo": info,
            "sProcessing": processing,
            "oPaginate": {
                "sNext": next,
                "sPrevious": previous
            }
        },
        select: {
            style: "os"
        }
    });

    rowSelection();
    return table;
}

// select row in Grid
function rowSelection() {
    $("#grid tbody").on("click", "tr", function () {
        $(this).toggleClass("selected");
    });
}

// Checkbox 
function renderCheckbox(data, type, row) {
    var id = data;
    return '<input type="checkbox" class="editor-active" name="select" id="' + id + '" >';
}

$("#selectAll").click(function () {

    $("input[name='select']").prop("checked", $("#selectAll").prop("checked"));
    if ($("#selectAll").prop("checked") == true) {
        if ($("input[name='select']:checked").length > 0) {
            $(".checkbox-grid").removeClass("disabled");
        }
    }
    else
        $(".checkbox-grid").addClass("disabled");
});

$(document).on('click', "input[name='select']", function () {

    if ($("input[name='select']").length == $("input[name='select']:checked").length) {
        $("#selectAll").prop("checked", "checked");
    }
    else {
        $("#selectAll").removeAttr("checked");
        $(".checkbox-grid").addClass("disabled");
    }
    if ($("input[name='select']:checked").length > 0) {
        $(".checkbox-grid").removeClass("disabled");
    }
    else
        $(".checkbox-grid").addClass("disabled");
});

// Render Date
function renderDate(data, type, row) {
    return data !== "" && data != undefined ? moment(data).format("DD/MM/YYYY") : "";
}

// Render Time
function renderTime(data) {
    return data !== "" && data != undefined ? moment(data).format("hh:mm a") : "";
}

// Render DateTime
function renderDateTime(data) {
    return data !== "" && data != undefined ? moment(data).format("DD/MM/YYYY hh:mm a") : "";
}

// Refresh Grid
function refreshGrid() {
    $("#grid").dataTable()._fnAjaxUpdateToStartFromZero();
}

function refreshMiniGrid() {
    $("#gridMini").dataTable()._fnAjaxUpdateToStartFromZero();
}
