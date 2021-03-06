﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
public class PathFinding : MonoBehaviour
{

    PathRequestManager requestManager;
    Grid grid;
    //To access the PathRequestManager(PRM) and the world Grid
    private void Awake()
    {
        grid = GetComponent<Grid>();
        requestManager = GetComponent<PathRequestManager>();
    }//Get the grid and the PRM
    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }//Start to find a new path

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        AStarNode startNode = grid.NodeFromWorldPoint(startPos);
        AStarNode targetNode = grid.NodeFromWorldPoint(targetPos);


        if (startNode.walkable && targetNode.walkable)
        {
            Heap<AStarNode> openSet = new Heap<AStarNode>(grid.MaxSize);
            HashSet<AStarNode> closedSet = new HashSet<AStarNode>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                AStarNode currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }

                foreach (AStarNode neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);

    }//Find the best possible path from a position to a position

    Vector3[] RetracePath(AStarNode startNode, AStarNode endNode)
    {
        List<AStarNode> path = new List<AStarNode>();
        AStarNode currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);

        return waypoints;
    }//Retrace the path the determine the best possible path

    Vector3[] SimplifyPath(List<AStarNode> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }//Simplify the path to an array of waypoints

    int GetDistance(AStarNode nodeA, AStarNode nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        return 14 * distX + 10 * (distY - distX);
    }
    //Get the distance between nodes


}
