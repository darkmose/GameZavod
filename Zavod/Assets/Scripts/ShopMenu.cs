using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ShopMenu : MonoBehaviour {
    public int cost;
    public Animator laser, cellAnim;
    Item[] itemsknife;
    Item[] itemspist;
    Transform MenuChoose;
    Transform Pics, Description, Cost;
    
    void Start () {
        MenuChoose = transform.GetChild(3).GetChild(0).GetChild(1).transform;
        Pics = MenuChoose.GetChild(0);
        Description = MenuChoose.GetChild(1);
        Cost = MenuChoose.GetChild(2);
    }

    void Open(int index)
    {
        if (index == 0)
        {

        }
        else
        {

        }

    }

    void Load(Item[] massiv) {
        MenuChoose.GetChild(0);

    }
	

	void Update ()
    {

	}
}
