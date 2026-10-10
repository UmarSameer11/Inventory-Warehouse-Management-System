document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("vehicleAssignmentUpdateForm");
    const button = document.getElementById("updateVehicleAssignmentBtn");

    if (!form || !button) return;

    const notifyError = (message) => {
        if (typeof showError === "function") {
            showError(message);
        } else {
            alert(message);
        }
    };

    form.addEventListener("submit", function (event) {
        const assigned = document.getElementById("AssignmentDate");
        const returned = document.getElementById("ReturnDate");

        if (assigned && returned && assigned.value && returned.value && returned.value < assigned.value) {
            event.preventDefault();
            notifyError("The return date cannot be earlier than the assignment date.");
            return;
        }

        if (!form.checkValidity()) return;

        button.disabled = true;
        button.innerHTML =
            '<span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>' +
            'Updating...';
    });

    const status = document.getElementById("IsActive");
    const label = document.getElementById("statusLabel");

    if (status && label) {
        const updateLabel = () => label.textContent = status.checked ? "Active" : "Inactive";
        status.addEventListener("change", updateLabel);
        updateLabel();
    }
});
