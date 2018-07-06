$(function () {  
        var menus = [     
        {
            id: "9001",
            text: "我的管理",
            icon: "fa fa-laptop",
            children: [
            {
                id: "90011",
                text: "已办事项",
                icon: "fa fa-circle-o",
                url: "Test",
                targetType: "iframe-tab"
            },
            {
                id: "90012",
                text: "已办事项",
                url: "../Work/Work1",
                targetType: "iframe-tab",
                icon: "fa fa-circle-o"
            },
            {
                id: "90013",
                text: "已办事项",
                url: "../Work/Work2",
                targetType: "iframe-tab",
                icon: "fa fa-circle-o"
            },
            {
                id: "90014",
                text: "已办事项",
                url: "UI/modals_iframe.html",
                targetType: "iframe-tab",
                icon: "fa fa-circle-o"
            },
            {
                id: "90015",
                text: "已办事项",
                url: "UI/sliders_iframe.html",
                targetType: "iframe-tab",
                icon: "fa fa-circle-o"
            },
            {
                id: "90016",
                text: "已办事项",
                url: "UI/timeline_iframe.html",
                targetType: "iframe-tab",
                icon: "fa fa-circle-o"
            }
            ]
        },
        {
            id: "9002",
            text: "我的记录",
            icon: "fa fa-edit",
            children: [
            {
                id: "90021",
                text: "待办事项",
                url: "forms/advanced_iframe.html",
                targetType: "iframe-tab",
                icon: "fa fa-circle-o"
            },
            {
                id: "90022",
                text: "待办事项",
                url: "forms/general_iframe.html",
                targetType: "iframe-tab",
                icon: "fa fa-circle-o"
            },
            {
                id: "90023",
                text: "待办事项",
                url: "forms/editors_iframe.html",
                targetType: "iframe-tab",
                icon: "fa fa-circle-o"
            },
            {
                id: "90024",
                text: "百度",
                url: "https://www.baidu.com",
                targetType: "iframe-tab",
                icon: "fa fa-circle-o",
                urlType: 'abosulte'
            }
            ]
        }
        ];
        $('.sidebar-menu').sidebarMenu({data: menus});  
});

//function addTab(title, url, menuid)
//{
//    debugger;
//    addTabs({
//        id: menuid,
//        title: title,
//        close: false,
//        url: url,
//        targetType: "iframe-tab",
//        icon: "fa fa-circle-o",
//    });
//    App.fixIframeCotent();
    
//}
  