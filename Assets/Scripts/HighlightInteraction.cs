using UnityEngine;

public class HighlightInteraction : MonoBehaviour
{
    public GameObject prefabToInstantiate;

    public void InstantiatePrefab()
    {
        if (prefabToInstantiate != null)
        {
            Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
        }
    }
}