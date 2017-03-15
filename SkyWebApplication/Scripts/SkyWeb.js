//获取地址栏参数
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}


var upFiledByAjax = function (table, strWhere, columnname, newval) {

    $.ajax({
        type: 'POST',
        url: "/Ajax/updateCusField",
        data: {
            table: table,
            strWhere: strWhere,
            columnname: columnname,
            columnvalue: newval,
        },
        dataType: "json",
        success: function (data) {
            if (data.MessageStatus == 'true') {
                alert("更新成功");
            }
            else {
                $("#" + id + "").val(oldval);
                //   alert("数据格式不太对");
                alert(data.MessageInfo);

            }

            $("." + id + "").remove();
        },
    });
 
}