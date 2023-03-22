using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RubeGoldBergController_Script : MonoBehaviour
{
    private PausePhysics pausePhysics;
    private void Awake()
    {
        pausePhysics = GameObject.Find("VRRig").GetComponent<PausePhysics>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("restarting scene");
    }

    public void PausePhysics()
    {
        pausePhysics.Pause();
    }

    public void RestartPhysics()
    {
        pausePhysics.Restart();
    }
}
