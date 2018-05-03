using UnityEngine;
using System.Collections;

public class TouchEvents : MonoBehaviour {

	public MainMenu Base;

	public bool Sound = false;
	public bool About = false;
	public bool Ratings = false;
	public bool Exit = false;
	public bool Play = false;


	void OnMouseDown(){
		if (Sound){
			Base.Sound ();
		}
		if (About) {
			Base.About ();
		}
		if (Exit) {
			Base.Exit ();
		}
		if (Play) {
			Base.Play ();
		}
	}




}
