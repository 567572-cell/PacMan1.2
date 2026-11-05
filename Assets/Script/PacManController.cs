using UnityEngine;
using UnityEngine.InputSystem;

public class PacmanController : MonoBehaviour
{
    public float speed = 4f;
    public LayerMask wallLayer;
    public float checkDistance = 0.6f;
    public float checkRadius = 0.35f;

    Rigidbody rb;
    Vector3 currentDirection = Vector3.zero;
    Vector3 queuedDirection = Vector3.zero;
    Vector3 facing = Vector3.forward;
    Vector3 startPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }
    void Start()
    {
        Vector3[] dirs = { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };

        foreach (Vector3 d in dirs)
        {
            if (IsClear(d))
            {
                facing = d;
                transform.rotation = Quaternion.LookRotation(facing);
                break;
            }
        }
    }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        // Forward: keep going the way he's facing
        if (kb.wKey.wasPressedThisFrame || kb.upArrowKey.wasPressedThisFrame)
            queuedDirection = facing;

        // Back: turn around
        if (kb.sKey.wasPressedThisFrame || kb.downArrowKey.wasPressedThisFrame)
            queuedDirection = -facing;

        // Left: 90° to his left
        if (kb.aKey.wasPressedThisFrame || kb.leftArrowKey.wasPressedThisFrame)
            queuedDirection = new Vector3(-facing.z, 0, facing.x);

        // Right: 90° to his right
        if (kb.dKey.wasPressedThisFrame || kb.rightArrowKey.wasPressedThisFrame)
            queuedDirection = new Vector3(facing.z, 0, -facing.x);
    }

    void FixedUpdate()
    {
        if (queuedDirection != Vector3.zero && IsClear(queuedDirection))
        {
            currentDirection = queuedDirection;
            facing = currentDirection;
            transform.rotation = Quaternion.LookRotation(facing);
        }

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

    public void ResetToStart()
    {
        rb.position = startPos;
        transform.position = startPos;
        rb.linearVelocity = Vector3.zero;
        currentDirection = Vector3.zero;
        queuedDirection = Vector3.zero;
    }

}