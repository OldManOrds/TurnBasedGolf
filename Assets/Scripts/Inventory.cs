using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public static event Action<List<InventoryItem>> OnInventoryChange;

    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    private void OnEnable()
    {
        Gem.OnGemCollected += Add;
    }
    private void OnDisable()
    {
        Gem.OnGemCollected -= Add;
    }
    public void Add(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToStack();
            Debug.Log($"{item.itemData.displayName} total stack is now {item.stackSize}");
            OnInventoryChange?.Invoke(inventory);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
            Debug.Log($"Added {itemData.displayName} for the first time");
            OnInventoryChange?.Invoke(inventory);
        }
    }
    public void Remove(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            if (item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
            OnInventoryChange?.Invoke(inventory);
        }
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
