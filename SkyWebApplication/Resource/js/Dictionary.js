﻿
//初始化页面
function init() {
    //加载树形菜单
    LoadTreeDictionary(0);
    //加载右侧内容
    dictionary(2);
    //右侧表单设置为只读
    formReadonly();
    //加载icheck的样式，初始化加载一次，每一次刷新都需要加载一次，所以封装一个方法。
    icheckcss("CategoryStatus");
    $('#CategoryFrom').bootstrapValidator({
        //        live: 'disabled',
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            CategoryName: {
                validators: {
                    notEmpty: {
                        message: '名称不能为空'
                    }
                }
            },
            CategoryInfo: {
                validators: {
                    notEmpty: {
                        message: '名称不能为空'
                    }
                }
            },
            CategorySort: {
                validators: {
                    notEmpty: {
                        message: '名称不能为空'
                    },
                    numeric: { message: '排序只能是数字' }
                }
            }

        }
    });
}
//以下部分可以封装为一个文件
//加载icheck的样式，初始化加载一次，每一次刷新都需要加载一次，所以封装一个方法。
function icheckcss(name) {
    $('[name=' + name + ']').iCheck({
        labelHover: false,
        cursor: true,
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%'
    });
}
//设置form为只读
function formReadonly() {
    //所有文本框只读
    $("input[name],textarea[name]").attr("readonly", "readonly");
    //所有单选框和复选框只读
    $('input[type=radio],input[type=checkbox]').prop("disabled", true);
    //隐藏取消、保存按钮
    $("#CategoryFrom .box-footer").hide();
    //还原新增、编辑、删除按钮样式
    $('[name="CategoryBtn"]').removeClass("btn-primary").addClass("btn-default");
}
//清空form值
function formClear() {
    //所有文本框只读,除了radio和checkbox
    $(':input[id]:not(:radio):not(:checkbox),textarea[name]').val('');
    $('input[type="radio"][name="CategoryStatus"][value="true"]').prop("checked", "checked");
    //还原校验框
    if ($("#CategoryFrom").data('bootstrapValidator'))
        $("#CategoryFrom").data('bootstrapValidator').resetForm();
}
//设置form为可写
function formWritable(action) {
    $("input[name],textarea[name]").removeAttr("readonly");
    $('input[type=radio],input[type=checkbox]').removeAttr("disabled");
    $('input[type=radio],input[type=checkbox]').removeAttr("readonly");
    $('input[type="radio"][name="CategoryStatus"][value="true"]').prop("checked", "checked");
    $("#CategoryFrom .box-footer").show();
    //  $('[name="CategoryBtn"]').removeClass("btn-primary").addClass("btn-default");
    $('#CategoryFrom').prop("action", action);
    icheckcss("CategoryStatus");
}
//设置上级ID值
function fillParent(selectedNode) {
    $("input[name='CategoryParentName']").val(selectedNode ? selectedNode.text : '系统字典').attr("readonly", "readonly");
    $("input[name='CategoryParentID']").val(selectedNode ? selectedNode.id : '1').attr("readonly", "readonly");
}
//设置Form值
function fillForm(selectedNode) {
    $.ajax({
        type: "get",
        url: "/Category/GetOneCategory",
        data: {
            id: selectedNode.id
        },
        dataType: "json",
        success: function (result) {
            // $('#thisCategoryName').val(result.CategoryName).prop("disabled", true);
            //  $('#thisID').val(result.ID).prop("disabled", true);
            $('#CategoryParentName').val(result.CategoryParentName).prop("readonly", true);
            $('#CategoryParentID').val(result.CategoryParentID).removeAttr("readonly");
            $('#CategoryName').val(result.CategoryName).removeAttr("readonly");
            $('#CategorySort').val(result.CategorySort).removeAttr("readonly");
            $('#CategoryInfo').val(result.CategoryInfo).removeAttr("readonly");
            $('input[type="radio"][name="CategoryStatus"]').removeAttr("disabled");

            $('input[type="radio"][name="CategoryStatus"][value=' + result.CategoryStatus.toString() + ']').prop("checked", "checked");

            $('#CategoryFrom').prop("action", "/Category/Edit");
            $("#CategoryFrom .box-footer").show();
            icheckcss("CategoryStatus");
        },
        error: function () {
            alert("选择的不对！")
        }
    });


}
//curd查按钮操作
function categoryaction(btn, ac) {

    $('[name="CategoryBtn"]').removeClass("btn-primary").addClass("btn-default");
    $(btn).removeClass("btn-default").addClass("btn-primary");
    var selectedArr = $("#CategoryTreeview").data("treeview").getSelected();
    var selectedNode = selectedArr.length > 0 ? selectedArr[0] : null;
    //$('#Category input').removeAttr("disabled");
    //$('#Category textarea').removeAttr("disabled");
    icheckcss("CategoryStatus");

    var thisCategoryName = $('#thisCategoryName').val();
    var thisID = $('#thisID').val();

    if (ac == 'addtop') {
        formClear();
        formWritable("/Category/Create");
        $('input[type="radio"][name="CategoryStatus"][value="true"]').prop("checked", "checked");
        fillParent(null);

    }
    if (ac == 'addnext') {
        if (!selectedNode) {
            alertconfirm('你尚未没有选择节点！');
        }
        else {

            formClear();
            formWritable("/Category/Create");
            $('input[type="radio"][name="CategoryStatus"][value="true"]').prop("checked", "checked");
            fillParent(selectedNode);
        }



    }
    if (ac == 'edit') {
        //需要重新获取这个栏目
        if (!selectedNode)
        { alertconfirm('你尚未没有选择节点！'); }
        else
        {
            fillForm(selectedNode);
        }


    }
    if (ac == 'cancel') {
        //需要重新获取这个栏目
        formReadonly();
        return false;
    }
    if (ac == 'delete') {
        //弹出确认对话框即可
        if (!selectedNode) {
            alertconfirm('你尚未没有选择节点！');
            return false;
        }
        else {
            if (selectedNode.nodes) {
                alertconfirm('此节点有子节点，不能删除！');
                return false;
            }
            else {
                fillForm(selectedNode);
                formReadonly();
                delconfirm(selectedNode.id, "/Category/Delete/");
            }

        }

    }


}

//加载树形菜单
function LoadTreeDictionary(rootId) {
    $.ajax({
        type: "get",
        url: "/category/TreeJson",
        data: {
            id: rootId
        },
        dataType: "json",
        success: function (result) {
            $('#CategoryTreeview').treeview({
                levels: 1,
                data: result,
                multiSelect: $('#chk-select-multi').is(':checked'),
                onNodeSelected: function (event, node) {
                    formClear();
                    dictionary(node.id);
                    formReadonly();

                    // alert($("#CategoryTreeview").data("treeview").getSelected()[0].text);
                    //  alert(node.text);
                    // selectedArr = $("#tree").data("treeview").getSelected();
                },
                onNodeUnselected: function (event, node) {




                },
                onNodeCollapsed: function (event, node) {



                },
                onNodeExpanded: function (event, node) {


                }
            });
        },
        error: function () {
            alert("树形结构加载失败！")
        }
    });

}

//右侧加载选中项目
function dictionary(nodeid) {
    $('[name="CategoryStatus"]').removeAttr("checked");
    $.ajax({
        type: "get",
        url: "/Category/GetOneCategory",
        data: {
            id: nodeid
        },
        dataType: "json",
        success: function (result) {

            $('#CategoryParentName').val(result.CategoryParentName);
            $('#CategoryParentID').val(result.CategoryParentID);
            $('#CategoryName').val(result.CategoryName);
            $('#ID').val(result.ID);
            $('#thisCategoryName').val(result.CategoryName);
            $('#thisID').val(result.ID);
            $('#CategorySort').val(result.CategorySort);
            $('#CategoryInfo').val(result.CategoryInfo);
            $('input[type="radio"][name="CategoryStatus"][value=' + result.CategoryStatus.toString() + ']').prop("checked", "checked");;
            icheckcss("CategoryStatus");
        },
        error: function () {
            alert("树形结构加载失败！")
        }
    });



}

