using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {
	
	public enum Direction {None, Left , Right , Up , Down};
	public float speed = 1;
	private GameObject endRotation;

	void Start(){
		endRotation = new GameObject();
	}

	public void Update(){
		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, endRotation.transform.rotation, Time.deltaTime*speed);
	}

	public void RotateCube(Direction enumDir){
				
		switch (enumDir)
			{
			case Direction.Down :
				Debug.Log("down");
				endRotation.transform.Rotate(Vector3.left, -90, Space.World);
			break;

			case Direction.Up :
				Debug.Log("UP");
				endRotation.transform.Rotate(Vector3.left, 90, Space.World);	
			break;

			case Direction.Right :
				Debug.Log("Right");
				endRotation.transform.Rotate(Vector3.up, -90, Space.World);
			break;

			case Direction.Left :
				Debug.Log("Left");
				endRotation.transform.Rotate(Vector3.up, 90, Space.World);
			break;
			}

	}
}
