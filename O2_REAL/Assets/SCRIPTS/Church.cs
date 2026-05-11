using UnityEngine;
using TMPro; // Important for TextMeshPro

public class BuildingCounter : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Drag the Text object that is CHILD of this building here")]
    public TMP_Text counterText;

    public string labelText = "NPCs clicked: ";

    void Update()
    {
        // Only updates if the building is active in the scene
        if (counterText != null)
        {
            counterText.text = labelText + BouncyNPC.totalNPCsClicked.ToString();
        }
    }
}