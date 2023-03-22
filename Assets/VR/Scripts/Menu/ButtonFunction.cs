using UnityEngine;
using UnityEngine.Events;

public class ButtonFunction : MonoBehaviour
{
    public UnityEvent ButtonPressed = new UnityEvent();

    private Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.localPosition;   
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = startingPos;
    }

    public void ButtonPressedCall()
    {
        ButtonPressed.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Button pressed");
        if(collision.gameObject.CompareTag("Hand"))
        {
            Debug.Log(" Hand pressed button");
            ButtonPressed.Invoke();
        }
    }

}
