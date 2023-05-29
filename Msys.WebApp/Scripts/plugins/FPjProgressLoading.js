//===============================================================================
// F.P Line-up
// Ksc.Web.UI.WebControls20
// Copyright (c) Ksc-Team Falcon Peregrinus.  All rights reserved.
//===============================================================================
// jQury for fpjprogressloadingExtender 
(function ($) {
    // init fpjprogressloading
    $.init_fpjprogressloading = function (divParent, property) {
        // Apply default properties
        property = $.extend({
            ImgLoadingId: "__masterImgLoading",
            ImgSpanLoadingId: "__masterSpanLoading",
            DelayTimer: 100,
            OnLoadingStartEventName: "OnLoadingStart",
            OnLoadingEndEventName: "OnLoadingEnd",

            // controls
            divParentJ: null,
            ImgLoading: null,
            ImgSpanLoading: null,
            postbackElement: null,

            dummy: ''    // end property
        }, property);

        var fpjProgressLoading = {
            //--------------------------------------------------
            // Property (server information)
            //--------------------------------------------------
            // Header Fixed
            get_DivParentJ: function () {
                return ((property.divParentJ.length > 0) ? property.divParentJ[0] : null);
            },
            //--------------------------------------------------
            // Public Method
            //--------------------------------------------------
            // Loadingイメージ表示開始 
            Start: function (postbackElement, disableSubmit) {
                $(document.body).css({ "cursor": "wait" });

                // Loadingイメージ表示
                var pos = this._getCenterPosition();
                property.divParentJ.show()
                                   .css({ "top": pos.top, "left": pos.left, "z-index": 99999 });

                // タイマーセット
                // ※Chrome,Safariはdisabledのコントロールからはイベントを受け取らないので
                //   postbackElement類のDisabledはタイマー(<click eventを抜けた後)でセット
                //   する必要あり。

//                setTimeout(function () {
//                    // 連打防止のためボタン類をdisabled設定
//                    // ※submitボタンは全てUpdatePanelで自動更新されることが前提です。
//                    //   ("disabled"を戻すロジックはありません。元の状態をsaveするのが煩雑なので) 
//                    if (disableSubmit) {
//                        $(document.body).find(":submit").attr("disabled", true);
//                    }
//                    property.postbackElement = $(postbackElement);
//                    if (property.postbackElement != null) {
//                        property.postbackElement.attr("disabled", true);
//                    }
//                }, property.DelayTimer);


                property.divParentJ.oneTime(property.DelayTimer, function () {
                    // 連打防止のためボタン類をdisabled設定
                    // ※submitボタンは全てUpdatePanelで自動更新されることが前提です。
                    //   ("disabled"を戻すロジックはありません。元の状態をsaveするのが煩雑なので) 
                    if (disableSubmit) {
                        $(document.body).find(":submit").attr("disabled", true);
                    }
                    property.postbackElement = $(postbackElement);
                    if (property.postbackElement != null) {
                        property.postbackElement.attr("disabled", true);
                    }
                });

                return;
            },
            // Loadingイメージ表示終了 
            End: function () {
                $(document.body).css({ "cursor": "auto" });
                if (property.postbackElement != null) {
                    property.postbackElement.attr("disabled", false);
                    property.postbackElement = null;
                }
                property.divParentJ.hide();
                return;
            },
            // _getCenterPosition 
            _getCenterPosition: function () {
                var pos = {};
                pos.left = Math.floor(($(window).width() - property.divParentJ.width()) / 2);
                pos.top = Math.floor(($(window).height() - property.divParentJ.height()) / 2);
                // 2013/03/08 add kusu for SCF
                pos.top += $(document).scrollTop();
                return (pos);
            },
            //--------------------------------------------------
            // Private Method
            //--------------------------------------------------
            // _initControls(set controls to property)
            _initControls: function () {
                property.divParentJ = $(divParent);
                property.ImgLoading = $('#' + property.ImgLoadingId);
                property.ImgSpanLoading = $('#' + property.ImgSpanLoadingId);

                property.divParentJ.hide();
                return;
            },
            //----------------------------------------------
            // _prepareProgressLoading
            //----------------------------------------------
            _prepareProgressLoading: function () {
                //alert("prepare jQuery ProgressLoading!");

                // initialize controls to property
                this._initControls();

                return;
            }
        };
        fpjProgressLoading._prepareProgressLoading();
        divParent.fpjProgressLoading = fpjProgressLoading;
    };

    var _fpjprogressloading_docloaded = false;
    $(document).ready(function () {
        _fpjprogressloading_docloaded = true;
    });

    // fpjprogressloading start
    $.fn.fpjprogressloading = function (property) {
        return this.each(function () {
            if (!_fpjprogressloading_docloaded) {
                var divParent = this;
                $(document).ready(function () {
                    $.init_fpjprogressloading(divParent, property);
                });
            } else {
                $.init_fpjprogressloading(this, property);
            }
        });
    }; //fpjprogressloading end

})(jQuery);
