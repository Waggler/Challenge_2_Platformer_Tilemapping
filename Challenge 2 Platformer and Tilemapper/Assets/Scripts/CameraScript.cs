using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (Input.GetKey("escape")) //if escape key is pressed end game
        {
            Application.Quit();
        }

        transform.position = player.transform.position + offset;
    }
}