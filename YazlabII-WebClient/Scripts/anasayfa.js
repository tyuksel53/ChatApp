var hostUrl = "http://192.168.1.105/yazlabi/"; 
var connection;
var hub;
$(document).ready(function (){

    if(sessionStorage.getItem("tokenKey"))
    {
        $("#LoginUsername").text("Hoş geldin, " + sessionStorage.getItem("username") )
        var today = new Date();
        var h = today.getHours();
        var m = today.getMinutes();
        var s = today.getSeconds();
        $("#GirisTarihi").text(`Giris saatin: ${h}:${m}:${s} ` )
        
        connection = $.hubConnection(hostUrl);
        connection.qs = { 'username' : sessionStorage.getItem("username") };
        hub = connection.createHubProxy('ChatHub');
        sessionStorage["hub"] = hub;
        sessionStorage["connection"] = connection;
        
        hub.on('hello', function(message) {
            console.log(message);
        });

        connection.start({ jsonp: true }).done(function() {
            hub.on('wantsToTalk',function(message,invitationUser){
                if (confirm(message)) {
                    hub.invoke('UserAcceptsTalk',sessionStorage.getItem("username"),invitationUser);
                    //window.open("Chat.html?username="+invitationUser);
                    //console.log("fired");
                    openChatSection(invitationUser);
                }
            });

            hub.on('userAcceptsTalk',function(message,user){
                openChatSection(user);
            });

            hub.on('hello',function(message){
                console.log(message);
            });
            hub.on('InComingMessage',function(mesaj,fromUser){
                    var today = new Date();
                    var h = today.getHours();
                    var m = today.getMinutes();
                    var s = today.getSeconds();
                    var line = `<li class="left clearfix">
                        <span class="chat-img pull-left">
                            <img src="${sessionStorage[fromUser + "Img"]}" width="50px" height="50px" alt="User Avatar" class="img-circle" />
                        </span>
                        <div class="chat-body clearfix">
                            <div class="header">
                                <strong class="primary-font">${fromUser}</strong> <small class="pull-right text-muted">
                                <span class="glyphicon glyphicon-time"></span>${h}:${m}:${s}</small>
                            </div>
                            <p>
                                ${mesaj}
                            </p>
                        </div>
                    </li>`
                    $("#"+fromUser+"chatContainer").append(line);
            });
            
        });

        kullanicilariGetir()
        setInterval(kullanicilariGetir, 15000);

    }else
    {
        window.location = "login.html";
    }
    
});
$("#baglantiyiKes").click(function(){
    hub.invoke('UserDisconnect',sessionStorage.getItem("username"));
    connection.stop();
    sessionStorage.clear();
    window.location = "login.html";
});


function gonder(username)
{

    console.log("as");
    if($("#chat-input-" + username).val())
    {
    if($("#chat-input-" + username).val())
        hub.invoke("mesaj_gonder",$("#chat-input-"+username).val(),username,sessionStorage.getItem("username"));
        var today = new Date();
        var h = today.getHours();
        var m = today.getMinutes();
        var s = today.getSeconds();
        var line = `<li class="left clearfix">
            <span class="chat-img pull-left">
                <img src="${sessionStorage[sessionStorage["username"] + "Img"]}" width="50px" height="50px" alt="User Avatar" class="img-circle" />
            </span>
            <div class="chat-body clearfix">
                <div class="header">
                    <strong class="primary-font">${sessionStorage["username"]}</strong> <small class="pull-right text-muted">
                    <span class="glyphicon glyphicon-time"></span>${h}:${m}:${s}</small>
                </div>
                <p>
                    ${$("#chat-input-"+username).val()}
                </p>
            </div>
        </li>`
        $("#"+username+"chatContainer").append(line);
    }
    
}

$('#btnUploadFile').on('click', function () {

    var data = new FormData();

    var files = $("#fileUpload").get(0).files;

    // Add the uploaded image content to the form data collection
    if (files.length > 0) {
         data.append("UploadedImage", files[0]);
    }

    // Make Ajax request with the contentType = false, and procesDate = false
    var ajaxRequest = $.ajax({
         type: "POST",
         url: hostUrl + "User/UploadFile?username="+sessionStorage["username"],
         contentType: false,
         processData: false,
         data: data
         });

    ajaxRequest.done(function (xhr, textStatus) {
                console.log("İşlem tamam");
           });
 });

function openChatSection(targetUser)
{
    var chat = `<div class="col-md-6">
               <div class="container-fluid">
                <div class="row">
                  <div class="col-md-8">
                      <h2 id="header">Suan, ${targetUser} ile konuşuyorsunuz</h2>
                      <div class="panel panel-primary">
                          <div class="panel-body">
                              <ul class="chat" id="${targetUser}chatContainer">
                                 
                              </ul>
                          </div>
                          <div class="panel-footer">
                              <div class="input-group">
                                  <input id="chat-input-${targetUser}" name="chat-input-${targetUser}" type="text" class="form-control input-sm" placeholder="Type your message here..." />
                                  <span class="input-group-btn">
                                      <button class="btn btn-success" name="btn-chat" onclick="gonder('${targetUser}')";>
                                          Gönder</button>
                                  </span>
                              </div>
                          </div>
                      </div>
                  </div>
              </div>
          </div>
            </div>`

            $("#chatSection").append(chat);
}


function kullanicilariGetir()
{
    $.ajax({
        Method:"get",
        url:hostUrl + "UserAuthentication/getallusers",
        headers: {
            "Authorization":"Bearer " + sessionStorage.getItem("tokenKey")
        }
    }).done(function(response)
        {
            $("#usersBody").empty();
            var tableBody;
            for(var i=0;i<response.length;i++)
            {
                if(response[i].Username != sessionStorage.getItem("username"))
                {
                    tableBody = tableBody + `<tr><th>${response[i].Username}</th>`;
                    if(response[i].IsUserOnline == true)
                    {
                        tableBody = tableBody + `<th><input class="btn btn-success" type="button" 
                        value="ONLINE KONUSMA BASLAT" onclick="KonusmaBaslat('${response[i].Username}')"/> </th>`;
                    }else
                    {
                        tableBody = tableBody + `<th>OFFLINE</th>`;
                    }
                    tableBody = tableBody + `<th >${response[i].CurrentIp}</th>`;
                    tableBody = tableBody + `<th><img class="img-thumbnails" height="50" width="50" src="${hostUrl}${response[i].ImgUrl}"</th></tr>`;
                    sessionStorage[response[i].Username + "Img"] = hostUrl + response[i].ImgUrl;
                }else
                {
                    $("#userImage").attr("src",hostUrl + response[i].ImgUrl);
                    sessionStorage[response[i].Username + "Img"] = hostUrl + response[i].ImgUrl;
                }
                
            }
            $("#usersBody").append(tableBody);
        }).fail(function(){
            console.log("pataldı");
        });
}

function KonusmaBaslat(username)
{
    hub.invoke('WantsToTalk',sessionStorage.getItem("username") + " seninle Konuşmak istiyor !",username,sessionStorage.getItem("username"));
}