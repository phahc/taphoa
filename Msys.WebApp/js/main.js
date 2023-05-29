/*price range*/

 $('#sl2').slider();

	var RGBChange = function() {
	  $('#RGB').css('background', 'rgb('+r.getValue()+','+g.getValue()+','+b.getValue()+')')
	};	
		
/*scroll to top*/

$(document).ready(function(){
	$(function () {
		$.scrollUp({
	        scrollName: 'scrollUp', // Element ID
	        scrollDistance: 300, // Distance from top/bottom before showing element (px)
	        scrollFrom: 'top', // 'top' or 'bottom'
	        scrollSpeed: 300, // Speed back to top (ms)
	        easingType: 'linear', // Scroll to top easing (see http://easings.net/)
	        animation: 'fade', // Fade, slide, none
	        animationSpeed: 200, // Animation in speed (ms)
	        scrollTrigger: false, // Set a custom triggering element. Can be an HTML string or jQuery object
					//scrollTarget: false, // Set a custom target element for scrolling to the top
	        scrollText: '<i class="fa fa-angle-up"></i>', // Text for element, can contain HTML
	        scrollTitle: false, // Set a custom <a> title if required.
	        scrollImg: false, // Set true to use image
	        activeOverlay: false, // Set CSS color to display scrollUp active point, e.g '#00FFFF'
	        zIndex: 2147483647 // Z-Index for the overlay
		});
	});
	
	posts = new app.Posts(); /* Instantiate the Posts Class */
	posts.init(); /* Load Posts class methods */
	posts.ajax_get_products($('.link-selected').attr('id')); /* Load Posts class methods */
    //posts.ajax_get_list_order();

	$(".linkMenu").click(function (e) {
	    e.preventDefault();
	    alert(1);
	    $("#productForm").submit();
	});
});

/**
 * Setup a App namespace to prevent JS conflicts.
 */
var app = {
    Posts: function () {
        /**
         * This method contains the list of functions that needs to be loaded
         * when the "Posts" object is instantiated.
         *
         */
        this.init = function () {
            this.get_all_items_pagination();
        }

        /**
         * Load front-end items pagination.
         */
        this.get_all_items_pagination = function () {

            _this = this;

            /* Check if our hidden form input is not empty, meaning it's not the first time viewing the page. */
            if ($('form.post-list input').val()) {
                /* Submit hidden form input value to load previous page number */
                data = JSON.parse($('form.post-list input').val());
                _this.ajax_get_all_items_pagination(data.page, data.name, data.sort);
            } else {
                /* Load first page */
                _this.ajax_get_all_items_pagination(1, $('.post_name').val(), $('.post_sort').val());
            }
            /* Search when Enter Key is triggered */
            $(".post_search_text").on('input', function (e) {
                _this.ajax_get_all_items_pagination(1, $('.post_name').val(), $('.post_sort').val());
            });

            /* Search */
            $('body').on('click', '.post_search_submit', function () {
                _this.ajax_get_all_items_pagination(1, $('.post_name').val(), $('.post_sort').val());
            });
            /* Search when Enter Key is triggered */
            $(".post_search_text").keyup(function (e) {
                if (e.keyCode == 13) {
                    _this.ajax_get_all_items_pagination(1, $('.post_name').val(), $('.post_sort').val());
                }
            });

            $('.linkMenu').on('click', function (event) {
                $(".linkMenu").removeClass("link-selected");
                $(this).addClass("link-selected");
                _this.ajax_get_products(event.target.id);
            });

            //$('.btn-order-new').on('click', function (event) {
            //    _this.ajax_register_order_new(event.target.id);
            //});

            ///* Pagination Clicks   */
            //$('body').on('click', '.pagination-nav li.active', function () {
            //    var page = $(this).attr('p');
            //    _this.ajax_get_all_items_pagination(page, $('.post_name').val(), $('.post_sort').val());
            //});
        }

        /**
         * AJAX front-end items pagination.
         */
        this.ajax_get_all_items_pagination = function (page, order_by_name, order_by_sort) {

            if ($(".pagination-container").length > 0 && $('.products-view-all').length > 0) {
                $(".pagination-container").html('<img src="/Content/Images/loading.gif" class="ml-tb" />');

                var post_data = {
                    page: page,
                    search: $('.post_search_text').val(),
                    name: order_by_name,
                    sort: order_by_sort,
                    max: $('.post_max').val(),
                };

                $('form.post-list input').val(JSON.stringify(post_data));

                var data = {
                    action: 'get-all-products',
                    data: JSON.parse($('form.post-list input').val())
                };

                $.ajax({
                    url: '/Home/Search',
                    type: 'POST',
                    data: data,
                    success: function (response) {
                        response = JSON.parse(response);
                        if ($(".pagination-container").html(response.content)) {
                            $('.btn-success').on('click', function (event) {
                                if ($('.tab-active').hasClass('tab-on')) {
                                    _this.ajax_register_order($('.tab-active.tab-on').attr('id'), event.target.id);
                                    event.preventDefault();
                                }
                                else {
                                    alert("Bạn chưa có giỏ hàng. Vui lòng tạo giỏ hàng.");
                                    return false;
                                }
                            });
                            $('.btn-warning').on('click', function (event) {
                                _this.ajax_register_order_new(event.target.id.split('-')[1]);
                                event.preventDefault();
                            });
                            $('.pagination-nav').html(response.navigation);
                           
                            $('.table-post-list th').each(function () {
                                /* Append the button indicator */
                                $(this).find('span.glyphicon').remove();
                                if ($(this).hasClass('active')) {
                                    if (JSON.parse($('form.post-list input').val()).th_sort == 'DESC') {
                                        $(this).append(' <span class="glyphicon glyphicon-chevron-down pull-right"></span>');
                                    } else {
                                        $(this).append(' <span class="glyphicon glyphicon-chevron-up pull-right"></span>');
                                    }
                                }
                            });
                        }
                    }
                });
            }
        }

        this.ajax_get_list_order = function () {
            if ($(".tab-order").length > 0) {
                $(".tab-order").html('<img src="/Content/Images/loading.gif" class="ml-tb" />');

                $.ajax({
                    url: '/Home/LoadListOrder',
                    type: 'POST',
                    data: null,
                    success: function (response) {
                        response = JSON.parse(response);
                        $(".tab-order").html(response.content_tab)
                        $(".tab-order-detail").html(response.detail_tab)
                        $('.tab-active').on('click', function (event) {
                            $(".tab-active").removeClass("tab-on");
                            $(this).addClass("tab-on");
                            _this.ajax_update_not_active_order(event.target.id);
                            event.preventDefault();
                        });

                        $('.cart_quantity_up').on('click', function (event) {
                            var arrId = event.target.id.split('-');
                            var orderid = arrId[1];
                            var productid = arrId[2];
                            var quantityId = ["quantity", orderid, productid].join('-');
                            // Thông tin trước khi thay đổi
                            var price = _this.FormatStringToNumber($(["p#cart_price", orderid, productid].join('-')).text());
                            var quantity = Number($('#' + quantityId).val());
                            var beforeTotal = price * quantity;
                            var afterTotal = 0;
                            $('#' + quantityId).val((Number($('#' + quantityId).val()) + 1).toString());
                            _this.ajax_update_order_detail(orderid, productid, $('#' + quantityId).val());
                            _this.calculator(orderid, productid, $('#' + quantityId).val());
                            // Số lượng sau khi tăng
                            quantity = Number($('#' + quantityId).val());
                            afterTotal = price * quantity;
         
                            // SỐ tiền sau khi tăng số lượng
                            var totalId = $('h2#' + ["total_order_money", orderid].join('-'));
                            var spanTotal = $(totalId)[0];
                            spanTotal.lastChild.nodeValue = _this.calculatorTotal(_this.FormatStringToNumber(spanTotal.lastChild.nodeValue), beforeTotal, afterTotal);

                            // Tính tổng tiền phải thu sau khi tăng số lượng
                            var discount_priceId = ["discount_price", orderid].join('-');
                            var minus = _this.FormatStringToNumber(spanTotal.lastChild.nodeValue) - _this.FormatStringToNumber($('#' + discount_priceId).val());
                            
                            var paymentId = ["total_money_payment", orderid].join('-');
                            var totalPaymentId = $('h2#' + paymentId);
                            var totalPayment = $(totalPaymentId)[0];
                            totalPayment.lastChild.nodeValue = _this.formatMoney(minus, 0, "");
       
                            // Tính lại tiền trả khách
                            var totalReturnId = $('h2#' + ["total_return_money", orderid].join('-'));
                            var spanReturnTotal = $(totalReturnId)[0];
                            spanReturnTotal.lastChild.nodeValue = _this.formatMoney(_this.FormatStringToNumber($('#customer_money-' + orderid).val()) - _this.FormatStringToNumber(totalPayment.lastChild.nodeValue), 0, "");

                            if (_this.FormatStringToNumber(spanReturnTotal.lastChild.nodeValue) < 0) {
                                ('#debit_panel-' + orderid).removeClass("hide");
                            } else {
                                $('#debit_panel-' + orderid).addClass("hide");
                            }
                            event.preventDefault();
                        });
                        $('.cart_quantity_down').on('click', function (event) {
                            var arrId = event.target.id.split('-');
                            var orderid = arrId[1];
                            var productid = arrId[2];

                            var quantityId = ["quantity", orderid, productid].join('-');
                            // Thông tin trước khi thay đổi
                            var price = _this.FormatStringToNumber($(["p#cart_price", orderid, productid].join('-')).text());
                            var quantity = Number($('#' + quantityId).val());
                            var beforeTotal = price * quantity;
                            var afterTotal = 0;
                            if (Number($('#' + quantityId + '').val()) > 1)
                                $('#' + quantityId).val((Number($('#' + quantityId).val()) - 1).toString());
                            else
                                $('#' + quantityId).val("1");

                            _this.ajax_update_order_detail(orderid, productid, $('#' + quantityId).val());
                            _this.calculator(orderid, productid, $('#' + quantityId).val());
                            // Số lượng sau khi tăng
                            quantity = Number($('#' + quantityId).val());
                            afterTotal = price * quantity;
                            var totalId = $('h2#' + ["total_order_money", orderid].join('-'));
                            var spanTotal = $(totalId)[0];
                            spanTotal.lastChild.nodeValue = _this.calculatorTotal(_this.FormatStringToNumber(spanTotal.lastChild.nodeValue), beforeTotal, afterTotal);
                           
                            // Tính tổng tiền phải thu
                            var discount_priceId = ["discount_price", orderid].join('-');
                            var minus = _this.FormatStringToNumber(spanTotal.lastChild.nodeValue) - _this.FormatStringToNumber($('#' + discount_priceId).val());
                            var paymentId = ["total_money_payment", orderid].join('-');
                            var totalPaymentId = $('h2#' + paymentId);
                            var totalPayment = $(totalPaymentId)[0];
                            totalPayment.lastChild.nodeValue = _this.formatMoney(minus, 0, "");

                            // Tính lại tiền trả khách
                            var totalReturnId = $('h2#' + ["total_return_money", orderid].join('-'));
                            var spanReturnTotal = $(totalReturnId)[0];
                            spanReturnTotal.lastChild.nodeValue = _this.formatMoney(_this.FormatStringToNumber($('#customer_money-' + orderid).val()) - _this.FormatStringToNumber(totalPayment.lastChild.nodeValue), 0, "");

                            if (_this.FormatStringToNumber(spanReturnTotal.lastChild.nodeValue) < 0) {
                                ('#debit_panel-' + orderid).removeClass("hide");
                            } else {
                                $('#debit_panel-' + orderid).addClass("hide");
                            }
                            event.preventDefault();
                        });
                        $('input').on('focusin', function () {
                            $(this).data('val', $(this).val());
                        });

                        $('input').on('change', function (event) {
                            
                            if ($(this).hasClass('cart_quantity_input')) {
                                var arrId = event.target.id.split('-');
                                var orderid = arrId[1];
                                var productid = arrId[2];
                                var quantityId = ["quantity", orderid, productid].join('-');
                                var price = parseFloat(_this.FormatStringToNumber($(["p#cart_price", orderid, productid].join('-')).text()));
                                var prevQuantity = $(this).data('val');
                                var currentQuantity = event.target.value;
                                var quantity = Number(currentQuantity) - Number(prevQuantity);

                                var moneyMinus = price * quantity;
                                if (event.target.value == "" || Number(event.target.value) <= 0) {
                                    $('#' + quantityId).val("1");
                                }
                                _this.ajax_update_order_detail(orderid, productid, $('#' + quantityId).val());
                                _this.calculator(orderid, productid, $('#' + quantityId).val());
                                // Số lượng sau khi thay đổi
                                var totalId = $('h2#' + ["total_order_money", orderid].join('-'));
                                var spanTotal = $(totalId)[0];
                                spanTotal.lastChild.nodeValue = _this.formatMoney(_this.FormatStringToNumber(spanTotal.lastChild.nodeValue) + moneyMinus, 0, "");
                               
                                // Tính tổng tiền phải thu
                                var discount_priceId = ["discount_price", orderid].join('-');
                                var minus = _this.FormatStringToNumber(spanTotal.lastChild.nodeValue) - _this.FormatStringToNumber($('#' + discount_priceId).val());
                                var paymentId = ["total_money_payment", orderid].join('-');
                                var totalPaymentId = $('h2#' + paymentId);
                                var totalPayment = $(totalPaymentId)[0];
                                totalPayment.lastChild.nodeValue = _this.formatMoney(minus, 0, "");

                                // Tính lại tiền trả khách
                                var totalReturnId = $('h2#' + ["total_return_money", orderid].join('-'));
                                var spanReturnTotal = $(totalReturnId)[0];
                                spanReturnTotal.lastChild.nodeValue = _this.formatMoney(_this.FormatStringToNumber($('#customer_money-' + orderid).val()) - _this.FormatStringToNumber(totalPayment.lastChild.nodeValue), 0, "");

                                if (_this.FormatStringToNumber(spanReturnTotal.lastChild.nodeValue) < 0)
                                {
                                    ('#debit_panel-' + orderid).removeClass("hide");
                                } else {
                                    $('#debit_panel-' + orderid).addClass("hide");
                                }
                            }
                            event.preventDefault();
                        });

                        $('.check_debit').on('click', function (event) {
                            if ($(this).hasClass('fa fa-check-square-o')) {
                                $(this).removeClass("fa fa-check-square-o");
                                $(this).addClass("fa fa-square-o");
                                $('#debit_panel-' + event.target.id.split('-')[1]).addClass("hide");
                            } else {
                                $('#debit_panel-' + event.target.id.split('-')[1]).removeClass("hide");
                                $(this).removeClass("fa fa-square-o");
                                $(this).addClass("fa fa-check-square-o");
                            }
                        });
                        // Xoá hoá đơn
                        $('.icon-double-angle-left').on('click', function (event) {
                            var result = confirm("Bạn có thật sự muốn xoá đơn hàng này không?");
                            if (!result) {
                                return false;
                            }

                            var arrId = event.target.id.split('-');
                            var orderid = arrId[1];
                           
                            _this.ajax_delete_order(orderid);
                            event.preventDefault();
                        });

                        // Xoá hàng hoá trong bảng chi tiết hoá đơn
                        $('.cart_quantity_delete').on('click', function (event) {
                            var result = confirm("Bạn có thật sự muốn xoá hàng hoá này ra khỏi đơn hàng không?");
                            if (!result) {
                                return false;
                            }
                            var arrId = event.target.id.split('-');
                            var orderid = arrId[1];
                            var productid = arrId[2];
                            _this.ajax_delete_product_in_order_detail(orderid, productid);
                            event.preventDefault();
                        });

                        // Thanh toán hoá đơn
                        $('.payment-on').on('click', function (event) {
                            var arrId = event.target.id.split('-');
                            var orderid = arrId[1];
                            // Số lượng sau khi thay đổi
                            var totalId = $('h2#' + ["total_money_payment", orderid].join('-'));
                            var spanTotal = $(totalId)[0];
                            // Tính lại tiền trả khách
                            var totalReturnId = $('h2#' + ["total_return_money", orderid].join('-'));
                            var spanReturnTotal = $(totalReturnId)[0];
                            var minus = _this.FormatStringToNumber($('#customer_money-' + orderid).val()) - _this.FormatStringToNumber(spanTotal.lastChild.nodeValue);
                            if (!($('#debit-' + orderid).hasClass('fa fa-check-square-o')) && ($('#customer_money-' + orderid).val() == "" || _this.FormatStringToNumber($('#customer_money-' + orderid).val()) <= 0 || minus < 0)) {
                                alert("Số tiền khách đưa bị thiếu. Vui lòng kiểm tra lại.");
                                return false;
                            }
                            var result = confirm("Bạn có thật sự muốn thanh toán đơn hàng không?");
                            if (!result) {
                                return false;
                            }
                            _this.ajax_payment(orderid);
                            event.preventDefault();
                        });

                        $('.discount_price').on('input', function (event) {
                            var arrId = event.target.id.split('-');
                            var orderid = arrId[1];
                            var discount_priceId = ["discount_price", orderid].join('-');
                            if (event.target.value == "" || _this.FormatStringToNumber(event.target.value) <= 0) {
                                $('#' + discount_priceId).val("0");
                            }
                            else {
                                $('#' + discount_priceId).val(_this.formatMoney(_this.FormatStringToNumber(event.target.value), 0, ""));
                            }

                            var totalId = $('h2#' + ["total_order_money", orderid].join('-'));
                            var spanTotal = $(totalId)[0];
                            // Tổng chi phí
                            var totalReturnId = $('h2#' + ["total_return_money", orderid].join('-'));
                            var paymentId = ["total_money_payment", orderid].join('-');
                            var minus = _this.FormatStringToNumber(spanTotal.lastChild.nodeValue) - _this.FormatStringToNumber(event.target.value);
                            // Tính tổng tiền phải thu
                            var totalPaymentId = $('h2#' + paymentId);
                            var totalPayment = $(totalPaymentId)[0];
                            totalPayment.lastChild.nodeValue = _this.formatMoney(minus, 0, "");
                            // Tính lại tiền trả khách
                            var totalReturnId = $('h2#' + ["total_return_money", orderid].join('-'));
                            var spanReturnTotal = $(totalReturnId)[0];
                            spanReturnTotal.lastChild.nodeValue = _this.formatMoney(_this.FormatStringToNumber($('#customer_money-' + orderid).val()) - _this.FormatStringToNumber(totalPayment.lastChild.nodeValue), 0, "");                           
                            event.preventDefault();
                        });
                        //$('.cart_quantity_input').on('input', function (event) {
                        //    var arrId = event.target.id.split('-');
                        //    var orderid = arrId[1];
                        //    var productid = arrId[2];
                        //    var quantityId = ["quantity", orderid, productid].join('-');
                        //    var price = parseFloat(_this.FormatStringToNumber($(["p#cart_price", orderid, productid].join('-')).text()));
                        //    var prevQuantity = $(this).data('val');
                        //    var currentQuantity = event.target.value;
                        //    var quantity = Number(currentQuantity) - Number(prevQuantity);
                        //    alert(quantity);
                        //    var moneyMinus = price * quantity;
                        //    if (event.target.value == "" || Number(event.target.value) <= 0) {
                        //        $('#' + quantityId).val("1");
                        //    }
                        //    _this.ajax_update_order_detail(orderid, productid, event.target.value);
                        //    _this.calculator(orderid, productid, currentQuantity);
                        //    // Số lượng sau khi thay đổi
                        //    var totalId = $('h2#' + ["total_order_money", orderid].join('-'));
                        //    var spanTotal = $(totalId)[0];
                        //    spanTotal.lastChild.nodeValue = _this.formatMoney(_this.FormatStringToNumber(spanTotal.lastChild.nodeValue) + moneyMinus, 0, "");
                        //    // Tính lại tiền trả khách
                        //    var totalReturnId = $('h2#' + ["total_return_money", orderid].join('-'));
                        //    var spanReturnTotal = $(totalReturnId)[0];
                        //    spanReturnTotal.lastChild.nodeValue = _this.formatMoney(_this.FormatStringToNumber($('#customer_money-' + orderid).val()) - _this.FormatStringToNumber(spanTotal.lastChild.nodeValue), 0, "");
                        //    event.preventDefault();
                        //});

                        $('.form-control.customer_money').on('input', function (event) {
                            var arrId = event.target.id.split('-');
                            var orderid = arrId[1]; 
                            var totalId = $('h2#' + ["total_money_payment", orderid].join('-'));
                            var totalReturnId = $('h2#' + ["total_return_money", orderid].join('-'));
                            var spanTotal = $(totalId)[0];
                            var spanReturnTotal = $(totalReturnId)[0];
                            spanReturnTotal.lastChild.nodeValue = _this.formatMoney(_this.FormatStringToNumber(event.target.value) - _this.FormatStringToNumber(spanTotal.lastChild.nodeValue), 0, "");
                            if (_this.FormatStringToNumber(spanReturnTotal.lastChild.nodeValue) < 0) {
                                    $('#debit_panel-' + orderid).removeClass("hide");
                                } else {
                                    $('#debit_panel-' + orderid).addClass("hide");
                                }
                            event.target.value = _this.formatMoney(_this.FormatStringToNumber(event.target.value), 0, "");
                            event.preventDefault();
                        });
                    }
                });
            }
        }

        this.ajax_get_products = function (_id) {
            if ($(".pagination-container-product").length > 0) {
                $(".pagination-container-product").html('<img src="/Content/Images/loading.gif" class="ml-tb" />');

                var post_data = {
                    id: _id,
                };

                $('form.post-list input').val(JSON.stringify(post_data));

                var data = {
                    action: 'get-products',
                    data: JSON.parse($('form.post-list input').val())
                };

                $.ajax({
                    url: '/Home/ProductList',
                    type: 'POST',
                    data: data,
                    success: function (response) {
                        response = JSON.parse(response);
                        if ($(".pagination-container-product").html(response.content)) {
                            $('.btn_select_product').on('click', function (event) {
                                if ($('.tab-active').hasClass('tab-on')) {
                                    _this.ajax_register_order($('.tab-active.tab-on').attr('id'), event.target.id.split('-')[1]);
                                    event.preventDefault();
                                }
                                else {
                                    alert("Bạn chưa có giỏ hàng. Vui lòng tạo giỏ hàng.");
                                    return false;
                                }
                            });
                            $('.btn_new_cart').on('click', function (event) {
                                _this.ajax_register_order_new(event.target.id.split('-')[1]);
                                event.preventDefault();
                            });
                        };
                    }
                });
            }
        }

        this.ajax_register_order = function (_orderid, _productid) {
            var post_data = {
                orderid: _orderid,
                productid: _productid,
            };

            $('form.post-list input').val(JSON.stringify(post_data));

            var data = {
                action: 'register-order',
                data: JSON.parse($('form.post-list input').val())
            };

            $.ajax({
                url: '/Home/InsertOrderDetail',
                type: 'POST',
                data: data,
                success: function (response) {
                    _this.ajax_get_list_order();
                }
            });
        }

        this.ajax_register_order_new = function (_productid) {
            var post_data = {
                productid: _productid,
            };

            $('form.post-list input').val(JSON.stringify(post_data));

            var data = {
                action: 'register-order-new',
                data: JSON.parse($('form.post-list input').val())
            };
            $.ajax({
                url: '/Home/RegisterOrder',
                type: 'POST',
                data: data,
                success: function (response) {
                    _this.ajax_get_list_order();
                }
            });
        }

        this.ajax_update_not_active_order = function (_id) {
            var post_data = {
                orderid: _id,
            };

            $('form.post-list input').val(JSON.stringify(post_data));

            var data = {
                action: 'update_not_active_order',
                data: JSON.parse($('form.post-list input').val())
            };

                $.ajax({
                    url: '/Home/UpdateNotActiveOrder',
                    type: 'POST',
                    data: data,
                    success: function (response) {
                        response = JSON.parse(response);
                        _this.ajax_get_list_order();
                    }
                });
        }

        this.ajax_update_order_detail = function (_orderid, _productid, _quantity) {
            var post_data = {
                orderid: _orderid,
                productid: _productid,
                quantity: _quantity,
            };

            $('form.post-list input').val(JSON.stringify(post_data));

            var data = {
                action: 'update_order_detail',
                data: JSON.parse($('form.post-list input').val())
            };

            $.ajax({
                url: '/Home/UpdateOrderDetail',
                type: 'POST',
                data: data,
                success: function (response) {
                    response = JSON.parse(response);
                }
            });
        }

        // Xoá đơn hàng
        this.ajax_delete_order = function (_orderid) {
           
            var post_data = {
                orderid: _orderid,
            };

            $('form.post-list input').val(JSON.stringify(post_data));

            var data = {
                action: 'delete_product_order_detail',
                data: JSON.parse($('form.post-list input').val())
            };

            $.ajax({
                url: '/Home/DeleteOrder',
                type: 'POST',
                data: data,
                success: function (response) {
                    response = JSON.parse(response);
                    _this.ajax_get_list_order();
                }
            });
        }

        // Xoá hàng hoá trong đơn hàng
        this.ajax_delete_product_in_order_detail = function (_orderid, _productid) {

            var post_data = {
                orderid: _orderid,
                productid: _productid,
            };

            $('form.post-list input').val(JSON.stringify(post_data));

            var data = {
                action: 'delete_order',
                data: JSON.parse($('form.post-list input').val())
            };

            $.ajax({
                url: '/Home/DeleteProductInOrderDetail',
                type: 'POST',
                data: data,
                success: function (response) {
                    response = JSON.parse(response);
                    _this.ajax_get_list_order();
                }
            });
        }

        // Thanh toán đơn hàng
        this.ajax_payment = function (_orderid) {

            var post_data = {
                orderid: _orderid,
            };

            $('form.post-list input').val(JSON.stringify(post_data));

            var data = {
                action: 'payment',
                data: JSON.parse($('form.post-list input').val())
            };

            $.ajax({
                url: '/Home/Payment',
                type: 'POST',
                data: data,
                success: function (response) {
                    response = JSON.parse(response);
                    alert("Đã thanh toán đơn hàng");
                    _this.ajax_get_list_order();
                    _this.ajax_get_products($('.link-selected').attr('id')); /* Load Posts class methods */
                }
            });
        }

        this.calculator = function (_orderid, _productid, _quantity) {
            var price = _this.FormatStringToNumber($(["p#cart_price", _orderid, _productid].join('-')).text());
            var quantity = Number($('#' + ["quantity", _orderid, _productid].join('-')).val());
           
            var money = _this.formatMoney(price * quantity, 0, "");
            $(["p#cart_total_price", _orderid, _productid].join('-') + ".cart_total_price").text(money);
        }

        this.calculatorTotal = function (_beforTotalOrderMoney, _beforeTtMoney, _afterTtMoney) {
            return total = _this.formatMoney(Number(_beforTotalOrderMoney) + (_afterTtMoney - _beforeTtMoney), 0, "");
        }

        this.FormatStringToNumber = function (_str) {
            return newString = parseFloat(_str.replace(/[^0-9.]/g, ""));
        }

        // Định dạng kiểu tiền
        this.formatMoney = function (number, places, symbol, thousand, decimal) {
            number = number || 0;
            places = !isNaN(places = Math.abs(places)) ? places : 2;
            symbol = symbol !== undefined ? symbol : "$";
            thousand = thousand || ",";
            decimal = decimal || ".";
            var negative = number < 0 ? "-" : "",
                i = parseInt(number = Math.abs(+number || 0).toFixed(places), 10) + "",
                j = (j = i.length) > 3 ? j % 3 : 0;
            return symbol + negative + (j ? i.substr(0, j) + thousand : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand) + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "");
        }

    }
}
