// 共用訊息
(function ($) {
    var current = $.Message = {};
    $.extend(current, {
        alertDialog: function (content, type, agreeFunction, note) {
            swal({
                title: content,
                html: transforHtml(note),
                type: type,
                //allowOutsideClick: false,
                //allowEscapeKey: false,
                //buttonsStyling: false,
                //confirmButtonClass: "btn second",
                //confirmButtonColor: "#6f94c9",
                confirmButtonText: "確認"
            }).then(
                function () {
                    if (agreeFunction == null || typeof agreeFunction != 'function') {
                        //console.log('agreeFunction is not function');
                        return;
                    }
                    agreeFunction();
                });
        },
        normalConfirm: function (content, agreeFunction, cancelFunction, note) {
            swal({
                title: content,
                html: transforHtml(note),
                type: "info",
                //allowOutsideClick: false,
                //allowEscapeKey: false,
                //buttonsStyling: false,
                //confirmButtonClass: "btn second",
                //confirmButtonColor: "#6f94c9",
                confirmButtonText: "確認",
                //cancelButtonClass: "btn second",
                showCancelButton: true,
                cancelButtonText: "取消"
            }).then(
                function () {
                    if (agreeFunction == null || typeof agreeFunction != 'function') {
                        //console.log('agreeFunction is not function');
                        return;
                    }

                    agreeFunction();
                }, function (dismiss) {
                    if (cancelFunction == null || typeof cancelFunction != 'function') {
                        return;
                    }
                    cancelFunction();
                });
        }

        //格式化的儲存詢問視窗: function (title, content, agreeFunction, cancelFunction) {

        //},
        //格式化的刪除詢問視窗: function (title, content, agreeFunction, cancelFunction) {

        //}

        //,
        //Error : function(title, msg, callback){
        //    swal({
        //        title: title,
        //        text: msg,
        //        type: "error",
        //        confirmButtonColor: "#D43F3A"
        //    }).then(callback);
        //},

        //Validate: function (errorProcess) {
        //    console.log('Validate');
        //    swal({
        //        title: '部分欄位尚未填值',
        //        text: '請檢查後在進行下一步',
        //        type: "warning",                
        //    }).then(errorProcess);
        //}      
    });
})(jQuery)

function transforHtml(content) {
    // 2017.09.04 解決沒傳入 content 時 replace 發生錯誤的 bug
    if (content != "" && content != undefined)
        return content.replace(/\n/g, "<br />");
    else
        return "";
}