using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {
    public GameObject Player;
    public GameObject nextLevel;
    Transform currentLevel;
    public GameObject centre;
    public GameObject fade;
    public GameObject Fix;
    public int nextLevelfixSize;
	void Start(){
        currentLevel = gameObject.transform.parent;
        

	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player")
        {
            gameObject.GetComponent<AudioSource>().Play();
            Player.GetComponent<Animator>().SetTrigger("Enter");
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Player.GetComponent<Rigidbody2D>().angularVelocity = 0;
            Player.GetComponent<Rigidbody2D>().isKinematic = true;
            Player.transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), centre.transform.position,10* Time.deltaTime);
            fade.GetComponent<Animator>().SetTrigger("Enter");
            Invoke("newAnim", 2.0f);
            Invoke("disableLevel", 1.0f);
            
        }
			
	}

    void newAnim()
    {
        fade.GetComponent<Animator>().SetTrigger("New");
    }

  

    void disableLevel()
    {
        currentLevel.gameObject.SetActive(false);
        loadLevel();
    }

    void loadLevel()
    {
        nextLevel.SetActive(true);
        Player.transform.position = nextLevel.transform.Find("StartPoint").transform.position;
        Player.GetComponent<Animator>().SetTrigger("New");
        Player.GetComponent<Rigidbody2D>().isKinematic = false;
        GameObject.Find("Levels").transform.rotation = Quaternion.identity;
        Fix.GetComponent<Fix>().fixCount = nextLevelfixSize;
        Fix.GetComponent<Fix>().refreshFix();
    }


}
