using UnityEngine;
using System.Collections;

public class SoundCollision : MonoBehaviour {


	public float Convertation_force;
	public float Max_force;
	public float Min_force;
	public float force_coll;
	private bool play = false;

	void OnCollisionEnter2D(Collision2D other){
		if (!play) {
			play = true;
			force_coll = (GetComponent<Rigidbody2D> ().velocity.x + GetComponent<Rigidbody2D> ().velocity.y);
			if (force_coll < 0) {
				force_coll *= -1;
			}
			force_coll = (force_coll - Min_force) / (Max_force - Min_force);
			if (force_coll > 0) {
				GetComponent<AudioSource> ().pitch = Random.Range (0.9f, 1f);
				GetComponent<AudioSource> ().volume = force_coll * Convertation_force;
				GetComponent<AudioSource> ().Play ();
			}
			StartCoroutine (Await ());
		}
	}


	IEnumerator Await(){
		yield return new WaitForSeconds (0.1f);
		play = false;
	}




}
