using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicButtonScript : MonoBehaviour
{
    public ChangeScene sceneManager;

    private void Awake()
    {
        // Find and assign the SceneManager script in the scene
        sceneManager = FindObjectOfType<ChangeScene>();
    }

    public void OnButtonClick()
    {
        // Call the ChangeScene method of the SceneManager
        sceneManager.New_Level("Bruno Test");
    }
}
