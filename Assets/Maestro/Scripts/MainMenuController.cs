using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject menu1, menu2, batonLeft, batonRight, staticBatonRight, staticBatonLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            batonLeft.SetActive(true);
            staticBatonLeft.SetActive(false);
            batonRight.SetActive(true);
            staticBatonRight.SetActive(false);
            menu1.SetActive(false);
            menu2.SetActive(true);
        }
    }
}
