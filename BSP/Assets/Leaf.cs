using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf
{
    public int xPos, zPos, width, depth, scale;
    int roomMin=5;
    public Leaf leftChild, rightChild;
    public Leaf(int x, int z, int w, int d, int s)
    {
        xPos = x;
        zPos = z;
        width = w;
        depth = d;
        scale = s;
    }

    public bool Split()
    {
        if (width <= roomMin || depth <= roomMin) return false;
        bool splitHorizontal = Random.Range(0, 100)<50;
        if (width>depth && width/depth >= 1.2)
        {
            splitHorizontal = false;
        }
        else if(depth>width && depth/width >= 1.2)
        {
            splitHorizontal = true;
        }

        int max = (splitHorizontal) ? depth : width - roomMin;
        if (max <= roomMin) return false;
        if (splitHorizontal)
        {
            int l1depth = Random.Range(roomMin, max);
            leftChild = new Leaf(xPos, zPos, width, l1depth, scale);
            rightChild = new Leaf(xPos, zPos + l1depth, width, depth - l1depth, scale);
        }
        else
        {
            int l1width = Random.Range(roomMin, max);
            leftChild = new Leaf(xPos, zPos, l1width, depth, scale);
            rightChild = new Leaf(xPos+l1width, zPos , width - l1width,  depth, scale);
        }
        return true;
    }
    
    public void Draw(byte[,] map)
    {
        /*Color color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
        for (int x = xPos; x < width + xPos; x++)
        {
            for (int z = zPos; z < depth + zPos; z++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(x * scale, 0, z * scale);
                cube.transform.localScale = new Vector3(scale, scale, scale);
                cube.GetComponent<Renderer>().material.SetColor("_Color", color);
            }
        }*/
        int wallSize = Random.Range(1, 3);
        for (int x = xPos + wallSize; x < width+xPos-wallSize; x++)
        {
            for (int z = zPos + wallSize; z < depth+zPos-wallSize; z++)
            {
                map[x, z] = 0;
            }
        }
    }
}
