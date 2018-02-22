using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewMachines : MonoBehaviour
{

    private GameObject Object;
    Collider2D[] otherObjects;
    bool canPlace;

    Color32 green = new Color32(4, 128, 0, 128);
    Color32 red = new Color32(255, 0, 0, 143);
    Color32 full = new Color32(255, 255, 255, 255);


    private void Start()
    {
        GetComponent<SpriteRenderer>().color = green;
        canPlace = false;
    }

    void FollowCursor()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.localScale = new Vector2(transform.localScale.x*-1,1);
        }
        
    }

    void SetPos()
    {
        if (Input.GetMouseButtonUp(0) && canPlace)
        {
            GetComponent<SpriteRenderer>().color = full;
            Destroy(gameObject.GetComponent<ScrewMachines>());

            GameObject.Find("Player").GetComponent<Move>().isAttack = false;

            try
            {
                GameObject.Find("ShootHand").GetComponent<Shooting>().canShoot = true;
            }
            catch (System.NullReferenceException)
            {
            }
        }
        else if (Input.GetMouseButtonUp(1) || Input.GetButtonUp("Inventory") || Input.GetButtonDown("CraftMenu"))
        {
            Destroy(gameObject);
        }
    }

    void CheckPlace()
    {
        Vector2 size = GetComponent<BoxCollider2D>().size;
        otherObjects = Physics2D.OverlapBoxAll(transform.position,size,0,GameObject.Find("Main Camera").GetComponent<MouseSystem>().mechMask);
        if (otherObjects.Length <= 3 && transform.GetChild(1).position.y >= 13.5f && transform.GetChild(1).position.y <=13.8f)
        {
            GetComponent<SpriteRenderer>().color = green;
            canPlace = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = red;
            canPlace = false;
        }
        print(otherObjects.Length);
    }

    void Update()
    {
        FollowCursor();
        SetPos();
    }
    private void FixedUpdate()
    {
        CheckPlace();

        GameObject.Find("Player").GetComponent<Move>().isAttack = true;

        try
        {
            GameObject.Find("ShootHand").GetComponent<Shooting>().canShoot = false;
        }
        catch (System.NullReferenceException)
        {          
        }           
        
    }
}
