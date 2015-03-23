var app = require('http').createServer(handler)
  , io = require('socket.io').listen(app)
  , fs = require('fs')

app.listen(80);

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

io.sockets.on('connection', function (socket) {		//클라이언트가 연결되는 경우 함수를 실행한다.
    socket.emit('news', { hello: 'world' });		//클라이언트에게 news라는 이름으로 JSON스트링을 전송한다.
    socket.on('my other event', function (data) {	//클라이언트로부터 my other event라는 이름의 JSON이 전송되기를 기다린다.
        console.log(data);
    });
});