using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightInteraction : MonoBehaviour
{
    public GameObject prefabToInstantiate;
	private GameObject previewInstance;

    public void InstantiatePrefab()
    {
        if (prefabToInstantiate != null)
        {

            var spawnedMap = Instantiate(prefabToInstantiate, (transform.position + new Vector3(-5.6f, -5.6f, 0)), Quaternion.identity);
            spawnedMap.name = prefabToInstantiate.name;
            spawnedMap.AddComponent<PersistentObject>();
        }
    }
}