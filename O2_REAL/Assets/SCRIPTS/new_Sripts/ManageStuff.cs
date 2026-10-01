using UnityEngine;
using UnityEngine.InputSystem;

public class GridManager : MonoBehaviour
{
    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Camera cam = Camera.main;
            if (cam == null) return;

            Vector2 mousepos = Mouse.current.position.ReadValue();
            Ray mouseRay = cam.ScreenPointToRay(mousepos);
            RaycastHit hitInfo;

            if (Physics.Raycast(mouseRay, out hitInfo))
            {

                if (hitInfo.transform.name.StartsWith("Grid_Cell"))
                {
                    Transform soluTransform = hitInfo.transform;
                    RakennaJaLukitseSolu(soluTransform);
                }
                else
                {

                }
            }
        }
    }

    private void RakennaJaLukitseSolu(Transform solu)
    {
        if (PrefabManager.Instance == null || PrefabManager.Instance.ValittuPrefab == null)
        {
  
            return;
        }
        GameObject uusiRakenne = Instantiate(PrefabManager.Instance.ValittuPrefab, solu.position, solu.rotation);

        Destroy(solu.gameObject);
    }
}