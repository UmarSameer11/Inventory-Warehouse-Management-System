
(function () {
    "use strict";

    const revokeForms = document.querySelectorAll(".js-revoke-session");

    revokeForms.forEach(function (form) {
        form.addEventListener("submit", function (event) {
            event.preventDefault();

            const current = form.querySelector('input[name="isCurrent"]')?.value === "True";
            const title = current ? "Log out from this device?" : "Revoke this session?";
            const text = current
                ? "Your current browser session will be ended."
                : "This device will no longer be able to use this session.";

            Swal.fire({
                title: title,
                text: text,
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: current ? "Yes, log out" : "Revoke session",
                cancelButtonText: "Cancel",
                reverseButtons: true,
                confirmButtonColor: "#dc3545"
            }).then(function (result) {
                if (result.isConfirmed) {
                    const button = form.querySelector("button");
                    if (button) {
                        button.disabled = true;
                        button.innerHTML = '<span class="spinner-border spinner-border-sm"></span>';
                    }
                    form.submit();
                }
            });
        });
    });

    const logoutAll = document.getElementById("logoutAllForm");
    if (logoutAll) {
        logoutAll.addEventListener("submit", function (event) {
            event.preventDefault();

            Swal.fire({
                title: "Log out from all devices?",
                text: "Every active session, including this device, will be ended.",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Yes, log out all",
                cancelButtonText: "Cancel",
                reverseButtons: true,
                confirmButtonColor: "#dc3545"
            }).then(function (result) {
                if (result.isConfirmed) logoutAll.submit();
            });
        });
    }
})();
