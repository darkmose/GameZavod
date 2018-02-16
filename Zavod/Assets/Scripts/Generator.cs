using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Mechanism
{
    public float energPerFrame = 1f;
    InputIO input;
    bool translateEnergy = true;
    public bool stopTranslate = true;
    Mechanism mech;

    void Start()
    {
        EnergyCell = 100f;
        EnergyCurrent = 0;
        Type = "Generator";
        InputCount = 2;
        AlignObjects = new GameObject[InputCount];
        AlignObjects[0] = transform.GetChild(0).gameObject;
        input = AlignObjects[0].GetComponent<InputIO>();
    }


    void EnergyOut()
    {
        if (translateEnergy && !stopTranslate)
        {
            mech = input.connectClient.transform.parent.GetComponent<Mechanism>();

            if (mech.EnergyCurrent < mech.EnergyCell)
            {
                mech.EnergyCurrent = Mathf.Clamp(energPerFrame * Time.deltaTime + mech.EnergyCurrent, 0, mech.EnergyCell);
                print("Client energy  " + mech.EnergyCurrent);
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
        if (!translateEnergy && !stopTranslate)
        {
            if (EnergyCurrent < 100)
            {
                EnergyCurrent = Mathf.Clamp(energPerFrame * Time.deltaTime + EnergyCurrent, 0, EnergyCell);
                print("Self energy  " + EnergyCurrent);
            }
            else
            {
                stopTranslate = true;
            }
        }
        if (mech.EnergyCurrent < mech.EnergyCell)
        {
            stopTranslate = false;
            translateEnergy = true;
        }
        else if (EnergyCurrent < EnergyCell)
        {
            stopTranslate = false;
            translateEnergy = false;
        }


    }

    void Update()
    {
       EnergyOut();
    }
}
