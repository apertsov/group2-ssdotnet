/// <reference path="../../../Scripts/jquery-1.5.1.js" />
/// <reference path="../../../Scripts/jquery-1.5.1-vsdoc.js" />

var showNotice, adminMenu, columns, validateForm, screenMeta;
(function (a) {
    adminMenu = {
        init: function () {
            var b = a("#adminmenu");
            a(".menu-toggle", b).each(function () {
                var c = a(this), d = c.siblings(".submenu");
                if (d.length) {
                    c.click(function () {
                        adminMenu.toggle(d)
                    })
                }
                else {
                    c.hide()
                }

            });
            this.favorites();
            a("#collapse-menu", b).click(function () {
                if (a("body").hasClass("folded")) {
                    adminMenu.fold(1);
                    //deleteUserSetting("mfold")
                }
                else {
                    adminMenu.fold();
                    //setUserSetting("mfold", "f")
                }
                return false
            });
            if (a("body").hasClass("folded")) {
                this.fold()
            }

        }
        , restoreMenuState: function () { }, toggle: function (b) {
            b.slideToggle(150, function () {
                var c = b.css("display", "").parent().toggleClass("menu-open").attr("id");
                if (c) {
                    a("li.has-submenu", "#adminmenu").each(function (f, g) {
                        if (c == g.id) {
                            var d = a(g).hasClass("menu-open") ? "o" : "c";
                            //setUserSetting("m" + f, d)
                        }

                    })
                }

            });
            return false
        }
        , fold: function (b) {
            if (b) {
                a("body").removeClass("folded");
                a("#adminmenu li.has-submenu").unbind()
            }
            else {
                a("body").addClass("folded");
                a("#adminmenu li.has-submenu").hoverIntent({
                    over: function (j) {
                        var d, c, g, k, i;
                        d = a(this).find(".submenu");
                        c = a(this).offset().top + d.height() + 1;
                        g = a("#wrap").height();
                        k = 60 + c - g;
                        i = a(window).height() + a(window).scrollTop() - 15;
                        if (i < (c - k)) {
                            k = c - i
                        }
                        if (k > 1) {
                            d.css({
                                marginTop: "-" + k + "px"
                            })
                        }
                        else {
                            if (d.css("marginTop")) {
                                d.css({
                                    marginTop: ""
                                })
                            }

                        }
                        d.addClass("sub-open")
                    }
                    , out: function () {
                        a(this).find(".submenu").removeClass("sub-open")
                    }
                    , timeout: 220, sensitivity: 8, interval: 100
                })
            }

        }
        , favorites: function () {
            a("#favorite-inside").width(a("#favorite-actions").width() - 4);
            a("#favorite-toggle, #favorite-inside").bind("mouseenter", function () {
                a("#favorite-inside").removeClass("slideUp").addClass("slideDown");
                setTimeout(function () {
                    if (a("#favorite-inside").hasClass("slideDown")) {
                        a("#favorite-inside").slideDown(100);
                        a("#favorite-first").addClass("slide-down")
                    }

                }
                , 200)
            }).bind("mouseleave", function () {
                a("#favorite-inside").removeClass("slideDown").addClass("slideUp");
                setTimeout(function () {
                    if (a("#favorite-inside").hasClass("slideUp")) {
                        a("#favorite-inside").slideUp(100, function () {
                            a("#favorite-first").removeClass("slide-down")
                        })
                    }

                }
                , 300)
            })
        }

    };
    a(document).ready(function () {
        adminMenu.init()
    });
    columns = {
        init: function () {
            var b = this;
            a(".hide-column-tog", "#adv-settings").click(function () {
                var d = a(this), c = d.val();
                if (d.prop("checked")) {
                    b.checked(c)
                }
                else {
                    b.unchecked(c)
                }
                columns.saveManageColumnsState()
            })
        }
        , saveManageColumnsState: function () {
            var b = this.hidden();
            a.post(ajaxurl, {
                action: "hidden-columns", hidden: b, screenoptionnonce: a("#screenoptionnonce").val(), page: pagenow
            })
        }
        , checked: function (b) {
            a(".column-" + b).show();
            this.colSpanChange(+1)
        }
        , unchecked: function (b) {
            a(".column-" + b).hide();
            this.colSpanChange(-1)
        }
        , hidden: function () {
            return a(".manage-column").filter(":hidden").map(function () {
                return this.id
            }).get().join(",")
        }
        , useCheckboxesForHidden: function () {
            this.hidden = function () {
                return a(".hide-column-tog").not(":checked").map(function () {
                    var b = this.id;
                    return b.substring(b, b.length - 5)
                }).get().join(",")
            }

        }
        , colSpanChange: function (b) {
            var d = a("table").find(".colspanchange"), c;
            if (!d.length) {
                return
            }
            c = parseInt(d.attr("colspan"), 10) + b;
            d.attr("colspan", c.toString())
        }

    };
    a(document).ready(function () {
        columns.init()
    });    
    
})(jQuery);
