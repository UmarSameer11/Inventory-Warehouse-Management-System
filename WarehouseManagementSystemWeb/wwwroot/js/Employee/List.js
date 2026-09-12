document.addEventListener("DOMContentLoaded", function () {

    const searchInput = document.getElementById("employeeSearch");
    const table = document.getElementById("employeeTable");

    if (!searchInput || !table)
        return;

    searchInput.addEventListener("input", function () {

        const searchText = this.value.toLowerCase().trim();

        const rows = table.querySelectorAll("tbody tr");

        rows.forEach(function (row) {

            const rowText = row.innerText.toLowerCase();

            row.style.display =
                rowText.includes(searchText)
                    ? ""
                    : "none";
        });
    });
});