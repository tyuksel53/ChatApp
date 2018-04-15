var hostUrl = "http://localhost:58474/"; 


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
        }).fail(function(){
            alert("patladı");
        });
        
    }else
    {
        alert("Bütün alanları doldurun");
    }


});

$(document).ready(function (){
    var connection = $.hubConnection(hostUrl);
    var hub = connection.createHubProxy("ChatHub");
    hub.on("AddMessage", Method);
    connection.start({ jsonp: true })
                .done(function () {
                console.log('connected');
            })
            .fail(function (a) {
                console.log('not connected'+a);
            });
});

function Method(messageFromHub)
{
    alert(messageFromHub);
}