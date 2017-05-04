using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    public bool displayGridGizmos;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    AStarNode[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

    void Awake()
    {
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

		CreateGrid ();

    }
    void Update()
    {
        CreateGrid();
    }
	void CreateGrid()
	{
		grid = new AStarNode[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;

		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius,unwalkableMask));
                grid[x, y] = new AStarNode(walkable, worldPoint,x,y);
            }
		}
	}
    public AStarNode NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        
        int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY-1) * percentY);

        return grid[x, y];
    }
    public List<AStarNode> GetNeighbours(AStarNode node)
    {
        List<AStarNode> neighbours = new List<AStarNode>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
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
    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
            if (grid != null && displayGridGizmos)
            {
                foreach (AStarNode item in grid)
                {
                    Gizmos.color = (item.walkable) ? Color.white : Color.red;
                    Gizmos.DrawCube(item.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
                }
            }

    }
}
