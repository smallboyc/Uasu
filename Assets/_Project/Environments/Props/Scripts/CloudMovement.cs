using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 1f;

    // Wrap limits
    public float maxX = 60f;
    public float minX = -60f;

    void Update()
    {
        // Move cloud along X
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);

        // Wrap cloud
        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(
                minX,
                transform.position.y,
                transform.position.z
            );
        }
    }
}
