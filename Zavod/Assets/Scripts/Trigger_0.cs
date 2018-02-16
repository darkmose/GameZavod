using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_0 : MonoBehaviour
{
    Inventory scrpt;
    World world;
    bool ignore;
    int count;

    private void Start()
    {
        ignore = false;
        scrpt = GameObject.Find("GlobalScripts").GetComponent<Inventory>();
        world = GameObject.Find("GlobalScripts").GetComponent<World>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "help")
        {
            scrpt.healthPl++;
            scrpt.healthPlus.text = scrpt.healthPl.ToString();
            Destroy(collision.gameObject);
        }

        if (collision.collider.tag == "Item")
        {
            count = GameObject.Find("GlobalScripts").GetComponent<Inventory>().ItemsCount();
            if (count < 9)
            {

                scrpt.items.Add(collision.collider.gameObject.GetComponent<Item>());
                Destroy(collision.collider.gameObject);
            }
            else ignore = true;
        }


        else if (collision.collider.tag == "Coins")
        {

            world.coins++;
            GameObject.Find("GlobalScripts").GetComponent<Inventory>().textinv.text = System.Convert.ToString(world.coins);
            GameObject.Find("GlobalScripts").GetComponent<Inventory>().textinvs.text = System.Convert.ToString(world.coins);
            GetComponent<AudioSource>().PlayOneShot(GetComponent<HelpPlayer>().coins);
            Destroy(collision.collider.gameObject);
   

        }
    }


    void Update()
    {
        Physics2D.IgnoreLayerCollision(9, 8, ignore);

    }

    private void FixedUpdate()
    {
        count = GameObject.Find("GlobalScripts").GetComponent<Inventory>().ItemsCount();
        if (count < 9)
        {
            ignore = false;
        }
    }
}