using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public Animator laser, cellAnim;
    public Item[] itemsknife = new Item[3];
    public Item[] itemspist = new Item[3];
    public Transform createZone;
    public Transform Pics, Description, Cost;
    public Transform infoScreen;
    GameObject container;
    public GameObject icon, icon2;
    [HideInInspector]
    public bool can = true;


    void Start()
    {
        container = Resources.Load<GameObject>("prefabs/container");
    }

    IEnumerator animate(Item item)
    {
        can = false;
        createZone.GetChild(0).GetChild(1).gameObject.SetActive(true);
        createZone.parent.gameObject.SetActive(true);
        createZone.GetComponent<Animator>().Play("Creator2Move");
        yield return new WaitForSeconds(createZone.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        createZone.GetChild(0).GetComponent<Animator>().Play("createItem2");
        createZone.GetChild(0).GetChild(1).GetComponent<Animator>().Play("createItem");
        yield return new WaitForSeconds(2f);
        createZone.GetChild(0).GetChild(1).gameObject.SetActive(false);
        if (item.GunDamage != 0)
        {
            item.AllAmmo = item.bullets - item.AmmoInOne;
            item.AmmoCurOne = item.AmmoInOne;
        }
        icon2.GetComponent<Image>().enabled = false;
        GameObject.Find("GlobalScripts").GetComponent<Inventory>().items.Add(item);
        createZone.GetComponent<Animator>().Play("Creator2Back");
        yield return new WaitForSeconds(createZone.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        createZone.parent.gameObject.SetActive(false);
        can = true;
    }

    public void Buy(HelperItems item)
    {

        int count = GameObject.Find("GlobalScripts").GetComponent<Inventory>().ItemsCount();

        if (GameObject.Find("GlobalScripts").GetComponent<World>().coins >= item.cost && count < 9 && can)
        {
            GameObject.Find("GlobalScripts").GetComponent<World>().coins -= item.cost;
            GameObject.Find("GlobalScripts").GetComponent<Inventory>().textinv.text = System.Convert.ToString(GameObject.Find("GlobalScripts").GetComponent<World>().coins);
            GameObject.Find("GlobalScripts").GetComponent<Inventory>().textinvs.text = System.Convert.ToString(GameObject.Find("GlobalScripts").GetComponent<World>().coins);
            icon2.GetComponent<Image>().enabled = true;
            icon2.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.sprite);
            createZone.gameObject.SetActive(true);
            StartCoroutine(animate(item.it));
        }
    }

    public void Open(int index)
    {
        PlayerPrefs.SetInt("ShopIndex", index);
        if (index == 0)
        {
            Load(itemsknife);
        }
        else
        {
            Load(itemspist);
        }

    }

    void Load(Item[] massiv)
    {
        for (int i = 0; i < 3; i++)
        {
            Item item = massiv[i];
            GameObject img = Pics.GetChild(i).GetChild(0).gameObject;
            img.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.sprite);
            Pics.GetChild(i).GetComponentInChildren<HelperItems>().helpsprefab = item.prefab;
            Pics.GetChild(i).GetComponentInChildren<HelperItems>().it = item;
            Pics.GetChild(i).GetComponentInChildren<HelperItems>().type = item.type;
            Pics.GetChild(i).GetComponentInChildren<HelperItems>().sprite = item.sprite;
            Pics.GetChild(i).GetComponentInChildren<HelperItems>().bullets = item.bullets;
            Pics.GetChild(i).GetComponentInChildren<HelperItems>().descr = item.descr;
            Pics.GetChild(i).GetComponentInChildren<HelperItems>().cost = item.cost;
            Pics.GetChild(i).GetComponentInChildren<HelperItems>().handPos = item.handPos;
            Pics.GetChild(i).GetComponentInChildren<HelperItems>().handRadius = item.handRadius;
            Pics.GetChild(i).GetComponentInChildren<HelperItems>().handAngle = item.handAngle;
            Pics.GetChild(i).GetComponentInChildren<HelperItems>().GunDamage = item.GunDamage;
            Pics.GetChild(i).GetComponentInChildren<HelperItems>().speed = item.speed;
            Pics.GetChild(i).GetComponentInChildren<HelperItems>().health = item.healthPlus;
            Pics.GetChild(i).GetComponentInChildren<HelperItems>().damage = item.damagePlus;
            //ItemsLoad::Pics
            Description.GetChild(i).GetComponentInChildren<Text>().text = Pics.GetChild(i).GetComponentInChildren<HelperItems>().descr;
            Cost.GetChild(i).GetChild(3).GetComponent<Text>().text = Pics.GetChild(i).GetComponentInChildren<HelperItems>().cost.ToString();
        }
    }

    public void TakeInfo(HelperItems item)
    {
        infoScreen.GetChild(0).GetComponent<Text>().text = (item.GunDamage == 0) ? item.damage.ToString() : item.GunDamage.ToString();
        infoScreen.GetChild(1).GetComponent<Text>().text = (item.speed != 0) ? item.speed.ToString() : "~~~";
        infoScreen.GetChild(2).GetComponent<Text>().text = (item.bullets != 0) ? item.bullets.ToString() : "~~~";
        icon.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.sprite);
    }
}
