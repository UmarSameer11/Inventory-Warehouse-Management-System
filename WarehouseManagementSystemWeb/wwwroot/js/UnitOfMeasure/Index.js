document.addEventListener("DOMContentLoaded", function () {

    const searchInput = document.getElementById("unitOfMeasureSearch");
    const table = document.getElementById("unitOfMeasureTable");

    if (searchInput && table) {
        searchInput.addEventListener("keyup", function () {
            const searchValue = this.value.toLowerCase().trim();

            table.querySelectorAll("tbody tr").forEach(function (row) {
                row.style.display =
                    row.textContent.toLowerCase().includes(searchValue) ? "" : "none";
            });
        });
    }

    const successMessage = document.getElementById("successMessage");
    if (successMessage) {
        const message = successMessage.dataset.message;
        if (message && typeof showSuccess === "function") {
            showSuccess(message);
        }
    }

    const errorMessage = document.getElementById("errorMessage");
    if (errorMessage) {
        const message = errorMessage.dataset.message;
        if (message && typeof showError === "function") {
            showError(message);
        }
    }
});
