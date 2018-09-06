using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilingMonsterCheck : MonoBehaviour {

    private Transform panel;
    private int xIndex;
    private int zIndex;
    

	// Use this for initialization
	void Awake () {
        panel = gameObject.transform;

        xIndex = (int) (panel.transform.position.x / 10);
        zIndex = (int) (panel.transform.position.z / 10);
    }
	
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            TerrainGenerator.itemsOnTile[xIndex, zIndex] = TerrainGenerator.Items.ENEMY;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            TerrainGenerator.itemsOnTile[xIndex, zIndex] = TerrainGenerator.Items.NONE;
        }
    }



}
