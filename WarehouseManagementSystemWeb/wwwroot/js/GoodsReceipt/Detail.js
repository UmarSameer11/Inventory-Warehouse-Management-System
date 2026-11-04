document.addEventListener("DOMContentLoaded", function () {

    // Product Search
    const searchInput = document.getElementById("grnProductSearch");
    const table = document.getElementById("grnDetailsTable");

    if (searchInput && table) {

        searchInput.addEventListener("input", function () {

            const searchValue = this.value.toLowerCase().trim();

            const rows = table.querySelectorAll(".grn-detail-row");

            rows.forEach(function (row) {

                const rowText = row.textContent.toLowerCase();

                row.style.display = rowText.includes(searchValue)
                    ? ""
                    : "none";

            });
        });
    }

    // Print GRN
    const printButton = document.getElementById("printGrnBtn");

    if (printButton) {

        printButton.addEventListener("click", function () {
            window.print();
        });
    }

});