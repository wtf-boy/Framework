
 
jQuery.fn.extend(
{
    ValidationValue: function (ValidationGroup) {

        var resultValidation = true;
     
        $("*[ValidationGroup='" + ValidationGroup + "']").each(function () {
            var valResult = true;

            if ($(this).attr("ValidationExpression") != undefined && $(this).attr("ValidationExpression") != "" && $.trim($(this).val()) != "") {
                valResult = $(this)._CheckErrorMask($(this).attr("ValidationExpression"));
            }
            else if ($(this).attr("CheckValueEmpty") != undefined) {

                if ($(this).attr("IsCheck") == undefined) {
                    if ($(this).attr("CheckValueEmpty").toLowerCase() == "true" && $.trim($(this).val()) == "") {
                        valResult = false;
                        $(this)._ShowErrorInfo();
                    }
                    else if ($(this).attr("CheckValueEmpty").toLowerCase() != "true" && $.trim($(this).val()) == "") {

                        $(this)._RemoveShowInfo();
                    }
                    else if ($(this).attr("CheckValueEmpty").toLowerCase() == "true" && $.trim($(this).val()) != "") {

                        $(this)._RemoveShowInfo();
                    }
                }
                else {

                    if ($(this).attr("CheckValueEmpty").toLowerCase() == "true" && $(this).find("input:checked").length == 0) {
                        valResult = false;

                        $(this)._ShowErrorInfo();
                    }
                    else if ($(this).attr("CheckValueEmpty").toLowerCase() != "true" && $(this).find("input:checked").length == 0) {

                        $(this)._RemoveShowInfo();
                    }
                    else if ($(this).attr("CheckValueEmpty").toLowerCase() == "true" && $(this).find("input:checked").length > 0) {

                        $(this)._RemoveShowInfo();
                    }

                }
            }
            if ($(this).attr("ControlToCompare") != undefined && $.trim($(this).attr("ControlToCompare")) != "") {
                if ($(this).val() != $("#" + $(this).attr("ControlToCompare")).val()) {
                    valResult = false;
                    $(this)._ShowErrorInfo();
                }
            }
            if ($(this).attr("MinimumValue") != undefined && $.trim($(this).attr("MinimumValue")) != "") {
                if (parseFloat($(this).val()) < parseFloat($(this).attr("MinimumValue"))) {
                    valResult = false;
                    $(this).ShowErrorInfo("请输入大于等于" + $(this).attr("MinimumValue"));
                }
            }
            if ($(this).attr("MaximumValue") != undefined && $.trim($(this).attr("MaximumValue")) != "") {
                if (parseFloat($(this).val()) > parseFloat($(this).attr("MaximumValue"))) {
                    valResult = false;
                    $(this).ShowErrorInfo("请输入小于等于" + $(this).attr("MaximumValue"));
                }
            }

            if ($(this).attr("MinLength") != undefined && $.trim($(this).attr("MinLength")) != "") {
                if ($(this).val() != "" && $(this)._GetWordLength() < parseInt($(this).attr("MinLength"))) {
                    valResult = false;
                    $(this).ShowErrorInfo("请输入大于等于" + $(this).attr("MinLength") + "个字,当前:" + $(this)._GetWordLength());

                }
            }
            if ($(this).attr("maxlength") != undefined && $.trim($(this).attr("maxlength")) != "") {
                if ($(this).val() != "" && $(this)._GetWordLength() > parseInt($(this).attr("maxlength"))) {
                    valResult = false;
                    $(this).ShowErrorInfo("请输入小于等于" + $(this).attr("maxlength") + "个字,当前:" + $(this)._GetWordLength());

                }
            }
            if ($(this).attr("MinCharLength") != undefined && $.trim($(this).attr("MinCharLength")) != "") {
                if ($(this).val() != "" && $(this)._GetCharLength() < parseInt($(this).attr("MinCharLength"))) {
                    valResult = false;
                    $(this).ShowErrorInfo("请输入大于等于" + $(this).attr("MinCharLength") + "个字符,当前:" + $(this)._GetCharLength());

                }
            }

            if ($(this).attr("MaxCharLength") != undefined && $.trim($(this).attr("MaxCharLength")) != "") {
                if ($(this).val() != "" && $(this)._GetCharLength() > parseInt($(this).attr("MaxCharLength"))) {
                    valResult = false;
                    $(this).ShowErrorInfo("请输入小于等于" + $(this).attr("MaxCharLength") + "个字符,当前:" + $(this)._GetCharLength());
                }
            }

            resultValidation = resultValidation && valResult;
        }
  )

        return resultValidation;

    },

    FocusValidationHint: function () {

        if ($(this).val() == "" && $(this).attr("HintMessage") != "") {
            $(this)._ShowHintInfo();
        }
    },
    BlurValidationError: function (BlurSucessCall) {
        var result = true;
        
        if ($(this).attr("ValidationExpression") != undefined && $(this).attr("ValidationExpression") !="" && $.trim($(this).val()) != "") {
            var checkresult = $(this)._CheckErrorMask($(this).attr("ValidationExpression"));
            if (checkresult && BlurSucessCall) {
                result = BlurSucessCall(this);
            }
        }
        else if ($(this).attr("CheckValueEmpty") != undefined && $(this).attr("IsCheck") == undefined) {
            if ($(this).attr("CheckValueEmpty").toLowerCase() == "true" && $.trim($(this).val()) == "") {

                $(this)._ShowErrorInfo();
                result = false;
            }
            else if ($(this).attr("CheckValueEmpty").toLowerCase() != "true" && $.trim($(this).val()) == "") {

                $(this)._RemoveShowInfo();
                if (BlurSucessCall) {
                    result = BlurSucessCall(this);
                }
            }
            else if ($(this).attr("CheckValueEmpty").toLowerCase() == "true" && $.trim($(this).val()) != "") {

                $(this)._RemoveShowInfo();
                if (BlurSucessCall) {
                    result = BlurSucessCall(this);
                }
            }
        }
        if (!result) {
            return;
        }
        if ($(this).attr("ControlToCompare") != undefined && $.trim($(this).attr("ControlToCompare")) != "") {
            if ($(this).val() != $("#" + $(this).attr("ControlToCompare")).val()) {
                $(this)._ShowErrorInfo();
                return;
            }

        }
        if ($(this).attr("MaximumValue") != undefined && $.trim($(this).attr("MaximumValue")) != "") {
            if (parseFloat($(this).val()) > parseFloat($(this).attr("MaximumValue"))) {

                $(this).ShowErrorInfo("请输入小于等于" + $(this).attr("MaximumValue"));
                return;
            }
        }
        if ($(this).attr("MinimumValue") != undefined && $.trim($(this).attr("MinimumValue")) != "") {
            if (parseFloat($(this).val()) < parseFloat($(this).attr("MinimumValue"))) {

                $(this).ShowErrorInfo("请输入大于等于" + $(this).attr("MinimumValue"));
                return;
            }
        }
        if ($(this).attr("MinLength") != undefined && $.trim($(this).attr("MinLength")) != "") {
            if ($(this).val() != "" && $(this)._GetWordLength() < parseInt($(this).attr("MinLength"))) {
                $(this).ShowErrorInfo("请输入大于等于" + $(this).attr("MinLength") + "个字,当前:" + $(this)._GetWordLength());
                return;
            }
        }
        if ($(this).attr("maxlength") != undefined && $.trim($(this).attr("maxlength")) != "") {
            if ($(this).val() != "" && $(this)._GetWordLength() > parseInt($(this).attr("maxlength"))) {
                valResult = false;
                $(this).ShowErrorInfo("请输入小于等于" + $(this).attr("maxlength") + "个字,当前:" + $(this)._GetWordLength());

            }
        }
        if ($(this).attr("MinCharLength") != undefined && $.trim($(this).attr("MinCharLength")) != "") {
            if ($(this).val() != "" && $(this)._GetCharLength() < parseInt($(this).attr("MinCharLength"))) {
                $(this).ShowErrorInfo("请输入大于等于" + $(this).attr("MinCharLength") + "个字符,当前:" + $(this)._GetCharLength());
                return;
            }
        }
        if ($(this).attr("MaxCharLength") != undefined && $.trim($(this).attr("MaxCharLength")) != "") {
            if ($(this).val() != "" && $(this)._GetCharLength() > parseInt($(this).attr("MaxCharLength"))) {
                $(this).ShowErrorInfo("请输入小于等于" + $(this).attr("MaxCharLength") + "个字符,当前:" + $(this)._GetCharLength());
                return;
            }
        }

    },
    _GetWordLength: function () {
        var str = $(this).val();
        if ($(this).attr("IsRich") != undefined) {
            str = GetXHtmlText(jQuery.trim(str));
        }
        return str.length;
    },
    _GetCharLength: function () {

        var str = $(this).val();
        if ($(this).attr("IsRich") != undefined) {
            str = GetXHtmlText(jQuery.trim(str));
        }
        str = str.replace(/[\u0391-\uFFE5]/g, "**");
        return str.length;
    },
    _CheckErrorMask: function (mask) {
        var resultCheck = true;
        var exp = mask.indexOf('^') == 0 || mask.indexOf('$') == mask.length - 1 ? RegExp(mask, "i") : RegExp('^' + mask + '$', "i");
        var reg = $(this).val().match(exp);
        if (reg == null) {

            $(this)._ShowErrorInfo();
            resultCheck = false;

        }
        else {
            $(this)._RemoveShowInfo();
        }
        return resultCheck;

    },
    _RemoveShowInfo: function () {
        $(this).removeClass("validation-error");
        if ($("#Val" + $(this).attr("id")).length != 0) {
            $("#Val" + $(this).attr("id")).remove();
        }
    },

    _ShowErrorInfo: function () {
        $(this)._RemoveShowInfo();
        var offset;
        var valDiv;
        if ($(this).attr("IsRich") == undefined) {
            offset = $(this).offset();
            valDiv = "<div  id='Val" + $(this).attr("id") + "' style='left:" + (offset.left + 3 + $(this).width()) + "px;top:" + offset.top + "px;'  class='validation-error-bar'>" + $(this).attr("ErrorMessage") + " </div>";

        }
        else {
            var rich = $(this).parent().find(".xheLayout").first();
            offset = rich.offset();
            valDiv = "<div  id='Val" + $(this).attr("id") + "' style='left:" + (offset.left + 3 + rich.width()) + "px;top:" + offset.top + "px;'  class='validation-error-bar'>" + $(this).attr("ErrorMessage") + " </div>";
        }

        $(this).after(valDiv);
        if ($(this).attr("MessageWidth")) {

            $("#Val" + $(this).attr("id")).width($(this).attr("MessageWidth"));
        }
        else {
            var messageLength = GetCharLength($(this).attr("ErrorMessage"));
            $("#Val" + $(this).attr("id")).width(messageLength * 7);

        }
        $("#Val" + $(this).attr("id")).fadeIn("fast");
        $(this).addClass("validation-error");
    },

    ShowErrorInfo: function (message) {
        $(this)._RemoveShowInfo();
        var offset;
        var valDiv;
        if ($(this).attr("IsRich") == undefined) {
            offset = $(this).offset();
            valDiv = "<div  id='Val" + $(this).attr("id") + "' style='left:" + (offset.left + 3 + $(this).width()) + "px;top:" + offset.top + "px;'  class='validation-error-bar'>" + message + " </div>";

        }
        else {
            var rich = $(this).parent().find(".xheLayout").first();
            offset = rich.offset();
            valDiv = "<div  id='Val" + $(this).attr("id") + "' style='left:" + (offset.left + 3 + rich.width()) + "px;top:" + offset.top + "px;'  class='validation-error-bar'>" + message + " </div>";
        }

        $(this).after(valDiv);
        if ($(this).attr("MessageWidth")) {

            $("#Val" + $(this).attr("id")).width($(this).attr("MessageWidth"));
        }
        else {
            var messageLength = GetCharLength(message);
            $("#Val" + $(this).attr("id")).width(messageLength * 7);

        }
        $("#Val" + $(this).attr("id")).fadeIn("fast");
        $(this).addClass("validation-error");
    },
    RemoveShowInfo: function () {
        $(this).removeClass("validation-error");
        if ($("#Val" + $(this).attr("id")).length != 0) {
            $("#Val" + $(this).attr("id")).remove();
        }
    },
    ShowHintInfo: function (message) {
        $(this)._RemoveShowInfo();
        var offset;
        var valDiv
        if ($(this).attr("IsRich") == undefined) {
            offset = $(this).offset();
            valDiv = "<div  id='Val" + $(this).attr("id") + "' style='left:" + (offset.left + 3 + $(this).width()) + "px;top:" + offset.top + "px;'  class='validation-hint-bar'>" + message + " </div>";

        }
        else {
            var rich = $(this).parent().find(".xheLayout").first();
            offset = rich.offset();

            valDiv = "<div  id='Val" + $(this).attr("id") + "' style='left:" + (offset.left + 3 + rich.width()) + "px;top:" + offset.top + "px;'  class='validation-hint-bar'>" + message + " </div>";

        }
        $(this).after(valDiv);
        if ($(this).attr("MessageWidth")) {

            $("#Val" + $(this).attr("id")).width($(this).attr("MessageWidth"));
        }
        else {
            var messageLength = GetCharLength(message);
            $("#Val" + $(this).attr("id")).width(messageLength * 7);

        }
        $("#Val" + $(this).attr("id")).fadeIn("fast");
    },
    _ShowHintInfo: function () {

        $(this)._RemoveShowInfo();
        var offset;
        var valDiv
        if ($(this).attr("IsRich") == undefined) {
            offset = $(this).offset();
            valDiv = "<div  id='Val" + $(this).attr("id") + "' style='left:" + (offset.left + 3 + $(this).width()) + "px;top:" + offset.top + "px;'  class='validation-hint-bar'>" + $(this).attr("HintMessage") + " </div>";

        }
        else {
            var rich = $(this).parent().find(".xheLayout").first();
            offset = rich.offset();

            valDiv = "<div  id='Val" + $(this).attr("id") + "' style='left:" + (offset.left + 3 + rich.width()) + "px;top:" + offset.top + "px;'  class='validation-hint-bar'>" + $(this).attr("HintMessage") + " </div>";

        }

        $(this).after(valDiv);
        if ($(this).attr("MessageWidth")) {

            $("#Val" + $(this).attr("id")).width($(this).attr("MessageWidth"));
        }
        else {
            var messageLength = GetCharLength($(this).attr("HintMessage"));
            $("#Val" + $(this).attr("id")).width(messageLength * 7);

        }
        $("#Val" + $(this).attr("id")).fadeIn("fast");

    }

}

);


function GetCharLength(message) {
    var str = message.replace(/[\u0391-\uFFE5]/g, "**");
    return str.length;
};
function GetXHtmlText(value) {




    //htmlbody = value.replace(/[\n\r]/g, '');//ie要先去了\n在处理
    //htmlbody = htmlbody.replace(/<(p|div)[^>]*>(<br\/?>|&nbsp;)<\/\1>/gi, '\n')
    //        .replace(/<br\/?>/gi, '\n')
    //        .replace(/<[^>/]+>/g, '')
    //        .replace(/<[^>]*>/g, "");
    ////取出来的空格会有c2a0会变成乱码，处理这种情况\u00a0
    //return htmlbody.replace(/\u00a0/g, ' ').replace(/&nbsp;/g,' ');
    var htmlbody = value.replace(/[\n\r]/g, '');
    htmlbody = htmlbody.replace(/<[^>]*>/g, "");
    htmlbody = htmlbody.replace(/&nbsp;/ig, " ");
    return htmlbody;
}
jQuery(function () {

    $(".help_hint_info").hover(function () {

        $(this).children("dd").show();

    }, function () {

        $(this).children("dd").hide();

    });
})