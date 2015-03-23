var app = require('http').createServer(handler)
, io = require('socket.io').listen(app)
, fs = require('fs')
app.listen(3000);
function handler (req, res) {
fs.readFile(__dirname + '/index.html',
function (err, data) {
if (err) {
res.writeHead(500);
return res.end('Error loading index.html');
}
res.writeHead(200);
res.end(data);
});
}


var usernames = {};

// rooms which are currently available in chat
var rooms = ['room1','room2','room3','room4','room5','roomfuck'];


io.sockets.on('connection', function (socket) {
    
   //update rooms
    socket.emit('updaterooms', rooms, 'room1');

    socket.on('adduser', function(username){
    
    
    socket.username = username;
    
    socket.room = 'room1';
    socket.join('room1');
    
    socket.emit('send message', 'you have connected to room1');
    
    usernames["username"] = username;
    
    socket.broadcast.to('room1').emit('updatechat', username + ' has connected to this room1');
    
		
                io.sockets.in(socket.room).emit('new message', socket.username, "hi there this is "+socket.username);
    
});


    
    
    // when the client emits 'sendchat', this listens and executes
socket.on('getallppl', function (data) {
            
            io.sockets.emit('allppl', socket.username, usernames);
            
            
console.log(data);
                
	});



socket.on('send message', function(data){
    
		//io.sockets.emit('new message', socket.username, data);
                
                //var res = data.replace(new RegExp("\\\\", "g"), "");
                
                io.sockets.in(socket.room).emit('new message', socket.username, data);
                
	});
        
 socket.on('sendpos', function(data){
    
		//io.sockets.emit('new pos', socket.username, data);
                
               // io.sockets.emit('new message', socket.username, data);
                
                io.sockets.in(socket.room).emit('new pos', socket.username, data);
                
                console.log(data);
                //var res = data.replace(new RegExp("\\\\", "g"), "");
                
               // io.sockets.in(socket.room).emit('new position', socket.username, data);
                
                
	});       
//switch rooms

socket.on('switchRoom', function(newroom){
    
    
		socket.leave(socket.room);
		socket.join(newroom);
		socket.emit('new message', 'SERVER', 'you have connected to '+ newroom);
		// sent message to OLD room
		socket.broadcast.to(socket.room).emit('new message', 'SERVER', socket.username+' has left this room');
		// update socket session room title
		socket.room = newroom;
		socket.broadcast.to(newroom).emit('new message', 'SERVER', socket.room+":  "+socket.username+' has joined this room');
		socket.emit('updaterooms', rooms, newroom);
                
                io.sockets.emit('new message', socket.username, socket.room+":  "+socket.username+' has joined this room');
                
                
console.log(newroom);
	});
        
});