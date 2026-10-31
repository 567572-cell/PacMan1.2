using UnityEngine;
using UnityEngine.InputSystem;

public class PacmanController : MonoBehaviour
{
    public float speed = 4f;
    public LayerMask wallLayer;
    private float checkDistance = 0.6f;
    private float checkRadius = 0.35f;

    Rigidbody rb;
    Vector3 currentDirection = Vector3.zero;
    Vector3 queuedDirection = Vector3.zero;

    void Awake() => rb = GetComponent<Rigidbody>();

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        Vector2 input = Vector2.zero;
        if (kb.wKey.isPressed || kb.upArrowKey.isPressed) input.y = 1;
        if (kb.sKey.isPressed || kb.downArrowKey.isPressed) input.y = -1;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) input.x = -1;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) input.x = 1;

        if (input != Vector2.zero)
            queuedDirection = CameraRelative(input);
        //Debug.Log($"input {input} -> queued {queuedDirection}");
    }

    void FixedUpdate()
    {
        // Take the queued turn if there's a gap that way
        if (queuedDirection != Vector3.zero && IsClear(queuedDirection))
            currentDirection = queuedDirection;

        // Stop if we're about to hit a wall
        if (currentDirection != Vector3.zero && !IsClear(currentDirection))
            currentDirection = Vector3.zero;

        // Move
        rb.linearVelocity = currentDirection * speed;
    }

   
        bool IsClear(Vector3 dir)
        {
            bool hit = Physics.SphereCast(transform.position, checkRadius, dir,
                out RaycastHit info, checkDistance, wallLayer);

            if (hit) Debug.Log($"Blocked going {dir} by {info.collider.name}");

            return !hit;
        }

    
    Vector3 CameraRelative(Vector2 input)
    {
        Transform cam = Camera.main.transform;

        // Camera's forward and right, flattened onto the floor
        Vector3 forward = cam.forward; forward.y = 0; forward.Normalize();
        Vector3 right = cam.right; right.y = 0; right.Normalize();

        Vector3 dir = forward * input.y + right * input.x;

        // Snap to the nearest grid axis so he never moves diagonally
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
            return new Vector3(Mathf.Sign(dir.x), 0, 0);
        else
            return new Vector3(0, 0, Mathf.Sign(dir.z));
    }
}