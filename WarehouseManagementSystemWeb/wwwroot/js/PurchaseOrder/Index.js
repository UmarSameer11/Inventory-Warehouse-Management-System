
document.addEventListener("DOMContentLoaded", function () {
    const searchInput = document.getElementById("poSearch");
    const statusFilter = document.getElementById("statusFilter");
    const rowsContainer = document.getElementById("purchaseOrderRows");
    const visibleOrders = document.getElementById("visibleOrders");
    const noResults = document.getElementById("poNoResults");

    if (!searchInput || !statusFilter || !rowsContainer) {
        return;
    }

    function filterPurchaseOrders() {
        const searchText = searchInput.value
            .trim()
            .toLowerCase();

        const selectedStatus = statusFilter.value
            .trim()
            .toLowerCase();

        const rows = Array.from(
            rowsContainer.querySelectorAll(".po-list-row")
        );

        let visibleCount = 0;

        rows.forEach(function (row) {
            const searchableText = row.dataset.search || "";
            const status = row.dataset.status || "";

            const matchesSearch =
                searchableText.includes(searchText);

            const matchesStatus =
                !selectedStatus || status === selectedStatus;

            const shouldShow = matchesSearch && matchesStatus;

            row.hidden = !shouldShow;

            if (shouldShow) {
                visibleCount++;
            }
        });

        if (visibleOrders) {
            visibleOrders.textContent = visibleCount;
        }

        if (noResults) {
            noResults.hidden =
                rows.length === 0 || visibleCount > 0;
        }
    }

    searchInput.addEventListener("input", filterPurchaseOrders);
    statusFilter.addEventListener("change", filterPurchaseOrders);

    filterPurchaseOrders();
});
