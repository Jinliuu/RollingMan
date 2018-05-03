using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDcounter : MonoBehaviour {
    public Sprite[] SpritesCount;
    float timer = 0.0f;
    public int cd = 6;
    public GameObject light1;
    lightEffect lighteffect;

    // Use this for initialization
    void Start () {
        
        
        lighteffect = light1.GetComponent<lightEffect>();
        GameObject.Find("CDCount").GetComponent<SpriteRenderer>().sprite = SpritesCount[cd];

    }
	
	// Update is called once per frame
	void Update () {
        if(cd == 0)
        {
            lighteffect.ifCool = false;
            
        }
        Timer();
       
    }

    public void Timer()
    {
        timer += Time.deltaTime;
        
        if(timer >= 1.0f)
        {
            if(cd != 0)
            {
                cd -= 1;
                GameObject.Find("CDCount").GetComponent<SpriteRenderer>().sprite = SpritesCount[cd];
                

            }
            
            timer = 0;
        }
    }

    


}
