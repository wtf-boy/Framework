function WorkDesktop() {
}

WorkDesktop.prototype = {
    _winWidth: 0,
    _winHeight: 0,
    _workAreaHeight: 0,
    _headerHeight: 0,
    _$Header: false,
    _$FunctionBar: false,
    _$FunctionFrm: false,
    _$Switch: false,
    _$maxWindow: false,
    _init: function () {
        this._headerHeight = $(".header_container").height() + 3;
        window.onresize = this._windowResize;
        this._initElement();
        this._windowResize();
        this._addSwitchEvent();
        this._addtaskliveEvent();
        this._addtaskmenuEvent();
        this._addtaskCurrentEvent("欢迎光临", "Welcome");
    },
    _initElement: function () {
        desktop._$Header = $(".header_container");
        desktop._$FunctionBar = $("#function_container");
        desktop._$FunctionFrm = $("#frmFunction");
        desktop._$Switch = $("#switch_container");
        desktop._$maxWindow = $("#maxWindow");
    },

    _initElementSize: function () {

        var oldAreaHeight = desktop._workAreaHeight;
        desktop._workAreaHeight = desktop._winHeight - desktop._headerHeight - $("#desktop_task_container").height() - 8;
        desktop._$FunctionFrm.height(desktop._winHeight - desktop._headerHeight);
        $(".desktop_task_content").width($("#desktop_task_container").width() - $(".desktop_task_menu").width() - 20);
        $("#desktop_Work_container  iframe").each(function () {
            $(this).height(desktop._workAreaHeight);
        });
        if (Math.abs(desktop._workAreaHeight - oldAreaHeight) > 10) {
            var autoPageSize = Math.ceil((desktop._workAreaHeight - 100) / 22);
            jQuery.post("../WindowHeight.ashx", "AutoPageSize=" + autoPageSize);
        }
    },
    _addtaskliveEvent: function () {

        $("#desktop_task_list > li").live("dblclick", function () {
            desktop.closeWindow($(this).attr("id"));
        });
        $("#desktop_task_list > li").live("click", function () {
            desktop.actionPageMenu(this);
        });

        $(".window_close").click(function () {

            desktop._$maxWindow.hide();
            desktop.closeWindow(desktop._$maxWindow.attr("moduleID"));

        });
        $(".window_restore").click(function () {

            maxframe = $("#frame" + desktop._$maxWindow.attr("moduleID"));
            maxframe.css({ height: desktop._workAreaHeight, width: "100%" });
            desktop._$maxWindow.hide();
            maxframe.appendTo("#desktop_Work_container");
            $("#desktop_Work_container").children().hide();

            maxframe.show();
            try {
                maxframe.contents().afreshWindow();
            } catch (e) {

            }

        })
    },

    _addtaskmenuEvent: function () {

        $(".desktop_task_menu span").click(function () {
            if ($(".desktop_task_menu").attr("moduleId") != "") {
                cmd = $(this).attr("cmd");
                moduleId = $(this).attr("moduleId")
                switch (cmd) {
                    case "close":
                        desktop.closeWindow(moduleId);

                        break;
                    case "max":
                        desktop.showMaxPage(moduleId);
                        break;
                }
            }
            return false;


        })


    },
    _addtaskCurrentEvent: function (currenttiltle, currentmoduleId) {

        $(".desktop_task_menu").attr({ title: currenttiltle, moduleId: currentmoduleId });
        $(".desktop_task_menu span").each(function () {
            $(this).attr({ title: $(this).attr("cmdtitle") + currenttiltle, moduleId: currentmoduleId });
        });

    },
    _addSwitchEvent: function () {
        this._$Switch.click(function () {

            var switchPoint = $("#switchPoint");

            if (switchPoint.attr("alt") == "隐藏左栏") {
                switchPoint.attr("alt", "打开左栏");
                switchPoint.attr("src", "../../App_Themes/Default/Image/ico_Arrow_Right.gif");

                desktop._$FunctionBar.hide();

            }
            else {
                switchPoint.attr("alt", "隐藏左栏");
                switchPoint.attr("src", "../../App_Themes/Default/Image/ico_Arrow_Left.gif");
                desktop._$FunctionBar.show();

            }
        });

    },
    //窗体变化
    _windowResize: function () {
       
        var winWidth = $(document).width();
        var winHeight = $(document).height();
        desktop._winWidth = winWidth;
        desktop._winHeight = winHeight;
        desktop._initElementSize();
    },
    _actionlastWindow: function () {
        var lastActiveDate = "1";
        $("#desktop_task_list li").each(function () {
            if (lastActiveDate == "1") {
                lastActiveDate = $(this).attr("activeDate");
            }
            if ($(this).attr("activeDate") > lastActiveDate) {
                lastActiveDate = $(this).attr("activeDate");
            }
        });
        if ($("#desktop_task_list li[activeDate='" + lastActiveDate + "']").length == 0) {
            this._addtaskCurrentEvent("", "");
        } else {
            $("#desktop_task_list li[activeDate='" + lastActiveDate + "']").click();
        }
    },

    closeWindow: function (moduleID) {
        $("#" + moduleID).remove();
        $("#frame" + moduleID).remove();
        desktop._actionlastWindow();
    },
    actionPageMenu: function (objFrame) {
        if ($(objFrame).hasClass("task_current")) {
            return;
        }
        $("#desktop_task_list > li").removeClass();
        $("#desktop_task_list > li").addClass("task");
        $(objFrame).removeClass();
        $(objFrame).addClass("task_current");
        $(objFrame).attr("activeDate", new Date().toString());
        $("#desktop_Work_container").children().hide();
        $("#frame" + objFrame.id).show();
        desktop._addtaskCurrentEvent(objFrame.title, objFrame.id);
    },
    showMaxPage: function (moduleID) {
        $window = $("#frame" + moduleID);
        desktop._$maxWindow.attr("moduleID", moduleID);
        desktop._$maxWindow.find(".windowframe").append($window);

        desktop._$maxWindow.find(".window_title").text($window.attr("title"));
        desktop._$maxWindow.show();
        $window.css({ width: desktop._winWidth - 3, height: desktop._winHeight - 25 });
        desktop._$maxWindow.animate({ width: desktop._winWidth - 2, height: desktop._winHeight - 2, top: "0px", left: "0px" }, 300, function () {
            try {
                $window.get(0).contentWindow.afreshWindow();
            } catch (e) {
            }

        });
    },
    _removeCurrentTaskOver: function () {
        var width = 10;
        $("#desktop_task_container ul li").each(function () {
            width = width + $(this).width() + 10;
        });
        if (width > $(".desktop_task_content").width()) {
            var lastActiveDate = new Date().toString();
            $("#desktop_task_list li").each(function () {
                if ($(this).attr("activeDate") < lastActiveDate) {
                    lastActiveDate = $(this).attr("activeDate");

                }
            });
            var moduleID = $("#desktop_task_list li[activeDate='" + lastActiveDate + "']").attr("id");
            $("#" + moduleID).remove();
            $("#frame" + moduleID).remove();
        }
    },
    openPageMenu: function (relModuleID, refImageUrl, refHref, refName) {

        if ($("#frame" + relModuleID).length != 0) {

            if ($("#" + relModuleID).hasClass("task_current")) {
                return;
            }
            $("#desktop_task_list > li").removeClass();
            $("#desktop_task_list > li").addClass("task");
            $("#" + relModuleID).removeClass();
            $("#" + relModuleID).addClass("task_current");
            $("#" + relModuleID).attr("activeDate", new Date().toString());
            $("#desktop_Work_container").children().hide();
            $("#frame" + relModuleID).show();
        }
        else {


            $("#desktop_task_list > li").removeClass();
            $("#desktop_task_list > li").addClass("task");
            var newLi = "<li class='task_current'  activeDate='" + new Date().toString() + "'   title='" + refName + "'  refHref='" + refHref + "' id='" + relModuleID + "' moduleId='" + relModuleID + "'> <span style='background-image:url(" + refImageUrl + ");'> " + refName + "</span></li>";
            $("#desktop_task_list").append(newLi);
            $("#desktop_Work_container").children().hide();
            var newFrame = " <iframe id='frame" + relModuleID + "' src='" + refHref + "' height=" + desktop._workAreaHeight + " class='desktop_window_frame' title='" + refName + "'   name='frame" + relModuleID + "' frameborder='0' scrolling='yes'></iframe>";
            $("#desktop_Work_container").append(newFrame);
            this._removeCurrentTaskOver();
        }
        desktop._addtaskCurrentEvent(refName, relModuleID);
    }

}

function OpenPageMenu(relModuleID, refImageUrl, refHref, refName) {

    desktop.openPageMenu(relModuleID, refImageUrl, refHref, refName);
}

function closeCurrentWindow() {

    desktop.closeWindow($(".desktop_task_menu").attr("moduleId"));
}
var desktop = new WorkDesktop();

$(function () {

    desktop._init();
    desktop.openPageMenu("Welcome", "../../app_themes/default/image/ico_Welcome_Bg.gif", "Welcome.aspx", "欢迎光临");


})