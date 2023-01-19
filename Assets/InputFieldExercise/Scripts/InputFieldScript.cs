using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UIElements;

public class InputFieldScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField name, age;
    //[SerializeField] private Text text;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Submit()
    {
        Debug.Log("Hello " + name.text + ", you are " + age.text + " years old");
    }

}
