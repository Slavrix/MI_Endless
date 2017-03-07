using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour {

    public string itemName;

    public int itemID;
    
    [Range(0f,100f)]
    public float rarityWeight; // weight to add for item drop probability

    [Tooltip ( "delay for item to be destroyed" )]
    public float delay = 1.15f;

    void OnEnable()
    {
        Destroy ( gameObject, delay );
    }
    
}
