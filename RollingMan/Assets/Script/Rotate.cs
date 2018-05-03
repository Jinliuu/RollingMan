using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    Quaternion rotate;
    // Use this for initialization
    void Start () {

    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.D))
        {
            

            transform.Rotate(Vector3.forward * 50 * Time.deltaTime);

           
        }

        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.back * 50 * Time.deltaTime);
        }

    }
}
