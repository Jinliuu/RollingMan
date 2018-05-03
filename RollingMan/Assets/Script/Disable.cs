using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour {

    public List<GameObject> listG;
	// Use this for initialization
	void Start () {
		foreach(GameObject a in listG)
        {
            a.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
