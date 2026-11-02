using UnityEngine;
using System.Collections.Generic;

public class PelletSpawner : MonoBehaviour
{
    public GameObject pelletPrefab;
    public Renderer floor;              // the floor, used to find the maze size
    public Transform player;            // used for the pellet height
    public LayerMask wallLayer;
    public float spacing = 12f;         // gap between pellets along a line
    public float wallCheckRadius = 4f;  // a pellet spot must have this much room
    public float scanStep = 2f;         // how fine the search grid is
    public float maxCorridorWidth = 80f; // wider than this = open area, no pellets

    void Start()
    {
        Bounds b = floor.bounds;
        float y = player.position.y;
        List<Vector3> placed = new List<Vector3>();

        for (float x = b.min.x; x < b.max.x; x += scanStep)
        {
            for (float z = b.min.z; z < b.max.z; z += scanStep)
            {
                Vector3 point = new Vector3(x, y, z);

                // Skip spots inside or right next to a wall
                if (Physics.CheckSphere(point, wallCheckRadius, wallLayer))
                    continue;

                // Only keep spots halfway between two walls (the corridor's centre line)
                bool centredAcross = IsCentred(point, Vector3.forward);  // corridor runs left-right
                bool centredAlong = IsCentred(point, Vector3.right);     // corridor runs up-down
                if (!centredAcross && !centredAlong)
                    continue;

                // Skip if another pellet is already close by
                if (TooClose(point, placed))
                    continue;

                placed.Add(point);
                Instantiate(pelletPrefab, point, Quaternion.identity, transform);
            }
        }

        Debug.Log("Pellets placed: " + placed.Count);

    }

    // Fires a ray each way along 'dir'. If both hit a wall at (almost) the
    // same distance, the point is in the middle of the corridor.
    bool IsCentred(Vector3 point, Vector3 dir)
    {
        // Fire the rays just above the floor so they can't pass over low walls
        point.y = floor.bounds.center.y + 1f;

        bool hitA = Physics.Raycast(point, dir, out RaycastHit a, maxCorridorWidth, wallLayer);
        bool hitB = Physics.Raycast(point, -dir, out RaycastHit b, maxCorridorWidth, wallLayer);

        if (!hitA || !hitB)
            return false;

        return Mathf.Abs(a.distance - b.distance) <= scanStep;
    }

    bool TooClose(Vector3 point, List<Vector3> placed)
    {
        foreach (Vector3 p in placed)
        {
            if (Vector3.Distance(point, p) < spacing)
                return true;
        }
        return false;
    }
}
