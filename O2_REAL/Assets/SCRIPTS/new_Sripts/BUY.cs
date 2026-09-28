
using UnityEngine;
using UnityEngine.InputSystem;
public class BUY : MonoBehaviour
{


    public void Update()
    {
        Camera cam = Camera.main;

        Vector2 mousepos = Mouse.current.position.ReadValue();

        Ray mouseRay = cam.ScreenPointToRay(mousepos);
        RaycastHit hitInfo = new RaycastHit();
        if(Physics.Raycast(mouseRay, out hitInfo))
        {
            if (hitInfo.collider.gameObject.CompareTag("Energy"))
            {
                if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
                {
                    Debug.Log("Hiiren oikeaa painiketta klikattu!");
                }
            }
        }
    }
}
