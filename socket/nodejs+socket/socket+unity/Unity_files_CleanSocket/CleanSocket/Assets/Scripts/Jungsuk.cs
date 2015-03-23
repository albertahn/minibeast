using UnityEngine;
using System.Collections;
using SocketIOClient;
using System.Collections.Generic;

public class Jungsuk : MonoBehaviour {

	
	public SocketIOClient.Client socket = null;
	// Use this for initialization
	void Start () {


		socket = new SocketIOClient.Client("http://127.0.0.1:3000/");

		socket.On("connect", (fn) => {
			
			
			//Debug.Log ("connect - socket");
			
			//Dictionary<string, string> args = new Dictionary<string, string>();
			//args.Add("send message", "what's up?");

			socket.Emit("send message", "yo new albert socket in town");
			
			//socket.Emit("send message", "yo new socket in town");
			
			socket.Message += SocketMessage;
			
		});

	

		socket.Connect();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void SocketMessage (object sender, MessageEventArgs e) {
		if ( e!= null && e.Message.Event == " new message") {
		//	string msg = e.Message.MessageText;

		

			Debug.Log(""+e.ToString() );
		}
	}
	
	
	
}
