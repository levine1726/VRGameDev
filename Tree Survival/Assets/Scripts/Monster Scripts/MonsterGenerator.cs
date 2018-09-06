using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class MonsterGenerator : MonoBehaviour {

   
    [SerializeField]
    private GameObject monsterParent;
    [SerializeField]
    private GameObject monsterPrefab;
    private bool isSpawningSystemActive = false;
    private ItemPackageSpawner bowSpawner;
    

    private void Awake()
    {
        bowSpawner = GameObject.Find("Longbow").GetComponentInChildren<ItemPackageSpawner>();
    }

    // Update is called once per frame
    void FixedUpdate () {
		if (CheckMonsterCountForSpawning() && bowSpawner.justPickedUpItem)
        {
            isSpawningSystemActive = true;
            StartCoroutine(BeginMonsterGeneration());
        }
	}

    private bool CheckMonsterCountForSpawning()
    {
        if (TerrainGenerator.monstersOnField < TerrainGenerator.maxNumberEnemiesAllowed && !isSpawningSystemActive)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }



    IEnumerator BeginMonsterGeneration()
    {
        while (TerrainGenerator.monstersOnField < TerrainGenerator.maxNumberEnemiesAllowed)
        { 
            CheckRandomTileAndSpawnMonster();
            yield return new WaitForSecondsRealtime(2f);
        }
        
        isSpawningSystemActive = false;
    }

    private void CheckRandomTileAndSpawnMonster()
    {
        int xAxisLength = TerrainGenerator.itemsOnTile.GetLength(0);
        int yAxisLength = TerrainGenerator.itemsOnTile.GetLength(1);
        int x = (int) Random.Range(0f, (float) xAxisLength);
        int y = (int) Random.Range(0f, (float) yAxisLength);
        bool monsterTileFound = false;
        while (!monsterTileFound)
        {
            if (TerrainGenerator.itemsOnTile[x, y] == TerrainGenerator.Items.NONE)
            {
                monsterTileFound = true;
                SpawnMonster(x, y);
            }
            else
            {         
                x = (int)Random.Range(0f, (float)xAxisLength);
                y = (int)Random.Range(0f, (float)yAxisLength);
            }
        }
        
    }


    private void SpawnMonster(int i, int j)
    {
        // Note that to allign monster, the transform needs to be shifted by 10 units in the x or y direction for the total amount of panels away you are from the center 
        float xPos = monsterParent.transform.position.x + i * 10f;
        float zPos = monsterParent.transform.position.z + j * 10f;

        // Create a temporary game object to get a transform with the x and z positions above
        GameObject tempMonsterInfo = new GameObject();
        tempMonsterInfo.transform.position = new Vector3(xPos, 50f, zPos);

        // Instantiate it in the game environment, update its name, and make it a child to the floorParent for improved clarity
        GameObject newMonster = Instantiate(monsterPrefab, tempMonsterInfo.transform);
        TerrainGenerator.monstersOnField++;

        newMonster.gameObject.name = "Monster Clone";
        newMonster.transform.parent = monsterParent.transform;
        
        newMonster.GetComponent<Rigidbody>().velocity = (new Vector3(0f, -100f, 0f));
        

        // Destroy the game element created to remove it from the game environment.
        Destroy(tempMonsterInfo);
    }
}
