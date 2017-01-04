using UnityEngine;
using System.Collections;

public class RollBall : MonoBehaviour {

//	public float power;
//	private Vector3 startPos;
//	Rigidbody rb;
//	float scaler;
//
//	// Use this for initialization
//	void Start () {
//		rb = GetComponent<Rigidbody>();
//		rb.sleepThreshold = 0.01f;
//	}
//	
//	// Update is called once per frame
//	void FixedUpdate () {
//		//Debug.Log(rb.angularVelocity.x + " : " + rb.angularVelocity.y + " : " + rb.angularVelocity.z);
//		rb.inertiaTensorRotation =new Quaternion(0.01f, 0.01f, 0.01f, 1);
//		rb.AddTorque(-rb.angularVelocity * scaler);
//	}
	Vector3 startPos;
	float startTime;
	public float modifier;
	public bool ballRolling;


    private float dist;
    private bool dragging = false;
    private Vector3 offset;
    private Transform toDrag;

	public AudioClip rollballsound, swooshsound;

	public AudioSource audioSourceRef;


	Vector3 firstpos, lastpos, currentpos; 
	Vector3 force; 
	Vector3 startpos; 
	float starttime, endtime, currenttime;
	public Vector3 gravityVector;


    void Update () 
	{
       
//		if (Input.GetAxis("Mouse X") != 0 && !ballRolling){
//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//			RaycastHit hit;
//
//			if( Physics.Raycast( ray, out hit ) ){
//
//				transform.position = new Vector3(hit.point.x,transform.position.y,transform.position.z);
//			}
//		}
		/////////////////////////////

		if (Input.GetMouseButtonDown(0))
		{
			ballRolling = true;
			//Store initial values
			startPos = Input.mousePosition;
			startTime = Time.time;
			Debug.Log("down");

		}
		if(dragging == false){
			if (Input.GetMouseButtonUp(0))
			{
				//Get end values
				Vector3 endPos = Input.mousePosition;
				float endTime = Time.time;
				Debug.Log("up");


				audioSourceRef.PlayOneShot(swooshsound);

				audioSourceRef.PlayOneShot(rollballsound);
				//Mouse positions distance from camera. Might be a better idea to use the cameras near plane
				startPos.z = 0.1f;
				endPos.z = 0.1f;

				//Makes the input pixel density independent
				startPos = Camera.main.ScreenToWorldPoint(startPos);
				endPos = Camera.main.ScreenToWorldPoint(endPos);

				//The duration of the swipe
				float duration = endTime - startTime;

				//The direction of the swipe
				Vector3 dir = endPos - startPos;

				//The distance of the swipe
				float distance = dir.magnitude *1.5f;

				//Faster or longer swipes give higher power
				float power = distance / duration;
				//float power2 = distance *

				//expected values are what power you get when you try 
				//desired values are what you want
				//you might want these as public values so they can be set from the inspector
				const float expectedMin = 50;
				const float expectedMax = 60;
				const float desiredMin = 15;
				const float desiredMax = 20;

				//Measure expected power here
				//Debug.Log(power);

				//change power from the range 50...60 to 0...1
				//			power -= expectedMin;
				//			power /= expectedMax - expectedMin;
				//
				//			//clamp value to between 0 and 1
				//			power = Mathf.Clamp01(power);
				//
				//			//change power to the range 15...20
				//			power *= desiredMax - desiredMin;
				//			power += desiredMin;

				//take the direction from the swipe. length of the vector is the power
				//Vector3 velocity = (transform.rotation * dir).normalized * power * modifier;

				Vector3 velocity = (transform.rotation * dir).normalized  * ( distance * modifier);
				Debug.Log(distance + " " + velocity + " ");
				//GetComponent<Rigidbody>().AddForce(velocity);
				GetComponent<Rigidbody>().AddForce(new Vector3(velocity.x,0,velocity.z));


				Destroy(gameObject,5);

				//BallManager.ResetZoom = false;
				//BallManager.zoomInToBall = true;
			}
		}
		/////////////////////////////////////

//		Vector3 v3;
//
//		if (Input.touchCount != 1)
//		{
//			dragging = false;
//			return;
//		}
//
//		Touch touch = Input.touches[0];
//		Vector3 pos = touch.position;
//
//		if (touch.phase == TouchPhase.Began)
//		{
//			RaycastHit hit;
//			Ray ray = Camera.main.ScreenPointToRay(pos);
//			if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "RollBall"))
//			{
//				Debug.Log("Here");
//				toDrag = hit.transform;
//				dist = hit.transform.position.z - Camera.main.transform.position.z;
//				v3 = new Vector3(pos.x, 0, dist);
//				v3 = Camera.main.ScreenToWorldPoint(v3);
//				offset = toDrag.position - v3;
//				dragging = true;
//			}
//		}
//		if (dragging  && touch.phase == TouchPhase.Moved)
//		{
//			Debug.Log("move");
//
//			v3 = new Vector3(Input.mousePosition.x, 0, dist);
//			v3 = Camera.main.ScreenToWorldPoint(v3);
//			toDrag.position = v3 + offset ;
//		}
//		if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
//		{
//			Debug.Log("no move");
//			//dragging = false;
//		}
//







		////////////////

//		movedownY = Input.GetAxis("Mouse X") * sensitivityY;
//		if (Input.GetAxis("Mouse X") != 0){
//			transform.Translate(Vector3.right * movedownY);
//		}
//		movedownY = 0;

	

	}

	void OnDestroy(){
		//PawBall.findRotation = true;
		//BallManager.zoomInToBall = false;
		//BallManager.ResetZoom = true;
	}
	void OnMouseDrag()
	{
		currenttime = Time.time;
		currentpos = Input.mousePosition;
		Vector3 distance = currentpos - startPos;
		//Debug.Log(distance); Debug.Log(distance);

		distance.z = distance.magnitude;
		// Vector3 force = (distance / ((currenttime - starttime) / .17f));
		force = distance * .05f;
		//  Debug.Log(force);
		if (force.magnitude > 0)
		{
			UpdateTrajectory(transform.position, force, gravityVector);

		}


	}


	void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
	{
		int numSteps = 20; // for example
		float timeDelta = 1.0f / initialVelocity.magnitude; // for example

		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount(numSteps);

		Vector3 position = initialPosition;
		Vector3 velocity = initialVelocity;
		for (int i = 0; i < numSteps; ++i)
		{
			lineRenderer.SetPosition(i, position);

			position += velocity * timeDelta ;//+ 0.5f * timeDelta * timeDelta;
//			velocity += gravity * timeDelta;
		}
	}

//	void OnMouseDown() {
//		startPos = Input.mousePosition;
//		startPos.z = transform.position.z - Camera.main.transform.position.z;
//
//		startPos = Camera.main.ScreenToWorldPoint(startPos);
//	}
//
//	void OnMouseUp() {
//		var endPos = Input.mousePosition;
//		endPos.z = transform.position.z - Camera.main.transform.position.z;
//		endPos = Camera.main.ScreenToWorldPoint(endPos);
//
//		var force = endPos - startPos;
//		force.z = force.magnitude;
//		force.Normalize();
//
//		GetComponent<Rigidbody>().AddForce(force * power);
//		//ReturnBall();
//	}

//	void ReturnBall() {
//		     yield WaitForSeconds(4.0);
//		     transform.position = Vector3.zero;
//		     GetComponent.<Rigidbody>().velocity = Vector3.zero;
//	}
}
