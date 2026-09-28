using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop2D : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;

    void Awake()
    {
        // Etsit‰‰n pelin p‰‰kamera valmiiksi
        mainCamera = Camera.main;
    }

    // Unity kutsuu t‰t‰ automaattisesti, kun objektin Collideria klikataan hiirell‰
    void OnMouseDown()
    {
        isDragging = true;

        // Lasketaan erotus hiiren ja objektin keskipisteen v‰lill‰, 
        // jotta objekti ei "hyp‰hd‰" keskelt‰ kiinni hiireen kun sit‰ klikataan.
        offset = transform.position - GetMouseWorldPosition();
    }

    void Update()
    {
        // Jos hiiren vasen painike vapautetaan, lopetetaan vet‰minen
        if (Mouse.current != null && !Input.GetMouseButtonDown(0))
        {
            isDragging = false;
        }

        // Jos objektia vedet‰‰n, liikutetaan se hiiren kohdalle (huomioiden offset)
        if (isDragging)
        {
            Vector3 mousePos = GetMouseWorldPosition();
            transform.position = new Vector3(mousePos.x + offset.x, mousePos.y + offset.y, transform.position.z);
        }
    }

    // Apufunktio, joka muuttaa hiiren ruutukoordinaatit pelimaailman 2D-koordinaateiksi
    private Vector3 GetMouseWorldPosition()
    {
        if (Mouse.current == null) return Vector3.zero;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, Mathf.Abs(mainCamera.transform.position.z)));

        return mouseWorldPos;
    }
}