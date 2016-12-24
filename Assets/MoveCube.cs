using UnityEngine;
using System.Collections;

public class MoveCube : MonoBehaviour {

	public float speed = 1;
	private GameObject endRotation;

	void Start(){
		endRotation = new GameObject();
	}



	void Update () {

//		float dot = Vector3.Dot(transform.forward, (Camera.main.transform.position - transform.position).normalized);
//		if(dot > 0.7f) { Debug.Log("Quite facing");}

		//iterating through the faces:

		//faceNormal[i].dot(-worldViewVector) > 0.8 //(should actually be >0.9999)


		//Take the dot-product of the view-vector (imagine an arrow coming out of the camera) and the surface normal. If it's negative, then the face is visible.

		if( Input.GetKeyDown( KeyCode.RightArrow ) ){
			endRotation.transform.Rotate(Vector3.forward, 90, Space.World);
		}
		if( Input.GetKeyDown( KeyCode.LeftArrow ) ){
			endRotation.transform.Rotate(Vector3.forward, -90, Space.World);
		}


		if( Input.GetKeyDown( "a" ) ){
			endRotation.transform.Rotate(Vector3.up, 90, Space.World);
		}
		if( Input.GetKeyDown( "d" ) ){
			endRotation.transform.Rotate(Vector3.up, -90, Space.World);
		}


		if( Input.GetKeyDown( "w" ) ){
			endRotation.transform.Rotate(Vector3.left, 90, Space.World);
		}
		if( Input.GetKeyDown( "s") ){
			endRotation.transform.Rotate(Vector3.left, -90, Space.World);
		}

		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, endRotation.transform.rotation, Time.deltaTime*speed);

	}
}
