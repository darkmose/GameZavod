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
    }

    void SetPos()
    {
        if (Input.GetMouseButtonUp(0) && canPlace)
        {
            GetComponent<SpriteRenderer>().color = full;
            Destroy(gameObject.GetComponent<ScrewMachines>());
        }
    }

    void CheckPlace()
    {
        otherObjects = Physics2D.OverlapBoxAll(transform.position, new Vector2(12,12),30);
        if (otherObjects.Length == 2 && transform.position.y == 14.19)
        {
            GetComponent<SpriteRenderer>().color = green;
            canPlace = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = red;
            canPlace = false;
        }
    }

    void Update()
    {
        FollowCursor();
    }
    private void FixedUpdate()
    {
        CheckPlace();
    }
}
