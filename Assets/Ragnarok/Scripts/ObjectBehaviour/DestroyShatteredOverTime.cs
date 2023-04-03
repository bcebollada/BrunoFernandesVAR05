using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShatteredOverTime : MonoBehaviour
{
    [SerializeField]
    private float destroyTime = 5;

    //After destroyTime(s), object is destroyed
    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
