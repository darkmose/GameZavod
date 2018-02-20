using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : Mechanism
{

    InputIO[] io;
    public bool isOpen;
    public bool playerInside;

    void Start()
    {
        InputCount = 8;
        io = new InputIO[InputCount];
        Type = "Capsule";
        EnergyCell = 10000f;
        EnergyCurrent = 0;
        io[0] = transform.GetChild(0).GetComponent<InputIO>();

        #region PlayerPrefs---PlayerInCapsule
        if (PlayerPrefs.HasKey("CapsulePlayerState"))
        {
            playerInside = (PlayerPrefs.GetInt("CapsulePlayerState") == 0) ? false : true;
        }
        else
        {
            playerInside = false;
            PlayerPrefs.SetInt("CapsuleState", 0);
        }
        #endregion

        #region PlayerPrefs---IsCapsuleOpen
        if (PlayerPrefs.HasKey("CapsuleState"))
        {
            isOpen = (PlayerPrefs.GetInt("CapsuleState") == 0) ? false : true;
        }
        else
        {
            playerInside = false;
            PlayerPrefs.SetInt("CapsuleState", 0);
        }
        #endregion

        if (!playerInside)
        {
            SetLayerName("Default");
            transform.GetChild(5).gameObject.SetActive(false);
        }
        else
        {
            SetLayerName("SoldierLayer");
            transform.GetChild(5).gameObject.SetActive(true);
        }



        if (isOpen)
        {
            transform.GetChild(1).localPosition = new Vector2(0, 3.2f);
            transform.GetChild(1).localRotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            transform.GetChild(1).localPosition = new Vector2(0, 0.562f);
            transform.GetChild(1).localRotation = Quaternion.Euler(0, 0, 0);
        }
    }


    public void SetLayerName(string name)
    {
        transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sortingLayerName = name;
        transform.GetChild(1).GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingLayerName = name;
        transform.GetChild(1).GetChild(1).gameObject.GetComponent<SpriteRenderer>().sortingLayerName = name;
        transform.GetChild(1).GetChild(2).gameObject.GetComponent<SpriteRenderer>().sortingLayerName = name;
    }

    public void OpenClose()
    {
        if (!isOpen)
        {
            GetComponent<Animator>().Play("CapsuleOpen");
            isOpen = true;

            transform.GetChild(5).gameObject.SetActive(true);

            if (playerInside)
            {
                GameObject.Find("Player").GetComponent<Move>().move = true;
            }
        }
        else
        {
            GetComponent<Animator>().Play("CapsuleClose");
            isOpen = false;

            if (playerInside)
            {
                GameObject.Find("Player").GetComponent<Move>().move = false;
            }
            else transform.GetChild(5).gameObject.SetActive(false);
        }
    }




    void Update()
    {

    }
}
