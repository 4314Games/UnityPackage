using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class Pathing : MonoBehaviour {

    public GameObject nodePrefab;
    List<GameObject> city = new List<GameObject>();

    //Linking
    //int isThis1stNode = 0;
    //int nodeOne, nodeTwo;
    
    //
    public Texture buttonUp;
    public Texture buttonDown;
    public bool spawningNodes;

    public GameObject nodeLinking;  //Node other nodes are linking too
    public bool isLinking;          //Are we linking nodes

    

    //TODO, look into adding tag at start of project
    void Start()
    {
        //city.Clear();
        //isThis1stNode = false;
        //isLinking = false;
       // print(isThis1stNode);
    }

    public void PrintStuff(string p_stuff)
    {
        print(p_stuff);
    }

    public void AddNode()
    {
        //Do IT HERE.
    }

    public void SpawnNode(Vector3 p_pos)
    {
        GameObject node = Instantiate(nodePrefab, p_pos, Quaternion.identity);                                     //Create Gizmo/node
        List<int> nodeIDs = new List<int>();                                        
        if(city.Count > 0) for(int x = 0; x < city.Count; x++) nodeIDs.Add(city[x].GetComponent<Node>().GetId());  //Adds Unique ID
        for (int x = 0; x < 1000; x++)
        {
            if (!nodeIDs.Contains(x))
            {
                node.GetComponent<Node>().Construct(x);                                                      //Give node id and position
                break;
            }
        }
        node.transform.parent = GameObject.Find("NodeParent").transform;                                           //Parent nodes to parent inside prefab
        city.Add(node);                                                                                            //Add node to city        
        print("Node Spawned");
    }

//    public void LinkNodes(int p_nodeId)
//    {
//        if(isThis1stNode == 0)                                                                      //if this is first node hit, save it.
//        {
//            nodeOne = p_nodeId;
//            print("First Node Selected : " + nodeOne + " " + p_nodeId);
//            isThis1stNode = 1;
//        }
//        else if(isThis1stNode == 1)                                                                                    //If seoncd node, add to first node using ID's
//        {
//            print("Second Node Selected : " + nodeOne + " " + p_nodeId);
//            int nodePosOne = 0, nodePosTwo = 0;
//            for(int x = 0; x < city.Count; x++)
//            {
//                if (city[x].GetComponent<Node>().GetId() == nodeOne) nodePosOne = x;
//                else if (city[x].GetComponent<Node>().GetId() == p_nodeId) nodePosTwo = x;
//            }
//            city[nodePosOne].GetComponent<Node>().AddConnectedNode(city[nodePosTwo]);
//            isThis1stNode = 0;
//            EventTrigger.Entry entry = new EventTrigger.Entry();
//;        }
        
//    }

//    public void FirstNodeLink(int p_nodeId)
//    {
//        if (isThis1stNode == 0)
//        {
//            nodeOne = p_nodeId;
//            isThis1stNode = 1;
//            print("1stNode");
//        }
//        else if (isThis1stNode == 1)
//        {
//            nodeTwo = p_nodeId;
//            LinkEmUp();
//            print("NodeTwos");
//        }
//    }

//      void LinkEmUp()
//    {
//        print("StartLinking");
//        int nodePosOne = 0, nodePosTwo = 0;
//        for (int x = 0; x < city.Count; x++)
//        {
//            if (city[x].GetComponent<Node>().GetId() == nodeOne) nodePosOne = x;
//            else if (city[x].GetComponent<Node>().GetId() == nodeTwo) nodePosTwo = x;
//        }
//        city[nodePosOne].GetComponent<Node>().AddConnectedNode(city[nodePosTwo]);
//        isThis1stNode = 0;
//    }

    public void RemoveNode(GameObject p_GameObject) //Remove city if node destroyed
    {
        city.Remove(p_GameObject);
        for(int x = 0; x < city.Count; x++) city[x].GetComponent<Node>().RemoveConnectedNode(p_GameObject);       
    }

  





}
