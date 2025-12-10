using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 1f;

    // Cloud wraps when passing this X value
    public float maxX = 60f;

    // The original spawn position
    private Vector3 startPos;

    void Start()
    {
        // Remember where the cloud started
        startPos = transform.position;
    }

    void Update()
    {
        // Move cloud along X
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);

        // Wrap cloud when it leaves the screen
        if (transform.position.x > maxX)
        {
            transform.position = startPos;
        }
    }
}
