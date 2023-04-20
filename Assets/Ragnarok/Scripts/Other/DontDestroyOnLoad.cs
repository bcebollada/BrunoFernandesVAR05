using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    /// <summary>
    /// Could be used to preserve data like username and scores.
    /// </summary>

    private void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("GameManager");

        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
