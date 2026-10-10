document.addEventListener("DOMContentLoaded", function () {

    const searchInput = document.getElementById("stockAdjustmentSearch");
    const table = document.getElementById("stockAdjustmentTable");

    if (!searchInput || !table) return;

    searchInput.addEventListener("keyup", function () {
        const searchValue = this.value.toLowerCase().trim();

        table.querySelectorAll("tbody tr").forEach(function (row) {
            row.style.display =
                row.textContent.toLowerCase().includes(searchValue) ? "" : "none";
        });
    });

    // Success / error toasts are shown by ~/js/Alert/showSuccessToast.js (loaded in the layout).
});
