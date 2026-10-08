using UnityEngine;
using UnityEngine.InputSystem;

public class BUY : MonoBehaviour
{
    private Camera cam;
    private bool odottaaRuudunValintaa = false;

    [Header("Hinta-asetukset")]
    [SerializeField] private int prefabHinta = 5; // Tavalliset prefabit (Coins & O2)
    [SerializeField] private int easterEggHinta = 500; // Easter Egg -painikkeen hinta

    private int valitunTuotteenHinta = 0; // Pit‰‰ kirjaa parhaillaan valitun tuotteen hinnasta

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

                // --- 1. KOHTEEN VALINTA JA RAHATARKISTUS ---
                if (hitInfo.collider.gameObject.CompareTag("Coins"))
                {
                    if (CoinManager.Instance.HasEnoughCoins(prefabHinta))
                    {
                        valitunTuotteenHinta = prefabHinta;
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
                        valitunTuotteenHinta = prefabHinta;
                        PrefabManager.Instance.ValitsePrefab(2);
                        odottaaRuudunValintaa = true;
                    }
                    else
                    {

                    }
                    return;
                }
                // --- UUSI: EASTER EGG PAINIKE ---
                else if (hitInfo.collider.gameObject.CompareTag("EasterEgg"))
                {
                    if (CoinManager.Instance.HasEnoughCoins(easterEggHinta))
                    {
                        valitunTuotteenHinta = easterEggHinta;
                        PrefabManager.Instance.ValitsePrefab(3); 
                        odottaaRuudunValintaa = true;

                    }
                    else
                    {
                        Debug.Log($"Ei varaa p‰‰si‰ismunaan! Tarvitset {easterEggHinta} kolikkoa.");
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

                            if (CoinManager.Instance.HasEnoughCoins(valitunTuotteenHinta))
                            {
                                CoinManager.Instance.RemoveCoins(valitunTuotteenHinta);
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