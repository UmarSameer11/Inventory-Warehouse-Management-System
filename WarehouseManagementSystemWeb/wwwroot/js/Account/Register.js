
(function () {
    "use strict";

    const form = document.getElementById("registerForm");
    const button = document.getElementById("registerButton");

    if (!form || !button) return;

    form.addEventListener("submit", function (event) {
        if (typeof $ !== "undefined" && !$(form).valid()) {
            event.preventDefault();
            return;
        }

        button.disabled = true;
        button.querySelector(".button-text")?.classList.add("d-none");
        button.querySelector(".button-loading")?.classList.remove("d-none");
    });
})();
