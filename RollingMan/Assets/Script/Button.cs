using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {
    AudioSource source;
    public AudioClip one;
    public GameObject Fade;
    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();

    }
    // Use this for initialization
    private void OnMouseDown()
    {

        
        source.PlayOneShot(one);
        Fade.GetComponent<Animator>().SetTrigger("Enter");
        Invoke("switchScene", 2.0f);
    }

    void switchScene()
    {
        SceneManager.LoadScene("Level1");
    }
}
