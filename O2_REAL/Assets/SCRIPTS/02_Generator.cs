using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Cryptography;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Rotate : MonoBehaviour
{


    private int MAX_tuotto = 10;
    private float NykyinenHappi = 100;

    private float Energia = 10f;
    private bool OnkoSähköä = false;
    private float Hapentuotto = 0f;

    private float TULO = 0f;
    
    private float HapenKulutus = 0f;
    public List<Fan> kaikki = new List<Fan>();

    public int CurrentNPC = 0;

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
    public void GetNewNpc()
    {
        CurrentNPC++;
        HapenKulutus += 1;
        Laske();
    }
    public void NpcKilled()
    {
        CurrentNPC--;
        HapenKulutus -= 1;
        Laske();
    }

    public void Laske()
    {
        TULO = Hapentuotto - HapenKulutus;
        if (TULO > 10)
        {
            TULO = 10;
        }
        HapenTila();
    }


    void Update()
    {
        


        if (OnkoSähköä == true)
        {
            if (Hapentuotto < MAX_tuotto)
            {
                Hapentuotto += Time.deltaTime * Energia;
            }
            if (Hapentuotto > MAX_tuotto)
            {
                Hapentuotto = MAX_tuotto; 
            }
        }

    }
    private void HapenTila()
    {
        NykyinenHappi = NykyinenHappi + TULO;
        if (NykyinenHappi > 100)
        {
            NykyinenHappi = 100;
        }
        
  
    }
    private void OnGUI()
    {
        GUIStyle labelBig = new GUIStyle(GUI.skin.label);
        labelBig.fontSize = 20;

        GUILayout.BeginHorizontal();

        GUILayout.Label(NykyinenHappi.ToString(),labelBig);

        GUILayout.EndHorizontal();
    }
}
