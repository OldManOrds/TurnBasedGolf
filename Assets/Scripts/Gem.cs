using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, Collectible
{
    public static event HandleGemCollected OnGemCollected;
    public delegate void HandleGemCollected(ItemData itemData);
    public ItemData gemData;
    public void Collect()
    {
        Destroy(gameObject);
        OnGemCollected?.Invoke(gemData);
    }
    //void Add(ItemData itemData);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
