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

    public Material materialLeft, materialRight, materialLeftScan, materialRightScan;

    public GameObject hLineUI, vLineUI, circleUI, childCube;

    public GameObject explosion;

    private float lerpDuration; 
    private float lerpTimer = 0;

    private bool rythmCueShouldGo;

    private Vector3 initialScale;

    public float spawnedTime = 0;
    public float maxSpawnedTime = 6;

    public NoteSpawner noteSpawner;
    public RythmGameController gameController;

    public Camera cam;

    public ScanShaderCommunicator scanShader;

    private void Awake()
    {
        int randomNum = Random.Range(0, noteTypesAvaliable.Length);
        noteType = noteTypesAvaliable[randomNum];
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<RythmGameController>();
    }

    private void Start()
    {
        if (isLeft)
        {
            childCube.GetComponent<MeshRenderer>().material = materialLeft;
            GetComponent<MeshRenderer>().material = materialLeftScan;
        }
        else
        {
            childCube.GetComponent<MeshRenderer>().material = materialRight;
            GetComponent<MeshRenderer>().material = materialRightScan;
        }


        if (noteType == "vLine")
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

        transform.LookAt(cam.transform.position);

    }
    public void OnDestroy()
    {
        Destroy( Instantiate(explosion, transform.position, transform.rotation), 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * transform.forward * Time.deltaTime;

        /*if (rythmCueShouldGo)
        {
            lerpTimer += Time.deltaTime;
            float t = lerpTimer / lerpDuration;

            scanShader.partialView = t;
            //rythmCue.transform.localScale = Vector3.Lerp(initialScale, new Vector3(1, 1, 1), t);

        }*/

        spawnedTime += Time.deltaTime;

        if(spawnedTime > maxSpawnedTime)
        {
            if (isLeft)
            {
                Destroy(noteSpawner.noteOrderLeft[noteSpawner.actualNoteToBeHitLeft]);
                noteSpawner.actualNoteToBeHitLeft += 1;
                gameController.AddScore(9, transform);
            }
            else
            {
                Destroy(noteSpawner.noteOrderRight[noteSpawner.actualNoteToBeHitRight]);
                noteSpawner.actualNoteToBeHitRight += 1;
                gameController.AddScore(9, transform);
            }
        }
    }

    public void ActivateCue(float timeOfCue, float timeToStartCue)
    {
        StartCoroutine(ActivateCueIEnumerator(timeOfCue, timeToStartCue));
    }

    private IEnumerator ActivateCueIEnumerator(float timeOfCue, float timeToStartCue)
    {
        yield return new WaitForSeconds(timeToStartCue);
        lerpDuration = timeOfCue;
        while (lerpDuration <= 1)
        {
            rythmCueShouldGo = true;
            lerpTimer += Time.deltaTime;
            float t = lerpTimer / lerpDuration;

            scanShader.partialView = t;
            if(t ==1) Destroy(this.gameObject);
            // Yielding here allows Unity to update the scene and then resume the coroutine in the next frame.
            yield return null;
        }

    }
}
