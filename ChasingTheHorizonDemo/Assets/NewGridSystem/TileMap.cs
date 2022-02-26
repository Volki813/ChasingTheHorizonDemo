using System.Collections.Generic;
using UnityEngine;
using System;

public class TileMap : MonoBehaviour
{
    public TextAsset mapData;
    public string mapValues;
    public UnitLoader selectedUnit; 

    public TileType[] tileTypes;
    public int[,] tiles;
    public Node[,] graph;

    int mapSizeX = 20;
    int mapSizeY = 20;

    public List<UnitLoader> allyUnits;
    public List<UnitLoader> enemyUnits;

    public GameObject redHighlight;
    public GameObject blueHighlight;

    public List<Node> walkableTiles;
    public List<Node> attackableTiles;

    private void Start()
    {
        for(int i = 0; i < mapData.text.Length; i++)
        {
            mapValues = mapValues + mapData.text[i];
        }

        GenerateMapData();
        GeneratePathfindingGraph();
        GenerateMapVisuals();
    }

    private void GenerateMapData()
    {       
        // Allocate our map tiles
        tiles = new int[mapSizeX, mapSizeY];

        // Initialize our map tiles to default values
        for(int x = 0; x < mapSizeX; x++){
            for(int y = 0; y < mapSizeY; y++){
                tiles[x, y] = 0;
            }
        }

        // Fill in tile values according to map data
        for(int x = 0; x < mapSizeX; x++)
        {
            tiles[x, 0] = (int)Char.GetNumericValue(mapValues[x]);
        }
    }
    private void GeneratePathfindingGraph()
    {
        // Initialize the array
        graph = new Node[mapSizeX, mapSizeY];

        // Initialize a Node for each spot in the array
        for(int x = 0; x < mapSizeX; x++){
            for(int y = 0; y < mapSizeY; y++){
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        // Now that all the nodes exist, calculate their neighbors
        for(int x = 0; x < mapSizeX; x++){
            for(int y = 0; y < mapSizeY; y++){
                //We have a 4-way map baby none of that 6-way hex bullshit                
                if(x > 0)
                    graph[x, y].neighbors.Add(graph[x - 1, y]); //Adds neighbor to the left
                if (x < mapSizeX - 1)
                    graph[x, y].neighbors.Add(graph[x + 1, y]); //Adds neighbor to the right
                if(y > 0)
                    graph[x, y].neighbors.Add(graph[x, y - 1]); //Adds neighbor below
                if(y < mapSizeY - 1)
                    graph[x, y].neighbors.Add(graph[x, y + 1]); //Adds neighbor above
            }
        }
    }
    private void GenerateMapVisuals()
    {
        for(int x = 0; x < mapSizeX; x++){
            for(int y = 0; y < mapSizeY; y++){

                TileType tt = tileTypes[tiles[x, y]];

                GameObject go = Instantiate(tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
                SelectableTile st = go.GetComponent<SelectableTile>();
                st.tileX = x;
                st.tileY = y;
                st.map = this;
            }
        }

        for(int x = 0; x < mapSizeX; x++){
            for(int y = 0; y < mapSizeY; y++){
                graph[x, y].redHighlight = Instantiate(redHighlight, new Vector2(x + 0.5f, y + 0.5f), Quaternion.identity, transform);
                graph[x, y].blueHighlight = Instantiate(blueHighlight, new Vector2(x + 0.5f, y + 0.5f), Quaternion.identity, transform);

                graph[x, y].redHighlight.SetActive(false);
                graph[x, y].blueHighlight.SetActive(false);
            }
        }
    }

    public void GeneratePathTo(int x, int y)
    {
        Dictionary<Node, float> distance = new Dictionary<Node, float>();
        Dictionary<Node, Node> previous = new Dictionary<Node, Node>();

        // List of nodes we haven't checked yet
        List<Node> unvisitedNodes = new List<Node>();

        Node source = graph[selectedUnit.GetComponent<UnitLoader>().tileX, selectedUnit.GetComponent<UnitLoader>().tileY];

        distance[source] = 0;
        previous[source] = null;

        // Initialize everything to have infinie distance
        // since we don't know any better right now.
        foreach (Node n in graph)
        {
            if (n != source)
            {
                distance[n] = Mathf.Infinity;
                previous[n] = null;
            }
            unvisitedNodes.Add(n);
        }

        // The main loop
        while (unvisitedNodes.Count > 0)
        {
            // "u" is going to be the unvisited node with the smallest distance.
            Node u = null;

            foreach (Node possibleU in unvisitedNodes)
            {
                if (u == null || distance[possibleU] < distance[u])
                    u = possibleU;
            }

            unvisitedNodes.Remove(u);

            foreach (Node n in u.neighbors)
            {
                float temp = distance[u] + CostToEnterTile(n.x, n.y);
                if (temp < distance[n])
                {
                    distance[n] = temp;
                    previous[n] = u;
                }
            }
        }
    }
    public List<Node> GenerateRange(int x, int y, int size, UnitLoader unit)
    {
        // Final list that gets returned
        List<Node> finalList = new List<Node>();

        // Temporary list of nodes that still need to be searched
        List<Node> workingList = new List<Node>();

        List<Node> tempList = new List<Node>();

        // Add starting position to temp list
        finalList.Add(graph[x, y]);

        foreach(Node n in graph[x, y].neighbors)
        {
            if(CostToEnterTile(n.x, n.y) < unit.unit.statistics.movement)
            {
                workingList.Add(n);
            }
        }

        for (int i = 0; i < size; i++)
        {
            // Clear the temp list
            tempList.Clear();

            // Copy working list over to temp list
            foreach(Node n in workingList)
            {
                tempList.Add(n);
            }

            // Copy working list over to final list
            foreach(Node n in workingList)
            {
                if(CostToEnterTile(n.x, n.y) < unit.unit.statistics.movement)
                    finalList.Add(n);
            }

            // Now that there are 2 copies of working list, clear it as it needs to be filled with some new nodes
            workingList.Clear();

            // For every node in the temp list, add all it's neighbors to the working list
            foreach(Node n in tempList)
            {
                foreach(Node e in n.neighbors)
                {
                    if(CostToEnterTile(e.x, e.y) < unit.unit.statistics.movement)
                        workingList.Add(e);
                }
            }

            i++;

            if(i == size)
            {
                foreach(Node n in workingList)
                    finalList.Add(n);
            }
        }
        return finalList;
    }
    
    private float CostToEnterTile(int x, int y)
    {
        TileType tt = tileTypes[tiles[x,y]];

        return tt.movementCost;
    }
    private bool CanTraverse(int x, int y)
    {
        if(tileTypes[tiles[x, y]].isWalkable == false)
        {
            return false;
        }
        foreach(UnitLoader u in enemyUnits)
        {
            if (x == (int)(u.transform.position.x - 0.5f) && y == (int)(u.transform.position.y - 0.5f))
            {
                return false;
            }
        }
        foreach(UnitLoader u in allyUnits)
        {
            if (x == (int)(u.transform.position.x - 0.5f) && y == (int)(u.transform.position.y - 0.5f) && u != selectedUnit)
            {
                return false;
            }
        }
        return true;
    }
    public TileType ReturnTileAt(int x, int y)
    {
        return tileTypes[tiles[x, y]];
    }

    public void HighlightTiles()
    {        
        if(walkableTiles != null)
        {
            foreach(Node n in walkableTiles)
            {
                if(CanTraverse(n.x, n.y))
                {
                    n.blueHighlight.SetActive(true);
                }
            }
        }
    }
    public void DehighlightTiles()
    {
        foreach(Node n in graph)
        {
            n.blueHighlight.SetActive(false);
            n.redHighlight.SetActive(false);
        }
        walkableTiles = null;
        attackableTiles = null;
    }
}
