using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCanvas : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (!audioSource.isPlaying && objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            objectToDeactivate.SetActive(false);
        }
    }
}
