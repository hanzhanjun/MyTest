var ilopt = {
    id: "HX010100"
};

$(function () {
    init();
    install()
})

function install() {
    if (!ilopt.permission.add) {        
        $("#btn_add").attr("style", "display:none;");
    }
    else {      
        $("#btn_add").attr("style", "display:block;");
    }
    if (!ilopt.permission.excel) {     
        $("#btn_excel").attr("style", "display:none;");
    }
    else {     
        $("#btn_excel").attr("style", "display:block;");
    }

}

function init() {
    $('#test-table').bootstrapTable({
        url: "../HX01/GetList",             //请求后台的URL（*）
        method: 'GET',                      //请求方式（*）                    
        striped: true,                      //是否显示行间隔色
        cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
        pagination: true,                   //是否显示分页（*）
        //sortable: false,                     //是否启用排序
        //sortOrder: "asc",                   //排序方式
        sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
        pageNumber: 1,                      //初始化加载第一页，默认第一页,并记录
        //pageSize: 15,                     //每页的记录行数（*）
        // pageList: [15, 30, 45, 60],        //可供选择的每页的行数（*）
        //search: false,                      //是否显示表格搜索
        strictSearch: true,
        // showColumns: true,                  //是否显示所有的列（选择显示的列）
        // showRefresh: true,                  //是否显示刷新按钮
        minimumCountColumns: 2,             //最少允许的列数
        clickToSelect: true,                //是否启用点击选中行
        height:680, //screen.availHeight - document.body.clientHeight,                     //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
        uniqueId: "Id",                     //每一行的唯一标识，一般为主键列
        //showToggle: true,                   //是否显示详细视图和列表视图的切换按钮
        //cardView: false,                    //是否显示详细视图
        // detailView: false,                  //是否显示父子表
        //search:true, 
        toolbar: "#toolbar",
        queryParams: function (params) {  //得到查询的参数
            //这里的键的名字和控制器的变量名必须一致，这边改动，控制器也需要改成一样的
            var temp = {
                rows: params.limit,                         //页面大小
                page: (params.offset / params.limit) + 1,   //页码
                userName: $("#userName").val(),
                startTime: $("#startTime").val(),
                mobile: $("#mobile").val(),
                endTime: $("#endTime").val(),
            };
            return temp;
        },
        columns: [{ checkbox: true, visible: true },//是否显示复选框  
                  { field: 'Id', title: '操作', width: 120, align: 'center', valign: 'middle', formatter: actionFormatter },
                  { field: 'UserName', title: '登录名', sortable: true, align: 'center', },
                  { field: 'RealName', title: '真实姓名', sortable: true, align: 'center', },
                  { field: 'Moblie', title: '手机号', sortable: true, align: 'center',  },
                  { field: 'Email', title: '邮箱', align: 'center' },
                  { field: 'CreateTime', title: '注册时间', align: 'center' },
                  { field: 'Birthday', title: '出生日期', sortable: true, align: 'center', },
                  { field: 'Qq', title: 'qq', align: 'center' },
                  { field: 'Sex', title: '性别', align: 'center' },

        ],
        onLoadSuccess: function () {
        },
        onLoadError: function () {
            showTips("数据加载失败！");
        },
        onDblClickRow: function (row, $element) {
            var id = row.ID;
            EditViewById(id, 'view');
        },
    });
};


function enpty()
{
    $("#RealName").val("");
    $("#birthday").val("");
    $("#passWard").val("");
    $("#email").val("");
    $("#UserName").val("");
    $("#Mobile").val("");
    $("#qq").val("");
    $('input').removeAttr('checked');
    $("#userId").val("");
}

//展示新增框
function addVideoShow() {
    enpty();
    $("#myModalLabel").text("新增用户");
    $('#UserName').removeAttr("disabled");
    $('#myModal').modal("show"); 
    $("#myModal").draggable()
}

//添加
function add()
{
    var mobile = $("#Mobile").val();
    var birthday = $("#birthday").val();
    var name = $("#RealName").val();
    var loginName = $("#UserName").val();
    var qq = $("#qq").val();
    var email = $("#email").val();
    var passWard = $("#passWard").val();
    var sex = $("input[name='sex']:checked").val();
    var id = $("#userId").val();
    if (!mobile) {
        $.alertInfo("手机号不能为空");
        return false;
    }
    if (!birthday) {
        $.alertInfo("出生日期不能为空");
        return false;
    }
    if (!name) {
        $.alertInfo("姓名不能为空");
        return false;
    }
    if (name.length > 0 && name.length > 20) {
        $.alertInfo("姓名最长为10个字");
        return false;
    }
    if (!sex) {
        $.alertInfo("性别不能为空");
        return false;
    }
    if (!loginName) {
        $.alertInfo("登录名不能为空");
        return false;
    }

    if (!passWard) {
        $.alertInfo("密码不能为空");
        return false;
    }
    if (!(/^1[3|4|5|7|8][0-9]\d{8}$/.test(mobile))) {
        $.alertInfo("手机号格式不正确");
        return false;
    }

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/HX01/AddAdmin",
        data: {id:id, mobile: mobile, birthday: birthday, name: name, loginName: loginName, qq: qq, email: email, passWard: passWard, sex: sex },
        success: function (json) {     
            if (json.code == 1) {
                alert(json.msg);
                $('#myModal').modal("hide");
                $('#test-table').bootstrapTable('refresh', { pageNumber: 1 });
            }
            else {
                alert(json.msg);
            }
        },       
    });
}

function showTips(str) {
    alert(str);
}

function dateFormatter(value, row, index) {
    return "<a href='" + value + "' title='单击打开连接' target='_blank'>" + value + "</a>";
}


function linkFormatter(value, row, index) {
    return "<a href='" + value + "' title='单击打开连接' target='_blank'>" + value + "</a>";
}
//Email字段格式化
function emailFormatter(value, row, index) {
    return "<a href='mailto:" + value + "' title='单击打开连接'>" + value + "</a>";
}
//性别字段格式化
function sexFormatter(value) {
    if (value == "女") {
        color = 'Red';
    }
    else if (value == "男") {
        color = 'Green';
    }
    else {
        color = 'Yellow';
    }
    return '<div  style="color: ' + color + '">' + value + '</div>';
}


function actionFormatter(value, row, index) {
    var id = value;
    var editcel = "";
    if (ilopt.permission.edit) {
        editcel = "<a href='javascript:;' class='btn btn-xs blue' onclick=\"EditViewById('" + id + "')\" title='编辑'><span class='glyphicon glyphicon-pencil'></span></a>";
    }

    var deletecel = "";
    if (ilopt.permission.view) {
        deletecel = "<a href='javascript:;' class='btn btn-xs red' onclick=\"DeleteByIds('" + id + "')\" title='删除'><span class='glyphicon glyphicon-remove'></span></a>";
    }
   
    var result = "";
    if ((ilopt.permission.view || ilopt.permission.edit || ilopt.permission.del)) {
        result = editcel + deletecel;
    }          
    return result;
}

function query()
{
    $('#test-table').bootstrapTable('refresh', { pageNumber: 1 });
}

function EditViewById(id) {  
    enpty();
        document.getElementById("UserName").disabled = "disabled";
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/HX01/GetUser",
            data: { id: id },
            success: function (json) {         
                if (json.code == 1) {
                    $("#RealName").val(json.data.RealName);
                    $("#birthday").val(json.data.Birthday);
                    $("#passWard").val(json.data.PassWard);
                    $("#email").val(json.data.Email);
                    $("#UserName").val(json.data.UserName);
                    $("#Mobile").val(json.data.Moblie);
                    $("#qq").val(json.data.Qq);
                    $("input:radio[name='sex'][value='" + json.data.Sex + "']").prop("checked", true);
                    $("#userId").val(json.data.Id);
                    $("#myModalLabel").text("编辑用户");
                    $('#myModal').modal("show");
                    $("#myModal").draggable()
                }
                else {
                    alert(json.msg);
                }
            },
        });
    
}

function excel()
{
    var userName = $("#userName").val();
    var startTime = $("#startTime").val();
    var mobile = $("#mobile").val();
    var endTime = $("#endTime").val();

    var queeryUrl = "/HX01/Excel?userName=" + userName + "&startTime=" + startTime + "&mobile=" + mobile + "&endTime=" + endTime + "";
    queeryUrl = encodeURI(queeryUrl);
    window.location.href = queeryUrl;
      
}