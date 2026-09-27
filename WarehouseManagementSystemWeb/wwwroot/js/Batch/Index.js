document.addEventListener("DOMContentLoaded", function () {

    /* ============================================
       Batch Search + Status Filter
       ============================================ */

    const searchInput = document.getElementById("batchSearch");
    const statusFilter = document.getElementById("batchStatusFilter");
    const batchTable = document.getElementById("batchTable");
    const noResult = document.getElementById("batchNoResult");

    if (!batchTable) {
        return;
    }

    // Rows that carry data-status are real records (the empty-state row does not)
    const rows = Array.from(batchTable.querySelectorAll("tbody tr[data-status]"));

    function applyFilters() {

        const searchValue = (searchInput ? searchInput.value : "").toLowerCase().trim();
        const statusValue = statusFilter ? statusFilter.value : "";

        let visibleCount = 0;

        rows.forEach(function (row) {

            const matchesSearch = row.textContent.toLowerCase().includes(searchValue);
            const matchesStatus = statusValue === "" || row.dataset.status === statusValue;

            const visible = matchesSearch && matchesStatus;

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

    if (statusFilter) {
        statusFilter.addEventListener("change", applyFilters);
    }

});
