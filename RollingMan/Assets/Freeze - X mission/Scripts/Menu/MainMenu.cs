using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	private bool touch = true;
	public string LinkToAbout;
	public Sprite[] Levels;
	public AudioClip SoundSwithOff;
	public AudioClip SoundSwithOn;

	private Camera Cam;

	void Start () {
		if (!PlayerPrefs.HasKey ("Sounds")) {
			PlayerPrefs.SetInt ("Sounds", 1);
		}
		AudioListener.pause = false;
		if (PlayerPrefs.GetInt ("Sounds") == 0) {
			GameObject.Find ("wSound").GetComponent<Animation> ().Play ("IFrom");
			AudioListener.pause = true;
		}

		if (!PlayerPrefs.HasKey ("iLevel")) {
			PlayerPrefs.SetInt ("iLevel", 1);
		}
		GameObject.Find("Level").GetComponent<SpriteRenderer>().sprite = Levels[PlayerPrefs.GetInt ("iLevel") - 1];

		Cam = GameObject.Find ("Camera").GetComponent<Camera>();
		StartCoroutine (PlayerBack());
	}

	IEnumerator PlayerBack(){
		yield return new WaitForSeconds (30);
		GameObject.Find ("Player").GetComponent<Rigidbody2D> ().AddForce (new Vector2(0,100f));
		yield return new WaitForSeconds (5);
		GameObject.Find ("Player").GetComponent<Rigidbody2D> ().AddForce (new Vector2(400,50f));
	}


	public void Sound(){
		if (touch) {
			touch = !touch;
			if (PlayerPrefs.GetInt ("Sounds") == 0) {
				GameObject.Find ("SOUNDS S").GetComponent<Animation> ().Play ("ITo");
				if (SoundSwithOn != null)
					GameObject.Find ("wSound").GetComponent<AudioSource> ().PlayOneShot (SoundSwithOn);
				PlayerPrefs.SetInt ("Sounds", 1);
			} else {
				GameObject.Find ("SOUNDS S").GetComponent<SpriteRenderer> ().color = new Color (1,1,1,0);
				if (SoundSwithOff != null)
					GameObject.Find ("wSound").GetComponent<AudioSource> ().PlayOneShot (SoundSwithOff);
				PlayerPrefs.SetInt ("Sounds", 0);
			}
			StartCoroutine (AwaitSound());
		}
	}

	IEnumerator AwaitSound(){
		yield return new WaitForSeconds (0.2f);
		AudioListener.pause = !AudioListener.pause;
		StartCoroutine (AwaitTouch());
	}

	public void About(){
		if (touch) {
			touch = !touch;
			Application.OpenURL (LinkToAbout);
			StartCoroutine (AwaitTouch());
		}
	}

	public void Exit(){
		Application.Quit ();
	}

	IEnumerator AwaitTouch(){
		yield return new WaitForSeconds (0.5f);
		touch = !touch;
	}

	public void Play(){
		if (touch) {
			touch = !touch;
			StartCoroutine (AwaitPlay ());
		}
	}

	IEnumerator AwaitPlay(){
		GameObject.Find ("iFon").GetComponent<Animation> ().Play ("from iFon");
		GameObject.Find ("PlayObj").GetComponent<AudioSource> ().Play ();
		float camCount = 1f;
		while (camCount > 0) {
			Cam.transform.position += new Vector3 (0.02f, 0, 0);
			camCount -= 0.02f;
			yield return new WaitForFixedUpdate ();
		}
		SceneManager.LoadScene ("Levels");
	}




	

}
