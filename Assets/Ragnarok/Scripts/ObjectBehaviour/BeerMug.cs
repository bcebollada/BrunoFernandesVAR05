using UnityEngine;

public class BeerMug : MonoBehaviour
{
    public Collider mugCollider;
    public Collider headCollider;
    public TargetLevelController targetLevelController;

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        if(contact.thisCollider == mugCollider && contact.otherCollider == headCollider)
        {
            Debug.Log("Mug Hit Head, Start Game");
            targetLevelController.gameStart = true;
        }

        if (contact.thisCollider == mugCollider && contact.otherCollider == headCollider && targetLevelController.levelOneComplete == true)
        {
            Debug.Log("Mug Hit Head, Start Level Two");
            targetLevelController.levelTwoStart = true;
        }
    }
}
