var hostUrl = "http://192.168.1.105/yazlabi/"; 


$("#girisYap").click(function(){

    if($("#username").val() && $("#password").val())
    {
        var loginData = {
            grant_type: 'password',
            username: $("#username").val(),
            password: $("#password").val(),
        };
        
        $.ajax({
            type: 'POST',
            url: hostUrl + 'getToken',
            headers: { "Accept": "application/json" },
            contentType: "application/x-www-form-url; charset=urf-8",
            data: loginData
        }).done(function (data) {
            sessionStorage.setItem("tokenKey", data.access_token);
            sessionStorage.setItem("username", loginData.username);
            alert("giris basarili");
            window.location = "Anasayfa.html";
        }).fail(function(){
            alert("Yanlış Kullanıcı Adı veya Şifre");
        });
        
    }else
    {
        alert("Bütün alanları doldurun");
    }


});


$("#kayitOl").click(function(){
    window.location = "kayit.html";
})