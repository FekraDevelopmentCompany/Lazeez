// add food type button
$(document).on("click", "#btn_AddFoodType", function () {
    $.ajax({
        url: links.addFoodType,
        type: "GET",
        success: function (data) {
            openPopupDialog(data);
            $.validator.unobtrusive.parse($("#frm_Modal"));
        }
    });
});

function refreshFoodTypesDropDownList(item) {
    var $ddlFoodTypes = $("#ddl_FoodTypes");
    $ddlFoodTypes.append("<option value=" + item.Value + ">" + item.Text + "</option>");
    $ddlFoodTypes.val(item.Value);
}