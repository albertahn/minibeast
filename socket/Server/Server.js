var io = require("socket.io").listen(3000);
    
var usernames = [];
     
io.sockets.on('connection', function (socket) {		//Ŭ���̾�Ʈ�� ����Ǵ� ��� �Լ��� �����Ѵ�.
    //socket.emit('news', { hello: 'world' });		//Ŭ���̾�Ʈ���� news��� �̸����� JSON��Ʈ���� �����Ѵ�.

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