(function($){
	
	var css = {
				border: 'none',
				padding: '5px',
				backgroundColor: '#000',
				'-webkit-border-radius': '10px',
				'-moz-border-radius': '10px',
				opacity: .5,
				color: '#fff',
			};
	
	jQuery.extend({
		// 開啓遮罩
		showBlock : function(msg, m_css) { 
			msg = msg ? msg : '<h5>Loading...</h5>';
			jQuery.blockUI({
				message: msg,
				baseZ: 99999,
				css: m_css ? m_css : css
			})
		},
		// 關閉遮罩
		unBlock : function(){
			jQuery.unblockUI();
		},
		// Ajax 自動彈出遮罩
		autoBlock: function (msg, m_css) {
			msg = msg ? msg : '<h5>Loading...</h5>';
			jQuery(document).ajaxStart(jQuery.blockUI({
				message: msg,
				baseZ: 99999,
				css: m_css ? m_css : css
			})).ajaxStop(jQuery.unblockUI());
		},
	    // 驗證大於等於0的整数
		isIntMinZero: function (number) {
		    var reg = /^([1-9]\d*|[0]{1,1})$/;
		    return reg.test(number);
		},
		// 民國年轉西元年
		toWestDate: function (dateStr, symbol) {
			var symbol = symbol ? symbol : "/";
			if (dateStr) {
				var splitIndex = dateStr.indexOf(symbol);
				if (splitIndex != -1) {
					var year = dateStr.substring(0, splitIndex);
					var newDateStr = parseInt(year) + 1911 + dateStr.substring(splitIndex, dateStr.length);
					return newDateStr;
				}
			}
			return "";
		}
	});
})(jQuery);

function formatMoney() {

    // 金額格式化︰三位一撇
    $.each($('.accountFormat'), function (key, val) {
        $(this).setThousandsSymbolToNumber();
    });

    $('.accountFormat').blur(function () { formatNumber(this); $(this).setThousandsSymbolToNumber(); });
    $('.accountFormat').focus(function () { $(this).deleteThousandsSymbol(); });

    $('.accountFormat').keydown(function (e) {
        
        var key;
        if (window.event) {
            key = e.keyCode;
        } else if (e.which) {
            key = e.which;
        } else {
            return true;
        }

        // 只能有一個小數點
        if ($(this).val().indexOf(".") > 0 && 110 == key) {
            return false;
        }

        var intlen = $(this).attr("intLength");
        if (intlen != "" && intlen != undefined) {
            
            // 整數部份的最大長度限制
            var nums = $(this).val().split(".");
            if (nums[0].length == intlen
            && $(this).val().indexOf(".") < 0
            && ((key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                return false;
            }

            if (nums.length == 2) {

                // 整數及小數部份都為最大長度後，不允許輸入
                var dotlen = $(this).attr("dotLength");

                if (nums[0].length == intlen && $(this).val().indexOf(".") > 0 && nums[1].length == dotlen
                && ((key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                    return false;
                }
            }
        }

        // 開始數字為0且第二位不為小數點時，清除開始的0
        if ($(this).val().charAt(0) == '0' && $(this).val().charAt(1) != '.') {
            $(this).val(parseFloat($(this).val()));
        }

        if ((key >= 48 && key <= 57)
            || (key >= 96 && key <= 105) //數字鍵盤
            || 8 == key || 46 == key || 37 == key || 39 == key //8:backspace 46:delete 37:左 39:右 (倒退鍵、刪除鍵、左、右鍵也允許作用)
            || 190 == key || 110 == key //小數點
            || (189 == key) //減號
            ) {
            return true;
        } else {
            return false;
        }
    });
}

function formatDouble() {

    $('.doubleFormat').blur(function () { formatNumber(this); });

    $('.doubleFormat').keydown(function (e) {

        var key;
        if (window.event) {
            key = e.keyCode;
        } else if (e.which) {
            key = e.which;
        } else {
            return true;
        }

        // 只能有一個小數點
        if ($(this).val().indexOf(".") > 0 && 110 == key) {
            return false;
        }

        var intlen = $(this).attr("intLength");
        if (intlen != "" && intlen != undefined) {

            // 整數部份的最大長度限制
            var nums = $(this).val().split(".");
            if (nums[0].length == intlen
            && $(this).val().indexOf(".") < 0
            && ((key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                return false;
            }

            if (nums.length == 2) {

                // 整數及小數部份都為最大長度後，不允許輸入
                var dotlen = $(this).attr("dotLength");

                if (nums[0].length == intlen && $(this).val().indexOf(".") > 0 && nums[1].length == dotlen
                && ((key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                    return false;
                }
            }
        }

        // 開始數字為0且第二位不為小數點時，清除開始的0
        if ($(this).val().charAt(0) == '0' && $(this).val().charAt(1) != '.') {
            $(this).val(parseFloat($(this).val()));
        }

        if ((key >= 48 && key <= 57)
            || (key >= 96 && key <= 105) //數字鍵盤
            || 8 == key || 46 == key || 37 == key || 39 == key //8:backspace 46:delete 37:左 39:右 (倒退鍵、刪除鍵、左、右鍵也允許作用)
            || 190 == key || 110 == key //小數點
            || (189 == key) //減號
            ) {
            return true;
        } else {
            return false;
        }
    });
}

function formatNumber(obj, dot) {
    var num = $(obj).val();

    var dot = $(obj).attr("dotLength");

    if (dot == "" || dot == undefined) {
        dot = 2;
    }

    if (num != "") {

        $(obj).val(parseFloat(num).toFixed(dot));
    }
}

// 民過年日曆插件初始化
function showPicker(obj, param) {

	var settings = {
		el: obj,
		dateFmt: 'yyy/MM/dd',
		skin: 'twoer',
		errDealMode: 1,
		onpicked: function (dp) {
			jQuery(dp.el).trigger('change');
		} 
	};

	if (param && !jQuery.isEmptyObject(param)) {
		jQuery.extend(settings, param);
	};

	WdatePicker(settings);
}

//可在Javascript中使用如同C#中的string.format
//使用方式 : var fullName = String.format('Hello. My name is {0} {1}.', 'FirstName', 'LastName');
String.format = function () {
	var s = arguments[0];
	if (s == null) return "";
	for (var i = 0; i < arguments.length - 1; i++) {
		var reg = getStringFormatPlaceHolderRegEx(i);
		s = s.replace(reg, (arguments[i + 1] == null ? "" : arguments[i + 1]));
	}
	return cleanStringFormatResult(s);
}
//可在Javascript中使用如同C#中的string.format (對jQuery String的擴充方法)
//使用方式 : var fullName = 'Hello. My name is {0} {1}.'.format('FirstName', 'LastName');
String.prototype.format = function () {
	var txt = this.toString();
	for (var i = 0; i < arguments.length; i++) {
		var exp = getStringFormatPlaceHolderRegEx(i);
		txt = txt.replace(exp, (arguments[i] == null ? "" : arguments[i]));
	}
	return cleanStringFormatResult(txt);
}
//讓輸入的字串可以包含{}
function getStringFormatPlaceHolderRegEx(placeHolderIndex) {
	return new RegExp('({)?\\{' + placeHolderIndex + '\\}(?!})', 'gm')
}

//當format格式有多餘的position時，就不會將多餘的position輸出
//ex:
// var fullName = 'Hello. My name is {0} {1} {2}.'.format('firstName', 'lastName');
// 輸出的 fullName 為 'firstName lastName', 而不會是 'firstName lastName {2}'
function cleanStringFormatResult(txt) {
	if (txt == null) return "";
	return txt.replace(getStringFormatPlaceHolderRegEx("\\d+"), "");
}



///驗證Email 是否符合格式
function CheckEmail(value) {

    //please input the test email to see is valid
    var strEmail = value;

    //Regular expression Testing
    emailRule = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z]+$/;

    //validate ok or not
    if (strEmail.search(emailRule) != -1) {
        return true;
    } else {
        return false;
    }

}

function ChkIdno(value) {
    var a = new Array('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'X', 'Y', 'W', 'Z', 'I', 'O');
    var b = new Array(1, 9, 8, 7, 6, 5, 4, 3, 2, 1);
    var c = new Array(2);
    var d;
    var e;
    var f;
    var g = 0;
    var h = /^[a-z](1|2)\d{8}$/i;
    if (value.search(h) == -1) {
        return false;
    }
    else {
        d = value.charAt(0).toUpperCase();
        f = value.charAt(9);
    }
    for (var i = 0; i < 26; i++) {
        if (d == a[i])//a==a
        {
            e = i + 10; //10
            c[0] = Math.floor(e / 10); //1
            c[1] = e - (c[0] * 10); //10-(1*10)
            break;
        }
    }
    for (var i = 0; i < b.length; i++) {
        if (i < 2) {
            g += c[i] * b[i];
        }
        else {
            g += parseInt(value.charAt(i - 1)) * b[i];
        }
    }
    if ((g % 10) == f) {
        return true;
    }
    if ((10 - (g % 10)) != f) {
        return false;
    }
}

function ChkIDCard(data) {
    var city = new Array(1, 10, 19, 28, 37, 46, 55, 64, 39, 73, 82, 2, 11, 20, 48, 29, 38, 47, 56, 65, 74, 83, 21, 3, 12, 30);
    id = data.toUpperCase();
    if (!id.match(/^[A-Z][A-D]\d{8}$/)) {
        return false;
    }
    else {
        var total = 0;
        // 外來人口統一證號
        //將字串分割為陣列(IE必需這麼做才不會出錯)
        id = id.split('');
        //計算總分
        total = city[id[0].charCodeAt(0) - 65];
        // 外來人口的第2碼為英文A-D(10~13)，這裡把他轉為區碼並取個位數，之後就可以像一般身分證的計算方式一樣了。
        id[1] = id[1].charCodeAt(0) - 65;
        for (var i = 1; i <= 8; i++) {
            total += eval(id[i]) * (9 - i);
        }
    }
    //補上檢查碼(最後一碼)
    total += eval(id[9]);
    //檢查比對碼(餘數應為0);
    if (total % 10 == 0) {
        return true;
    }
    else {
        return false;
    }
}

function Chktel(data) {
    if (!/^(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,30}$/.test(data)) {
        return false;
    }
}

function Chkphone(phone) {
    if (!(/^09\d{2}-?\d{3}-?\d{3}$/.test(phone))) {       
        return false;
    }
}