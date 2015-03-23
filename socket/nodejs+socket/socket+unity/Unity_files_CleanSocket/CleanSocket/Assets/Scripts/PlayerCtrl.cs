using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;

using System.Collections.Generic;

public class PlayerCtrl : MonoBehaviour {


	private Transform tr;

	public float normalSpeed = 10.0f;
	public float dashSpeed = 20.0f;

	public float moveSpeed = 10.0f;
	public float rotSpeed = 100.0f;

	public int hp = 100;
	public int maxHp = 100;


	public Material trail;

	public SocketNetworkFucker NetworkFucker;// = GetComponent<SocketNetworkFucker> ();
	//socket

	
	void Start () {

		NetworkFucker = GameObject.Find("Main Camera").GetComponent<SocketNetworkFucker> ();
		tr = this.transform;


	}

	
	// Update is called once per frame
	void Update () {


		if (Input.GetKey (KeyCode.W)) {

			tr.Translate(Vector3.forward * normalSpeed * Time.deltaTime);		


			//NetworkFucker.sendfukMessage ("forwads");

			Dictionary<string, string> args = new Dictionary<string, string>();

			args.Add("zcock", ""+tr.position.z );
			
			args.Add("xcock", ""+tr.position.x );

			//Debug.Log("cokit: "+ tr.position.z);

			NetworkFucker.sendfukMessage (args);

		}

		if (Input.GetKey (KeyCode.S) ){
			tr.Translate(-Vector3.forward * normalSpeed*Time.deltaTime);	

		//	NetworkFucker.sendfukMessage ("{\"z\": \""+tr.position.z+"\"}");

			//JSONObject obj = new JSONObject();
			//obj.Add("zcock",tr.position.z );

			Dictionary<string, string> args = new Dictionary<string, string>();
			args.Add("zcock", ""+tr.position.z );

			args.Add("xcock", ""+tr.position.x );
			
			NetworkFucker.sendfukMessage (args);
		}

		if (Input.GetKey (KeyCode.D) ){
			tr.Translate(Vector3.right * normalSpeed*Time.deltaTime);		

		

			Dictionary<string, string> args = new Dictionary<string, string>();
			args.Add("zcock", ""+tr.position.z );
			
			args.Add("xcock", ""+tr.position.x );
			//Debug.Log("cockmania"+ obj.ToString());
			
			NetworkFucker.sendfukMessage (args);
		}

		if (Input.GetKey (KeyCode.A)) {
			tr.Translate(-Vector3.right * normalSpeed*Time.deltaTime);	

			//NetworkFucker.sendfukMessage ("{\"x\": \""+tr.position.x+"\"}");

			
			
			Dictionary<string, string> args = new Dictionary<string, string>();
			args.Add("zcock", ""+tr.position.z );
			
			args.Add("xcock", ""+tr.position.x );
			//Debug.Log("cockmania"+ obj.ToString());
			
			NetworkFucker.sendfukMessage (args);
		}
	}




}
