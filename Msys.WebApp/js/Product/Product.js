
$(document).ready(function () {
    $('.linkMenu').on('click', function (event) {
        $(".linkMenu").removeClass("link-selected");
        $(this).addClass("link-selected");
        $.fn.Product.GetProductByCategory(event.target.id.split('-')[1]);
    });

    $('.add-to-cart').on('click', function (event) {
        $.fn.Product.GotoProductDetail(event.target.id.split('-')[1]);
    });
});

jQuery.fn.extend({
    Product: {
        GetProductByCategory: function (categoryId) {
                $.ajax({
                    url: '/Product/ProductListByCategory',
                    type: 'POST',
                    data: { id: categoryId },
                    success: function (response) {
                        $('#ProductResultList').html(response);
                    }
                });
        },
        GotoProductDetail: function (productId) {
            window.location.href = "/product/product_detail?id=" + productId + "";
        }
    }
});