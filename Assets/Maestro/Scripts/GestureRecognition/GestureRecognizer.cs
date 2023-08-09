using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using static TreeEditor.TreeEditorHelper;

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
    public LineRenderer visibleLineRenderer; // this is the one that will be visible
    private Gradient originalGradient;
    private float originalStartWidht;
    private float originalEndtWidht;

    public int pointsPerInterval = 5; // Number of points to add between each actual position

    public bool isLeft;

    public float velMagMinimum;

    private Vector3 previousPosition;

    private NoteSpawner noteSpawner;

    public float coolDownTime = 0.5f;
    private float coolDownCurrentTime;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name != "MainMenu")
        {
            noteSpawner = GameObject.Find("NoteSpawner").GetComponent<NoteSpawner>();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        string[] gestureFiles = Directory.GetFiles(Application.dataPath + "\\Maestro\\Gestures", "*.xml");
        print(Application.dataPath + "\\Maestro\\Gestures");
        foreach (var item in gestureFiles)
        {
            trainingSets.Add(GestureIO.ReadGestureFromFile(item)); ;
        }

        originalGradient = lineRenderer.colorGradient;
        originalStartWidht = lineRenderer.startWidth;
        originalEndtWidht = lineRenderer.endWidth;

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

        coolDownCurrentTime += Time.deltaTime;

    }

    void StartMovement()
    {
        Debug.Log("Start Movement");
        isMoving = true;
        positionsList.Clear();
        positionsList.Add(movementSource.position);
        Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity), 3f);

        lineRenderer.enabled = true;

        lineRenderer.startWidth = originalStartWidht;
        lineRenderer.endWidth = originalEndtWidht;
        lineRenderer.colorGradient = originalGradient;
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
            if (result.Score > 0.6 && coolDownCurrentTime >= coolDownTime) GestureFinalized(result.GestureClass);
            print(result.Score + result.GestureClass);
        }

        //StartCoroutine(DissolveSingleLine());


    }

    private void FixedUpdate()
    {
        //Updating The Movement
        if (isMoving && isPressingButton)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, movementSource.position);
        }

    }

    void UpdateMovement()
    {
        Debug.Log("Update Movement");
        Vector3 lastPosition = positionsList[positionsList.Count - 1];
        Vector3 currentPosition = movementSource.position;

        float distance = Vector3.Distance(currentPosition, lastPosition);

        if (distance > 0f)
        {
            int numPointsToAdd = Mathf.CeilToInt(distance / newPositionThresholdDistance) * pointsPerInterval;
            float stepSize = 1f / numPointsToAdd;

            for (int i = 0; i < numPointsToAdd; i++)
            {
                float t = i * stepSize;
                Vector3 point = Vector3.Lerp(lastPosition, currentPosition, t);
                positionsList.Add(point);
                Destroy(Instantiate(debugCubePrefab, point, Quaternion.identity), 3f);
            }
        }

        // Update lineRenderer
        lineRenderer.positionCount = positionsList.Count;
        lineRenderer.SetPositions(positionsList.ToArray());
    }

    IEnumerator DissolveLine()
    {
        lineRenderer.startWidth = 0;
        lineRenderer.endWidth = 0;
        // Get the current gradient from the visibleLineRenderer
        Gradient gradient = visibleLineRenderer.colorGradient;
        Gradient gradient2 = lineRenderer.colorGradient;

        // Get the current alpha keys
        GradientAlphaKey[] alphaKeys = gradient.alphaKeys;
        GradientAlphaKey[] alphaKeys2 = gradient2.alphaKeys;

        // Set the starting alpha to 1 (fully opaque)
        float startingAlpha = 1f;
        float startingAlpha2 = (50 / 255);

        // Time it takes for the line to dissolve
        float dissolveDuration = 1f;

        // Time elapsed during the dissolve process
        float elapsedTime = 0f;

        while (elapsedTime <= dissolveDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / dissolveDuration;

            // Calculate the new alpha value using Lerp
            float newAlpha = Mathf.Lerp(startingAlpha, 0f, normalizedTime);
            float newAlpha2 = Mathf.Lerp(startingAlpha2, 0f, normalizedTime);

            // Update the alpha values of the alpha keys
            alphaKeys[0].alpha = newAlpha;
            alphaKeys[1].alpha = newAlpha;
            alphaKeys2[0].alpha = newAlpha2;
            alphaKeys2[1].alpha = newAlpha2;

            // Assign the modified alpha keys back to the gradient
            gradient.alphaKeys = alphaKeys;
            gradient2.alphaKeys = alphaKeys2;

            // Assign the updated gradient back to the visibleLineRenderer
            visibleLineRenderer.colorGradient = gradient;
            lineRenderer.colorGradient = gradient2;

            yield return null;
        }

        // Ensure the alpha is fully set to 0
        alphaKeys[0].alpha = 0f;
        alphaKeys[1].alpha = 0f;
        gradient.alphaKeys = alphaKeys;
        visibleLineRenderer.colorGradient = gradient;

        // Reset the lineRenderer and disable it
        visibleLineRenderer.positionCount = 0;
        visibleLineRenderer.enabled = false;

    }

    IEnumerator DissolveSingleLine()
    {
        // Get the current gradient from the visibleLineRenderer
        Gradient gradient2 = lineRenderer.colorGradient;

        GradientAlphaKey[] alphaKeys2 = gradient2.alphaKeys;

        float startingAlpha2 = (50 / 255);

        // Time it takes for the line to dissolve
        float dissolveDuration = 1f;

        // Time elapsed during the dissolve process
        float elapsedTime = 0f;

        while (elapsedTime <= dissolveDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / dissolveDuration;

            // Calculate the new alpha value using Lerp
            float newAlpha2 = Mathf.Lerp(startingAlpha2, 0f, normalizedTime);

            // Update the alpha values of the alpha keys
            alphaKeys2[0].alpha = newAlpha2;
            alphaKeys2[1].alpha = newAlpha2;

            // Assign the modified alpha keys back to the gradient
            gradient2.alphaKeys = alphaKeys2;

            // Assign the updated gradient back to the visibleLineRenderer
            lineRenderer.colorGradient = gradient2;

            yield return null;
        }
    }
    private void GestureFinalized(string noteType)
    {
        if (noteType == "circle2") noteType = "circle";
        Debug.Log("note" + noteType);

        // Update visibleLineRenderer with the positions from lineRenderer
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);

        visibleLineRenderer.enabled = true;
        visibleLineRenderer.positionCount = lineRenderer.positionCount;
        visibleLineRenderer.SetPositions(positions);

        StartCoroutine(DissolveLine());

        Scene scene = SceneManager.GetActiveScene();

        Debug.Log("will change");

        if (scene.name == "MainMenu" && GameObject.FindGameObjectWithTag("checker") != null)
        {
            Debug.Log("should change");

            if (noteType == "vLine") SceneManager.LoadScene("Music1");
            else if (noteType == "hLine") SceneManager.LoadScene("Music2");
        }
        else
        {
            if (isLeft) noteSpawner.NoteHitLeft(noteType);
            else noteSpawner.NoteHitRight(noteType);

            coolDownCurrentTime = 0;
        }
    }

    public void PressingButton()
    {
        isPressingButton = true;
    }

    public void ReleasedButton()
    {
        isPressingButton = false;
    }

    public void DebugAction()
    {
        GestureFinalized("circle");
    }
}
