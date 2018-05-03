using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {
    public GameObject fade;
    public GameObject centre;
    public GameObject player;
    public float SpeedRotate;
    public AudioClip sawOn;
    AudioSource source;
    public bool ifMove;
    public GameObject endEffect;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, SpeedRotate);
        if (ifMove)
        {
            transform.position += transform.right * 1.0f * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            PlayerGameOver(other.gameObject.transform, endEffect.transform);
            source.PlayOneShot(sawOn);

        }
    }

    private void PlayerGameOver(Transform Player, Transform Particle)
    {
        Player.GetComponent<Animator>().SetTrigger("die");
        Particle.gameObject.SetActive(true);
        Player.GetComponent<SoundCollision>().enabled = false;
        Player.GetComponent<Collider2D>().enabled = false;
        
        Invoke("enterAnim", 2.0f);
       
    }

    void enterAnim()
    {
        fade.GetComponent<Animator>().SetTrigger("Enter");
        Invoke("reborn", 0.5f);
        Invoke("newAnim", 2.0f);
    }

    void reborn()
    {
        endEffect.gameObject.SetActive(false);
        gameObject.transform.parent.parent.parent.parent.transform.rotation = Quaternion.identity;
        player.GetComponent<Animator>().SetTrigger("reborn");
        player.GetComponent<Collider2D>().enabled = true;
    }

    void newAnim()
    {
        fade.GetComponent<Animator>().SetTrigger("New");
        player.transform.position = centre.transform.position;
        player.transform.localScale = new Vector3(1, 1, 1);
    }

}
