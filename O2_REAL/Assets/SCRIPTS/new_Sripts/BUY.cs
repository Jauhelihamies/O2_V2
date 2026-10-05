using UnityEngine;
using UnityEngine.InputSystem;

public class BUY : MonoBehaviour
{
    private Camera cam;
    private bool odottaaRuudunValintaa = false;

    [Header("Hinta-asetukset")]
    [SerializeField] private int prefabHinta = 5; // Molemmat prefabit maksavat nyt 5 kolikkoa

    private void Start()
    {
        cam = Camera.main;
    }

    public void Update()
    {
        if (cam == null || Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousepos = Mouse.current.position.ReadValue();
            Ray mouseRay = cam.ScreenPointToRay(mousepos);
            RaycastHit hitInfo;

            if (Physics.Raycast(mouseRay, out hitInfo))
            {
                if (PrefabManager.Instance == null || CoinManager.Instance == null) return;

                if (hitInfo.collider.gameObject.CompareTag("Coins"))
                {

                    if (CoinManager.Instance.HasEnoughCoins(prefabHinta))
                    {
                        PrefabManager.Instance.ValitsePrefab(1);
                        odottaaRuudunValintaa = true;
                    }
                    else
                    {
    
                    }
                    return;
                }
                else if (hitInfo.collider.gameObject.CompareTag("O2"))
                {

                    if (CoinManager.Instance.HasEnoughCoins(prefabHinta))
                    {
                        PrefabManager.Instance.ValitsePrefab(2);
                        odottaaRuudunValintaa = true;
                    }
                    else
                    {

                    }
                    return;
                }

                if (odottaaRuudunValintaa)
                {
                    GridCell klikattuRuutu = hitInfo.collider.GetComponent<GridCell>();

                    if (klikattuRuutu != null)
                    {
                        if (!klikattuRuutu.onkoVarattu)
                        {
                            if (CoinManager.Instance.HasEnoughCoins(prefabHinta))
                            {
                                CoinManager.Instance.RemoveCoins(prefabHinta);

                                PrefabManager.Instance.SpawnValittuPrefab(klikattuRuutu.transform.position, klikattuRuutu);
                                odottaaRuudunValintaa = false;
                            }
                            else
                            {

                                odottaaRuudunValintaa = false;
                            }
                        }
                        else
                        {

                        }
                    }
                }
            }
        }
    }
}