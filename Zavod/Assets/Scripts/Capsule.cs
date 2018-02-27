using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : Mechanism
{

    InputIO[] io;
    public bool isOpen;
    public bool playerInside;
    bool canOpClose = true;

    void Start()
    {
        InputCount = 6;
        io = new InputIO[InputCount];
        Type = "Capsule";
        EnergyCell = 10000f;
        EnergyCurrent = 0;
        io[0] = transform.GetChild(0).GetComponent<InputIO>();

        IsConnected=true;

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
        if (canOpClose)
        {
            if (!isOpen)
            {
                GetComponent<Animator>().Play("CapsuleOpen");
                isOpen = true;

                transform.GetChild(5).gameObject.SetActive(true);

                if (playerInside)
                {
                    GameObject.Find("Player").GetComponent<Move>().move = true;
                    try
                    {
                        GameObject.Find("ShootHand").GetComponent<Shooting>().canShoot = true;
                    }
                    catch (System.NullReferenceException)
                    {
                    }
                }
            }
            else
            {
                GetComponent<Animator>().Play("CapsuleClose");
                isOpen = false;

                if (playerInside)
                {
                    try
                    {
                        GameObject.Find("ShootHand").GetComponent<Shooting>().canShoot = false;
                    }
                    catch (System.NullReferenceException)
                    {
                    }
                    GameObject.Find("Player").GetComponent<Move>().move = false;
                }
                else transform.GetChild(5).gameObject.SetActive(false);
                
            }

            StartCoroutine(Timer(2));
        }
    }


    IEnumerator Timer(float time)
    {
        canOpClose = false;
        yield return new WaitForSeconds(time);
        canOpClose = true;
    }


    void FixedUpdate()
    {
        if (IsConnected)
        {
            transform.GetChild(1).GetChild(0).gameObject.GetComponent<Animator>().enabled = false;
            transform.GetChild(1).GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            transform.GetChild(1).GetChild(0).gameObject.GetComponent<Animator>().enabled = true;
            transform.GetChild(1).GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        try
        {
            if (io[0].connectClient.transform.parent.GetComponent<Generator>().translateEnergy && io[0].connectClient.transform.parent.GetComponent<Generator>().fuel > 0.00001)
            {
                transform.GetChild(1).GetChild(2).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        catch (UnassignedReferenceException)
        {
            transform.GetChild(1).GetChild(2).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        catch (System.NullReferenceException)
        {
            transform.GetChild(1).GetChild(2).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
