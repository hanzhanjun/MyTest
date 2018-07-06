$(function () {
    $.ajax({
        url: "/Home/GetMenu",
        cache: false,
        dataType: "json",
        success: function (json) {         
            var menu=null;        
            var child=null;
            if (json.code == 1) {
                var menus = [];
                for (var i = 0; i < json.data.length; i++) {
                    menu = json.data[i];               
                    var children = [];
                    for (var j = 0; j < menu.Children.length; j++) {
                        child = menu.Children[j];
                        children.push({ "id": child.Id, "text": child.Name, "icon": "fa fa-circle-o", "url": ".." + child.Url, "targetType": "iframe-tab"});
                    }
                    menus.push({ "id": menu.Id, "text": menu.Name, "icon": "fa fa-laptop", "children": children });
                }
                $('.sidebar-menu').sidebarMenu({ data: menus });
            }
        }
    });       
});

  