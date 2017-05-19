using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float speed = 8;//speed to move
    Vector3[] path;//Path to follow
    int targetIndex;//Index of the waypoint to goto 
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }//When a path has succesfully been found stop ant routines running and start to follow that path
    }
    public void GotoPath(Vector3 path)
    {
        PathRequestManager.RequestPath(transform.position, path, OnPathFound);
        targetIndex = 0;
        path = new Vector3();
        //clear the path after requesting one
    }
    IEnumerator FollowPath()
    {
        if (path.Length != 0)
        {
            Vector3 currrentWaypoint = path[0];
            while (true)
            {
                if (transform.position == currrentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currrentWaypoint = path[targetIndex];
                }
                transform.position = Vector3.MoveTowards(transform.position, currrentWaypoint, speed * Time.deltaTime);
                yield return null;
            }
        }//Go aloing the grid following a set of waypoints determined by the a star node calculation
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }//Just drawing a line to the node it is travelling to
}
