using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gamePlayScript : MonoBehaviour {
	AudioSource gunShotAudioSource;
	public AudioClip gunShotClip;

	public GameObject timerTextView;
	public GameObject bazookaGo;
	public GameObject crossHairGo;

	public int gameTime;
	int timeMinutes;
	int timeSeconds;

	// Use this for initialization
	void Start () {
		gunShotAudioSource = gameObject.GetComponent<AudioSource> ();
		InvokeRepeating ("timeCounterAction", 0, 1);
	}
	
	void timeCounterAction(){
		if (gameTime > 0) {
			timeMinutes = (int)(gameTime / 60);
			timeSeconds = gameTime % 60;
			string timerTextString = timeMinutes.ToString ("D1") + "\' " + timeSeconds.ToString ("D2") + "\"";
			timerTextView.GetComponent<Text> ().text = timerTextString;
			gameTime = gameTime - 1;
		} else {
			Debug.Log ("Game is Over");
		}
	}

	void shootAction(){
		Debug.Log ("Shoot");
		gunShotAudioSource.PlayOneShot (gunShotClip);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Space)) {
			shootAction ();
		}

		bazookaGo.transform.LookAt (crossHairGo.transform);

		float h = Input.GetAxis ("Mouse X") / 5;
		float v = Input.GetAxis ("Mouse Y") / 5;

		Vector3 crossHairMov = new Vector3(h, v, 0);
		crossHairGo.transform.Translate (crossHairMov);

		float x = crossHairGo.transform.position.x;
		float y = crossHairGo.transform.position.y;

		if (x > 10)
			x = 10;
		if (x < -10)
			x = -10;
		if (y > 7)
			y = 7;
		if (y < -5)
			y = -5;

		crossHairGo.transform.position = new Vector3 (x, y, 0);
	}
}
