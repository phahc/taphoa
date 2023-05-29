$(document).ready(function () {
    $('.cart_quantity_up').on('click', function (event) {
        $('#input-' + event.target.id.split('-')[1]).val(Number($('#input-' + event.target.id.split('-')[1]).val()) + 1)
    });

    $('.cart_quantity_down').on('click', function (event) {
        $('#input-' + event.target.id.split('-')[1]).val(Number($('#input-' + event.target.id.split('-')[1]).val()) - 1);
    });
});

jQuery.fn.extend({
   
});