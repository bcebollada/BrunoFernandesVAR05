using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public Vector3 spawnAreaSize;
    public GameObject objectToSpawn;
    public int numObjectsToSpawn;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, spawnAreaSize);
    }

    public void SpawnObjects()
    {
        for (int i = 0; i < numObjectsToSpawn; i++)
        {
            Vector3 randomPoint = new Vector3(
                Random.Range(transform.position.x - spawnAreaSize.x / 2, transform.position.x + spawnAreaSize.x / 2),
                Random.Range(transform.position.y - spawnAreaSize.y / 2, transform.position.y + spawnAreaSize.y / 2),
                Random.Range(transform.position.z - spawnAreaSize.z / 2, transform.position.z + spawnAreaSize.z / 2)
            );

            Instantiate(objectToSpawn, randomPoint, transform.rotation  );
        }
    }
}
