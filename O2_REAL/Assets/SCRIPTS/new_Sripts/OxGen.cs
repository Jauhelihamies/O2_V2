using UnityEngine;

public class OxGen : MonoBehaviour
{
    void Start()
    {
        if (HappiManager.Instance != null) HappiManager.Instance.RegisterOxygenGenerator();
    }
}
