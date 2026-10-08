
(function () {
    "use strict";

    const form = document.getElementById("changePasswordForm");
    const button = document.getElementById("changePasswordButton");

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
