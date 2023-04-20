using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private float height = 1.0f;
  
    [SerializeField]
    private float width = 1.0f;

    private float xStartPosition;
    private float yStartPosition;

    void Start()
    {
        xStartPosition = transform.position.x;
        yStartPosition = transform.position.y;
    }

    void Update()
    {
        float x = xStartPosition + Mathf.Sin(Time.time * speed) * width;
        float y = yStartPosition + Mathf.Sin(Time.time * speed * 2.0f) * height;

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
