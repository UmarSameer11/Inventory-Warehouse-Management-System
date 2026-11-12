
document.addEventListener("DOMContentLoaded", function () {

    const deleteForm = document.getElementById("goodsReceiptDeleteForm");
    const deleteButton = document.getElementById("deleteGoodsReceiptBtn");

    if (!deleteForm || !deleteButton) {
        return;
    }

    let isConfirmed = false;

    deleteForm.addEventListener("submit", function (event) {

        if (!isConfirmed) {

            event.preventDefault();

            const confirmed = window.confirm(
                "Are you sure you want to delete this Goods Receipt?\n\n" +
                "This action cannot be undone."
            );

            if (!confirmed) {
                return;
            }

            isConfirmed = true;
            deleteButton.click();

            return;
        }

        deleteButton.disabled = true;

        deleteButton.innerHTML =
            '<span class="spinner-border spinner-border-sm me-2"></span>' +
            'Deleting...';

    });

});
