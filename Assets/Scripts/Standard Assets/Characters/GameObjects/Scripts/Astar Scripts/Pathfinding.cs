using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    public Transform seeker, target;

    Grid grid;

    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    void Update()
    {
        FindPath(seeker.position, target.position);
    }

    /// <summary>
    /// Finds what locations are closed and open, and makes them into a list
    /// then determines what distance the neighbours are from the start and end positions
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="targetPos"></param>
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Nodes startNode = grid.NodeFromWorldPoint(startPos);
        Nodes targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Nodes> openSet = new List<Nodes>();
        HashSet<Nodes> closedSet = new HashSet<Nodes>();
        openSet.Add(startNode);
        
        while(openSet.Count>0)
        {
            Nodes currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if ((openSet[i].fCost < currentNode.fCost) || openSet[i].fCost == currentNode.fCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Nodes neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour)) {
                    continue;
                }

                int newMoveCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMoveCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMoveCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    public void RetracePath(Nodes startNode, Nodes endNode)
    {
        List<Nodes> path = new List<Nodes>();
        Nodes currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;

    }

    /// <summary>
    /// determines which equation to use, uses the first return if the end location
    /// is closer in the X directions, and uses the second one otherwise
    /// </summary>
    /// <param name="nodeA"></param>
    /// <param name="nodeB"></param>
    /// <returns></returns>
    public int GetDistance(Nodes nodeA, Nodes nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
