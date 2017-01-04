using UnityEngine;
using System.Collections;

public class SwipeBase : MonoBehaviour {


	private float fingerStartTime  = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;

	private bool isSwipe = false;
	private float minSwipeDist  = 50.0f;
	private float maxSwipeTime = 0.5f;

	//inside class
	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;

	public virtual void Update(){
		SwipeMouse();
	}

	public void SwipeMouse()
	{
		if(Input.GetMouseButtonDown(0))
		{
			//save began touch 2d point
			firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		}
		if(Input.GetMouseButtonUp(0))
		{
			//save ended touch 2d point
			secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);

			//create vector from the two points
			currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

			//normalize the 2d vector
			currentSwipe.Normalize();


			//swipe upwards
			if(currentSwipe.y > 0  && currentSwipe.x > -0.5f  && currentSwipe.x < 0.5f)
			{
				Debug.Log("up swipe");
				SwipeUp();

			}
			//swipe down
			if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
			{
				Debug.Log("down swipe");
				SwipeDown();

			}
			//swipe left
			if(currentSwipe.x < 0 && currentSwipe.y > -0.5f  &&currentSwipe.y < 0.5f)
			{
				Debug.Log("left swipe");

				SwipeLeft();

			}
			//swipe right
			if(currentSwipe.x > 0  && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
			{
				Debug.Log("right swipe");
				SwipeRight();

			}
		}
	}

	void SwipeTouch () {
		
		if (Input.touchCount > 0){
			foreach (Touch touch in Input.touches)
			{
				switch (touch.phase)
				{
				case TouchPhase.Began :
					/* this is a new touch */
					isSwipe = true;
					fingerStartTime = Time.time;
					fingerStartPos = touch.position;
					break;

				case TouchPhase.Canceled :
					/* The touch is being canceled */
					isSwipe = false;
					break;

				case TouchPhase.Ended :

					float gestureTime = Time.time - fingerStartTime;
					float gestureDist = (touch.position - fingerStartPos).magnitude;

					if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist){
						Vector2 direction = touch.position - fingerStartPos;
						Vector2 swipeType = Vector2.zero;

						if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
							// the swipe is horizontal:
							swipeType = Vector2.right * Mathf.Sign(direction.x);
						}else{
							// the swipe is vertical:
							swipeType = Vector2.up * Mathf.Sign(direction.y);
						}

						if(swipeType.x != 0.0f){
							if(swipeType.x > 0.0f){
								// MOVE RIGHT
								SwipeRight();
							}else{
								// MOVE LEFT
								SwipeLeft();
							}
						}

						if(swipeType.y != 0.0f ){
							if(swipeType.y > 0.0f){
								// MOVE UP
								SwipeUp();
							}else{
								// MOVE DOWN
								SwipeDown();
							}
						}
					}
					break;
				}//end switch
			}//end for
		}//end if
	}//end update

	public virtual void SwipeUp(){

	}
	public virtual void SwipeDown(){

	}
	public virtual void SwipeLeft(){

	}
	public virtual void SwipeRight(){
		
	}
} 