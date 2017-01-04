using UnityEngine;
using System.Collections;

public class CameraNavigation : MonoBehaviour {
	
	public Transform[] navPoints;
	private Vector3 pointToMove;
	private Quaternion pointToRotate;
	public float speed;


	public void MoveToNavPoint(int index){
		Debug.Log("index " + index);
		StopAllCoroutines();
		pointToMove = navPoints[index].transform.position;
		pointToRotate = navPoints[index].transform.rotation;
		StartCoroutine(MovingToPoint());

	}
	IEnumerator MovingToPoint(){

		while(true){
			Debug.Log(pointToMove);
			Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, pointToMove, Time.deltaTime * speed);
			if(Vector3.Distance(Camera.main.transform.position, pointToMove) < .5f){
				Camera.main.transform.position = pointToMove;
				Debug.Log("less than .5");

			}
			if(Vector3.Distance(Camera.main.transform.position, pointToMove) == 0){
				Debug.Log("at zero");

				break;
			}
			yield return null;
		}

	}


}
