using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{

    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();//Queue of pathrequest --- manages all the paths with easy access
    PathRequest currentPathRequest;//Current path we are requesting

    static PathRequestManager instance;//Current Path request manager
    PathFinding pathFinding;//Path finder

    bool isProcessingPath;//Are we processing a path

    public void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }//Set the instance to this and get the path finder

    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);//Queue within the Queue
        instance.TryProcessNext();//Try to move along the queue and find a path
    }//Request to move along a path

    void TryProcessNext()
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathFinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }//Dequeue the next element and if it needs to request a path find it a path

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }//once a path has finished being processed tell it to stop processing

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }//START END and callback for a path request
    }
}
