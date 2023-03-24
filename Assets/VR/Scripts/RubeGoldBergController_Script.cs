using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RubeGoldBergController_Script : MonoBehaviour
{
    private PausePhysics pausePhysics;
    public Material transparentMat;
    public Material defaultMat;
    public List<Material> materials;
    private Material[] mats;
    public bool isDebugMenu;


    private void Awake()
    {
        pausePhysics = GameObject.Find("VRRig").GetComponent<PausePhysics>();

        GameObject[] props = GameObject.FindGameObjectsWithTag("Prop");
        mats = new Material[props.Length];

        for (int i = 0; i < props.Length; i++)
        {
            if (props[i].GetComponent<Renderer>() != null)
            {
                mats[i] = props[i].GetComponent<Renderer>().material;
            }
        }
    }

    private void Start()
    {
        if(!isDebugMenu)GameObject.Find("VRRig").GetComponent<MenuMain_Script>().menu = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartScene()
    { 
        GrabbableObject[] grabbables = FindObjectsOfType<GrabbableObject>();
        foreach(GrabbableObject grabbable in grabbables)
        {
            Destroy(grabbable.gameObject);
        }
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
        GameObject[] props = GameObject.FindGameObjectsWithTag("Prop");

        for (int i = 0; i < props.Length; i++)
        {
            if (props[i].GetComponent<Renderer>() != null)
            {
                Renderer renderer = props[i].GetComponent<Renderer>(); // Get the renderer component of the object
                if (renderer.material == mats[i]) renderer.material = transparentMat;
                else renderer.material = mats[i];
            }
        }
    }

    public void GetMats()
    {
        GameObject[] props = GameObject.FindGameObjectsWithTag("Prop");
        mats = new Material[props.Length];

        for (int i = 0; i < props.Length; i++)
        {
            if (props[i].GetComponent<Renderer>() != null)
            {
                mats[i] = props[i].GetComponent<Renderer>().material;
            }
        }
    }
}
