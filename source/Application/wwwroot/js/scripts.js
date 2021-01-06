window.onload = function () {
    var items = document.getElementsByClassName('nav-link');
    var currentURL = window.location.pathname;
    for (var i = 0; i < items.length; i++) {
        if (items[i].pathname === currentURL) {
            items[i].classList.add("active-link");
        }
        else if (items[i].classList.contains("active-link")) {
            items[i].classList.remove("active-link");
        }
    }
}