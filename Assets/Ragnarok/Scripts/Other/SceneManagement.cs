using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    /// <summary>
    /// This is the script that will launch the menu to the training scene (probably attached to a button in the UI)
    /// 
    /// Also, a function to go from the training scene to the first course.
    /// 
    /// </summary>

    public void ToTrainingScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("Bruno_Scene");
    }

    public void ToFirstCourse()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("MainRagnarok_Scene");
    }
}
