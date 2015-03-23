using UnityEngine;
using System.Collections;

public class MoveCtrl : MonoBehaviour {

	private Transform tr;
	Vector3 pre_tr;
	private CharacterController _controller;

	public float h = 0.0f;
	public float v = 0.0f;

	public float movSpeed = 5.0f;
	public float rotSpeed = 50.0f;

	private Vector3 movDir = Vector3.zero;

	private string ClientID;


	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		pre_tr = t2v(tr);
		_controller = GetComponent<CharacterController> ();
		ClientID = ClientState.id;
	}

	public void setMove(string id,Vector3 pos){
		if(ClientID!=id)
			tr.TransformPoint(pos);
	}

	string data;
	// Update is called once per frame
	void Update () {
		if (ClientID == gameObject.name) {//움직이는게 나일때
			h = Input.GetAxis ("Horizontal");
			v = Input.GetAxis ("Vertical");

			tr.Rotate (Vector3.up * Input.GetAxis ("Mouse X") * rotSpeed * Time.deltaTime);
			movDir = (tr.forward * v) + (tr.right * h);
			movDir.y -= 20f * Time.deltaTime;

			_controller.Move (movDir * movSpeed * Time.deltaTime);//일단 내걸 움직인다.

			if(!isSame (tr,pre_tr)){//내 위치에 변화가 생길경우( 랙가능성 )
				data = ClientID+":"+tr.position.x+","+tr.position.y+","+tr.position.z;
				SocketStarter.Socket.Emit("movePlayerREQ",data);//내위치를 서버에 알린다.
				pre_tr = t2v(tr);
			}
		}
	}



	bool isSame(Transform a,Vector3 b){
		if (a.position.x == b.x &&
						a.position.y == b.y &&
						a.position.z == b.z)
						return true;
				else
						return false;
	}

	Vector3 t2v(Transform t){
		Vector3 a;
		a.x = t.position.x;
		a.y = t.position.y;
		a.z = t.position.z;
		return a;
	}
}