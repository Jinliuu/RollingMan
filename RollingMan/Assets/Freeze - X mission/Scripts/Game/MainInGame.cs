using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainInGame : MonoBehaviour {


	public GameObject[] Levels;


	private static bool touch = false;
	private Camera Cam;

	private bool fix = false;
	public Sprite[] SpritesCount;
	[HideInInspector]
	public int fixCount;
	private GameObject CurrentLevel;

	private bool GameOver = false;

	IEnumerator Start () {
		CurrentLevel = Levels [PlayerPrefs.GetInt ("iLevel") - 1];
		CurrentLevel.SetActive (true);
		fixCount = Levels [PlayerPrefs.GetInt ("iLevel") - 1].GetComponent<PlatformRotation> ().CountOfFix;
		GameObject.Find ("FixCount").GetComponent<SpriteRenderer>().sprite = SpritesCount[fixCount];
		touch = false;
		Cam = GameObject.Find ("Camera").GetComponent<Camera>();
		if (GameObject.Find ("Player").GetComponent<Rigidbody2D> ().isKinematic)
			FIX ();
		yield return new WaitForSeconds (0.5f);
		touch = true;
	}

	public void Menu(){
		if (touch) {
			touch = false;
			StartCoroutine (AwaitMenu ());
		}
	}

	IEnumerator AwaitMenu(){
		Time.timeScale = 1;
		GameObject.Find ("iFon").GetComponent<Animation> ().Play ("from iFon");
		GameObject.Find ("ButtonMenu").GetComponent<AudioSource> ().Play ();
		float camCount = 1f;
		while (camCount > 0) {
			Cam.transform.position += new Vector3 (0.02f, 0, 0);
			camCount -= 0.02f;
			yield return new WaitForFixedUpdate ();
		}
		SceneManager.LoadScene ("Menu");
	}



	public void Pause(){
		if (touch && Time.timeScale != 0) {
			touch = false;
			StartCoroutine (AwaitPause ());
		}
	}

	IEnumerator AwaitPause(){
		GameObject.Find ("Pause").GetComponent<AudioSource> ().Play ();
		GameObject.Find ("Pause Panel").GetComponent<Animation> ().Play ("To Pause");
		GameObject.Find ("Music").GetComponent<AudioSource> ().pitch = 0;
		yield return new WaitForSeconds (0.4f);
		Time.timeScale = 0;
		AudioListener.pause = true;
		touch = true;
	}

	public void Continue(){
		if (touch && Time.timeScale == 0) {
			touch = false;
			StartCoroutine (AwaitContinue ());
		}
	}

	IEnumerator AwaitContinue(){
		if (PlayerPrefs.GetInt ("Sounds") == 1)
			AudioListener.pause = false;
		GameObject.Find ("Continue").GetComponent<AudioSource> ().Play ();
		GameObject.Find ("Pause Panel").GetComponent<Animation> ().Play ("From Pause");
		Time.timeScale = 1;
		GameObject.Find ("Music").GetComponent<AudioSource> ().pitch = 1;
		yield return new WaitForSeconds (0.4f);
		touch = true;
	}



	public void FIX(){
		if (fix == false) {
			if (fixCount > 0) {
				fixCount--;
				fix = !fix;
				GameObject.Find ("Fix").GetComponent<Animation> ().Play ("To Fix");
				StartCoroutine (AwaitFIX());
				CurrentLevel.GetComponent<PlatformRotation> ().Player.velocity = new Vector2 (0, 0);
				CurrentLevel.GetComponent<PlatformRotation> ().Player.angularVelocity = 0;
				CurrentLevel.GetComponent<PlatformRotation> ().Player.isKinematic = true;
				CurrentLevel.GetComponent<PlatformRotation> ().FixOn.SetActive(true);
				GameObject.Find ("Pause").GetComponent<AudioSource> ().Play ();
			}
		} else {
			fix = !fix;
			GameObject.Find ("Fix").GetComponent<Animation> ().Play ("From Fix");
			CurrentLevel.GetComponent<PlatformRotation> ().Player.isKinematic = false;
			CurrentLevel.GetComponent<PlatformRotation> ().FixOn.SetActive(false);
			GameObject.Find ("Continue").GetComponent<AudioSource> ().Play ();
		}
	}

	IEnumerator AwaitFIX(){
		yield return new WaitForSeconds (0.165f);
		GameObject.Find ("FixCount").GetComponent<SpriteRenderer>().sprite = SpritesCount[fixCount];
	}

	public void Next(){
		if (touch) {
			touch = false;
			StartCoroutine (AwaitNext ());
		}
	}

	IEnumerator AwaitNext(){
		Time.timeScale = 1;
		GameObject.Find ("iFon").GetComponent<Animation> ().Play ("from iFon");
		GameObject.Find ("ButtonMenu").GetComponent<AudioSource> ().Play ();
		float camCount = 1f;
		while (camCount > 0) {
			Cam.transform.position += new Vector3 (0.02f, 0, 0);
			camCount -= 0.02f;
			yield return new WaitForFixedUpdate ();
		}
		if (!PlayerPrefs.HasKey ("endLevels") || GameOver) {
			SceneManager.LoadScene ("Levels");
		} else {
			SceneManager.LoadScene ("Menu");
		}
	}


	IEnumerator AwaitGameOver(){
		yield return new WaitForSeconds (2);
		GameOver = true;
		Next ();
	}
}
