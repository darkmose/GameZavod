using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputIO : MonoBehaviour {

    [HideInInspector]
    public int connects;
    [HideInInspector]
    public List<GameObject> connectClient;
    [HideInInspector]
    public List<GameObject> WireRef;
    

    void Start ()
    {
        connectClient = new List<GameObject>();
        WireRef = new List<GameObject>();
        connects = 0;
	}
    
    public void Delete()
    {
        try
        {
            if (Input.GetMouseButtonUp(1) && GameObject.Find("GlobalScripts").GetComponent<Inventory>().armor[4].type == "instrument")
            {
                RaycastHit2D hit2d = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, 4f, GameObject.Find("Main Camera").GetComponent<MouseSystem>().mask);

                if (hit2d.collider.gameObject == this.gameObject)
                {
                    if (WireRef[connects - 1].isStatic && GameObject.Find("GlobalScripts").GetComponent<Inventory>().items.Count < 9 && connects > 0)
                    {
                            Destroy(WireRef[connects - 1]);
                            connectClient[connects - 1].transform.parent.GetComponent<Mechanism>().IsConnected = false;
                            connectClient[connects - 1].GetComponent<InputIO>().connectClient.Remove(transform.Find("Input").gameObject);
                            connectClient[connects - 1].GetComponent<InputIO>().connects--;
                            transform.parent.GetComponent<Mechanism>().IsConnected = false;
                            connectClient.Remove(connectClient[connects-1].transform.Find("Input").gameObject);
                            connects--;                          
                            GameObject.Find("GlobalScripts").GetComponent<Inventory>().items.Add(Resources.Load<GameObject>("prefabs/Mach/WireItem").GetComponent<Item>());  
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
