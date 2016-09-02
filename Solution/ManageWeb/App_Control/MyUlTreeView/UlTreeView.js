function MyTree() {
}

MyTree.prototype = {
    tree: null,
    options: {},
    defaultoption: {
        refFather: true,
        refChild: true
    },
    initTree: function (options) {
        this.options = jQuery.extend({}, this.defaultoption, options || {});
        this.tree.children("ul").css("padding-left", "0")
        this.tree.find(".node").each(function () {

            if ($(this).next("li").length > 0) {
                $(this).children("ul").addClass("treeline");
            }

        });
        this.tree.find("ul").each(function () {
            $(this).children("li:last").not(".folder,.node").addClass("lastnode");
        });
        this.tree.find("ul .Expand").each(function () {
            $(this).next(".node").show();


        })
        this._addExpandEvent();
        this._addCheckEvent();
        this._addAdbEvent();
        this._addAClickEvent();
    },

    _addExpandEvent: function () {

        this.tree.find(".folder span").click(function (event) {
            var $node = $(this).parent("li");
            if ($node.hasClass("Expand")) {

                $node.removeClass("Expand");
                $node.next(".node").hide();
            }
            else {
                $node.addClass("Expand");
                $node.next(".node").show();

            }

        })
         
        if (this.tree.find("input").length == 0) {

            this.tree.find(".isCliecka a").click(function (event) {
                var $node = $(this).parent("li");
                if ($node.hasClass("Expand")) {

                    $node.removeClass("Expand");
                    $node.next(".node").hide();
                }
                else {
                    $node.addClass("Expand");
                    $node.next(".node").show();

                }

            })
        }
        

    },
    _addCheckEvent: function () {

        this.tree.find(":checkbox").click(this, function (event) {

            var v = $(this).prop("checked");

            if (v) {

                $(this).parent("li").next(".node").show();
                $(this).parent(".folder").addClass("Expand");
                if (event.data.options.refFather) {
                    $(this).parents(".node").prev("li").find(":checkbox").prop("checked", v);
                }
            }
            else {
                if (event.data.options.refChild) {
                    $(this).parent().next(".node").find(":checkbox").prop("checked", v);
                }

            }
            event.stopPropagation();
        })

    },
    _addAdbEvent: function () {

        this.tree.find("a").dblclick(this, function (event) {

            $parent = $(this).parent("li");
            var v = $parent.find(":checkbox").prop("checked");
            v = !v;
            $parent.find(":checkbox").prop("checked", v);
            $parent.find(":checkbox").trigger("click", event.data);
            $parent.next(".node").find(":checkbox").prop("checked", v);
            $parent.find(":checkbox").prop("checked", v);
        })

    },
    _addAClickEvent: function () {

        this.tree.find("a").click(this, function (event) {

            event.data.tree.find("a").removeClass("currenta");

            $(this).addClass("currenta");

        })
    },
    clearSelect: function () {
        this.tree.find("input:checked").prop("checked", false);

    },
    setSelectValue: function (SelectValue) {
        var tree = this;
        event.data = this;
        $.each(SelectValue.split(','), function (i, n) {
            var $selectnode = tree.tree.find(":checkbox[value='" + n + "']");
            $selectnode.prop("checked", true);
            var $node = $selectnode.parent("li");
            if ($node.hasClass("Expand")) {
                $node.removeClass("Expand");
                $node.next(".node").hide();
            }
            else {
                $node.addClass("Expand");
                $node.next(".node").show();

            }

        });
    },
    getSelectValue: function () {
        var SelectValue = "";
        this.tree.find("input:checked").each(function () {
            SelectValue += $(this).val() + ",";

        });
        if (SelectValue.length > 0) {
            SelectValue = SelectValue.substring(0, SelectValue.length - 1);
        }
        return SelectValue;
    },
    getLastSelectValue: function () {
        var LastSelectValue = "";
        this.tree.find("input:checked").each(function () {

            isLast = $(this).parent("li").next(".node").find("input:checked").length > 0;
            if (!isLast) {
                LastSelectValue += $(this).val() + ",";
            }

        });
        if (LastSelectValue.length > 0) {
            LastSelectValue = LastSelectValue.substring(0, LastSelectValue.length - 1);
        }
        return LastSelectValue;
    }

}

jQuery.fn.extend(
{
    createmytree: function (Initoptions) {
        var objMyTree = new MyTree();
        objMyTree.tree = this;
        objMyTree.initTree(Initoptions);
        return objMyTree;
    }
});

