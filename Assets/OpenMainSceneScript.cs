using UnityEngine;
using System.Collections;

public class OpenMainSceneScript : MonoBehaviour {

	public void GoToNextSceneAction () {
		Application.LoadLevel ("GamePlay Scene");
	}
}
