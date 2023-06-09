/*
Masked Input plugin for jQuery
Copyright (c) 2007-2011 Josh Bush (digitalbush.com)
Licensed under the MIT license (http://digitalbush.com/projects/masked-input-plugin/#license) 
Version: 1.3
*/
//===============================================================================
// F.P Line-up
// Ksc.Web.UI.WebControls20
// Copyright(C) 2012 System Consultant Co., Ltd. All Rights Reserved.
//===============================================================================
// jQury modified for FPValidator series 
//
//【修正履歴】
//   ・2011/10/01 新規作成
//
(function ($) {
    var pasteEventName = ($.browser.msie ? 'paste' : 'input') + ".fpmask";
    var iPhone = (window.orientation != undefined);

    $.fpmask = {
        //Predefined character definitions
        definitions: {
            '9': "[0-9]",
            'a': "[A-Za-z]",
            '*': "[A-Za-z0-9]"
        },
        dataName: "rawMaskFn"
    };

    $.fn.extend({
        //Helper Function for Caret positioning
        fpcaret: function (begin, end) {
            if (this.length == 0) return;
            if (typeof begin == 'number') {
                end = (typeof end == 'number') ? end : begin;
                return this.each(function () {
                    if (this.setSelectionRange) {
                        this.setSelectionRange(begin, end);
                    } else if (this.createTextRange) {
                        var range = this.createTextRange();
                        range.collapse(true);
                        range.moveEnd('character', end);
                        range.moveStart('character', begin);
                        range.select();
                    }
                });
            } else {
                if (this[0].setSelectionRange) {
                    begin = this[0].selectionStart;
                    end = this[0].selectionEnd;
                } else if (document.selection && document.selection.createRange) {
                    var range = document.selection.createRange();
                    begin = 0 - range.duplicate().moveStart('character', -100000);
                    end = begin + range.text.length;
                }
                return { begin: begin, end: end };
            }
        },
        fpunmask: function () { return this.trigger("fpunmask"); },
        fpmask: function (mask, settings) {
            if (!mask && this.length > 0) {
                var input = $(this[0]);
                return input.data($.fpmask.dataName)();
            }
            settings = $.extend({
                placeholder: "_",
                clearOnError: false,
                completed: null
            }, settings);

            var defs = $.fpmask.definitions;
            var tests = [];
            var partialPosition = mask.length;
            var firstNonMaskPos = null;
            var len = mask.length;

            $.each(mask.split(""), function (i, c) {
                if (c == '?') {
                    len--;
                    partialPosition = i;
                } else if (defs[c]) {
                    tests.push(new RegExp(defs[c]));
                    if (firstNonMaskPos == null)
                        firstNonMaskPos = tests.length - 1;
                } else {
                    tests.push(null);
                }
            });

            return this.trigger("fpunmask").each(function () {
                var input = $(this);
                var buffer = $.map(mask.split(""), function (c, i) { if (c != '?') return defs[c] ? settings.placeholder : c });
                var focusText = input.val();

                function seekNext(pos) {
                    while (++pos <= len && !tests[pos]);
                    return pos;
                };
                function seekPrev(pos) {
                    while (--pos >= 0 && !tests[pos]);
                    return pos;
                };

                function shiftL(begin, end) {
                    if (begin < 0)
                        return;
                    for (var i = begin, j = seekNext(end) ; i < len; i++) {
                        if (tests[i]) {
                            if (j < len && tests[i].test(buffer[j])) {
                                buffer[i] = buffer[j];
                                buffer[j] = settings.placeholder;
                            } else
                                break;
                            j = seekNext(j);
                        }
                    }
                    writeBuffer();
                    input.fpcaret(Math.max(firstNonMaskPos, begin));
                };

                function shiftR(pos) {
                    for (var i = pos, c = settings.placeholder; i < len; i++) {
                        if (tests[i]) {
                            var j = seekNext(i);
                            var t = buffer[i];
                            buffer[i] = c;
                            if (j < len && tests[j].test(t))
                                c = t;
                            else
                                break;
                        }
                    }
                };

                function keydownEvent(e) {
                    var k = e.which;

                    //backspace, delete, and escape get special treatment
                    if (k == 8 || k == 46 || (iPhone && k == 127)) {
                        var pos = input.fpcaret(),
							begin = pos.begin,
							end = pos.end;

                        if (end - begin == 0) {
                            begin = k != 46 ? seekPrev(begin) : (end = seekNext(begin - 1));
                            end = k == 46 ? seekNext(end) : end;
                        }
                        clearBuffer(begin, end);
                        shiftL(begin, end - 1);

                        return false;
                    } else if (k == 27) {//escape
                        input.val(focusText);
                        input.fpcaret(0, checkVal());
                        return false;
                    }
                };

                function keypressEvent(e) {
                    var k = e.which,
						pos = input.fpcaret();
                    if (e.ctrlKey || e.altKey || e.metaKey || k < 32) {//Ignore
                        return true;
                    } else if (k) {
                        if (pos.end - pos.begin != 0) {
                            clearBuffer(pos.begin, pos.end);
                            shiftL(pos.begin, pos.end - 1);
                        }

                        var p = seekNext(pos.begin - 1);
                        if (p < len) {
                            var c = String.fromCharCode(k);
                            if (tests[p].test(c)) {
                                shiftR(p);
                                buffer[p] = c;
                                writeBuffer();
                                var next = seekNext(p);
                                input.fpcaret(next);
                                if (settings.completed && next >= len)
                                    settings.completed.call(input);
                            }
                        }
                        return false;
                    }
                };

                function clearBuffer(start, end) {
                    for (var i = start; i < end && i < len; i++) {
                        if (tests[i])
                            buffer[i] = settings.placeholder;
                    }
                };
                // 2011/10/12 add kusu
                function getClearBuffer(start, end) {
                    var buff = [];
                    var arrMask = mask.split("");
                    for (var i = start; i < end && i < len; i++) {
                        buff[i] = (tests[i]) ? settings.placeholder : (arrMask[i]);
                    }
                    return (buff.join(''));
                };
                function notEnterdAnything() {
                    var value = input.val();
                    return (value == "" || value == getClearBuffer(0, mask.length));
                }
                //---

                function writeBuffer() { return input.val(buffer.join('')).val(); };

                function checkVal(allow) {
                    //try to place characters where they belong
                    var test = input.val();
                    var lastMatch = -1;
                    for (var i = 0, pos = 0; i < len; i++) {
                        if (tests[i]) {
                            buffer[i] = settings.placeholder;
                            while (pos++ < test.length) {
                                var c = test.charAt(pos - 1);
                                if (tests[i].test(c)) {
                                    buffer[i] = c;
                                    lastMatch = i;
                                    break;
                                }
                            }
                            if (pos > test.length)
                                break;
                        } else if (buffer[i] == test.charAt(pos) && i != partialPosition) {
                            pos++;
                            lastMatch = i;
                        }
                    }
                    if (!allow && lastMatch + 1 < partialPosition) {
                        // 2011/10/11 settings.clearOnError追加
                        // クリア指定時、または入力なしの時はクリア
                        if (settings.clearOnError == true || notEnterdAnything()) {
                            input.val("");
                            clearBuffer(0, len);
                        }
                        //input.val("");
                        //clearBuffer(0, len);
                        //---
                    } else if (allow || lastMatch + 1 >= partialPosition) {
                        writeBuffer();
                        if (!allow) input.val(input.val().substring(0, lastMatch + 1));
                    }
                    return (partialPosition ? i : firstNonMaskPos);
                };

                input.data($.fpmask.dataName, function () {
                    return $.map(buffer, function (c, i) {
                        return tests[i] && c != settings.placeholder ? c : null;
                    }).join('');
                })

                if (!input.attr("readonly"))
                    input
					.one("fpunmask", function () {
					    input
							.unbind(".fpmask")
							.removeData($.fpmask.dataName);
					})
					.bind("focus.fpmask", function () {
					    focusText = input.val();
					    var pos = checkVal();
					    writeBuffer();
					    var moveCaret = function () {
					        if (pos == mask.length)
					            input.fpcaret(0, pos);
					        else
					            input.fpcaret(pos);
					    };
					    ($.browser.msie ? moveCaret : function () { setTimeout(moveCaret, 0) })();
					})
					.bind("blur.fpmask", function () {
					    // 2011/10/11 settings.clearOnError追加
					    // クリア指定時、または入力なしの時はクリア
					    if (settings.clearOnError == true || notEnterdAnything()) {
					        input.val("");
					        clearBuffer(0, len);
					        return;
					    }
					    //checkVal(); //<-----Loop???
					    //---
					    if (input.val() != focusText)
					        input.change();
					})
					.bind("keydown.fpmask", keydownEvent)
					.bind("keypress.fpmask", keypressEvent)
					.bind(pasteEventName, function () {
					    setTimeout(function () { input.fpcaret(checkVal(true)); }, 0);
					});

                checkVal(); //Perform initial check for existing values
            });
        }
    });
})(jQuery);
