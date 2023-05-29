$(document).ready(function () {
    //$('.cart').on('click', function (event) {
    //    $.fn.ProductDetail.RegisterOrder(event.target.id.split('-')[1]);
    //});
});

jQuery.fn.extend({
    ProductDetail: {
        // Đăngy1 ky1 vào giỏ hàng
        RegisterOrder: function (_productid) {
            $.ajax({
                url: '/Product/RegisterOrder',
                type: 'POST',
                data: {
                    productid: _productid,
                },
                success: function (response) {
                    //_this.ajax_get_list_order();
                }
            });
        },
        // Di chuyển đến trang xác nhận đơn hàng
        GotoChekOut: function (productId) {
            window.location.href = "/product/product_detail?id=" + productId + "";
        }
    }
});