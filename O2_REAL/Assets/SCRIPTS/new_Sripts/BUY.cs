using UnityEngine;
using UnityEngine.InputSystem;

public class BUY : MonoBehaviour
{
    public void Update()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        Vector2 mousepos = Mouse.current.position.ReadValue();
        Ray mouseRay = cam.ScreenPointToRay(mousepos);
        RaycastHit hitInfo;

        if (Physics.Raycast(mouseRay, out hitInfo))
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {

                if (PrefabManager.Instance == null)
                {

                    return;
                }

                if (hitInfo.collider.gameObject.CompareTag("Coins"))
                {

                    PrefabManager.Instance.ValitsePrefab(1);
                }
                else if (hitInfo.collider.gameObject.CompareTag("O2"))
                {

                    PrefabManager.Instance.ValitsePrefab(2);
                }
            }
        }
    }
}