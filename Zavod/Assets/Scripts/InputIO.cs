﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputIO : MonoBehaviour {

    [HideInInspector]
    public int connects;
    [HideInInspector]
    public GameObject connectClient;
    [HideInInspector]
    public GameObject WireRef;
    

    void Start () {
        connects = 0;
	}
    
    void Delete()
    {
        try
        {
            if (Input.GetMouseButtonUp(1) && GameObject.Find("GlobalScripts").GetComponent<Inventory>().armor[4].type == "instrument")
            {
                RaycastHit2D hit2d = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, 4f, GameObject.Find("Main Camera").GetComponent<MouseSystem>().mask);

                if (hit2d.collider.gameObject == this.gameObject)
                {
                    if (WireRef.isStatic)
                    {
                        Destroy(WireRef);
                        connectClient.transform.parent.GetComponent<Mechanism>().IsConnected = false;
                        connectClient.GetComponent<InputIO>().connects = 0;
                        connectClient.GetComponent<InputIO>().connectClient = null;
                        transform.parent.GetComponent<Mechanism>().IsConnected = false;
                        connects = 0;
                        connectClient = null;
                    }
                }
            }
        }
        catch (System.NullReferenceException)
        {            
        }

    }

    void Update () {
        Delete();
    }
}