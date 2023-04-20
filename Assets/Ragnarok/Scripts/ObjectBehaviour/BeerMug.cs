using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerMug : MonoBehaviour
{
    public Collider mugCollider;
    public Collider headCollider;
    public TargetLevelController targetLevelController;

    [SerializeField]
    private Transform head;

    [SerializeField]
    private float distanceToHead;

    private void Update()
    {
        if (Vector3.Distance(transform.position, head.position) < distanceToHead)
        {
            Debug.Log("Mug Hit Head, Start Game");
            targetLevelController.gameStart = true;
        }

        if(Vector3.Distance(transform.position, head.position) > distanceToHead && targetLevelController.levelOneComplete == true )
        {
            Debug.Log("Mug Hit Head, Start Level Two");
            targetLevelController.levelTwoStart = true;
        }
    }

}
