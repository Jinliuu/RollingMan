using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {
    GameObject Player;
    GameObject nextLevel;
    public string gameObjectname;
    Transform currentLevel;
    GameObject levels;
    public GameObject centre;
    GameObject fade;
    GameObject Fix;
    public int nextLevelfixSize;
	void Start(){
        fade = GameObject.Find("Levels").transform.GetChild(1).gameObject;
        Fix = GameObject.Find("HUD").transform.GetChild(0).gameObject;
        gameObjectname = gameObject.transform.parent.gameObject.name;
        gameObjectname = gameObjectname.Substring(0, 6);
        FindLevel();
        currentLevel = gameObject.transform.parent;
        levels = gameObject.transform.parent.parent.parent.gameObject;
        Player = levels.transform.GetChild(2).gameObject;

	}
    void FindLevel()
    {
        Debug.Log(gameObjectname);
        if(gameObjectname == "Level1")
        {
            nextLevel = GameObject.Find("Levels").transform.GetChild(3).transform.Find("Level2").gameObject;
           
        }
        else if (gameObjectname == "Level2")
        {
            nextLevel = GameObject.Find("Levels").transform.GetChild(3).transform.Find("Level3").gameObject;

        }
        else if (gameObjectname == "Level3")
        {
            nextLevel = GameObject.Find("Levels").transform.GetChild(3).transform.Find("Level4").gameObject;

        }
        else if (gameObjectname == "Level4")
        {
            nextLevel = GameObject.Find("Levels").transform.GetChild(3).transform.Find("Level5").gameObject;

        }
        else if (gameObjectname == "Level5")
        {
            nextLevel = GameObject.Find("Levels").transform.GetChild(3).transform.Find("Level6").gameObject;

        }
        else if (gameObjectname == "Level6")
        {
            nextLevel = GameObject.Find("Levels").transform.GetChild(3).transform.Find("Level7").gameObject;

        }
        else if (gameObjectname == "Level7")
        {
            nextLevel = GameObject.Find("Levels").transform.GetChild(3).transform.Find("Level8").gameObject;

        }
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
        Debug.Log(nextLevel.name);
        nextLevel.SetActive(true);
        Player.transform.position = nextLevel.transform.Find("StartPoint").transform.position;
        Player.GetComponent<Animator>().SetTrigger("New");
        Player.GetComponent<Rigidbody2D>().isKinematic = false;
        GameObject.Find("Levels").transform.rotation = Quaternion.identity;
        Fix.GetComponent<Fix>().fixCount = nextLevelfixSize;
        Fix.GetComponent<Fix>().refreshFix();
    }


}
