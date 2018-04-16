var hostUrl = "http://192.168.1.105/yazlabi/";

$("#kayitOl").click(function(){

    if($("#username").val() && $("#password").val())
    {
        $.ajax({
            method:"post",
            url: hostUrl + "Register/Register",
            data: {username:$("#username").val(),password:$("#password").val()}
        }).done(function(response){
            if(response == "Basarili")
            {
                alert("kayit basarili");
                window.location = "login.html";
            }else
            {
                alert("bu kullanıcı adı alınmış");
            }
        }).fail(function(response){
            console.log(response);
            alert("bir seyler ters gitti");
        });
    }else
    {
        alert("boslukları doldur");
    }

});