using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Inventory : MonoBehaviour
{

    [HideInInspector]
    public bool isInv;
    [HideInInspector]
    public float playerscale;
    public GameObject creator;
    GameObject player;
    public GameObject container;
    public List<Item> items;
    public Item[] armor;
    public Transform fullinventory;
    public Transform armorInv;
    [HideInInspector]
    public Transform playerpos;
    [HideInInspector]
    public Transform rhand, footsL, footsR, torso;
    public Text textinv, textinvs;
    public GameObject weatherMenu;
    [HideInInspector]
    public bool isShooting = false;
    public GameObject shoot;
    public Text healthPlus;
    public int healthPl = 0;
    public GameObject timerrr;



    //fullinventory === Итемы в правой части
    //itembar === Итемы сверху
    //armorInv === Итемы в левой части

    //ARMOR CELLS:
    //1-head
    //2-Torso
    //3-Legs
    //4-R Hand
    //5-L Hand



    public void Start()
    {
        player = GameObject.Find("Player");
        rhand = player.transform.GetChild(0).GetChild(7).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform;
        footsL = player.transform.GetChild(0).GetChild(7).GetChild(0).GetChild(2).transform;
        footsR = player.transform.GetChild(0).GetChild(7).GetChild(0).GetChild(1).transform;
        torso = player.transform.GetChild(0).GetChild(3).transform;

        isInv = false;
        armor = new Item[5];
        items = new List<Item>();

        if (!PlayerPrefs.HasKey("ShopIndex"))
        {
            PlayerPrefs.SetInt("ShopIndex", 0);
        }
        weatherMenu.SetActive(false);
        healthPlus.text = "0";
    }
    public Transform getInvTransform()
    {
        return fullinventory;
    }

    public int ItemsCount()
    {
        return items.Count;
    }



    public void HelperInvOpen()
    {
        playerpos = player.transform;
        playerscale = player.transform.localScale.x;

        if (!isInv)
        {
            fullinventory.parent.gameObject.SetActive(true);
        }
        if (isInv)
        {
            for (int i = 0; i < 8; i++)
            {

                if (fullinventory.GetChild(i).transform.childCount > 0)
                {

                    Destroy(fullinventory.GetChild(i).transform.GetChild(0).gameObject);

                }

            }

        }

        for (int i = 0; i < items.Count; i++)
        {
            Item item = items[i];
            GameObject img = Instantiate(container);
            img.transform.SetParent(fullinventory.GetChild(i).transform);
            img.GetComponent<RectTransform>().localScale = Vector2.one;
            img.GetComponent<RectTransform>().localPosition = Vector2.zero;
            img.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.sprite);
            fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().helpsprefab = item.prefab;
            fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().it = item;
            fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().type = item.type;
            fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().sprite = item.sprite;
            fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().bullets = item.bullets;
            fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().descr = item.descr;
            fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().cost = item.cost;
            fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().handPos = item.handPos;
            fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().handRadius = item.handRadius;
            fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().handAngle = item.handAngle;
            fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().GunDamage = item.GunDamage;
            fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().speed = item.speed;
            fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().health = item.healthPlus;
            fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().damage = item.damagePlus;
        }
        isInv = true;
    }
    public void HelperInvClose()
    {
        if (!GameObject.Find("GlobalScripts").GetComponent<PauseMenu>().isPause)
        {
            player.SetActive(true);
        }
        for (int i = 0; i < 8; i++)
        {

            if (fullinventory.GetChild(i).transform.childCount > 0)
            {

                Destroy(fullinventory.GetChild(i).transform.GetChild(0).gameObject);

            }

        }

        fullinventory.parent.gameObject.SetActive(false);

        isInv = false;
    }
    public void HelperArmorOpen()
    {

        for (int i = 1; i < 6; i++)
        {
            if (armorInv.GetChild(i).childCount > 0)
            {
                Destroy(armorInv.GetChild(i).GetChild(0).gameObject);
            }
        }

        for (int i = 1; i < 6; i++)
        {
            Item item;
            try
            {
                item = armor[i - 1];
                Sprite test = Resources.Load<Sprite>(item.sprite);
            }
            catch (System.NullReferenceException)
            {
                goto lable;
            }
            GameObject img = Instantiate(container);
            img.transform.SetParent(armorInv.GetChild(i).transform);
            img.GetComponent<RectTransform>().localScale = Vector2.one;
            img.GetComponent<RectTransform>().localPosition = Vector2.zero;
            img.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.sprite);
            armorInv.GetChild(i).GetComponentInChildren<HelperItems>().helpsprefab = item.prefab;
            armorInv.GetChild(i).GetComponentInChildren<HelperItems>().it = item;
            armorInv.GetChild(i).GetComponentInChildren<HelperItems>().type = item.type;
            armorInv.GetChild(i).GetComponentInChildren<HelperItems>().sprite = item.sprite;
            armorInv.GetChild(i).GetComponentInChildren<HelperItems>().bullets = item.bullets;
            armorInv.GetChild(i).GetComponentInChildren<HelperItems>().descr = item.descr;
            armorInv.GetChild(i).GetComponentInChildren<HelperItems>().cost = item.cost;
            armorInv.GetChild(i).GetComponentInChildren<HelperItems>().handPos = item.handPos;
            armorInv.GetChild(i).GetComponentInChildren<HelperItems>().handRadius = item.handRadius;
            armorInv.GetChild(i).GetComponentInChildren<HelperItems>().handAngle = item.handAngle;
            armorInv.GetChild(i).GetComponentInChildren<HelperItems>().GunDamage = item.GunDamage;
            armorInv.GetChild(i).GetComponentInChildren<HelperItems>().speed = item.speed;
            armorInv.GetChild(i).GetComponentInChildren<HelperItems>().health = item.healthPlus;
            armorInv.GetChild(i).GetComponentInChildren<HelperItems>().damage = item.damagePlus;
        lable:
            continue;
        }
    }
    public void HelperArmorClose()
    {
        for (int i = 1; i < 6; i++)
        {

            if (armorInv.GetChild(i).transform.childCount > 0)
            {

                Destroy(armorInv.GetChild(i).transform.GetChild(0).gameObject);

            }


        }
    }



    void ShowInventory()
    {

        if (!isInv && Input.GetButtonDown("Inventory") && !GameObject.Find("GlobalScripts").GetComponent<PauseMenu>().isPause && !creator.activeSelf)
        {
            player.GetComponent<Move>().move = false;
            HelperInvOpen();
            HelperArmorOpen();
            shoot.SetActive(false);
        }
        else if ((isInv && Input.GetButtonDown("Inventory")) || (isInv && Input.GetButtonDown("Cancel")))
        {
            HelperInvClose();
            HelperArmorClose();
            player.GetComponent<Move>().move = true;
            if (isShooting)
            {
                shoot.SetActive(true);
            }
            else
            {
                shoot.SetActive(false);
            }
        }

    }
    void showWeather()
    {
        if (!weatherMenu.activeSelf)
        {
            weatherMenu.SetActive(true);
        }
        else
        {
            weatherMenu.SetActive(false);
        }
    }


    void Update()
    {
        if (!player.GetComponent<Move>().isAttack)
        {
            ShowInventory();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            showWeather();
        }
        if ((Input.GetButtonDown("CraftMenu") || Input.GetButtonDown("Inventory")) && !isInv)
        {
            if (creator.activeSelf && creator.GetComponent<ShopMenu>().can)
            {
                if (isShooting)
                {
                    shoot.SetActive(true);
                }
                creator.SetActive(false);
            }
            else if (!creator.activeSelf && Input.GetButtonDown("CraftMenu"))
            {
                if (isShooting)
                {
                    shoot.SetActive(false);
                }
                creator.SetActive(true);
                creator.GetComponent<ShopMenu>().createZone.gameObject.SetActive(false);
                creator.GetComponent<ShopMenu>().Open(PlayerPrefs.GetInt("ShopIndex"));
            }

        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            timerrr.SetActive(true);
            timerrr.GetComponent<Timer>().SetTime(1);
        }

    }
}
