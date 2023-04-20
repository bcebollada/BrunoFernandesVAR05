using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterEffect_Target : MonoBehaviour
{
    /// <summary>
    /// This script is attached to the original whole object that is to be fractured.
    /// </summary>
    /// 
    [SerializeField]
    GameObject _shatteredObject;
    
    [SerializeField]
    float _breakForce;

    [SerializeField]
    int health = 1;

    [SerializeField]
    float _xAxisOffset, _yAxisOffset, _zAxisOffset;

    private bool isQuitting = false;

    // Triggers collider when object hits anything tagged "Ground".
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("recallObject"))
        {
            BreakTheThing();
            health--;

        }
    }

    private void Update()
    {
        if (health == 0)
        {
            Destroy(gameObject); 
        }
    }
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    //private void OnDestroy()
    //{
    //    if (!isQuitting)
    //    {
    //        BreakTheThing();
    //    }
    //}

    private void BreakTheThing()
    {
        GameObject shattered = Instantiate(_shatteredObject, transform.position, transform.rotation * 
            Quaternion.Euler(_xAxisOffset, _yAxisOffset, _zAxisOffset));
        foreach (Rigidbody rb in shattered.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * _breakForce;
            rb.AddForce(force);
        } 
    }
}