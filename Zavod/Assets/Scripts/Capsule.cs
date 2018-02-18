using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : Mechanism
{

    InputIO[] io;
    public bool isOpen;

    void Start()
    {
        InputCount = 8;
        io = new InputIO[InputCount];
        Type = "Capsule";
        EnergyCell = 10000f;
        EnergyCurrent = 0;
        io[0] = transform.GetChild(0).GetComponent<InputIO>();

        if (PlayerPrefs.HasKey("CapsuleState"))
        {
            isOpen = (PlayerPrefs.GetInt("CapsuleState") == 0) ? false : true;
        }
        else
        {
            isOpen = false;
            PlayerPrefs.SetInt("CapsuleState", 0);
        }

        if (isOpen)
        {
            GetComponent<Animator>().Play("CapsuleOpen");
        }
        else
        {
            GetComponent<Animator>().Play("CapsuleClose");
        }
    }


    public void OpenClose()
    {
        if (!isOpen)
        {
            GetComponent<Animator>().Play("CapsuleOpen");
            isOpen = true;
        }
        else
        {
            GetComponent<Animator>().Play("CapsuleClose");
            isOpen = false;
        }

    }




    void Update()
    {

    }
}
