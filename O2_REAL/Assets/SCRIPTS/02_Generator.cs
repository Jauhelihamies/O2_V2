using System.Collections.Generic;
using System.Security.Cryptography;
using NUnit.Framework;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float HappiKerroin = 0.2f;
    private float Energia = 10f;
    private bool OnkoSähköä = false;
    private float Hapentuotto = 0f;
    public List<Fan> kaikki = new List<Fan>();

    public void Start()
    {
        OnkoSähköä=true;
        Energy();
    }
    public void Energy()
    {
        Energia += 10;
        foreach (Fan k in kaikki)
        {
            k.RotateFan();
        }
    }


    void Update()
    {
        


        if (OnkoSähköä == true)
        {
            Hapentuotto += Time.deltaTime *Energia*HappiKerroin;
            //Debug.Log(Hapentuotto.ToString());
        }

    }
}
