using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvButtons : MonoBehaviour {
    GameObject cell;
    public GameObject prefab;
    string PathPanel;
    string PathPopup;
    GameObject popap;
    Button[] menu;
    string[] menuname = new string[2];

    [HideInInspector]
    public bool isEvent,isEndAnimButton;
    Inventory inventorrrrry;
    HelpPlayer player;
    Text dmg, armr;
    float colliderXradius;

    private void Start()
    {   
        
        dmg = GameObject.Find("StatsOnScreen").transform.GetChild(0).GetComponent<Text>();
        armr = GameObject.Find("StatsOnScreen").transform.GetChild(1).GetComponent<Text>();
        player = GameObject.Find("Player").GetComponent<HelpPlayer>();
        colliderXradius = 1.11f;
        menu = new Button[2];
        isEndAnimButton = false;
        inventorrrrry = GameObject.Find("GlobalScripts").GetComponent<Inventory>();
        isEvent = false;

        

        cell = this.gameObject;
        PathPanel = "prefabs/Panel";
        PathPopup = "prefabs/Popup";
    }

    void SetMenuName()
    {
        if (cell.transform.parent == inventorrrrry.getInvTransform())
        {
            menuname[0] = "Equip";
            menuname[1] = "Throw Out";
        }
        else
        {
            menuname[0] = "Unequip";
            menuname[1] = "ThrowOut";
        }
    }

    IEnumerator TextSize(int i) {

        for (byte j = 0; j < 250; j += 50)
        {
            menu[i].GetComponentInChildren<Text>().color = new Color32(255, 255, 255, j);
            yield return new WaitForSeconds(0.01f);
        }

    }
    IEnumerator MenuExic()
    {
        for (int i = 0; i < menu.Length; i++)
        {
            menu[i] = Instantiate<Button>(Resources.Load<Button>(PathPopup));
            menu[i].transform.SetParent(popap.transform);
            menu[i].transform.localScale = new Vector2(1f, 1f);
            menu[i].GetComponentInChildren<Text>().color = new Color32(25, 25, 25, 0);
            menu[i].GetComponentInChildren<Text>().text = menuname[i]; 
            yield return StartCoroutine(TextSize(i));
        }
        if (cell.transform.parent != inventorrrrry.getInvTransform())
        {
            menu[0].onClick.AddListener(UnEquip);
            menu[1].onClick.AddListener(Remove);
        }
        else
        {
            menu[0].onClick.AddListener(Equip);
            menu[1].onClick.AddListener(Remove);
        }
        isEndAnimButton = true;
    }

    void Remove()
    {
            inventorrrrry.items.Remove(cell.GetComponentInChildren<HelperItems>().it);
            GameObject nobj = Instantiate<GameObject>(Resources.Load<GameObject>(cell.GetComponentInChildren<HelperItems>().helpsprefab));
            nobj.transform.position = new Vector2(inventorrrrry.playerpos.position.x + 5 * inventorrrrry.playerscale, inventorrrrry.playerpos.position.y+5);
            DestroyObject(cell.transform.GetChild(0).gameObject);
            cell.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/boxSelect");
            if (isEvent && isEndAnimButton)
            {
               Destroy(popap);
               isEvent = false;
            }
    }

    void ItemBack(int i) {
        Item temp = inventorrrrry.armor[i - 1];
        inventorrrrry.armor[i - 1] = cell.GetComponentInChildren<HelperItems>().it;
        inventorrrrry.items.Remove(cell.GetComponentInChildren<HelperItems>().it);
        inventorrrrry.items.Add(temp);
        Destroy(inventorrrrry.rhand.GetChild(0).gameObject);
    }

    private void InCellArmorHelper(int index)
    {
        if (inventorrrrry.armorInv.GetChild(index).childCount == 0)
        {
            inventorrrrry.armor[index - 1] = cell.GetComponentInChildren<HelperItems>().it;
            inventorrrrry.items.Remove(cell.GetComponentInChildren<HelperItems>().it);
            DestroyObject(cell.transform.GetChild(0).gameObject);
            cell.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/boxSelect");
            inventorrrrry.HelperArmorOpen();
        }
        else
        {
            ItemBack(index);
        }
        if (isEvent && isEndAnimButton)
        {
            Destroy(popap);
            isEvent = false;
        }

    }

    void HandHelper(string sprite, HelperItems item)
    {
        if (inventorrrrry.rhand.childCount>0)
        {
            Destroy(inventorrrrry.rhand.GetChild(0).gameObject);
        }
        GameObject newObj = Instantiate(prefab);
        newObj.transform.SetParent(inventorrrrry.rhand);
        newObj.transform.localPosition = item.handPos;
        newObj.transform.localRotation = Quaternion.Euler(0f, 0f, item.handAngle.z);
        newObj.transform.localScale = new Vector2(1f, 1f);
        newObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(sprite);
        newObj.GetComponent<SpriteRenderer>().sortingLayerName = "RobotLayer";
        newObj.GetComponent<SpriteRenderer>().sortingOrder = 300;
    }


    void Equip()
    {
        if (cell.GetComponentInChildren<HelperItems>().type == "hands" || cell.GetComponentInChildren<HelperItems>().type == "instrument")
        {
            HandHelper(cell.GetComponentInChildren<HelperItems>().sprite,cell.GetComponentInChildren<HelperItems>());
            InCellArmorHelper(5);

            if (inventorrrrry.armor[4].GunDamage != 0)
            {
                player.damage = 5 + cell.GetComponentInChildren<HelperItems>().GunDamage;
                dmg.text = cell.GetComponentInChildren<HelperItems>().GunDamage.ToString();
                inventorrrrry.isShooting = true;
                player.transform.GetChild(0).GetComponent<CapsuleCollider2D>().size = new Vector2(colliderXradius, 2.11f);
            }
            else
            {
                player.damage = 5 + cell.GetComponentInChildren<HelperItems>().damage;
                dmg.text = player.damage.ToString();
                inventorrrrry.isShooting = false;
                player.transform.GetChild(0).GetComponent<CapsuleCollider2D>().size = new Vector2(colliderXradius + cell.GetComponentInChildren<HelperItems>().handRadius,2.11f);
            }


          inventorrrrry.HelperInvOpen();
          inventorrrrry.HelperArmorOpen();
        }
        else if (cell.GetComponentInChildren<HelperItems>().type == "legs")
        {
            InCellArmorHelper(3);
        }
        else if (cell.GetComponentInChildren<HelperItems>().type == "head")
        {
            InCellArmorHelper(1);
        }
    }
    void UnEquip() {
        if (inventorrrrry.items.Count < 9)
        {
            if (cell.GetComponentInChildren<HelperItems>().type == "hands")
            {
                player.transform.GetChild(0).GetComponent<CapsuleCollider2D>().size = new Vector2(colliderXradius, 2.11f);
                player.damage = 5;
                Destroy(inventorrrrry.rhand.GetChild(0).gameObject);
                inventorrrrry.armor[4] = null;
                inventorrrrry.items.Add(cell.GetComponentInChildren<HelperItems>().it);
                Destroy(cell.transform.GetChild(0).gameObject);
                inventorrrrry.isShooting = false;
            }
            else if (cell.GetComponentInChildren<HelperItems>().type == "legs")
            {

            }
            else
            {

            }
            inventorrrrry.HelperInvOpen();
            inventorrrrry.HelperArmorOpen();
        }
    }

    public void Enter()
    {       
        transform.localScale = new Vector2(1.05f,1.05f);
        GetComponent<Image>().color = new Color32(184, 177, 169, 164);
    }

    public void Exit()
    {
        transform.localScale = new Vector2(1f, 1f);
        GetComponent<Image>().color = new Color32(210, 205, 198, 154);
    }

    public void PopupAd()
    {
        if (cell.transform.childCount > 0 && !isEvent)
        {
            SetMenuName();
            isEndAnimButton = false;
            popap = Instantiate(Resources.Load<GameObject>(PathPanel));
            popap.transform.SetParent(cell.transform.parent);
            popap.GetComponent<RectTransform>().anchorMin = cell.GetComponent<RectTransform>().anchorMin;
            popap.GetComponent<RectTransform>().anchorMax = cell.GetComponent<RectTransform>().anchorMax;
            popap.GetComponent<RectTransform>().offsetMin = cell.GetComponent<RectTransform>().offsetMin;
            popap.GetComponent<RectTransform>().offsetMax = cell.GetComponent<RectTransform>().offsetMax;
            popap.GetComponent<popHelp>().cel = this.gameObject;
            popap.transform.position = cell.transform.position;
            popap.transform.localScale = new Vector2(1.5f, 1.5f);
            StartCoroutine(MenuExic());

            isEvent = true;
        }
    }

    
   

}
