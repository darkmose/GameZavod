using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Mechanism
{
    public float energPerFrame = 1f;
    InputIO input;
    public bool translateEnergy = true;
    public bool stopTranslate = true;
    public float fuel = 0f;

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
        IsConnected = false;
    }


    void EnergyOut()
    {
        if (IsConnected && fuel > 0.00001f && input.connectClient.transform.parent.gameObject.name == "Capsule")
        {
            GetComponent<Animator>().SetBool("On", true);
            if (translateEnergy && !stopTranslate)
            {
                mech = input.connectClient.transform.parent.GetComponent<Mechanism>();

                if (mech.EnergyCurrent < mech.EnergyCell)
                {
                    mech.EnergyCurrent = Mathf.Clamp(energPerFrame * Time.deltaTime + mech.EnergyCurrent, 0, mech.EnergyCell);
                    fuel -= Time.deltaTime;
                    AnimateEngine();
                    print("Client energy  " + mech.EnergyCurrent);
                    print("Current fuel  " + fuel);
                }
                else
                {
                    if (EnergyCurrent < EnergyCell)
                    {
                        translateEnergy = false;
                    }
                    else
                    {
                        DontAnimateEngine();
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
                    AnimateEngine();
                    print("Self energy  " + EnergyCurrent);
                }
                else
                {
                    DontAnimateEngine();
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
        else
        {
            stopTranslate = false;
            DontAnimateEngine();
            GetComponent<Animator>().SetBool("On", false);
        }
    }

    void AnimateEngine()
    {
        transform.Find("Fire").gameObject.SetActive(true);
        transform.Find("Smoke").gameObject.SetActive(true);
        transform.Find("Energy").gameObject.SetActive(true);
    }
    void DontAnimateEngine()
    {
        transform.Find("Fire").gameObject.SetActive(false);
        transform.Find("Smoke").gameObject.SetActive(false);
        transform.Find("Energy").gameObject.SetActive(false);
    }

    void AddFuel(int count)
    {
        fuel = Mathf.Clamp(fuel + count, 0, 1000);
    }

    void Update()
    {
        EnergyOut();
        if (Input.GetKeyUp(KeyCode.Z))
        {
            AddFuel(3);
        }
    }
}
