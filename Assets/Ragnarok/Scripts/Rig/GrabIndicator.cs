using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GrabIndicator : MonoBehaviour
{

    public Transform objectToFollow;


    void Update()
    {
        if(objectToFollow == null)
        {
            GetComponent<Canvas>().enabled = false;
        }
        else
        {
            GetComponent<Canvas>().enabled = true;
            transform.position = Camera.main.WorldToScreenPoint(objectToFollow.position);
        }
    }
}
