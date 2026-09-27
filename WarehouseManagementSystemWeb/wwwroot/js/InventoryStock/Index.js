document.addEventListener("DOMContentLoaded", function () {

    /* ============================================
       Stock Search + Warehouse Filter
       ============================================ */

    const searchInput = document.getElementById("stockSearch");
    const warehouseFilter = document.getElementById("stockWarehouseFilter");
    const stockTable = document.getElementById("stockTable");
    const noResult = document.getElementById("stockNoResult");

    if (!stockTable) {
        return;
    }

    // Rows that carry data-warehouse are real records (the empty-state row does not)
    const rows = Array.from(stockTable.querySelectorAll("tbody tr[data-warehouse]"));

    function applyFilters() {

        const searchValue = (searchInput ? searchInput.value : "").toLowerCase().trim();
        const warehouseValue = warehouseFilter ? warehouseFilter.value : "";

        let visibleCount = 0;

        rows.forEach(function (row) {

            const matchesSearch = row.textContent.toLowerCase().includes(searchValue);
            const matchesWarehouse = warehouseValue === "" || row.dataset.warehouse === warehouseValue;

            const visible = matchesSearch && matchesWarehouse;

            row.style.display = visible ? "" : "none";

            if (visible) {
                visibleCount++;
            }
        });

        if (noResult) {
            noResult.style.display =
                rows.length > 0 && visibleCount === 0 ? "block" : "none";
        }
    }

    if (searchInput) {
        searchInput.addEventListener("input", applyFilters);
    }

    if (warehouseFilter) {
        warehouseFilter.addEventListener("change", applyFilters);
    }

});
