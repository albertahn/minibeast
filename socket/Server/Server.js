var io = require("socket.io").listen(3000);
    
var usernames = [];
     
io.sockets.on('connection', function (socket) {		//클라이언트가 연결되는 경우 함수를 실행한다.
    //socket.emit('news', { hello: 'world' });		//클라이언트에게 news라는 이름으로 JSON스트링을 전송한다.

    console.log("A user connected !")
    
    socket.join('room1');
    
    socket.on("Msg", function(data) {
        console.log(data);
        socket.emit("MsgRes", data);
    });
    
    socket.on("createPlayerREQ", function(data) {
        var ret = data.split(":");
        //data = leejin:23,24,12
        //ret[0] = leejin
        //ret[1] = 23,24,12
        
        usernames.push(ret[0]);
        for(var key in usernames){
             console.log("hello "+usernames[key]);
        }
        io.sockets.in('room1').emit("createPlayerRES", data);
        //socket.emit("createPlayerRes", data);
    });    
    
    socket.on("movePlayerREQ",function(data){        
            console.log(data);
        io.sockets.in('room1').emit("movePlayerRES", data);
        console.log(data);                
    });
});