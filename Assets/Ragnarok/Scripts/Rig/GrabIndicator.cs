using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabIndicator : MonoBehaviour
{
    private Transform vrCamera;
    private float hoverTimer;
    public bool isHovered;


    private void Awake()
    {
        vrCamera = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        if(!isHovered)
        {
            hoverTimer += Time.deltaTime;

        }
        else
        {
            hoverTimer = 0;  
        }

        if(hoverTimer < 0.1f)
        {
            GetComponentInChildren<Image>().enabled = true;
            transform.LookAt(vrCamera);
        }
        else
        {
            GetComponentInChildren<Image>().enabled = false;
        }

        isHovered = false;
    }
}
