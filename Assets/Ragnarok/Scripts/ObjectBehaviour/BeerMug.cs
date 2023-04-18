using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerMug : MonoBehaviour
{
    public Collider mugCollider;
    public Collider headCollider;
    public TargetLevelController targetLevelController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        if(contact.thisCollider == mugCollider && contact.otherCollider == headCollider)
        {
            Debug.Log("Mug Hit Head, Start Game");
            targetLevelController.gameStart = true;
        }
    }
}
