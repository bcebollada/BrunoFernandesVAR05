using System.Collections;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public bool hasBeenHit = false;
    public bool axeHasBeenRecalled = false;
    public Animator targetSinks; // Reference to the Animator component

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        hasBeenHit = true;
        if(other.gameObject.CompareTag("AxeBlade")) audioSource.Play();
    }

    private void OnTriggerExit(Collider collision)
    {
        axeHasBeenRecalled = true;
    }

    public IEnumerator PlayDownAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        targetSinks.SetTrigger("PlayDownAnimation"); // Play the animation

        // Wait until the animation has finished playing
        AnimatorStateInfo stateInfo = targetSinks.GetCurrentAnimatorStateInfo(0);
        while (stateInfo.normalizedTime < 1.0f)
        {
            stateInfo = targetSinks.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
    }

    public IEnumerator PlayUpAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        targetSinks.SetTrigger("PlayUpAnimation"); // Play the
    }
}
