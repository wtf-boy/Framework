function SetDefult(id) {
    $("#" + id).empty();
    $("#" + id).attr("checkvalueempty", "false");
    $("#" + $("#" + id).attr("SelectNameID")).val("");
    $("<option value=''>" + $("#" + id).attr("SelectDefault") + "</option>").appendTo("#" + id);
    if ($("#" + id).attr("ChildCityID") != undefined) {
        SetDefult($("#" + id).attr("ChildCityID"));
    }
}
function InitSelectValue(id, ParentID, isFirist) {

    $("#" + id).find("option[value='']").text("正在加载数据");
    $.getJSON(DistrictUrl, "AreaID=" + ParentID + "&IsALLAreaData=" + IsALLAreaData, function (json) {
        $("#" + id).find("option[value='']").text($("#" + id).attr("SelectDefault"));

        jQuery.each(json.Data, function (i, item) {

            $("#" + id).attr("checkvalueempty", "true");
            $("<option value='" + item.AreaID + "'>" + item.AreaName + "</option>").appendTo("#" + id);
        });
        var SelectValue = $("#" + id).attr("SelectValue");
        if (SelectValue != undefined) {
            $("#" + id).val(SelectValue);
            $("#" + $("#" + id).attr("SelectNameID")).val($("#" + id).find("option[value='" + SelectValue + "']").text());
            if ($("#" + id).attr("ChildCityID") != undefined) {
                InitSelectValue($("#" + id).attr("ChildCityID"), SelectValue, false);
            }
        }

    });
}
function InitCity(id) {
    SetDefult(id);
    InitSelectValue(id, 0, true);

};
function SelectCity(e, childID) {
    var selectValue = $(e).val();
    if (childID != '') {
        SetDefult(childID);
    }
    if (selectValue != "") {
        $("#" + $(e).attr("SelectNameID")).val($(e).find("option[value='" + selectValue + "']").text());
        if (childID != '') {
            $("#" + childID).find("option[value='']").text("正在加载数据");
            $.getJSON(DistrictUrl, "AreaID=" + selectValue + "&IsALLAreaData=" + IsALLAreaData, function (json) {
                $("#" + childID).find("option[value='']").text($("#" + childID).attr("SelectDefault"));
                jQuery.each(json.Data, function (i, item) {
                    $("#" + childID).attr("checkvalueempty", "true");
                    $("<option value='" + item.AreaID + "'>" + item.AreaName + "</option>").appendTo("#" + childID);
                });
            });
        }
    }
    else {
        $("#" + $(e).attr("SelectNameID")).val("");
    }
};