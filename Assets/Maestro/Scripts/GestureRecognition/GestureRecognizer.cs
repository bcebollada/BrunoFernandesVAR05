using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
using System.IO;

public class GestureRecognizer : MonoBehaviour
{
    public RagnarokVRInputController actions;


    public bool isPressingButton;
    public Transform movementSource;

    private bool isMoving;
    private List<Vector3> positionsList = new List<Vector3>();

    public float newPositionThresholdDistance = 0.1f;
    public GameObject debugCubePrefab;
    public bool creationMode;
    public string newGestureName;

    private List<Gesture> trainingSets = new List<Gesture>();

    private LineRenderer lineRenderer;

    public bool isLeft;

    public float velMagMinimum;

    private Vector3 previousPosition;

    private NoteSpawner noteSpawner;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        noteSpawner = GameObject.Find("NoteSpawner").GetComponent<NoteSpawner>();
    }


    // Start is called before the first frame update
    void Start()
    {
        string[] gestureFiles = Directory.GetFiles(Application.dataPath + "\\Maestro\\Gestures", "*.xml");
        print(Application.dataPath + "\\Maestro\\Gestures");
        foreach (var item in gestureFiles)
        {
            print(item);
            trainingSets.Add(GestureIO.ReadGestureFromFile(item));
            print("count" + trainingSets.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (movementSource.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.1) isMoving = true;
        //else isMoving = false;

        //Start The Movement
        Vector3 velocity = (movementSource.position - previousPosition) * Time.deltaTime;
        float velMagnitude = velocity.magnitude;


        if (velMagnitude > velMagMinimum) isPressingButton = true;
        else isPressingButton = false;

        /*if (isLeft)
        {
            if (actions.TriggerPressedLeft) isPressingButton = true;
            else if (!actions.TriggerPressedLeft) isPressingButton = false;
        }
        else
        {
            if (actions.TriggerPressedRight) isPressingButton = true;
            else if (!actions.TriggerPressedRight) isPressingButton = false;
        }*/

        if (!isMoving && isPressingButton)
        {
            StartMovement();
        }

        //Ending The Movement
        else if (isMoving && !isPressingButton)
        {
            EndMovement(); //problema ta aqui
        }

        //Updating The Movement
        else if (isMoving && isPressingButton)
        {
            UpdateMovement();
        }

        previousPosition = movementSource.position;

    }

    void StartMovement()
    {
        Debug.Log("Start Movement");
        isMoving = true;
        positionsList.Clear();
        positionsList.Add(movementSource.position);
        Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity), 3f);

        lineRenderer.enabled = true;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, movementSource.position);

    }

    void EndMovement()
    {
        Debug.Log("End Movement");
        isMoving = false;

        //create gesture from list
        Point[] pointArray = new Point[positionsList.Count];

        for (int i = 0; i < positionsList.Count; i++)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionsList[i]);

            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
        }

        //problema ta aqui
        Gesture newGesture = new Gesture(pointArray);


        //add new gesture to training sets
        if (creationMode)
        {
            newGesture.Name = newGestureName;
            trainingSets.Add(newGesture);

            string fileName = Application.dataPath + "\\Maestro\\Gestures" + "/" + newGestureName + ".xml";
            GestureIO.WriteGesture(pointArray, newGestureName, fileName);
        }
        //recognize
        else
        {
            Result result = PointCloudRecognizer.Classify(newGesture, trainingSets.ToArray());
            Debug.Log(result.GestureClass + result.Score);
            if(result.Score > 0.7) GestureFinalized(result.GestureClass);
        }

        //StartCoroutine(DissolveLine());
    }

    void UpdateMovement()
    {
        Debug.Log("Update Movement");
        Vector3 lastPosition = positionsList[positionsList.Count - 1];

        if(Vector3.Distance(movementSource.position,lastPosition) > newPositionThresholdDistance)
        {
            positionsList.Add(movementSource.position);
            Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity), 3f);
        }

        //update lineRenderer
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, movementSource.position);
    }

    IEnumerator DissolveLine()
    {
        print("dissolving");
        float timeToDissolve = 2f;
        float delayBetweenDissolves = timeToDissolve / lineRenderer.positionCount;
        int points = lineRenderer.positionCount;

        for (int i = 1; i < points; i++)
        {
            int points2 = lineRenderer.positionCount;

            
            for (int j = 0; j < points2; j++)
            {
                Vector3 newPos = lineRenderer.GetPosition(j+1);
                lineRenderer.SetPosition(j, newPos);
            }
            lineRenderer.positionCount--;

            yield return new WaitForSeconds(delayBetweenDissolves);
        }
    }

    private void GestureFinalized(string noteType)
    {
        if(isLeft) noteSpawner.NoteHitLeft(noteType);
        else noteSpawner.NoteHitRight(noteType);
    }

    public void PressingButton()
    {
        isPressingButton = true;
    }

    public void ReleasedButton()
    {
        isPressingButton = false;
    }
}
