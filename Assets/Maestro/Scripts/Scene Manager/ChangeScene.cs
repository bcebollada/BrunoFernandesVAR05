using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void New_Level(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
