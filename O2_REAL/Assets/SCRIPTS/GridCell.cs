using TMPro;
using UnityEngine;

public class GridCell : MonoBehaviour
{

    public bool onkoVarattu { get; private set; } = false;

    public void AsetaVaratuksi()
    {
        onkoVarattu = true;
        gameObject.SetActive(false);
    }

    public void VapautaRuutu()
    {
        onkoVarattu = false;
    }
}