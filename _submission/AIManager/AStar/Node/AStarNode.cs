using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNode : iHeapItem<AStarNode>
{
    public bool walkable;//Is the node walkable
    public Vector3 worldPosition;//World position of the node
    public int gCost;//G cost of the node
    public int hCost;//H cost of the node
    public int gridX, gridY;//Grid x + y the node is within
    public AStarNode parent;//Parent node
    int heapIndex;//Index that this is accessd by within the heap

    public AStarNode(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }//Create a node stating wether it is walkable? its world pos and grid x y pos

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }//Returning the fcost of the node
    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }//Getting or setting the heapindex of the node
    public int CompareTo(AStarNode nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }//Compare this node against another determing whichh is cheapest
}
