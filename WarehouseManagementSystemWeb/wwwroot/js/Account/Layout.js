
(function () {
    "use strict";

    document.querySelectorAll(".js-logout").forEach(function (button) {
        button.addEventListener("click", function (event) {
            event.preventDefault();

            const form = button.closest("form");

            Swal.fire({
                title: "Sign out?",
                text: "Your current browser session will be ended.",
                icon: "question",
                showCancelButton: true,
                confirmButtonText: "Yes, logout",
                cancelButtonText: "Cancel",
                reverseButtons: true,
                confirmButtonColor: "#dc3545"
            }).then(function (result) {
                if (result.isConfirmed && form) {
                    button.disabled = true;
                    button.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span> Signing out...';
                    form.submit();
                }
            });
        });
    });

    document.querySelectorAll(".password-toggle").forEach(function (button) {
        button.addEventListener("click", function () {
            const input = document.getElementById(button.dataset.target);
            const icon = button.querySelector("i");
            if (!input) return;

            const showing = input.type === "text";
            input.type = showing ? "password" : "text";
            icon.classList.toggle("bi-eye", showing);
            icon.classList.toggle("bi-eye-slash", !showing);
            button.setAttribute("aria-label", showing ? "Show password" : "Hide password");
        });
    });

    const error = document.getElementById("errorMessage");
    if (error?.dataset.message) {
        Swal.fire({
            toast: true,
            position: "top-end",
            icon: "error",
            title: error.dataset.message,
            showConfirmButton: false,
            timer: 4500,
            timerProgressBar: true
        });
    }
})();
