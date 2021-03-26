using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDungeon : MonoBehaviour
{
    public int mapWidth = 50;
    public int mapDepth = 50;
    public int scale = 2;
    private Leaf root;
    
    // Start is called before the first frame update
    void Start()
    {
        root = new Leaf(0, 0, mapWidth, mapDepth, scale);
        BSP(root, 3);
    }

    void BSP(Leaf leaf, int splitDepth)
    {
        if (leaf == null) return;
        if (splitDepth <= 0) return;
        if (leaf.Split(splitDepth))
        {
            BSP(leaf.leftChild, splitDepth - 1);
            BSP(leaf.rightChild, splitDepth - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
