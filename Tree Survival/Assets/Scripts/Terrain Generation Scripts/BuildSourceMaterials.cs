using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildSourceMaterials : MonoBehaviour {

    public NavMeshBuildSource FloorTile(GameObject tile)
    {
        NavMeshBuildSource tileSrc = new NavMeshBuildSource();
        //tileSrc.area;
        //tileSrc.component;
        tileSrc.shape = NavMeshBuildSourceShape.Mesh;
        tileSrc.size = tile.transform.worldToLocalMatrix.GetScale();
        tileSrc.sourceObject = tile;
        tileSrc.transform = tile.transform.localToWorldMatrix;
        return tileSrc;
    }

    public NavMeshBuildSource Tree(GameObject tree)
    {
        NavMeshBuildSource treeSrc = new NavMeshBuildSource();
        //tileSrc.area;
        //tileSrc.component;
        treeSrc.shape = NavMeshBuildSourceShape.Mesh;
        treeSrc.size = tree.transform.worldToLocalMatrix.GetScale();
        treeSrc.sourceObject = tree;
        treeSrc.transform = tree.transform.localToWorldMatrix;
        return treeSrc;
    }
}
