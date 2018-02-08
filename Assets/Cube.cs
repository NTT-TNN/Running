using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour {
	public GameObject scoreObject;
	int score=0;
	float planeLengh = 5000; // độ dài của mỗi đường chạy khởi 
	float plane_X = -1355;
	float cube_Z;//ranger -450 -750
	float cube_X = -3000;// range >-3000
	float cubeScale_Z;
	float sphere_Z;//ranger -450 -750
	float sphere_X=-1554;// range >-2000
	float sphereScale_Z;
	float moveSpeed = 900f;// tốc độ theo chiều ngang
	float runSpeed = 400f; // tốc độ theo chiều 
	float jumpHeight = 60f;
	Color red = new Color (1, 0, 0, 1);
	Color yellow = new Color (1, 0.92f, 0.016f, 1);
	Color blue = new Color (0, 0, 1, 1);
	Color playerColor;
	public GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		playerColor = player.GetComponent<Renderer> ().material.color;
		print (playerColor);
	}
	
//	 Update is called once per frame
	void Update () {
		Vector3 currentPosition = transform.position;
		if (currentPosition.y <= 37.5) {
			transform.Translate (runSpeed* Time.deltaTime, jumpHeight*Input.GetAxis("Jump"),moveSpeed*Input.GetAxis("Vertical") * Time.deltaTime);
		}

		if (currentPosition.y > 37.5) {
			transform.position = new Vector3 (currentPosition.x + runSpeed* Time.deltaTime, (currentPosition.y-10f), currentPosition.z + runSpeed*Input.GetAxis("Vertical")* Time.deltaTime);
		}

		if (currentPosition.x > (cube_X - 1000)) {//nếu cube(player) đến cách cube cuối cùng 2000f thì sẽ tạo thêm cube ca thêm Sphere
			
			for(int i=0;i<100;++i) {
				//them cube
				var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				var boxCollider = (BoxCollider)cube.AddComponent<BoxCollider>();
				boxCollider.center = new Vector3(0,0,0);

				cube_Z = Random.Range(-750f, -450f);
				float distanceCube_X = Random.Range(400, 700);
				int flagColor=Random.Range(0,3);
				if (flagColor == 0) {
					cube.name = "red_cube";
					cube.GetComponent<Renderer> ().material.color = red;
				} else if (flagColor == 1) {
					cube.name = "blue_cube";
					cube.GetComponent<Renderer> ().material.color = blue;
				} else {
					cube.name = "yellow_cube";
					cube.GetComponent<Renderer>().material.color = yellow;
				}
				cube.transform.position = new Vector3(cube_X+distanceCube_X, .5f, cube_Z);
				cube_X = cube_X + distanceCube_X;
				int cubeWidth = Random.Range (100, 200);
				cube.transform.localScale = new Vector3 (20, 100, cubeWidth);
				//them Sphere
				int flagSphere=Random.Range(1,2);

				if (flagSphere == 1) {
					var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
					if (flagColor == 0) {
						sphere.name = "red_sphere";
						sphere.GetComponent<Renderer> ().material.color = red;
					} else if (flagColor == 1) {
						sphere.name = "blue_sphere";
						sphere.GetComponent<Renderer> ().material.color = blue;
					} else {
						sphere.name = "yellow_sphere";
						sphere.GetComponent<Renderer>().material.color = yellow;
					}

					var sphereCollider = (BoxCollider)sphere.AddComponent<BoxCollider>();
					sphereCollider.center = new Vector3(0,0,0);

					cube_Z = Random.Range(-750f, -450f);
					float distanceShepre_X = Random.Range(400, 700);
					sphere.transform.position = new Vector3(sphere_X+distanceShepre_X, .5f, cube_Z);
					sphere_X = sphere_X + distanceShepre_X;
					sphere.transform.localScale = new Vector3 (80, 80, 80);
				}
			}

		}
		//them plane
		if (currentPosition.x > (plane_X - 500)) {// nếu cube (player) đi gần hết plane thì thêm plane 
			var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
			plane.GetComponent<MeshCollider> ().enabled = false; 
			plane.GetComponent<Renderer>().material.color = new Color(0,1,1,1);
			plane.transform.position = new Vector3(plane_X+planeLengh, 0, -591);
			plane_X = plane_X + planeLengh;
			plane.transform.localScale = new Vector3 (500, 2, 50);
		}

		if (currentPosition.z < -860 || currentPosition.z > -320) {
			print ("out of land => dead");
			runSpeed = 0;
			moveSpeed = 0;
			jumpHeight = 0;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		print("collision" + collision.transform.name + "=> dead ");
		if (collision.transform.name == "blue_sphere" ) {
			player.GetComponent<Renderer> ().material.color = blue;
			playerColor = blue;
			score+=15;
			scoreObject.GetComponent<Text> ().text = score.ToString();// hien thi ra man hinh
//			print ("change to blue");
		} else if (collision.transform.name == "red_sphere") {
			player.GetComponent<Renderer> ().material.color = red;
			playerColor = red;
			score+=15;
			scoreObject.GetComponent<Text> ().text = score.ToString();
//			print ("change to red");
		} else if (collision.transform.name == "yellow_sphere" ) {
			player.GetComponent<Renderer> ().material.color = yellow;
			playerColor = yellow;
			score+=15;
			scoreObject.GetComponent<Text> ().text = score.ToString();
//			print ("change to yellow");
		} else if (collision.transform.name == "blue_cube" && playerColor == blue) {
			score+=5;
			scoreObject.GetComponent<Text> ().text = score.ToString();
		} else if (collision.transform.name == "red_cube" && playerColor == red) {
			score+=5;
			scoreObject.GetComponent<Text> ().text = score.ToString();
		} else if (collision.transform.name == "yellow_cube" && playerColor == yellow) {
			score+=5;
			scoreObject.GetComponent<Text> ().text = score.ToString();
		} else {
			moveSpeed = 0;
			runSpeed = 0;
			jumpHeight = 0;
		}
	}
	void OnCollisionExit(Collision collisionInfo) {
		print("Exit cube " + collisionInfo.transform.name);
	}
	void OnTriggerEnter(Collider collider)
	{
		print ("trigger");
	}
}
