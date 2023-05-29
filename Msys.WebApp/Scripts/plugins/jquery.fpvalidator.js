//------------------------------------------------
// Copyright(C) 2012 System Consultant Co., Ltd. All Rights Reserved.
// jquery.fpvalidator.js
//------------------------------------------------
jQuery.fn.extend({
    fpvalidator: {
        // MaxLengthチェック
        isMaxLengthOver: function (value, maxLength, checkMode) {
            var length = (checkMode == "1") ? $.fn.fpcore.getByte(value) : value.length;
            return (length <= maxLength);
        },
        // 日付取得(カレンダー日付でない時はnullを返す)
        getDate: function (year, month, day) {
           var date = new Date(year, month - 1, day);
            if ((date.getFullYear() == year) && (date.getMonth() == month - 1) &&
                                                (date.getDate() == day)) {
                return date;
            } else {
                return null;
            }
        },
        // 日付チェック
        isDate: function (year, month, day) {
            //2013-01-09 terada update start
            //SQLAzureのDate型のチェックをクライアント側で実装する
            //※ 1900-01-01 ～ 2100-12-31 の範囲を許可する
            if ($.fn.fpvalidator.getDate(year, month, day) == null) {
                return false;
            }
            if (year < 1900 || year > 2100) {
                return false;
            }
            return true;
            //return ($.fn.fpvalidator.getDate(year, month, day) != null);
            //2013-01-09 terada update end
        },
        // 時刻チェック
        isTime: function (hour, minute, second) {
            if (isNaN(hour) || isNaN(minute) || isNaN(second)) {
                return false;
            }
            if (Number(hour) >= 0 && Number(hour) <= 23 &&
                Number(minute) >= 0 && Number(minute) <= 59 &&
                Number(second) >= 0 && Number(second) <= 59) {
                return true;
            }
            return false;
        },
        // エラー時Focusセット
        setFocus: function (elementJ) {
            if (elementJ.is(":focusable") && elementJ.length > 0) {
                elementJ[0].focus();
                try { elementJ[0].select(); } catch (e) { }
            }
        },
        // value取得(MultipleのListBoxでは先頭のSelect値)
        getValue: function (elementJ, type) {
            var value = "";
            var selector = "";
            switch (type) {
                case "TextBox":
                case "DropDownList":
                case "HiddenField":
                    value = elementJ.val();
                    break;
                case "ListBox":
                    selector = "option:selected";
                    break;
                case "RadioButtonList":
                    selector = "input[type=radio]:checked";
                    break;
                case "CheckBoxList":
                    selector = "input[type=checkbox]:checked";
                    break;
            }
            if (selector != "") {
                if (elementJ.find(selector).length > 0) {
                    value = elementJ.find(selector).val();
                }
            }
            return value;
        }
    }
});
