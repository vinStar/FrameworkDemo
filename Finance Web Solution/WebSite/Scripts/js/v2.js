var Public = Public || {};
//快捷键
Public.keyCode = {
    ALT: 18,
    BACKSPACE: 8,
    CAPS_LOCK: 20,
    COMMA: 188,
    COMMAND: 91,
    COMMAND_LEFT: 91, // COMMAND
    COMMAND_RIGHT: 93,
    CONTROL: 17,
    DELETE: 46,
    DOWN: 40,
    END: 35,
    ENTER: 13,
    ESCAPE: 27,
    HOME: 36,
    INSERT: 45,
    LEFT: 37,
    MENU: 93, // COMMAND_RIGHT
    NUMPAD_ADD: 107,
    NUMPAD_DECIMAL: 110,
    NUMPAD_DIVIDE: 111,
    NUMPAD_ENTER: 108,
    NUMPAD_MULTIPLY: 106,
    NUMPAD_SUBTRACT: 109,
    PAGE_DOWN: 34,
    PAGE_UP: 33,
    PERIOD: 190,
    RIGHT: 39,
    SHIFT: 16,
    SPACE: 32,
    TAB: 9,
    UP: 38,
    F7: 118,
    F12: 123,
    S: 83,
    WINDOWS: 91 // COMMAND
}





var keyCode = Public.keyCode,
ENTER = keyCode.ENTER,
TAB = keyCode.TAB,
F7 = keyCode.F7,
SPACE = keyCode.SPACE,
F12 = keyCode.F12,
KEY_S = keyCode.S,
ESC = keyCode.ESCAPE;
var SUBJECT_DATA = [], SUB_SUBJECT_DATA = [];


getSubject();



function getSubject() {
    //$.getJSON('js/json4.json',
    $.getJSON('/Fund/VoucherOperate/JsonSubject',
    function (data) {
        if (data) {
            SUBJECT_DATA = data;
            SUB_SUBJECT_DATA = [];
            $.each(SUBJECT_DATA,
            function (i, n) {
                //if (n.detail) {
                SUB_SUBJECT_DATA.push(n);
                // }
            });
        }
    });
}


//科目只取手续费
function getCashierSubject() {
    $.getJSON('/Fund/VoucherOperate/JsonSubject',
 function (data) {
     var ss = new Array();
     var index = 0;
     $.each(data, function (i) {
         if (data[i].ID == "13_98" || data[i].ID == "4_7" || data[i].ID == "5_6" || data[i].ID == "14_157" || data[i].ID == "14_158") {
             ss[index] = data[i];
             index = index + 1;
         }
     });
     if (ss) {
         SUBJECT_DATA = ss;
         SUB_SUBJECT_DATA = [];
         $.each(SUBJECT_DATA,
         function (i, n) {
             //if (n.detail) {
             SUB_SUBJECT_DATA.push(n);
             // }
         });
     }
 });
}







var Voucher = Voucher || {};  //凭证对象
Voucher.items = {},	//辅助核算
Voucher.wrapper = $('.wrapper');
Voucher.colTotal = $('.col_total');
/**
    *初始化事件
*/
Voucher.initEvent = function () {
    Voucher.activateEdit();


    /*
    
    //科目弹窗
    $('#voucher').on('click', '.selSub', function(e){
        Business.subjectTreePop($(this), function(data,target){
            target = target.parents("tr");
            target.find('.col_subject').trigger('click');
            Voucher.subjectFeild.getCombo().selectByValue(data.id);
            Voucher._focusJump(target, data, false);
        }, true);
    });
    
    */



    //点击页面其他地方关闭编辑
    $(document).on('click', function (e) {
        var target = e.target;
        //点击非录入区域时，关闭编辑状态
        if (Voucher.balancePop && Voucher.balancePop.is(':visible')) {

            Voucher.balancePop.hide().find('.check-balance').html('查询中...');
        }
        if ($(target).closest('#voucher td[data-edit]').length == 0) {
            if ($(target).closest('#isItem').length == 0) {
                Voucher.cancelEdit();
            }
        }
    });

}

/**
    *激活编辑事件
*/
Voucher.activateEdit = function () {
    //点击编辑
    $('#voucher').on('click', '.entry_item td[data-edit]', function (e) {
        Voucher.editCell($(this));
    });

    //显示划过状态
    $('#voucher').on('mouseover', 'tbody .entry_item', function (e) {
        $(this).addClass("current").siblings().removeClass("current");
    });
    $('#voucher').on('mouseleave', 'tbody .entry_item', function (e) {
        $(this).removeClass("current");
    });
}


/**
    *取消编辑事件
*/
Voucher.deactivateEdit = function () {
    $('#voucher').off('click', 'td[data-edit]');
    $('#voucher').off('mouseover', 'tbody .entry_item');
    $('#voucher').off('mouseleave', 'tbody .entry_item');

    Voucher.cancelEdit();
}


/**
    *编辑单元格
*/
Voucher.editCell = function (cell) {
    if (cell.data('onEdit')) { return; }
    if (Voucher.curEditCell) {
        if (!Voucher.cancelEdit()) {
            return false;
        }
    };
    Voucher.curEditCell = cell.data('onEdit', true);
    var editType = cell.attr('data-edit');
    var valObj = cell.find('.cell_val').hide();
    var curVal, subjectInfo;
    if (!editType) { return; }
    switch (editType) {
        case 'summary':
            Voucher.curEditField = $('#voucher .edit_summary');
            if (Voucher.curEditField.length == 0) {
                Voucher.createSummaryFeild()
                Voucher.curEditField = Voucher.summaryFeild;
            }
            curVal = valObj.text();
            break;
        case 'subject':
            Voucher.curEditField = $('#voucher .edit_subject');
            if (Voucher.curEditField.length == 0) {
                Voucher.createSubjectFeild()
                Voucher.curEditField = Voucher.subjectFeild;
            };
            subjectInfo = valObj.data('subjectInfo');
            if (subjectInfo) {
                curVal = subjectInfo.ID + ' ' + subjectInfo.Subjectname;
            } else {
                curVal = '';
            }
            break;
        case 'money':
            Voucher.curEditField = $('#voucher .edit_money');
            if (Voucher.curEditField.length == 0) {
                Voucher.createMoneyFeild()
                Voucher.curEditField = Voucher.moneyFeild;
            }
            curVal = valObj.attr('data-val');
            if (curVal == 0) {
                curVal = '';
            }
            break;
    }
    Voucher.curEditField.insertAfter(valObj).show();//.select().focus();

    if (editType == 'subject') {
        //Voucher.curEditField.getCombo().selectByText(curVal, false);
        Voucher.curEditField.getCombo().selectByText(curVal, true);
    } else {
        Voucher.curEditField.val(curVal);
    }

    window.setTimeout(function () {
        if (Voucher.curEditField.val()) {
            Voucher.curEditField.select();
        } else {
            Voucher.curEditField.select().focus();
        }
    }, 0);
}


/**
    *取消编辑
*/
Voucher.cancelEdit = function () {
    if (!Voucher.curEditCell) { return; }
    var cell = Voucher.curEditCell, val = realValue = Voucher.curEditField.val(), valObj = cell.find('.cell_val'),
    itemsData = valObj.data('itemsData'), subjectInfo = valObj.data('subjectInfo');
    if (cell.data('edit') == 'money') {
        val = parseFloat(val);
        if (isNaN(val)) {
            val = 0;
        } else if (Math.abs(val) >= 1000000000) {
            val = 0;
            //Public.tips({type:2, content : '只能输入10亿以下的金额！'});
        }
        Voucher.showAmout(valObj, val);
        Voucher.calTotalAmount();
    } else if (cell.data('edit') == 'subject') {

        if ($('#voucher .edit_subject').length > 0) {
            val = $('#voucher .edit_subject').getCombo().getText();
            if (subjectInfo && subjectInfo.isItem) {
                val += '<span>_';
                var itemArr = ["itemKH", "itemGYS", "itemZY", "itemXM", "itemBM", "itemCH", "itemZDY", "itemSFXD"];
                for (var i = 0, len = itemArr.length; i < len; i++) {
                    var itemData = itemsData[itemArr[i]];
                    if (itemData) {
                        var number = itemData.number || '';
                        val += number + '' + itemData.name + '_';
                    }
                }
                val = val.slice(0, val.length - 1);
                val += '</span>'
            }
        } else {
            val = '';
        };
        valObj.html(val);
    } else {
        valObj.text(val);
    }
    valObj.show();
    Voucher.curEditField.hide();
    cell.data('onEdit', false);
    Voucher.curEditCell = null;
    return true;
}




/**
    *摘要弹窗
*/

/**
    *生成摘要文本域
*/
Voucher.createSummaryFeild = function () {
    var tr = $("#voucher tbody tr.entry_item");
    Voucher.summaryFeild = $('<input type="text" class="edit_summary" autocomplete="off" />').on('keydown', function (e) {
        var _self = $(this);
        var keyValue = e.which, parentTr = _self.parents('tr.entry_item'), lastTr = parentTr.prev();
        if (e.shiftKey && (keyValue == ENTER || keyValue == TAB)) {
            if (lastTr.length === 0) {
                $('#vch_date').focus();
            } else {
                Voucher.setAmountFocus(lastTr);
            }
            return false;
        }

        switch (keyValue) {
            case ENTER:
            case TAB:
                e.preventDefault();
                parentTr.find('.col_subject').trigger('click');
                break;

        };
        //TODO .. //快捷键
    }).on('keyup', function () {
        var parentTr = $(this).parents('tr.entry_item'), lastTr = parentTr.prev();
        var curValue = $(this).val(), curIndex = tr.index(parentTr);
        if (lastTr.length === 0) return;
        if (curValue === "//") {// 快捷键“//”复制第一条分录摘要
            var firstTr = $('tr.entry_item').eq(0);
            $(this).val(firstTr.find(".summary_val").html());
        } else if (curValue === "..") {// 快捷键“..”复制上一条分录摘要                    
            var prevSummary = lastTr.find(".summary_val").html();
            $(this).val(prevSummary);
        }
    });
}
/**
    *生成科目编辑域
*/
Voucher.createSubjectFeild = function () {
    Voucher.subjectFeild = $('<input type="text" class="edit_subject" autocomplete="off" />').combo({
        data: function () {
            return SUB_SUBJECT_DATA;
        },
        formatText: function (data) {
            return data.ID + ' ' + data.subjectName;
        },
        value: 'id',
        defaultSelected: -1,
        editable: true,
        customMatch: function (text, query) {
            query = query.toLowerCase().replace(/[-[\]{}()*+?.,\\^$|#\s]/g, '\\$&');
            var idx = text.toLowerCase().search(query);
            if (/^\d+$/.test(query)) {
                if (idx == 0) { return true; }
            } else {
                if (idx != -1) { return true; }
            }
            return false;
        },
        //extraListHtml: '<a href="javascript:void(0);" id="quickAddSubject" class="quick-add-link"><i class="ui-icon-add"></i>新增科目</a>',
        maxListWidth: 500,
        cache: false,
        forceSelection: true,
        maxFilter: 10,
        trigger: false,
        listHeight: 182,
        listWrapCls: 'ui-droplist-wrap ui-subjectList-wrap',
        callback: {
            onChange: function (data) {
                var parentTr = this.input.parents('tr.entry_item'), balance = parentTr.find(".balance");

                /*					if((Voucher.tempSubject == $('tr.entry_item').index(parentTr) && this.getSelectedIndex() !== -1) || this.getSelectedIndex() !== -1) {
                                        Voucher.setAmountFocus(parentTr, data);
                                        //Voucher._focusJump(this.input, data);
                                    }*/
                //if(this.getSelectedIndex() === -1){
                this.input.prev('.cell_val').removeData('itemsData');

                $('#isItem').hide();
                parentTr.find('.quantity_val').html('');
                parentTr.find('.curr_val').html('');
                //}
                parentTr.find('.balance-pop').remove();
                parentTr.find('.option:eq(1)').removeClass('show');
                if (data) {
                    if (balance.length > 0) {

                        balance.attr("data-number", data.number);
                    } else {
                        //parentTr.find(".option:eq(1)").append('<a class="balance" data-id =' + data.id +' data-number =' + data.number +' data-cur =' + data.cur + ' >余额</a>');
                    }
                } else {
                    balance.remove();
                };

                // $('#isItem').hide();
                this.input.prev('.cell_val').data('subjectInfo', data);
            },
            onListClick: function () {

                var parentTr = this.input.parents('tr.entry_item');
                var subjectInfo = this.input.prev().data("subjectInfo");
                //Voucher._focusJump(parentTr, subjectInfo);
                Voucher.cancelEdit();
            }
        },
        queryDelay: 0,
        inputCls: 'edit_subject',
        wrapCls: 'edit_subject_wrap',
        focusCls: '',
        disabledCls: '',
        activeCls: ''
    }).bind('keydown', function (e) {
        var keyValue = e.which, parentTr = $(this).parents('tr.entry_item');
        if (e.shiftKey && (keyValue === ENTER || keyValue === TAB)) {
            parentTr.find('.col_summary').trigger('click');
            return false;
        }

        switch (keyValue) {
            case ENTER:
            case TAB:
                e.preventDefault();
                var subjectInfo = $(this).prev().data("subjectInfo");
                Voucher._focusJump(parentTr, subjectInfo);
                break;
            case F7:
                e.preventDefault();
                Business.subjectTreePop($(this), function (data, target) {
                    target.getCombo().selectByValue(data.id);
                }, true);
                break;
            case SPACE: return false;
            default: break;
        }
    }).bind('focus', function (e) {
        var $this = $(this);
        var subjectInfo = $this.prev().data("subjectInfo");
        window.setTimeout(function () {
            if (!subjectInfo && !$this.getCombo().isExpanded) {
                $this.getCombo().doQuery('');
            } else if (subjectInfo) {
                Voucher.originalSubject = subjectInfo.number;
                if (subjectInfo.isItem && !$('#isItem').is(':visible')) {
                    Voucher.createSubjectItem($this, subjectInfo);
                }
            } else {
                Voucher.originalSubject = '';
            };
        }, 0);
    });


};

function fun(obj) {

    var vouType = $(obj).parent().attr("vouType");
    if (vouType != null && vouType != "" && vouType == "1") {
        var SourceCode = $(obj).parent().parent().parent().parent().parent().find("#hid_SourceCode").val();
        if (SourceCode == "" || SourceCode == "0") {
            var subject = $(obj).parent().children()[0].innerText;
            var subjectA = "";
            var subjectB = "";
            if (subject != null && subject != "") {
                if (subject.indexOf('_') > 0) {
                    subjectA = subject.split(' ')[0].split('_')[0]; ////一级科目ID
                    subjectB = subject.split(' ')[0].split('_')[1]; ////二级科目ID
                }
            }

            if (subjectA != "" && subjectA == "14" && subjectB != "" && subjectB == "149") {
                $(obj).parent().parent().parent().parent().parent().find("#di_payee").hide();
            } else {
                var arrysub = $(obj).parent().parent().parent().children().find(".col_subject").find(".subject_val");
                var len = $(obj).parent().parent().parent().children().find(".col_subject").find(".subject_val").length;
                var subA = "";
                var subB = "";
                var isFase = 0;
                for (var i = 0; i < len; i++) {
                    var sub = arrysub[i].innerText;
                    if (sub != null && sub != "") {
                        if (sub.indexOf('_') > 0) {
                            subA = sub.split(' ')[0].split('_')[0]; ////一级科目ID
                            subB = sub.split(' ')[0].split('_')[1]; ////二级科目ID
                        }
                    }
                    if (subA != "" && subA == "14" && subB != "" && subB == "149") {
                        isFase = 1;
                    }
                }
                if (isFase == 1) {
                    $(obj).parent().parent().parent().parent().parent().find("#di_payee").hide();
                } else {
                    $(obj).parent().parent().parent().parent().parent().find("#di_payee").show();
                }

            }
        }
    }
}





/**
    *生成金额输入域
*/
Voucher.createMoneyFeild = function () {
    Voucher.moneyFeild = $('<input type="text" class="edit_money" autocomplete="off" />').numberField({
        decimal: true,
        max: 999999999.99,
        keyEnable: false
    }).bind('keydown', function (e) {
        var keyValue = e.which, value = $(this).val(), parentTd = $(this).parent(),

        parentTr = $(this).parents('tr.entry_item'), nextTr = parentTr.next();
        //$(this).prev().attr("id","money"+parentTr.index()+parentTd.index());
        if (e.shiftKey && (keyValue === ENTER || keyValue === TAB)) {
            parentTr.find('.col_subject').trigger('click');
            return false;
        }

        if (parentTd.hasClass('col_debite')) {
            parentTr.find('.credit_val').attr('data-val', 0).text('');
        } else {
            parentTr.find('.debit_val').attr('data-val', 0).text('');
        }

        switch (keyValue) {
            case ENTER:
            case TAB:
                e.preventDefault();
                if ($(this).val() != '') {
                    if (nextTr.length == 0) {
                        Voucher.addEntry();
                        nextTr = parentTr.next();
                    };
                    if (Math.abs($(this).val()) >= 1000000000) {
                        //Public.tips({type:2, content : '只能输入10亿以下的金额！'});
                        $(this).select();
                        return;
                    }
                    nextTr.find('.col_summary').trigger('click');
                } else {
                    if (parentTd.hasClass('col_debite')) {
                        parentTd.next().trigger('click');
                    };
                }
                return false;
                break;

            case SPACE:
                var targetTd;
                if (parentTd.hasClass('col_debite')) {
                    targetTd = parentTr.find('.col_credit');
                } else {
                    targetTd = parentTr.find('.col_debite');
                }
                $(this).val('');
                targetTd.find('.cell_val').text(value).attr('data-val', value);
                targetTd.trigger('click');
                return false;
                break;
        }
    }).bind('keyup', function (e) {
        var equalCode = $.browser.opera ? 107 : $.browser.mozilla ? 61 : 187;
        if (e.keyCode === 187 || e.which === 61 || e.keyCode === 107) {
            Voucher.setBalanceValue($(this));
        }
    }).bind('focus', function () {
        Voucher.originalAmount = $(this).val() === '' ? 0 : Number($(this).val()).toFixed(2);
    }).bind('blur', function () {
        // console.log($("#div_debit_money1").data("realValue"));
        //  console.log("data-val:"+$("#div_debit_money1").attr("data-val"));

        var currAmount = $(this).val() === '' ? 0 : Number($(this).val()).toFixed(2);
        if (!currAmount) {
            return;
        }
        //值更改反算处理
        if (Voucher.originalAmount !== currAmount) {
            var parentTr = $(this).parents('tr.entry_item');
            var subjectVal = parentTr.find('.subject_val').data('subjectInfo');
            if (subjectVal) {
                var hasQtyaux = subjectVal.isQtyaux;
                var hasCurr = subjectVal.isCur;

                if (hasCurr) {
                    var originalVal = Number(parentTr.find('.original').val());
                    if (originalVal) {
                        parentTr.find('.rate').val(Math.abs((currAmount / originalVal)).toFixed(3) * 1);
                        //确保原币与金额符号相同
                        if ((currAmount < 0 && originalVal > 0) || (currAmount > 0 && originalVal < 0)) {
                            parentTr.find('.original').val(originalVal * -1);
                        };
                        return;
                    }
                }
                if (hasQtyaux) {
                    var quantityVal = Number(parentTr.find('.quantity').val());
                    if (quantityVal) {
                        parentTr.find('.unit-price').val((currAmount / quantityVal).toFixed(3) * 1);
                        return;
                    }
                }
            }
        }
    });

    //Voucher.moneyFeild.calculator({callback:function(){ }});
}

/**
    *试算金额平衡
*/
Voucher.setBalanceValue = function (input) {
    input = $(input);
    input.prev().attr('data-val', 0).text('');//清零原来的值
    var totalDebit = Voucher.getTotalAccountAmount('debit');
    var totalCredit = Voucher.getTotalAccountAmount('credit');
    var balanceValue = totalDebit - totalCredit;
    if (input.prev().hasClass('debit_val')) {
        balanceValue = totalCredit - totalDebit;
    }
    input.val(balanceValue.toFixed(2));
}

/**
    *设置金额的显示值和正负样式
*/
Voucher.showAmout = function (obj, amount) {
    obj = $(obj);
    amount = parseFloat(amount);
    if (isNaN(amount)) {
        return;
    }
    amount = amount.toFixed(2);
    obj.attr('data-val', amount);
    amount = Math.round(amount * 100);
    if (amount >= 0) {
        obj.removeClass('money-negative');
    } else {
        amount = -amount;
        obj.addClass('money-negative');
    }
    obj.text(amount || '');
}

/**
    *计算合计金额
*/
Voucher.calTotalAmount = function () {
    var totalDebit = Voucher.getTotalAccountAmount('debit');
    var totalCredit = Voucher.getTotalAccountAmount('credit');
    Voucher.showAmout($('#debit_total'), totalDebit);
    Voucher.showAmout($('#credit_total'), totalCredit);
    if (totalDebit == totalCredit && totalDebit != 0) {
        var amountWords = Voucher.formatAmountToChinese(totalDebit);
        $('#capAmount').text(amountWords);
        if (totalDebit > 0) {
            $('#capAmount').removeClass('money-negative');
        } else {
            $('#capAmount').addClass('money-negative');
        };
    } else {
        $('#capAmount').text('');
    }
};


/**
    *将阿拉伯金额转换成中文金额
    @param {number string} example: 100
    @return {string} example: '一百元整'
*/
Voucher.formatAmountToChinese = function (amount) {
    amount = parseFloat(amount);
    if (isNaN(amount)) { return; }// || Math.abs(amount) > 99999999999.99
    amount = Math.round(amount * 100);
    var isInt = amount % 100 == 0 ? true : false;
    var numArr = ["零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖"];
    var unitArr = ["分", "角", "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟"];
    var resultStr = '', num, unitIdx, len, zeroCount = 0;
    if (amount == 0) {
        return '零元整';
    };
    if (amount < 0) {
        resultStr += '负';
        amount = -amount;
    }
    amount = amount.toString();
    len = amount.length;
    for (var i = 0; i < len ; i++) {
        num = parseInt(amount.charAt(i));
        unitIdx = len - 1 - i;
        if (num == 0) {
            //元 万 亿 输出单位
            if (unitIdx == 2 || unitIdx == 6 || unitIdx == 11) {
                resultStr += unitArr[unitIdx];
                zeroCount = 0;
            } else {
                zeroCount++;
            }
        } else {
            if (zeroCount > 0) {
                resultStr += '零';
                zeroCount = 0;
            };
            resultStr = resultStr + numArr[num] + unitArr[unitIdx];
        }
    };

    if (isInt) {
        resultStr += '整';
    };

    return resultStr;
};


/**
    *计算借方或贷方总数
    *@param ｛string｝ 'credit' or 'debit'
    *@return {number} 计算出的金额

*/
Voucher.getTotalAccountAmount = function (account) {
    var objs, total = 0, val;
    if (account == 'credit') {
        objs = $('#voucher .entry_item .credit_val');
    } else if (account == 'debit') {
        objs = $('#voucher .entry_item .debit_val');
    }
    $.each(objs, function (idx) {
        val = parseFloat($(this).attr('data-val')) * 100;
        if (isNaN(val)) { return; }
        total += val;
    });
    return total / 100;
};


/**
    * 设置录入金额焦点位置
    * @param	{JqueryElement}		当前所在行
    * @param	{object}	 所选科目数据
    * 规则：1.如果借方或者贷方金额不为空，则不为空的获取焦点；
    *	    2.如果两者为空，则依据科目信息获取焦点（科目默认为借方则借方获取焦点，科目默认为贷方则贷方获取焦点）
    *       3.如果科目信息为空，则默认借方获取焦点
*/
Voucher.setAmountFocus = function ($obj, data) {
    var debiteCol = $obj.find(".col_debite"), debiteAmount = debiteCol.find(".debit_val").text(),
    creditCol = $obj.find(".col_credit"), creditAmount = creditCol.find(".credit_val").text();
    if (debiteAmount !== "") {
        debiteCol.trigger("click");
        return;
    } else if (creditAmount !== "") {
        creditCol.trigger("click");
        return;
    };
    if (data == null) {
        debiteCol.trigger("click");
        return;
    }
    if ($(".entry_item").index($obj) === 0) {
        debiteCol.trigger("click");
        return;
    };
    if (data.dc == 1) {//借方
        debiteCol.trigger("click");
        return;
    } else if (data.dc == -1) {//贷方
        creditCol.trigger("click");
        return;
    };
};






Voucher.firstFocus = function () {
    $("#voucher td.col_summary").eq(0).trigger("click");
}




Voucher.initEvent();


