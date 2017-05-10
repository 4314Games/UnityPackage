using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class Pathing : MonoBehaviour
{

    public GameObject nodePrefab;
    public List<GameObject> city = new List<GameObject>();

    //Linking
    //int isThis1stNode = 0;
    //int nodeOne, nodeTwo;

    //
    public Texture buttonUp;
    public Texture buttonDown;
    public bool spawningNodes;

    public GameObject nodeLinking;  //Node other nodes are linking too
    public bool isLinking = false;          //Are we linking nodes

    public float heightOffGround;
    public GameObject linkIcon;
    public GameObject tempNode;
    public bool is1stNode;

    //TODO, look into adding tag at start of project
    void Awake()
    {
       
    }

    public void PrintStuff(string p_stuff)
    {
        print(p_stuff);
    }

    public void SpawnNode(Vector3 p_pos)
    {
        Vector3 position = new Vector3(p_pos.x, p_pos.y + heightOffGround, p_pos.z);
        GameObject node = Instantiate(nodePrefab, position, Quaternion.identity);                                     //Create Gizmo/node
        List<int> nodeIDs = new List<int>();
        if (city.Count > 0) for (int x = 0; x < city.Count; x++) nodeIDs.Add(city[x].GetComponent<Node>().GetId());  //Adds Unique ID
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

    public void RemoveNode(GameObject p_GameObject) //Remove city if node destroyed
    {
        city.Remove(p_GameObject);
        for (int x = 0; x < city.Count; x++) city[x].GetComponent<Node>().RemoveConnectedNode(p_GameObject);

        nodeLinking = tempNode;

        //Debug.Log("Node Removed");

    }






}