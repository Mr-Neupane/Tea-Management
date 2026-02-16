$(document).on('change', '.quantity-input ', function () {
    let quantity = $(this).val();
    let currentRow = $(this).closest('tr');
    let rate = currentRow.find('.rate-input');
    let waterQuantity = parseFloat(
        currentRow.find('.water-quantity-input').val()
    ) || 0;
    let amount = currentRow.find('.amount');
    amount.val((quantity - waterQuantity) * rate.val());
    calculateGrandTotal();
});

$(document).on('change', '.water-quantity-input ', function () {
    let waterQuantity = $(this).val();
    let currentRow = $(this).closest('tr');
    let rate = currentRow.find('.rate-input');
    let quantity = currentRow.find('.quantity-input');
    let amount = currentRow.find('.amount');
    amount.val((quantity.val() - waterQuantity) * rate.val());
    calculateGrandTotal();
});

$(document).on('change', '.rate-input ', function () {
    let rate = $(this).val();
    let currentRow = $(this).closest('tr');
    let quantity = currentRow.find('.quantity-input');
    let waterQuantity = parseFloat(
        currentRow.find('.water-quantity-input').val()
    ) || 0;
    let amount = currentRow.find('.amount');
    amount.val(rate * (quantity.val() - waterQuantity));
    calculateGrandTotal();
});

function calculateGrandTotal() {
    let total = 0;

    $(".amount").each(function () {
        let amount = parseFloat($(this).val()) || 0;
        total += amount;
    });

    $(".total-amount").val(total.toFixed(2));

}