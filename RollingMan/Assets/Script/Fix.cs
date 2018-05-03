using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fix : MonoBehaviour {
    private bool fix = false;

    public GameObject Player;
    public GameObject FixOn;
    Rigidbody2D rb;
    public Sprite[] SpritesCount;
    public int fixCount = 2;
	// Use this for initialization
	void Start () {
        rb = Player.GetComponent<Rigidbody2D>();
        GameObject.Find("FixCount").GetComponent<SpriteRenderer>().sprite = SpritesCount[fixCount];
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FIX();
        }
	}

    public void FIX()
    {
        if (fix == false)
        {
            if (fixCount > 0)
            {
                fixCount--;
                fix = !fix;
                gameObject.GetComponent<Animation>().Play("To Fix");
                StartCoroutine(AwaitFIX());
                rb.velocity = new Vector2(0, 0);
                rb.angularVelocity = 0;
                rb.isKinematic = true;
                FixOn.SetActive(true);
                //GameObject.Find("Pause").GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            fix = !fix;
            GameObject.Find("Fix").GetComponent<Animation>().Play("From Fix");
            rb.isKinematic = false;
            FixOn.SetActive(false);
            //GameObject.Find("Continue").GetComponent<AudioSource>().Play();
        }

    }

    public void refreshFix()
    {
        GameObject.Find("FixCount").GetComponent<SpriteRenderer>().sprite = SpritesCount[fixCount];
    }

    IEnumerator AwaitFIX()
    {
        yield return new WaitForSeconds(0.165f);
        GameObject.Find("FixCount").GetComponent<SpriteRenderer>().sprite = SpritesCount[fixCount];
    }

}
