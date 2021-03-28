using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDungeon : MonoBehaviour
{
    public int mapWidth = 50;
    public int mapDepth = 50;
    public int scale = 2;
    private Leaf root;
    private List<Vector2> corridorList = new List<Vector2>();
    byte[,] map;
    // Start is called before the first frame update
    void Start()
    {
        map = new byte[mapWidth, mapDepth];
        for (int z = 0; z < mapDepth; z++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                map[x, z] = 1;
            }
        }
        root = new Leaf(0, 0, mapWidth, mapDepth, scale);
        BSP(root, 6);
        AddCorridors();
        AddRandomCorridors(10);
        DrawMap();
    }

    private void DrawMap()
    {
        for (int z = 0; z < mapDepth; z++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (map[x,z]==1)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(x * scale, 10, z * scale);
                    cube.transform.localScale = new Vector3(scale, scale, scale);
                }
                /*else if (map[x,z]==2)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(x * scale, 10, z * scale);
                    cube.transform.localScale = new Vector3(scale, scale, scale);
                    cube.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }*/
            }
        }
    }

    void BSP(Leaf leaf, int splitDepth)
    {
        if (leaf == null) return;
        if (splitDepth <= 0)
        {
            leaf.Draw(map);
            corridorList.Add(new Vector2(leaf.xPos + leaf.width/2,leaf.zPos + leaf.depth/2));
            return;
        }
        if (leaf.Split())
        {
            BSP(leaf.leftChild, splitDepth - 1);
            BSP(leaf.rightChild, splitDepth - 1);
        }
        else
        {
            leaf.Draw(map);
            corridorList.Add(new Vector2(leaf.xPos + leaf.width/2,leaf.zPos + leaf.depth/2));
        }
    }

    void AddCorridors()
    {
        for (int i = 1; i < corridorList.Count; i++)
        {
            if ((int) corridorList[i].x == (int) corridorList[i-1].x ||
                (int) corridorList[i].y == (int) corridorList[i-1].y)
            {
                line(
                    (int) corridorList[i].x, 
                    (int) corridorList[i].y, 
                    (int) corridorList[i-1].x, 
                    (int) corridorList[i-1].y
                );
            }
            else
            {
                line(
                    (int) corridorList[i].x, 
                    (int) corridorList[i].y, 
                    (int) corridorList[i].x, 
                    (int) corridorList[i-1].y
                );
                line(
                    (int) corridorList[i].x, 
                    (int) corridorList[i].y, 
                    (int) corridorList[i-1].x, 
                    (int) corridorList[i].y
                );
            }
        }
    }

    void AddRandomCorridors(int numHalls)
    {
        for (int i = 0; i < numHalls; i++)
        {
            int startX = Random.Range(5, mapWidth - 5);
            int startZ = Random.Range(5, mapDepth - 5);
            int lengthX = Random.Range(5, mapWidth - 5);
            int lengthZ = Random.Range(5, mapDepth - 5);
            if (Random.Range(0,100) < 50)
            {
                line(startX, startZ, lengthX, startZ);
            }
            else
            {
                line(startX, startZ, startX, lengthZ);
            }
        }
    }
    
    public void line(int x, int y, int x2, int y2)
    {
        int w = x2 - x;
        int h = y2 - y;
        int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
        if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
        if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
        if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
        int longest = Mathf.Abs(w);
        int shortest = Mathf.Abs(h);
        if (!(longest > shortest))
        {
            longest = Mathf.Abs(h);
            shortest = Mathf.Abs(w);
            if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
            dx2 = 0;
        }
        int numerator = longest >> 1;
        for (int i = 0; i <= longest; i++)
        {
            map[x,y] = 0;
            numerator += shortest;
            if (!(numerator < longest))
            {
                numerator -= longest;
                x += dx1;
                y += dy1;
            }
            else
            {
                x += dx2;
                y += dy2;
            }
        }
    }
}
