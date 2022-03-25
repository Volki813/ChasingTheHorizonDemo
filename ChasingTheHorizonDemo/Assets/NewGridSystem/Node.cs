using System.Collections.Generic;
using UnityEngine;
public class Node
{
    public List<Node> neighbors;

    private Node leftNode;
    private Node rightNode;
    private Node aboveNode;
    private Node belowNode;

    public int x;
    public int y;

    public GameObject redHighlight;
    public GameObject blueHighlight;

    public Node()
    {
        neighbors = new List<Node>();
    }

    public void SetNeighbors()
    {
        foreach(Node n in neighbors)
        {
            if(n.x > x && rightNode == null)
            {
                rightNode = n;
            }
            else if(n.x < n.y && leftNode == null)
            {
                leftNode = n;
            }
            else if(n.y > y && aboveNode == null)
            {
                aboveNode = n;
            }
            else if(n.y < y && belowNode == null)
            {
                belowNode = n;
            }
        }
    }

    public int DistanceTo(Node n)
    {
        return ((int)Mathf.Abs(x - n.x) + (int)Mathf.Abs(y - n.y));
    }
}
