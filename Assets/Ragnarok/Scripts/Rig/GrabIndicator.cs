using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabIndicator : MonoBehaviour
{

    public Transform objectToFollow;


    void Update()
    {
        if(objectToFollow == null)
        {
            GetComponent<Image>().enabled = false;
        }
        else
        {
            GetComponent<Image>().enabled = true;
            transform.position = Camera.main.WorldToScreenPoint(objectToFollow.position);
        }
    }
}
