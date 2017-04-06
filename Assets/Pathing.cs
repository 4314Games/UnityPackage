using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Pathing : MonoBehaviour {

    public GameObject nodePrefab;
    List<GameObject> city = new List<GameObject>();

    //Linking
    bool isThis1stNode = false;
    int nodeOne;
    //
    public Texture buttonUp;
    public Texture buttonDown;
    public bool spawningNodes;

    //TODO, look into adding tag at start of project
    void Start()
    {
        //city.Clear();
        isThis1stNode = false;
        print(isThis1stNode);
    }

    public void PrintStuff(string p_stuff)
    {
        print(p_stuff);
    }

    public void SpawnNode(Vector3 p_pos)
    {
        GameObject node = Instantiate(nodePrefab, p_pos, Quaternion.identity);                                     //Create Gizmo/node
        List<int> nodeIDs = new List<int>();                                        
        if(city.Count > 0) for(int x = 0; x < city.Count; x++) nodeIDs.Add(city[x].GetComponent<Node>().GetId());  //Adds Unique ID
        for(int x = 0; x < 1000; x++)   if(!nodeIDs.Contains(x)) node.GetComponent<Node>().Construct(x, p_pos);    //Give node id and position
        node.transform.parent = GameObject.Find("NodeParent").transform;                                           //Parent nodes to parent inside prefab
        city.Add(node);                                                                                            //Add node to city        
        print("Node Spawned");
    }

    public void LinkNodes(int p_nodeId)
    {
        if(!isThis1stNode)                                                                      //if this is first node hit, save it.
        {
            nodeOne = p_nodeId;
            print("First Node Selected");
            isThis1stNode = true;
        }
        else                                                                                    //If seoncd node, add to first node using ID's
        {
            print("Second Node Selected");
            int nodePosOne = 0, nodePosTwo = 0;
            for(int x = 0; x < city.Count; x++)
            {
                if (city[x].GetComponent<Node>().GetId() == nodeOne) nodePosOne = x;
                else if (city[x].GetComponent<Node>().GetId() == p_nodeId) nodePosTwo = x;
            }
            city[nodePosOne].GetComponent<Node>().AddConnectedNode(city[nodePosTwo]);
            isThis1stNode = false;
        }
    }


    public void RemoveNode(GameObject p_GameObject) //Remove city if node destroyed
    {
        city.Remove(p_GameObject);
        for(int x = 0; x < city.Count; x++) city[x].GetComponent<Node>().RemoveConnectedNode(p_GameObject);       
    }

   IEnumerator shitFight()
    {
        yield return new WaitForSeconds(0.2f);
        if (spawningNodes) spawningNodes = false;
        else spawningNodes = true;

        print("CuntRash");
    }





}
