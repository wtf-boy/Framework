function MyBabbar() {
}

MyBabbar.prototype = {
    _winWidth: 0,
    _winHeight: 0,
    _workAreaHeight: 0,
    _headerHeight: 0,

    _init: function () {


        window.onresize = this._windowResize;

        this._windowResize();
        this._addTagEvent();
        this._loadTag();


    },
    _addTagEvent: function () {

        $(".tabsbarevent a").live("click", function () {

            if ($(this).attr("refUrl") != undefined && $(this).attr("refUrl") != "" && $(this).attr("relModuleID") != undefined) {
                myBabbar.openPageMenu($(this).attr("relModuleID"), $(this).attr("refUrl"));
                $(".tabsbarevent a").removeClass("tab_current");
                $(this).addClass("tab_current");
            }
        })

    },
    _loadTag: function () {

        if ($(".tabsbarevent .tab_current").length > 0) {
            $(".tabsbarevent .tab_current").each(function () {
                myBabbar.openPageMenu($(this).attr("relModuleID"), $(this).attr("refUrl"));
                $(".tabsbarevent a").removeClass("tab_current");
                $(this).addClass("tab_current");
            });
        }
        else {

            if ($(".tabsbarevent a[disabled!='disabled']").length > 0) {
                $(".tabsbarevent a").removeClass("tab_current");
                $first = $(".tabsbarevent a[disabled!='disabled']").first();
                $first.addClass("tab_current");
                myBabbar.openPageMenu($first.attr("relModuleID"), $first.attr("refUrl"));

            }


        }

    },
    _initElementSize: function () {

        var oldAreaHeight = myBabbar._workAreaHeight;
        myBabbar._workAreaHeight = myBabbar._winHeight - 35 - $("#mytabbar").height();
        $("#tab_container  iframe").each(function () {
            $(this).height(myBabbar._workAreaHeight);
        });

    },

    //窗体变化
    _windowResize: function () {

        var winWidth = $(document).width();
        var winHeight = $(document).height();
        myBabbar._winWidth = winWidth;
        myBabbar._winHeight = winHeight;
        myBabbar._initElementSize();
    },
    openPageMenu: function (relModuleID, refHref) {

        if ($("#Tabframe" + relModuleID).length != 0) {

            $("#tab_container").children().hide();
            $("#Tabframe" + relModuleID).show();
        }
        else {
            $("#tab_container").children().hide();
            var newFrame = " <iframe id='Tabframe" + relModuleID + "' src='" + refHref + "' height=" + myBabbar._workAreaHeight + "  title=''  class='tab_container_frame'  name='Tabframe" + relModuleID + "' frameborder='0' scrolling='yes'></iframe>";
            $("#tab_container").append(newFrame);
        }

    },
    TabEnableAll: function () {
        $(".tabsbarevent a").removeAttr("disabled");
    },
    TabNext: function () {

        var nexti = $(".tabsbarevent a").index($(".tabsbarevent .tab_current")) + 1;
        if ($(".tabsbarevent a").length >= nexti + 1) {
            var $nexttab = $(".tabsbarevent a").eq(nexti);
            $(".tabsbarevent a").removeClass("tab_current");
            $nexttab.addClass("tab_current");
            $nexttab.removeAttr("disabled");
            myBabbar.openPageMenu($nexttab.attr("relModuleID"), $nexttab.attr("refUrl"));
        }
    }
}

var myBabbar = new MyBabbar();

function TabEnableAll() {

    myBabbar.TabEnableAll();

}
function TabNext() {
    myBabbar.TabNext();
}
$(function () {

    myBabbar._init();

})