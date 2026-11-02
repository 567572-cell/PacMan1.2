using UnityEngine;

// Chase: head for Pac-Man's current position.
public class ChaseState : IState
{
    Ghost ghost;
    float timer;

    public ChaseState(Ghost ghost)
    {
        this.ghost = ghost;
    }

    public void Enter()
    {
        ghost.agent.speed = ghost.chaseSpeed;
        Debug.Log(ghost.name + " entered Chase");
    }

    public void Tick()
    {
        // Only recalculate the path every 0.2s (pathfinding is expensive)
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            ghost.agent.SetDestination(ghost.pacman.position);
            timer = 0.2f;
        }
    }

    public void Exit() { }
}
