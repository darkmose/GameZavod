using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillGenerator : Generator {
    

    void Start()
    {
        energPerFrame = 2f;
        fuel = 1;
        stopTranslate = true;
        translateEnergy = true;
        EnergyCell = 150f;
        EnergyCurrent = 0;
        Type = "WindmillGenerator";
        InputCount = 1;
        AlignObjects = new GameObject[InputCount];
        AlignObjects[0] = transform.GetChild(0).gameObject;
        input = AlignObjects[0].GetComponent<InputIO>();
        IsConnected = false;
    }

    void EnergyOutHelper()
    {
        if (translateEnergy && !stopTranslate)
        {
            mech = input.connectClient[0].transform.parent.GetComponent<Mechanism>();        


            if (mech.EnergyCurrent < mech.EnergyCell)
            {
                mech.EnergyCurrent = Mathf.Clamp(energPerFrame * Time.deltaTime + mech.EnergyCurrent, 0, mech.EnergyCell);
                fuel -= Time.deltaTime;
             
            }
            else
            {
                if (EnergyCurrent < EnergyCell)
                {
                    translateEnergy = false;
                }
                else
                {
                 
                    stopTranslate = true;
                }
            }
        }
        else if (!translateEnergy && !stopTranslate)
        {
            if (EnergyCurrent < EnergyCell)
            {
                EnergyCurrent = Mathf.Clamp(energPerFrame * Time.deltaTime + EnergyCurrent, 0, EnergyCell);
                fuel -= Time.deltaTime;
            }
            else
            { 
                stopTranslate = true;
            }
        }

        if (EnergyCurrent < EnergyCell)
        {
            stopTranslate = false;
            translateEnergy = false;
        }
        if (mech.EnergyCurrent < mech.EnergyCell)
        {
            stopTranslate = false;
            translateEnergy = true;
        }
    }


    void EnergyOut()
    {
            if (IsConnected && input.connectClient[0].transform.parent.gameObject.name == "Capsule")
            {
                EnergyOutHelper();
            }
            else
            {
                stopTranslate = false;
            }  
    }

    void Update ()
    {
        EnergyOut();
	}
}
