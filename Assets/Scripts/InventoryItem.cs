using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class InventoryItem
{
    public ItemData itemData;
    public int stackSize;

    public InventoryItem(ItemData item)
    {
        itemData = item;
        AddToStack();
    }
    public void AddToStack()
    {
        stackSize++;
    }
    public void RemoveFromStack()
    {
        stackSize--;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
