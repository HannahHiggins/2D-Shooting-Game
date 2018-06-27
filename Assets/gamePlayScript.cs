using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gamePlayScript : MonoBehaviour {
	public GameObject gameOverPanel;
	AudioSource gunShotAudioSource;
	public AudioClip gunShotClip;
	bool gameIsPaused = false;

	public ParticleSystem muzzleFlashPar;

	public GameObject timerTextView;
	public GameObject scoreTextView;
	int roundsShot = 0;
	int enemyKilled = 0;

	public GameObject bazookaGo;
	public GameObject crossHairGo;

	public int gameTime;
	int timeMinutes;
	int timeSeconds;

	// Use this for initialization
	void Start () {
		scoreTextView.GetComponent<Text> ().text = enemyKilled.ToString () + " / " + roundsShot.ToString ();
		gunShotAudioSource = gameObject.GetComponent<AudioSource> ();
		InvokeRepeating ("timeCounterAction", 0, 1);
	}
	
	void timeCounterAction(){
		if (gameTime >= 0) {
			timeMinutes = (int)(gameTime / 60);
			timeSeconds = gameTime % 60;
			string timerTextString = timeMinutes.ToString ("D1") + "\' " + timeSeconds.ToString ("D2") + "\"";
			timerTextView.GetComponent<Text> ().text = timerTextString;
			gameTime = gameTime - 1;
		} else {
			Time.timeScale = 0;
			gameIsPaused = true;
			gameOverPanel.GetComponent<CanvasGroup> ().alpha = 1;
			gameOverPanel.GetComponent<CanvasGroup> ().interactable = true;
			gameOverPanel.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		}
	}

	void shootAction(){
		gunShotAudioSource.PlayOneShot (gunShotClip);
		muzzleFlashPar.Emit (1);
		roundsShot++;

		Vector2 dir = new Vector2 (crossHairGo.transform.position.x, crossHairGo.transform.position.y);
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.transform.position, dir);
		if (hit.collider != null && hit.collider.gameObject != crossHairGo) {
			enemyKilled++;
			Destroy (hit.collider.gameObject);
		}

		scoreTextView.GetComponent<Text> ().text = enemyKilled.ToString () + " / " + roundsShot.ToString ();
	}

	// Update is called once per frame
	void Update () {
		if (!gameIsPaused) {
			if (Input.GetKeyUp (KeyCode.Space)) {
				shootAction ();
			}

			bazookaGo.transform.LookAt (crossHairGo.transform);

			float h = Input.GetAxis ("Mouse X") / 5;
			float v = Input.GetAxis ("Mouse Y") / 5;

			Vector3 crossHairMov = new Vector3 (h, v, 0);
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

	public void RestartAction(){
		Time.timeScale = 1;
		gameIsPaused = false;
		gameOverPanel.GetComponent<CanvasGroup> ().alpha = 0;
		gameOverPanel.GetComponent<CanvasGroup> ().interactable = false;
		gameOverPanel.GetComponent<CanvasGroup> ().blocksRaycasts = false;

		gameTime = 150;
		enemyKilled = 0;
		roundsShot = 0;
		scoreTextView.GetComponent<Text> ().text = enemyKilled.ToString () + " / " + roundsShot.ToString ();
		timeCounterAction ();
	}
}
