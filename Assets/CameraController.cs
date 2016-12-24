using UnityEngine;
using System.Collections;
using System.IO;

public class CameraController : MonoBehaviour
{
	public WebCamTexture mCamera = null;
	public GameObject plane,plane2;
	public Texture2D testTexture;
	// Use this for initialization

	WebCamTexture _CamTex;
	///private string _SavePath = Application.dataPath;
	int _CaptureCounter = 0;


	public Texture2D TakePic(){

		//Texture2D PhotoTaken= new Texture2D(mCamera.width, mCamera.height);
		Texture2D PhotoTaken= new Texture2D(mCamera.width, mCamera.height);

		PhotoTaken.SetPixels(mCamera.GetPixels());
		PhotoTaken.Apply();

		return PhotoTaken;

	}
	void Update(){
		//add sides array of pics
		//auto rotate to correct side, wait for click to load pic on tht side
		//arrow keys move cube
		if(Input.GetKey(KeyCode.Space)){
			StopAllCoroutines();
		
			//Texture2D PhotoTaken= new Texture2D(mCamera.width, mCamera.height);
			Texture2D PhotoTaken= new Texture2D(mCamera.width, mCamera.height);

			PhotoTaken.SetPixels(mCamera.GetPixels());
			PhotoTaken.Apply();

			mCamera.Stop ();

			plane2.GetComponent<Renderer>().material.mainTexture = mCamera;

			//var bytes1 = testTexture.EncodeToPNG ();
			var bytes1 = PhotoTaken.EncodeToPNG ();
			var path = System.IO.Path.Combine("Assets/Resources/" , "gloopity gloop" + ".png");
			System.IO.File.WriteAllBytes (path, bytes1);
			Debug.Log(path);

		}
	}


	IEnumerator Start ()
	{

		//SaveTextureToFile(testTexture, "wooo");
		//TakeSnapshot();
		mCamera = new WebCamTexture ();
		plane.GetComponent<Renderer>().material.mainTexture = mCamera;

		mCamera.Play ();
		if(mCamera.width <= 16){ 
			while(!mCamera.didUpdateThisFrame){ 
				yield return new WaitForEndOfFrame(); 
			} 
			mCamera.Pause(); 
			Color32[] colors = mCamera.GetPixels32(); 
			mCamera.Stop();

			yield return new WaitForEndOfFrame();

			mCamera.Play(); 
		}
	}
}