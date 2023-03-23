using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RubeGoldBergController_Script : MonoBehaviour
{
    private PausePhysics pausePhysics;
    public Material transparentMat;
    public Material defaultMat;

    private bool propsTransparent;

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

    public void ChangeMat()
    {
        propsTransparent = true;
        GameObject[] props = GameObject.FindGameObjectsWithTag("Prop");
        foreach (GameObject prop in props)
        {
            if (prop.GetComponent<Renderer>() != null)
            {
                print("yyy");
                Renderer renderer = prop.GetComponent<Renderer>(); // Get the renderer component of the object
                if(renderer.sharedMaterial.name == defaultMat.name) renderer.material = transparentMat;
                else renderer.material = defaultMat;
            }
        }
    }
}
