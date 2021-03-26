using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf
{
    private int xPos, zPos, width, depth, scale;
    public Leaf leftChild, rightChild;
    public Leaf(int x, int z, int w, int d, int s)
    {
        xPos = x;
        zPos = z;
        width = w;
        depth = d;
        scale = s;
    }

    public bool Split(int level)
    {
        if (Random.Range(0, 100)<50)
        {
            int l1depth = Random.Range((int) (depth * 0.1f), (int) (depth * 0.7f));
            leftChild = new Leaf(xPos, zPos, width, l1depth, scale);
            rightChild = new Leaf(xPos, zPos + l1depth, width, depth - l1depth, scale);
        }
        else
        {
            int l1width = Random.Range((int) (depth * 0.1f), (int) (depth * 0.7f));
            leftChild = new Leaf(xPos, zPos, l1width, depth, scale);
            rightChild = new Leaf(xPos+l1width, zPos , width - l1width,  depth, scale);
        }
       leftChild.Draw(level);
       rightChild.Draw(level);
        return true;
    }
    
    public void Draw(int level)
    {
        Color color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
        for (int x = xPos; x < width+xPos; x++)
        {
            for (int z = zPos; z < depth+zPos; z++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(x * scale, level * 3, z * scale);
                cube.transform.localScale = new Vector3(scale, scale, scale);
                cube.GetComponent<Renderer>().material.SetColor("_Color", color);
            }
        }
    }
}
