using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using UnityEditor;
using TMPro;

public class PrefabLoader : MonoBehaviour
{
    public string folderPath = "Assets/Prefabs";
    public GameObject prefabButton;
    public Transform panel;
    private GameObject selectedPrefab = null;

    void Start()
    {
        LoadPrefabs();
    }

    void LoadPrefabs()
    {
        string[] prefabFiles = Directory.GetFiles(folderPath, "*.prefab", SearchOption.AllDirectories);

        foreach (string prefabFile in prefabFiles)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabFile);
            
            if (prefab != null)
            {
                GameObject buttonInstance = Instantiate(prefabButton, panel);

                TextMeshProUGUI buttonText = buttonInstance.GetComponentInChildren<TextMeshProUGUI>();

                if (buttonText != null)
                {
                    buttonText.text = prefab.name;
                }

                buttonInstance.GetComponent<Button>().onClick.AddListener(() => OnPrefabClicked(prefab));
            }
        }
    }

    void OnPrefabClicked(GameObject prefab)
    {
        selectedPrefab = prefab;
    }
    
    void Update()
    {
        if (selectedPrefab != null && Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                GameObject[] highlights = GameObject.FindGameObjectsWithTag("Highlight");
                
                foreach (var highlight in highlights)
                {
                    HighlightInteraction interaction = highlight.GetComponent<HighlightInteraction>();
                    if (interaction != null)
                    {
                        interaction.prefabToInstantiate = selectedPrefab;
                        interaction.InstantiatePrefab();
                    }
                }
            }
        }
    }
}
