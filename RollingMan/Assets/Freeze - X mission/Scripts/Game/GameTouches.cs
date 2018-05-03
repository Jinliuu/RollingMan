using UnityEngine;
using System.Collections;


public class GameTouches : MonoBehaviour {

	public MainInGame Base;
	public bool Menu = false;
	public bool Pause = false;
	public bool Continue = false;
	public bool FIX = false;


	void OnMouseDown(){
		if (Menu){
			Base.Menu ();
		}
		if (Pause){
			Base.Pause ();
		}
		if (Continue){
			Base.Continue ();
		}
		if (FIX){
			Base.FIX ();
		}
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Base.Menu();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            
            Base.FIX();
        }
    }
}
