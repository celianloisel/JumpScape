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
    private Button lastSelectedButton = null;

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

                Button buttonComponent = buttonInstance.GetComponent<Button>();

                // Pass both the prefab and the button to the listener
                buttonComponent.onClick.AddListener(() => OnPrefabClicked(prefab, buttonComponent));
            }
        }
    }

    // Update this method to accept both the prefab and the button component
    void OnPrefabClicked(GameObject prefab, Button buttonComponent)
    {
        selectedPrefab = prefab;
        lastSelectedButton = buttonComponent; // Now we assign the clicked button to lastSelectedButton
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

                        // Deactivate the button after placing the prefab
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
