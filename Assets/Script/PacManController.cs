using UnityEngine;
using UnityEngine.InputSystem;

public class PacmanController : MonoBehaviour
{
    public float speed = 4f;
    public LayerMask wallLayer;

    Rigidbody rb;
    Vector3 currentDirection = Vector3.zero;
    Vector3 queuedDirection = Vector3.zero;

    void Awake() => rb = GetComponent<Rigidbody>();

    void Update()
    {
        // TODO: read WASD/arrow keys with Keyboard.current
        // and set queuedDirection (Vector3.forward, back, left, right)
    }

    void FixedUpdate()
    {
        // TODO: if queuedDirection is clear, make it the current direction
        // TODO: if currentDirection is blocked, stop
        // TODO: set rb.linearVelocity
    }

    bool IsClear(Vector3 dir)
    {
        // TODO: Physics.Raycast from transform.position in dir,
        // length ~0.6, using wallLayer. Return true if nothing hit.
        return true;
    }
}