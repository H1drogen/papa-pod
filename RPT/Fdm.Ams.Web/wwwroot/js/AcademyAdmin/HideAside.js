$(document).ready(function () {
    $(".normal-aside").on("click", ".aside__toggle-bar", function () {
        $(this).parent("aside").toggleClass("zero-width");
    });

    $("aside").on("click", ".aside__toggle-bar", function () {
        $(this).find("i").toggleClass("fa-chevron-left");
        $(this).find("i").toggleClass("fa-chevron-right");
    });

    $(function () {
        $("aside").each(function () {
            if ($(this).children(".aside__toggle-bar").length > 0) {
                $(this).addClass("aside--toggle-bar-padding");
            }
        });
    });

    $(".admin-aside").on("click", ".aside__toggle-bar", function () {
        toggleAdminAside();
    });

    $(".admin-aside a").on("click", function () {
        if (!$("aside .aside__inner h2").is(":visible")) {
            toggleAdminAside();
        }
    });

    function toggleAdminAside() {
        if ($(".admin-aside #dataLinks").hasClass("show")) {
            $('.admin-aside a[data-target="#dataLinks"').click();
        }

        $(".admin-aside.aside--toggle-bar-padding").toggleClass("");
        $(".admin-aside .aside__inner h2").toggle();
        $(".admin-aside span").toggle();
        $(".admin-aside i.fa-arrow-down").toggle();
        $(".admin-aside .accordion a").toggle();

        if ($(".admin-aside").width() !== 60) {
            $(".admin-aside").animate({ width: 80 }, 150);
        } else {
            $(".admin-aside").animate({ width: 280 }, 150);
        }
    }
});