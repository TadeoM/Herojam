using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes {

    // attributes
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Nodes parent;

    // determines what places in the world are walkable
    public Nodes(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    // calculates the total resource cost, which is fcost
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
