using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField] float _pushPower = 5.0f;

    private bool _isPushing = false;
    public bool IsPushing => _isPushing;
    private float _pushCooldown = 0.1f;
    private float _lastHitTime = 0f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        if (rb == null)
            return;

        Vector3 distance = (hit.gameObject.transform.position - transform.position).normalized;

        if (Vector3.Dot(distance, transform.forward) > 0.75)
        {
            Vector3 pushDir = new(hit.moveDirection.x, 0, hit.moveDirection.z);

            rb.AddForce(pushDir * _pushPower, ForceMode.Impulse);

            _lastHitTime = Time.time;

            if (!_isPushing)
            {
                _isPushing = true;
            }
        }
    }

    public void CheckForObstacleCollision()
    {
        if (_isPushing && Time.time - _lastHitTime > _pushCooldown)
        {
            _isPushing = false;
        }
    }
}