using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;
    private GameObject previewInstance;

    void OnMouseEnter()
    {
        _highlight.SetActive(true);

        if (PrefabLoader.selectedPrefab != null && previewInstance == null)
        {
            previewInstance = Instantiate(PrefabLoader.selectedPrefab, transform.position + new Vector3(-5.6f, -5.6f, 0), Quaternion.identity);
            previewInstance.name = PrefabLoader.selectedPrefab.name + "_Preview";
            SetTransparency(previewInstance, 0.5f);
        }
    }

    void OnMouseExit()
    {
        if (previewInstance != null)
        {
            Destroy(previewInstance);
            previewInstance = null;
        }

        _highlight.SetActive(false);
    }

    private void SetTransparency(GameObject obj, float alpha)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Color color = renderer.material.color;
            color.a = alpha;
            renderer.material.color = color;
        }
    }
}
