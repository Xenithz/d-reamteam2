using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathFinding : MonoBehaviour
{
    //SINGLETON for Pathfinding is different to GameManager SINGLETONs
    //The case for the Singleton will change
    public static pathFinding _instance;

    void Awake()
    {
        _instance = this;
    }

    //Nodes stores the position of each node
    public Vector3[] NodesPositions;

    //Distances stores the lengths between each node
    public float[][] Distances;

    // Use this for initialization
    void Start()
    {
        NodesPositions = new Vector3[transform.childCount];
        Distances = new float[transform.childCount][];
        for (int i = 0; i < transform.childCount; i++)
        {
            NodesPositions[i] = transform.GetChild(i).position;
            Distances[i] = new float[transform.childCount];
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.childCount; j++)
            {
                if (i == j)
                {
                    Distances[i][j] = -1;
                }
                else
                {
                    Vector3 dir = NodesPositions[j] - NodesPositions[i];
                    if (!Physics.Raycast(NodesPositions[i], dir, dir.magnitude))
                    {
                        //connect to the nodes

                    }
                    else
                    {
                        //Dont connect the nodes
                    }
                }
            }
        }
    }

    public List<Vector3> GetPath(Vector3 start, Vector3 target)
    {
        float shortestDistance = float.MaxValue;
        int startNode = 0;
        int targetNode = 0;

        for (int i = 0; i < NodesPositions.Length; i++)
        {
            if (Vector3.Distance(start, NodesPositions[i]) < shortestDistance)
            {
                shortestDistance = Vector3.Distance(start, NodesPositions[i]);
                startNode = i;
            }
        }

        shortestDistance = float.MaxValue;

        for (int i = 0; i < NodesPositions.Length; i++)
        {
            if (Vector3.Distance(target, NodesPositions[i]) < shortestDistance)
            {
                shortestDistance = Vector3.Distance(target, NodesPositions[i]);
                targetNode = i;
            }
        }


        //DIJKSTRA ALGORITHM
        Queue<int> waitingList = new Queue<int>();
        float[] AccShortestDist = new float[NodesPositions.Length];
        bool[] isVisited = new bool[NodesPositions.Length];
        int[] fromNode = new int[NodesPositions.Length];

        //Initialize variables to default state
        for (int i = 0; i < NodesPositions.Length; i++)
        {
            AccShortestDist[i] = float.MaxValue;
            isVisited[i] = false;
            fromNode[i] = -1;
        }
        AccShortestDist[startNode] = 0;
        waitingList.Enqueue(startNode);

        //MAIN ALGORITHM LOOP
        while (waitingList.Count != 0)
        {
            int curNode = waitingList.Dequeue();
            for (int c = 0; c < NodesPositions.Length; c++)
            {
                if (Distances[curNode][c] != -1 && !isVisited[c])
                {
                    waitingList.Enqueue(c);
                    if (AccShortestDist[curNode] + Distances[curNode][c] < AccShortestDist[c])
                    {
                        AccShortestDist[c] = AccShortestDist[curNode] + Distances[curNode][c];
                        fromNode[c] = curNode;
                    }
                }
            }
            isVisited[curNode] = true;
        }


        //BACKTRACKING
        int tNode = targetNode;
        List<Vector3> path = new List<Vector3>();

        path.Add(NodesPositions[targetNode]);
        while (tNode != startNode)
        {
            tNode = fromNode[tNode];
            path.Add(NodesPositions[tNode]);
        }
        path.Add(NodesPositions[startNode]);
        path.Reverse();
        return path;

    }

    // Update is called once per frame
    void Update()
    {


    }
}