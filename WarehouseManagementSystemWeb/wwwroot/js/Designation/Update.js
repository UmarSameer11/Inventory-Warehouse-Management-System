document.addEventListener("DOMContentLoaded", function () {

const form = document.getElementById("designationUpdateForm");
const submitButton = document.getElementById("updateDesignationBtn");

const isActive = document.getElementById("IsActive");
const statusLabel = document.getElementById("statusLabel");


// --------------------------------------------
// Update Status Label
// --------------------------------------------

function updateStatusLabel() {

    if (!isActive || !statusLabel) {
        return;
    }

    if (isActive.checked) {
        statusLabel.textContent = "Active";
    }
    else {
        statusLabel.textContent = "Inactive";
    }
}


if (isActive) {

    updateStatusLabel();

    isActive.addEventListener("change", function () {
        updateStatusLabel();
    });

}


// --------------------------------------------
// Prevent Double Submit
// --------------------------------------------

if (form && submitButton) {

    form.addEventListener("submit", function () {

        if (typeof $ !== "undefined" &&
            typeof $(form).valid === "function" &&
            !$(form).valid()) {

            return;
        }

        submitButton.disabled = true;

        submitButton.innerHTML =
            '<span class="spinner-border spinner-border-sm me-2" ' +
            'role="status" aria-hidden="true"></span>' +
            'Updating...';

    });

}

});
