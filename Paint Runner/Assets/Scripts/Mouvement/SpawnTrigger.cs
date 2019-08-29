using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public Transform terrainPrefab, pointsDeSpawnTerrainSuivant;
    public Transform[] pointsDeSpawnSuivants;
    EcranSpawner ecranSpawner;
    


    // Start is called before the first frame update
    void Start()
    {
        ecranSpawner = EcranSpawner.instance;
    }

    private void OnTriggerEnter(Collider c)
    {
        if(!ecranSpawner)
            ecranSpawner = EcranSpawner.instance;


        if (c.CompareTag("Player"))
        {
            ecranSpawner.pointsDeSpawn = pointsDeSpawnSuivants;
            ecranSpawner.terrainParent = transform.parent;
            ecranSpawner.SpawnEcrans();
            ObjectPooler.instance.SpawnFromPool(terrainPrefab.name.Replace("(Clone)", null), pointsDeSpawnTerrainSuivant.position, pointsDeSpawnTerrainSuivant.rotation, ecranSpawner.pointDeRotationTerrain);
            
        }
    }
}
