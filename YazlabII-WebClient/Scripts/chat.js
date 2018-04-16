var hostUrl = "http://192.168.1.105/yazlabi/"; 
var connection2;
var hub2;

$(document).ready(function (){
    if(sessionStorage.getItem("tokenKey"))
    {
        var url_string = window.location.href;
        var url = new URL(url_string);
        var targetUsername = url.searchParams.get("username");

        connection2 = $.hubConnection(hostUrl);
        connection2.qs = { 'username' : sessionStorage.getItem("username") };
        hub2 = connection2.createHubProxy('ChatHub');
       
        connection2.start({ jsonp: true }).done(function() {
            console.log("connected");
            

            
        });

    }else
    {
        window.location = "login.html";
    }
    
});
