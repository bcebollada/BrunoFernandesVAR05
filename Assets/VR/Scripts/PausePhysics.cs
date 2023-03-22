using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePhysics : MonoBehaviour
{
    public GameObject[] props;
    public Rigidbody[] rigidbodies;
    public Vector3[] velocities;
    public Vector3[] angularVelocities;

    private void Start()
    {
        

        
    }

    private void Update()
    {

    }

    public void Pause()
    {
        props = GameObject.FindGameObjectsWithTag("Prop");
        rigidbodies = new Rigidbody[props.Length];
        velocities = new Vector3[props.Length];
        angularVelocities = new Vector3[props.Length];

        // Save initial velocities for all props
        for (int i = 0; i < props.Length; i++)
        {
            if (props[i].GetComponent<Rigidbody>() != null)
            {
                rigidbodies[i] = props[i].GetComponent<Rigidbody>();
                velocities[i] = rigidbodies[i].velocity;
                angularVelocities[i] = rigidbodies[i].angularVelocity;
            }

        }   

        // pause physics simulation for all props and save their velocities
        for (int i = 0; i < props.Length; i++)
        {
            Debug.Log("pause");
            if (props[i].GetComponent<Rigidbody>() == null ) continue;
            Debug.Log("pause2");

            rigidbodies[i].isKinematic = true;
        }
    }

    public void Restart()
    {
        // rsesume physics simulation for all props with their previous velocities
        for (int i = 0; i < props.Length; i++)
        {
            if (props[i].GetComponent<Rigidbody>() != null)
            {
                rigidbodies[i].isKinematic = false;
                rigidbodies[i].velocity = velocities[i];
                rigidbodies[i].angularVelocity = angularVelocities[i];
            }
            
        }
    }
}
