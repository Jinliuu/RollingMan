using UnityEngine;
using System.Collections;

public class CamNormalize : MonoBehaviour {

	void Start () {
		float Sides = (float) Screen.height / (float) Screen.width;

		if(Sides > 0.65f){
			GetComponent<Camera>().orthographicSize = 5.3f;
		} else if (Sides > 0.62f){
			GetComponent<Camera>().orthographicSize = 4.9f;
		} else if(Sides > 0.58f){
			GetComponent<Camera>().orthographicSize = 4.75f;
		} else if(Sides > 0.55f){
			GetComponent<Camera>().orthographicSize = 4.6f;
		}else{
			GetComponent<Camera>().orthographicSize = 4.5f;
		}
	}



}
