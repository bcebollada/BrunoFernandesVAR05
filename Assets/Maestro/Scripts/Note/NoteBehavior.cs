using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteBehavior : MonoBehaviour
{
    public float speed;

    public string noteType;

    private string[] noteTypesAvaliable = new string[] { "vLine", "hLine", "circle" };

    public bool isLeft; //to know if the not is to be used in left or right

    public Material materialLeft, materialRight;

    public GameObject hLineUI, vLineUI, circleUI, rythmCue;

    public GameObject explosion;

    private float lerpDuration; 
    private float lerpTimer = 0;

    private bool rythmCueShouldGo;

    private Vector3 initialScale;

    public float spawnedTime = 0;

    private void Awake()
    {
        int randomNum = Random.Range(0, noteTypesAvaliable.Length);
        noteType = noteTypesAvaliable[randomNum];
        initialScale = rythmCue.transform.localScale;
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

        if (rythmCueShouldGo)
        {
            lerpTimer += Time.deltaTime;
            float t = lerpTimer / lerpDuration;

            rythmCue.transform.localScale = Vector3.Lerp(initialScale, new Vector3(1, 1, 1), t);
            //if (t >= 1) Destroy(this.gameObject);

        }

        spawnedTime += Time.deltaTime;
    }

    public void ActivateCue(float timeOfCue, float timeToStartCue)
    {
        StartCoroutine(ActivateCueIEnumerator(timeOfCue, timeToStartCue));
    }

    private IEnumerator ActivateCueIEnumerator(float timeOfCue, float timeToStartCue)
    {
        yield return new WaitForSeconds(timeToStartCue);
        rythmCue.SetActive(true);
        lerpDuration = timeOfCue;
        rythmCueShouldGo = true;

    }
}
