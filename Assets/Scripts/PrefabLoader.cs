using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using UnityEditor;
using TMPro;

public class PrefabLoader : MonoBehaviour
{
    public string folderPath = "Assets/Prefabs/Map";
    public GameObject prefabButton;
    public Transform panel;
    private GameObject selectedPrefab = null;
    private Button lastSelectedButton = null;

    void Start()
    {
        LoadPrefabs();
    }

    void LoadPrefabs()
    {
        folderPath = Path.Combine(folderPath, $"1-{GameData.level.ToString()}");
        
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

                Button buttonComponent = buttonInstance.GetComponent<Button>();

                buttonComponent.onClick.AddListener(() => OnPrefabClicked(prefab, buttonComponent));
            }
        }
    }

    void OnPrefabClicked(GameObject prefab, Button buttonComponent)
    {
        selectedPrefab = prefab;
        lastSelectedButton = buttonComponent;
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

                        if (lastSelectedButton != null)
                        {
                            lastSelectedButton.interactable = false;
                            lastSelectedButton = null;
                        }

                        selectedPrefab = null;
                    }
                }
            }
        }
    }
}
