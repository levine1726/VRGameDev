using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;
public class TerrainGenerator : MonoBehaviour {


    /* 
     * Overview of terrain generator:
     * A prefab of a plane is set at the middle of the coordinate system. Each panel has the opportunity to spawn a tree in the center of it, which currently
     * is based upon a random number generator (RNG) and a chance rate set by the player. This script will take the desired size of the level (number of panels * 2 x number of panels * 2), and then instantiate 
     * each panel along with a tree if it is allowed by the RNG.
     */

    [SerializeField]
    private int horizontalLengthOfGrid; // How many tiles the player wants to specify horizontally
    [SerializeField]
    private int verticalLengthOfGrid; //  How many tiles the player wants to specify horizontally
    [SerializeField]
    private float chanceToSpawnTree = 0.25f;


    [SerializeField]
    private GameObject floorPrefab; 
    [SerializeField]
    private GameObject treePrefab;
    [SerializeField]
    private GameObject monsterPrefab;
    [SerializeField]
    private GameObject floorParent; // An empty game object to hold floor tiles
    [SerializeField]
    private GameObject treeParent; // An empty game object to hold trees
    [SerializeField]
    private GameObject monsterParent;

    public static int maxNumberEnemiesAllowed = 15;
    public static int minNumberEnemiesAllowed = 5;
    private bool monstersSpawned;

    public static int monstersKilled = 0;

    public static int monstersOnField = 0;

    

    public GameObject NavMeshBoundingBox;

    [HideInInspector]
    public static Items[,] itemsOnTile;

    public enum Items
    {
        NONE,
        TREE,
        ENEMY
    }

    

    // Use this for initialization
    void Awake()
    {
       
        itemsOnTile = new Items[horizontalLengthOfGrid, verticalLengthOfGrid];
        GenerateTerrain();
        SetUpNavMeshArea();

        monstersOnField = 0;
        monstersKilled = 0;

    }

   
    /**
     * Method which will generate terrain (floor panels and trees).
     * Assumes that you have applied all variables above in the Unity Development Environment.
     */
    private void GenerateTerrain()
    {
        // Create two for loops to establish a top down 2D array
        for (int i = 0; i < horizontalLengthOfGrid; i++)
        {
            for (int j =  0; j < verticalLengthOfGrid; j++)
            {
                SpawnFloorPanel(i, j);
                if (i != 0 || j != 0)
                {
                    
                    if (Random.Range(0f, 1f) <= chanceToSpawnTree)
                    {
                        SpawnTree(i, j);
                    }

                    
                }
                else
                {
                    itemsOnTile [0,0]  = Items.NONE;
               }
            }
        }
    }

    /**
     * Method which will instaniate a floor tile based off the iterations of the for loop values.
     */
    private void SpawnFloorPanel(int i, int j)
    {
        itemsOnTile[i, j] = Items.NONE;

        //Note that to allign panels, the transform needs to be shifted by 10 units in the x or y direction for the total amount of panels away you are from the center grid
        float xPos = floorParent.transform.position.x + i * 10f;
        float zPos = floorParent.transform.position.y + j * 10f;

        // Create a temporary game object to get a transform with the x and z positions above
        GameObject tempFloorInfo = new GameObject();
        tempFloorInfo.transform.position = new Vector3(xPos, 0f, zPos);

        // Instantiate it in the game environment, update its name, and make it a child to the floorParent for improved clarity
        GameObject newPanel = Instantiate(floorPrefab, tempFloorInfo.transform);
        newPanel.gameObject.name = "Floor Clone";
        newPanel.transform.parent = floorParent.transform;

       

        // Destroy the game element created to remove it from the game environment.
        Destroy(tempFloorInfo);
    }

    /**
     * Method with instaniate a tree tile based off the iterations of the for loop values.
     */
    private void SpawnTree(int i, int j)
    {

        itemsOnTile[i, j] = Items.TREE;

        // Note that to allign trees, the transform needs to be shifted by 10 units in the x or y direction for the total amount of panels away you are from the center 
        float xPos = treeParent.transform.position.x + i * 10f;
        float zPos = treeParent.transform.position.y + j * 10f;

        // Create a temporary game object to get a transform with the x and z positions above
        GameObject tempTreeInfo = new GameObject();
        tempTreeInfo.transform.position = new Vector3(xPos, 0f, zPos);

        // Instantiate it in the game environment, update its name, and make it a child to the floorParent for improved clarity
        GameObject newTree = Instantiate(treePrefab, tempTreeInfo.transform);
        newTree.gameObject.name = "Tree Clone";
        newTree.transform.parent = treeParent.transform;
        
        // Destroy the game element created to remove it from the game environment.
        Destroy(tempTreeInfo);
    }

    

    

   private void SetUpNavMeshArea()
    {
        NavMeshBoundingBox.transform.position = new Vector3((horizontalLengthOfGrid / 2.0f) * 10, 0f, (verticalLengthOfGrid / 2.0f) * 10);
        LocalNavMeshBuilder bakeBox = NavMeshBoundingBox.GetComponent<LocalNavMeshBuilder>();
        bakeBox.m_Size = new Vector3((horizontalLengthOfGrid + 2f) * 10f, 100f, (verticalLengthOfGrid + 2f) * 10);
        Vector3 scaleForNavMeshTravelPanel = new Vector3(bakeBox.m_Size.x, 0f, bakeBox.m_Size.z);
        GameObject navMeshPanel = Instantiate(floorPrefab, NavMeshBoundingBox.transform.position, Quaternion.Euler(0f, 0f, 0f));
        navMeshPanel.gameObject.name = "FloorForNavMeshTravel";
        navMeshPanel.transform.parent = floorParent.transform;
        navMeshPanel.transform.localScale = scaleForNavMeshTravelPanel;
        navMeshPanel.GetComponent<Renderer>().enabled = false;
        navMeshPanel.AddComponent<NavMeshSourceTag>();
    }

    
    
}
