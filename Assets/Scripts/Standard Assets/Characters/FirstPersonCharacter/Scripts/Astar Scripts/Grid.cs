using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    // attriubtes
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Nodes[,] grid;

    // diameter of each node, and the grid system
    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    /// <summary>
    /// Creates a grid for A* to analyze
    /// </summary>
    void CreateGrid()
    {
        grid = new Nodes[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        // for each point of the map in the grid, it makes a square for each each point, and determines whether or not the AI can walk on it
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Nodes(walkable, worldPoint, x,y);
            }
        }
    }

    public List<Nodes> GetNeighbours(Nodes node)
    {
        List<Nodes> neighbours = new List<Nodes>();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y < 1; y++) {
                if(x==0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    /// <summary>
    /// Gets the location of the player
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public Nodes NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x,y];
    }

    public List<Nodes> path;

    /// <summary>
    /// draws the color if each point on the grid, 
    /// cyan for the player, red for unwalkable, 
    /// and white for walkable locationss
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if(grid != null)
        {
            foreach (Nodes n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;

                if(path != null)
                {
                    Debug.Log("I got here");
                    if(path.Contains(n))
                    {
                        Debug.Log("got here");
                        Gizmos.color = Color.black;
                    }
                }


                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
            }
        }
    }
}
