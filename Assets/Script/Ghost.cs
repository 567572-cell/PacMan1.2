using UnityEngine;
using UnityEngine.AI;

// The ghost owns a state machine and ticks it every frame.
// What the ghost actually does is decided by its current state.
[RequireComponent(typeof(NavMeshAgent))]
public class Ghost : MonoBehaviour
{
    public Transform pacman;
    public Color colour = new Color(1f, 0.4f, 0.7f);
    public float chaseSpeed = 30f;
    Vector3 startPos;

    [HideInInspector] public NavMeshAgent agent;
    StateMachine machine = new StateMachine();

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.angularSpeed = 720f;
        agent.acceleration = 100f;

        GetComponent<Renderer>().material.color = colour;

        // Find Pac-Man by his tag if nobody dragged him in
        if (pacman == null)
            pacman = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
        // If the ghost was placed slightly off the NavMesh, snap it onto the nearest point
        if (!agent.isOnNavMesh && NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 50f, NavMesh.AllAreas))
            agent.Warp(hit.position);

        machine.ChangeState(new ChaseState(this));
        startPos = transform.position;
    }

    void Update()
    {
        machine.Tick();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.PacmanCaught();
    }

    public void ResetToStart()
    {
        agent.Warp(startPos);
        machine.ChangeState(new ChaseState(this));
    }
}
