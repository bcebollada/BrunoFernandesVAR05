using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    public float speed;

    public string noteType;

    private string[] noteTypesAvaliable = new string[] { "vLine", "hLine", "circle" };

    public bool isLeft; //to know if the not is to be used in left or right

    public Material materialLeft, materialRight;

    public GameObject hLineUI, vLineUI, circleUI;

    public GameObject explosion;

    private void Awake()
    {
        int randomNum = Random.Range(0, noteTypesAvaliable.Length);
        noteType = noteTypesAvaliable[randomNum];
    }

    private void Start()
    {
        if (isLeft)
        {
            GetComponent<MeshRenderer>().material = materialLeft;
        }
        else GetComponent<MeshRenderer>().material = materialRight;


        if(noteType == "vLine")
        {
            vLineUI.SetActive(true);
        }
        else if(noteType == "hLine")
        {
            hLineUI.SetActive(true);
        }
        else if (noteType == "circle")
        {
            circleUI.SetActive(true);
        }

    }
    public void OnDestroy()
    {
        Destroy( Instantiate(explosion, transform.position, transform.rotation), 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * transform.forward * Time.deltaTime;
    }
}