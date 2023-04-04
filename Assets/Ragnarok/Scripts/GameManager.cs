using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // score will be increased by the target's accessing this script once hit, hence public
    public int score = 0;

    //Optional course timer depending on level requirement
    [SerializeField]
    private float countdownTimer = 30f;

    void Start()
    {
        
    }

    void Update()
    {
        countdownTimer -= Time.deltaTime; 
    }
}
