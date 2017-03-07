using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ChestState {closed = 0, open };

public class SelectRandomChest : MonoBehaviour {

    public GameObject [ ] rewardItemArray;

    public ChestState chestState;

    public Sprite chestClosed;
    public Sprite chestOpen;
    SpriteRenderer sr;

    [Tooltip ( "offset value for the creation location of generated item" )]
    public Vector3 offsetPos;

    [Tooltip ( "Text object Must be set in inspector" )]
    public Text rarityDisplayText;
    

    void OnEnable ()
    {
        sr = GetComponent<SpriteRenderer> ( );
        chestState = ChestState.closed;
        sr.sprite = chestClosed;
    }

   


    void OnMouseDown ( )

    {
        //change the sprite
        //instantiate item


        if(this.chestState == ChestState.closed)
        {
            chestState = ChestState.open;
            sr.sprite = chestOpen;
            Debug.Log ( chestState );
            spawnRandomItem ( );
            Invoke ( "CloseChest", 1.15f );
        }
  

    }

    void CloseChest()
    {
        chestState = ChestState.closed;
        sr.sprite = chestClosed;
        rarityDisplayText.text = "";

    }


    void spawnRandomItem()
    {
        float totalItemWeight = 0;

        for ( int i = 0; i < rewardItemArray.Length; i++ )
        {

            itemScript itemCompScript = rewardItemArray [ i ].GetComponent<itemScript> ( );
            totalItemWeight += itemCompScript.rarityWeight;

        }

        float rolledNumber = Random.Range ( 1, totalItemWeight );
        totalItemWeight = 0; //clear totalweight


        for ( int i = 0; i < rewardItemArray.Length; i++ )
        {
            Debug.Log ( "-------------------------------loop " + i + " of " + rewardItemArray.Length + "---------------------------------------" );

            itemScript itemCompScript = rewardItemArray [ i ].GetComponent<itemScript> ( );
            Debug.Log ( "loop at " + itemCompScript.gameObject.name + " at index" + i );
            Debug.Log ( "total weight is " + totalItemWeight + " item weight of " + itemCompScript.rarityWeight );

            Debug.Log ( "item " + itemCompScript.gameObject.name + " range is " + totalItemWeight + " to " + (totalItemWeight + itemCompScript.rarityWeight) );

            if(rolledNumber <= totalItemWeight + itemCompScript.rarityWeight )
            {
                Vector3 pos = transform.position + offsetPos;
                Instantiate ( rewardItemArray [ i ], pos, transform.rotation );
                Debug.Log ( "spawning " + rewardItemArray [ i ].name );

                if (rarityDisplayText != null)
                {

                rarityDisplayText.text = rewardItemArray[i].name + " ; Rarity =" + itemCompScript.rarityWeight + " %";

                }

                break;
                
            }

            totalItemWeight += itemCompScript.rarityWeight;

            Debug.Log ( "-------------------------------loop " + i + " of " + rewardItemArray.Length + " ended --------------------------------" );

        }
    }

}
