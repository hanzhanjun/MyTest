function showCheck(a) {
    var c = document.getElementById("myCanvas");
    var ctx = c.getContext("2d");
    ctx.clearRect(0, 0, 1000, 1000);
    ctx.font = "80px 'Microsoft Yahei'";
    ctx.fillText(a, 0, 100);
    ctx.fillStyle = "white";
}
var code;
function createCode() {
    code = "";
    var codeLength = 4;
    var selectChar = new Array(1, 2, 3, 4, 5, 6, 7, 8, 9, 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z');
    for (var i = 0; i < codeLength; i++) {
        var charIndex = Math.floor(Math.random() * 60);
        code += selectChar[charIndex];
    }
    if (code.length != codeLength) {
        createCode();
    }
    showCheck(code);
}

function validate() {
    var inputCode = document.getElementById("validatecode").value.toUpperCase();
    var codeToUp = code.toUpperCase();
    if (inputCode.length <= 0) {
        document.getElementById("validatecode").setAttribute("placeholder", "验证码");
        createCode();
        return false;
    }
    else if (inputCode != codeToUp) {
        document.getElementById("validatecode").value = "";
        document.getElementById("validatecode").setAttribute("placeholder", "验证码");
        createCode();
        return false;
    }
    else {
        window.open(document.getElementById("J_down").getAttribute("data-link"));
        document.getElementById("validatecode").value = "";
        createCode();
        return true;
    }

}

function checkLogin()
{
    var username = $("#username").val();
    var password = $("#password").val();
    var validatecode = $("#validatecode").val();
    if (username == "") {
        $("#err-msg").html("请您输入用户名！");
        return false;
    }
    if (password == "") {
        $("#err-msg").html("请您输入密码！");
        return false;
    }
    if (validatecode == "") {
        $("#err-msg").html("请您输入验证码！");
        return false;
    }  
    if (validatecode.toUpperCase() != code.toUpperCase()) {
        $("#err-msg").html("验证码不正确");
        return false;
    }
}
