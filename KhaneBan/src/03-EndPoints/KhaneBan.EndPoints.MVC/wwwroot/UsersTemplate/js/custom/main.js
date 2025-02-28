document.addEventListener("DOMContentLoaded", function () {
    $(window).on("scroll", function () {
        $(this).scrollTop() > 100 ? $(".header-part").addClass("header-fixed") : $(".header-part").removeClass("header-fixed");
    });

    $(".header-option-btn").on("click", function () {
        $(".header-search-option").toggle("slow");
        $(".header-main-search .form-control").toggleClass("active");
    });

    $(".header-src").on("click", function () {
        $(".header-search").toggleClass("active");
    });

    $(".navbar-dropdown a").on("click", function () {
        $(this).next().toggle();
        if ($(".dropdown-list:visible").length > 1) {
            $(".dropdown-list:visible").hide();
            $(this).next().show();
        }
    });

    $(".nasted-menu").on("click", function () {
        $(this).next().toggle();
        if ($(".nasted-menu-list:visible").length > 1) {
            $(".nasted-menu-list:visible").hide();
            $(this).next().show();
        }
    });

    $(".header-menu, .navbar-user").on("click", function () {
        $(".sidebar-part").addClass("active");
    });

    $(".sidebar-cross, .cross-btn").on("click", function () {
        $(".sidebar-part").removeClass("active");
    });

    $(".feature-bookmark button").on("click", function () {
        $(this).toggleClass("active");
    });

    $(".product-widget .fa-heart").on("click", function () {
        $(this).toggleClass("fas");
    });

    $(".eye").on("click", function () {
        $(".eye").toggleClass("fa-eye-slash fa-eye");
        let passField = $("#pass");
        passField.attr("type", passField.attr("type") === "password" ? "text" : "password");
    });

    $(".navbar-widget li").on("click", function () {
        $(".navbar-widget li").removeClass("active");
        $(this).addClass("active");
    });

    $(".edit-btn").on("click", function () {
        $(".edit-option").addClass("active");
    });

    $(".cancel").on("click", function () {
        $(".edit-option").removeClass("active");
    });

    $(".grid-hori").on("click", function () {
        $(".grid-hori").addClass("active");
        $(".grid-verti").removeClass("active");
        $(".card-grid").addClass("col-sm-12 col-md-12 col-lg-12");
        $(".product-card").addClass("inline");
    });

    $(".grid-verti").on("click", function () {
        $(".grid-verti").addClass("active");
        $(".grid-hori").removeClass("active");
        $(".card-grid").removeClass("col-sm-12 col-md-12 col-lg-12");
        $(".product-card").removeClass("inline");
    });

    let tabButton = document.querySelectorAll(".tab-btn");
    let tabPanel = document.querySelectorAll(".tab-panel");

    function showPanel(e) {
        tabPanel.forEach(panel => panel.style.display = "none");
        if (tabPanel[e]) {
            tabPanel[e].style.display = "block";
        } else {
            console.error(`tabPanel[${e}] ???? ???!`);
        }
    }

    if (tabPanel.length > 0) {
        showPanel(0);
    }

    tabButton.forEach((btn, index) => {
        btn.addEventListener("click", function () {
            showPanel(index);
        });
    });
});