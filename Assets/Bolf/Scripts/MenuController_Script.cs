using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Track1()
    {
        Debug.Log("Load Track 1");
        SceneManager.LoadScene("Track1_Scene");
    }

    public void Track2()
    {
        Debug.Log("Load Track 2");
        SceneManager.LoadScene("Track2_Scene");
    }
}
