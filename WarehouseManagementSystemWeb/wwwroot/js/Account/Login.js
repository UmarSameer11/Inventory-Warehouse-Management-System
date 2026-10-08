
(function () {
    "use strict";

    const form = document.getElementById("loginForm");
    const button = document.getElementById("loginButton");

    if (!form || !button) return;

    form.addEventListener("submit", function () {
        if (typeof $ !== "undefined" && !$(form).valid()) return;

        button.disabled = true;
        button.querySelector(".button-text")?.classList.add("d-none");
        button.querySelector(".button-loading")?.classList.remove("d-none");
    });
})();
