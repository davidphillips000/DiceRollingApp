using UnityEngine;
using System.Collections;

public class DiceSwipe : SwipeBase {


	public RotateObject moveCube;

	public override void Update ()
	{
		base.Update ();
	}
	public override void SwipeDown ()
	{
		moveCube.RotateCube( RotateObject.Direction.Down);
	}
	public override void SwipeUp ()
	{
		moveCube.RotateCube( RotateObject.Direction.Up);

	}
	public override void SwipeLeft ()
	{
		moveCube.RotateCube( RotateObject.Direction.Left);

	}
	public override void SwipeRight ()
	{
		moveCube.RotateCube( RotateObject.Direction.Right);

	}
}
