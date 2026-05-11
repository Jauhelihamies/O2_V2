using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PowerGrid : MonoBehaviour
{
    public interface IPowered
    {

        void SetPower(bool on);
        bool GetPower();
        List<IPowered> GetConnected();
        void ConnectTo(IPowered target);

    }
    void DistributePower(IPowered start)
    {
        List<IPowered> worklist = new List<IPowered>();

        worklist.Add(start);
        for (int i = 0; i < worklist.Count; i++)
        {
            IPowered target = worklist[i];
            target.SetPower(true);

            List<IPowered> connected = target.GetConnected();

            foreach (IPowered item in connected)
            {
                if (worklist.Contains(item) == false)
                {
                    worklist.Add(item);
                }
            }
        }

    }

  


    // Update is called once per frame
    void Update()
    {
        
    }
}
