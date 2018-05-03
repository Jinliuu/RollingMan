using UnityEngine;
using System.Collections;

public class DangerBase : MonoBehaviour {
    GameObject levels;
    GameObject fade;
    public GameObject centre;
    GameObject player;
    string oldname;
    string levelname;
    string levelNum;
    GameObject endEffect;
    GameObject currentLevel;
    GameObject Fix;
    public int FixCont;
    AudioSource source;
    public AudioClip sawOn;
    [Header("Rotation")]
	public bool Rotate;
	public float SpeedRotate;

	[Header("Folow")]
	public bool FolowToPlayer;
	public Transform Player;
	public float SpeedFolow;

	[Header("Move")]
	public bool Move;
	public Vector2 Distance;
	private Vector3 StartPosition;
	public float MoveSpeed;
	private Vector2 States = new Vector2 (1, 1);

	[Header("Other")]
	private bool death = false;
	public AudioClip SawPlayer;
	public AudioClip FirePlayer;
	public bool ThisSaw = false;

	[Header("Fire")]
	public bool JustFire = false;

	void Start(){
        Fix = GameObject.Find("HUD").transform.GetChild(0).gameObject;
        levels = GameObject.Find("Levels");
        player = levels.transform.GetChild(2).gameObject;
        endEffect = levels.transform.GetChild(3).GetChild(0).gameObject;
        fade = levels.transform.GetChild(1).gameObject;
        currentLevel = transform.parent.parent.gameObject;
        oldname = currentLevel.name;
        levelname = oldname.Substring(0, 6);
        

        source = GetComponent<AudioSource>();
        if (Distance.x == 0)
			States = new Vector2 (0, States.y);
		if (Distance.y == 0)
			States = new Vector2 (States.x, 0);
		StartPosition += transform.localPosition;

		if (transform.localPosition.x < Distance.x)
			States = new Vector2 (-1, States.y);
		if (transform.localPosition.y < Distance.y)
			States = new Vector2 (States.x, -1);
		
	}


	void FixedUpdate(){
		if (Rotate)
			transform.Rotate (0, 0, SpeedRotate);

		if (FolowToPlayer) {
			if (transform.position.x >= Player.position.x && transform.position.y >= Player.position.y)
				transform.position += new Vector3 (-SpeedFolow, -SpeedFolow, 0);
			if (transform.position.x >= Player.position.x && transform.position.y <= Player.position.y)
				transform.position += new Vector3 (-SpeedFolow, SpeedFolow, 0);
			if (transform.position.x <= Player.position.x && transform.position.y <= Player.position.y)
				transform.position += new Vector3 (SpeedFolow, SpeedFolow, 0);
			if (transform.position.x <= Player.position.x && transform.position.y >= Player.position.y)
				transform.position += new Vector3 (SpeedFolow, -SpeedFolow, 0);
		}

		if (Move) {
			if (States.x == 1) {
				if (transform.localPosition.x > Distance.x)
						transform.localPosition += new Vector3 (-MoveSpeed, 0, 0);
					else
						States = new Vector2 (2, States.y);
			}
			if (States.x == 2) {
				if (transform.localPosition.x < StartPosition.x)
					transform.localPosition += new Vector3 (MoveSpeed, 0, 0);
				else
					States = new Vector2 (1, States.y);
			}

			if (States.x == -1) {
				if (transform.localPosition.x < Distance.x)
					transform.localPosition += new Vector3 (MoveSpeed, 0, 0);
				else
					States = new Vector2 (-2, States.y);
			}
			if (States.x == -2) {
				if (transform.localPosition.x > StartPosition.x)
					transform.localPosition += new Vector3 (-MoveSpeed, 0, 0);
				else
					States = new Vector2 (-1, States.y);
			}
				
			if (States.y == 1) {
				if (transform.localPosition.y > Distance.y)
					transform.localPosition += new Vector3 (0, -MoveSpeed, 0);
				else
					States = new Vector2 (States.x, 2);
			}
			if (States.y == 2) {
				if (transform.localPosition.y < StartPosition.y)
					transform.localPosition += new Vector3 (0, MoveSpeed, 0);
				else
					States = new Vector2 (States.x, 1);
			}

			if (States.y == -1) {
				if (transform.localPosition.y < Distance.y)
					transform.localPosition += new Vector3 (0, MoveSpeed, 0);
				else
					States = new Vector2 (States.x, -2);
			}
			if (States.y == -2) {
				if (transform.localPosition.y > StartPosition.y)
					transform.localPosition += new Vector3 (0, -MoveSpeed, 0);
				else
					States = new Vector2 (States.x, -1);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
            PlayerGameOver(other.gameObject.transform, endEffect.transform);
            source.PlayOneShot(sawOn);
        }
	}
    private void PlayerGameOver(Transform Player, Transform Particle)
    {
        Player.GetComponent<Animator>().SetTrigger("die");

        Particle.gameObject.SetActive(true);
        Particle.transform.position = Player.transform.position;
        Player.GetComponent<SoundCollision>().enabled = false;
        Player.GetComponent<Collider2D>().enabled = false;

        Invoke("enterAnim", 2.0f);

    }

    void enterAnim()
    {
        fade.GetComponent<Animator>().SetTrigger("Enter");
        Invoke("reborn", 0.5f);
        
    }

    void reborn()
    {
        endEffect.gameObject.SetActive(false);
        gameObject.transform.parent.parent.parent.parent.transform.rotation = Quaternion.identity;
        player.GetComponent<Animator>().SetTrigger("reborn");
        player.GetComponent<Collider2D>().enabled = true;
        if(levelname == "Level1")
        {
            Fix.GetComponent<Fix>().fixCount = 2;
            Fix.GetComponent<Fix>().refreshFix();
        }
        else if (levelname == "Level2")
        {
            Fix.GetComponent<Fix>().fixCount = 0;
            Fix.GetComponent<Fix>().refreshFix();
        }
        else if (levelname == "Level3")
        {
            Fix.GetComponent<Fix>().fixCount = 1;
            Fix.GetComponent<Fix>().refreshFix();
        }
        else if (levelname == "Level4")
        {
            Fix.GetComponent<Fix>().fixCount = 0;
            Fix.GetComponent<Fix>().refreshFix();
        }
        else if (levelname == "Level5")
        {
            Fix.GetComponent<Fix>().fixCount = 0;
            Fix.GetComponent<Fix>().refreshFix();
        }
        else if (levelname == "Level6")
        {
            Fix.GetComponent<Fix>().fixCount = 1;
            Fix.GetComponent<Fix>().refreshFix();
        }
        else if (levelname == "Level7")
        {
            Fix.GetComponent<Fix>().fixCount = 4;
            Fix.GetComponent<Fix>().refreshFix();
        }
       

        Instantiate(Resources.Load(levelname,typeof(GameObject)) as GameObject, levels.transform.GetChild(3));
        Invoke("newAnim", 0.5f);
    }

    void newAnim()
    {

        
        player.transform.position = centre.transform.position;
        player.transform.localScale = new Vector3(1, 1, 1);
        
        fade.GetComponent<Animator>().SetTrigger("New");
        Destroy(currentLevel);
    }

}
