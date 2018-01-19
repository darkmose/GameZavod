using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
   
    int itembarelement;
    public bool isBar,isInv;
    public float playerscale;
    public GameObject itembar;
    private GameObject player;
    public GameObject container;
    public List<Item> items;
    public List<GameObject> cellist;
    public List<Item> hotbar;
    public Item[] armor;
    private Transform fullinventory;
    public Transform armorInv;
    public Transform playerpos;
    public Transform rhand,footsL,footsR,torso;
    public Text textinv,textinvs;
    Animator anima;
    Image imagecur, imageprev;




    //fullinventory === Итемы в правой части
    //itembar === Итемы сверху
    //armorInv === Итемы в левой части
   
            //ARMOR CELLS:
    //imenno 1, a ne 0////1-head
    //2-Torso
    //3-Legs
    //4-R Hand
    //5-L Hand



    public void Start () {
        player = GameObject.Find("Player");
        rhand = player.transform.GetChild(0).GetChild(7).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform;
        footsL = player.transform.GetChild(0).GetChild(7).GetChild(0).GetChild(2).transform;
        footsR = player.transform.GetChild(0).GetChild(7).GetChild(0).GetChild(1).transform;
        torso = player.transform.GetChild(0).GetChild(3).transform;
        isBar = false;
        isInv = false;
        armor = new Item[5];
        hotbar = new List<Item>();
        cellist = new List<GameObject>();
        items = new List<Item>();
        anima = itembar.GetComponent<Animator>();
        itembar.SetActive(false);

        for (int i = 0; i < itembar.transform.childCount; i++)
        {
          cellist.Add(itembar.transform.GetChild(i).gameObject);
        }
             
        itembarelement = 0;
        imagecur = cellist[itembarelement].GetComponent<Image>();
        imagecur.color = new Color32(137, 137, 137, 200);
        fullinventory = itembar.transform.parent.GetChild(1).GetChild(0).transform;
        armorInv = itembar.transform.parent.GetChild(1).GetChild(1).transform;
    
    }

    public int ItemsCount() {
        return items.Count; }


    void CurrentBar() {
        if (Input.GetAxisRaw("ScrollWheel") != 0) {
            if (Input.GetAxisRaw("ScrollWheel") < 0f)
            {
                if (itembarelement < 4)
                {
                    itembarelement++;
                    imagecur = cellist[itembarelement].GetComponent<Image>();
                    imageprev = cellist[itembarelement - 1].GetComponent<Image>();
                    imagecur.color = new Color32(137, 137, 137, 200);
                    imageprev.color = new Color32(255, 255, 255, 200);
                }
            }
            else if (Input.GetAxisRaw("ScrollWheel") > 0f)
            {
                if (itembarelement > 0)
                {
                    itembarelement--;
                    imagecur = cellist[itembarelement].GetComponent<Image>();
                    imageprev = cellist[itembarelement + 1].GetComponent<Image>();

                    imagecur.color = new Color32(137, 137, 137, 200);
                    imageprev.color = new Color32(255, 255, 255, 200);

                }

            }
        }
    }



    public void HelperBarOpen() {
        itembar.SetActive(true);
        anima.SetInteger("state", 1);
        
        if (isBar)
        {
            for (int i = 0; i < 5; i++)
            {
                if (cellist[i].transform.childCount > 0)
                {
                    Destroy(cellist[i].transform.GetChild(0).gameObject);
                }
                
            }

        }

        for (int i = 0; i < hotbar.Count; i++)
        {
                if (itembar.transform.childCount >= i)
                {
                    Item item = hotbar[i];
                    GameObject img = Instantiate(container);
                    img.transform.SetParent(cellist[i].transform);
                    img.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                    img.GetComponent<RectTransform>().localPosition = new Vector3(img.GetComponent<RectTransform>().position.x, img.GetComponent<RectTransform>().position.y, 0);
                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.sprite);
                    cellist[i].GetComponentInChildren<HelperItems>().helpsprefab = item.prefab;
                    cellist[i].GetComponentInChildren<HelperItems>().it = item;
                    cellist[i].GetComponentInChildren<HelperItems>().type = item.type;
                    cellist[i].GetComponentInChildren<HelperItems>().sprite = item.sprite;
                }
                else break;         
        }
        isBar = true;
    }
    public void HelperBarClose() {
        for (int i = 0; i < itembar.transform.childCount; i++)
        {
            if (cellist[i].transform.childCount > 0)
            {
                Destroy(cellist[i].transform.GetChild(0).gameObject);
            }
        }

        anima.SetInteger("state", 2);
        itembar.SetActive(false);
        isBar = false;
    }
    public void HelperInvOpen() {
        playerpos = player.transform;
        playerscale = player.transform.localScale.x;
        player.SetActive(false);
       
        fullinventory.parent.gameObject.SetActive(true);
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
            if (fullinventory.childCount > i)
            {
                if (fullinventory.GetChild(i).childCount==0)
                {
                    Item item = items[i];
                    GameObject img = Instantiate(container);
                    img.transform.SetParent(fullinventory.GetChild(i).transform);
                    img.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    img.GetComponent<RectTransform>().localPosition = new Vector3(img.GetComponent<RectTransform>().position.x, img.GetComponent<RectTransform>().position.y, 0);
                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.sprite);
                    fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().helpsprefab = item.prefab;
                    fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().it = item;
                    fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().type = item.type;
                    fullinventory.GetChild(i).GetComponentInChildren<HelperItems>().sprite = item.sprite;
                }
               
            }
            else break;
        }
        isInv = true;

    }
    public void HelperInvClose() {
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
    public void HelperArmorOpen() {
      
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
                img.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                img.GetComponent<RectTransform>().localPosition = new Vector3(img.GetComponent<RectTransform>().position.x, img.GetComponent<RectTransform>().position.y, 0);
                img.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.sprite);
                armorInv.GetChild(i).GetComponentInChildren<HelperItems>().helpsprefab = item.prefab;
                armorInv.GetChild(i).GetComponentInChildren<HelperItems>().it = item;
                armorInv.GetChild(i).GetComponentInChildren<HelperItems>().type = item.type;
                armorInv.GetChild(i).GetComponentInChildren<HelperItems>().sprite = item.sprite;

        lable:
            continue;
        }
    }
    public void HelperArmorClose() {
        for (int i = 1; i < 6; i++)
        {

            if (armorInv.GetChild(i).transform.childCount > 0)
            {

                Destroy(armorInv.GetChild(i).transform.GetChild(0).gameObject);

            }


        }
    }


    void ShowBar()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isBar && !GameObject.Find("GlobalScripts").GetComponent<PauseMenu>().isPause)
            {
                HelperBarOpen();
            }

            else if (isBar)
            {
                HelperBarClose();
            }
        }        
    }

    void ShowInventory() {

        if (!isInv && Input.GetKeyDown(KeyCode.BackQuote) && !GameObject.Find("GlobalScripts").GetComponent<PauseMenu>().isPause)
        {
            if (!isBar)
            {
                HelperBarOpen();                
            }
            HelperInvOpen();
            HelperArmorOpen();
        }
        else if ((isInv && Input.GetKeyDown(KeyCode.BackQuote)) || (isInv && Input.GetButtonDown("Cancel"))) 
        {
            HelperInvClose();
            HelperBarClose();
            HelperArmorClose();
        }

    }
	

	void Update () {

        ShowBar();
        CurrentBar();
        ShowInventory();
     
    }
}
