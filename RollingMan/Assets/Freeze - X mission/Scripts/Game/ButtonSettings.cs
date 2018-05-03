using UnityEngine;
using System.Collections;

public class ButtonSettings : MonoBehaviour {

	public Sprite ButtonOn;
	public Sprite ButtonOff;
	public AudioClip ButtonOffSound;
	public AudioClip ButtonOnSound;
    public bool specialLevel;
    public GameObject wall1;
    public GameObject wall2;
	private Vector2 Once = new Vector2(0, 0);

	[Header ("Method 1")]
	public bool StopSame = false;
	[Header ("Method 2")]
	public bool StopPlay = false;
	public bool RotateControl = true;
	[Header ("Method 3")]
	public bool TransformOfObject = false;
	public Vector2 MoveTransform;
	public float MoveSpeed;

	private Vector2 SaveMT;
	private int i = 0;
	private bool s = false;
	private bool back = false;

	public Transform SubControl;


	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			if (Once.x == 0 && Once.y == 0){
				//Once = new Vector2(1, Once.y);
				if (GetComponent<SpriteRenderer> ().sprite == ButtonOff) {
					GetComponent<SpriteRenderer> ().sprite = ButtonOn;
					GetComponent<AudioSource> ().PlayOneShot (ButtonOnSound);
					CallProc();
				}else if (GetComponent<SpriteRenderer> ().sprite == ButtonOn) {
                    CallProc();
                    GetComponent<SpriteRenderer>().sprite = ButtonOff;
                    GetComponent<AudioSource>().PlayOneShot(ButtonOffSound);
                }
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		/*if (other.gameObject.name == "Player") {
			Once = new Vector2 (0, Once.y);
			if (back) {
				GetComponent<SpriteRenderer> ().sprite = ButtonOff;
				GetComponent<AudioSource> ().PlayOneShot (ButtonOffSound);
				back = false;
				CallProc();
			}
		}*/
	}

	void CallProc(){
		if (StopSame){
			SubControl.GetComponent<DangerBase> ().Move = !SubControl.GetComponent<DangerBase> ().Move;
			SubControl.GetComponent<DangerBase> ().Rotate = !SubControl.GetComponent<DangerBase> ().Rotate;
			Once = new Vector2(Once.x, 0);
		}
		if (StopPlay){
			SubControl.GetComponent<DangerBase> ().FolowToPlayer = !SubControl.GetComponent<DangerBase> ().FolowToPlayer;
			if (RotateControl)
				SubControl.GetComponent<DangerBase> ().Rotate = !SubControl.GetComponent<DangerBase> ().Rotate;
			if (SubControl.GetComponent<DangerBase> ().FolowToPlayer)
				SubControl.GetComponent<AudioSource> ().Play ();
			else
				SubControl.GetComponent<AudioSource> ().Stop ();
			Once = new Vector2(Once.x, 0);
		}
		if (TransformOfObject) {
			SaveMT = MoveTransform;
			if (SaveMT.x < 0)
				SaveMT = new Vector2 (-SaveMT.x, SaveMT.y);
			if (SaveMT.y < 0)
				SaveMT = new Vector2 (SaveMT.x, -SaveMT.y);
			Once = new Vector2(Once.x, 1);
			if (i == 0)
				i = -1;
			else if (i == -1)
				i = 1;
			else
				i = -1;
			s = true;
		}

        if (specialLevel)
        {
            wall1.GetComponent<Animator>().SetTrigger("button");
            wall2.GetComponent<Animator>().SetTrigger("button");
        }
	}

	void FixedUpdate(){
		if (s) {
			if (SaveMT.x > 0 || SaveMT.y > 0) {
				if (i == 1) {
					if (MoveTransform.x > 0) {
						SubControl.localPosition -= new Vector3 (MoveSpeed, 0, 0);
						SaveMT -= new Vector2 (MoveSpeed, 0);
					}
					if (MoveTransform.x < 0) {
						SubControl.localPosition += new Vector3 (MoveSpeed, 0, 0);
						SaveMT -= new Vector2 (MoveSpeed, 0);
					}
					if (MoveTransform.y > 0) {
						SubControl.localPosition -= new Vector3 (0, MoveSpeed, 0);
						SaveMT -= new Vector2 (0, MoveSpeed);
					}
					if (MoveTransform.y < 0) {
						SubControl.localPosition += new Vector3 (0, MoveSpeed, 0);
						SaveMT -= new Vector2 (0, MoveSpeed);
					}
				}
				
				if (i == -1) {
					if (MoveTransform.x > 0) {
						SubControl.localPosition += new Vector3 (MoveSpeed, 0, 0);
						SaveMT -= new Vector2 (MoveSpeed, 0);
					}
					if (MoveTransform.x < 0) {
						SubControl.localPosition -= new Vector3 (MoveSpeed, 0, 0);
						SaveMT -= new Vector2 (MoveSpeed, 0);
					}
					if (MoveTransform.y > 0) {
						SubControl.localPosition += new Vector3 (0, MoveSpeed, 0);
						SaveMT -= new Vector2 (0, MoveSpeed);
					}
					if (MoveTransform.y < 0) {
						SubControl.localPosition -= new Vector3 (0, MoveSpeed, 0);
						SaveMT -= new Vector2 (0, MoveSpeed);
					}
				}
			} else {
				Once = new Vector2 (Once.x, 0);
				s = false;
			}
		}
	}


}
