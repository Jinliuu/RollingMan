using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightEffect : MonoBehaviour {

    
    public bool ifCool = false;
    Animator myAnimator;
    public GameObject player;
    public GameObject cdCounter;
    CDcounter cdcounter;
	// Use this for initialization
	void Start () {
        myAnimator = GetComponent<Animator>();
        cdcounter = cdCounter.GetComponent<CDcounter>();
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = player.transform.position;

        if (!ifCool)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myAnimator.SetTrigger("flash");
                ifCool = true;
                cdcounter.cd = 6;
               
            }
        }
        
	}

    void disableLamb()
    {
      
    }
}
