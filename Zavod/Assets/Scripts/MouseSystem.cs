using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSystem : MonoBehaviour
{

    GameObject wire, newWire;
    public LayerMask mask;
    [HideInInspector]
    public bool isWireSet = false;
    GameObject CurrentIO;

    void Start()
    {
        wire = Resources.Load<GameObject>("prefabs/wire");
    }


    void ChekIO()
    {

        if (Input.GetMouseButtonUp(0) && !isWireSet && GameObject.Find("GlobalScripts").GetComponent<Inventory>().armor[4].type == "instrument")
        {
            if (!GameObject.Find("GlobalScripts").GetComponent<Inventory>().isShooting)
            {
                RaycastHit2D hit = Physics2D.Raycast((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, 4f, mask);

                try
                {
                    if (hit.collider.CompareTag("WireIO") && hit.collider.GetComponent<InputIO>().connects == 0)
                    {
                        CurrentIO = hit.collider.gameObject;
                        newWire = Instantiate(wire, Vector2.zero, Quaternion.identity, GameObject.Find("Wires").transform);
                        newWire.GetComponent<Wire>().OutPut = hit.collider.gameObject;
                        newWire.GetComponent<Wire>().side = (hit.collider.gameObject.transform.localPosition.x > 0) ? "right" : "left";
                        isWireSet = true;
                    }
                }
                finally{}
            }

        }

    }

    private void Connect()
    {
        if (isWireSet)
        {
            if (Input.GetMouseButtonUp(0) && !GameObject.Find("GlobalScripts").GetComponent<Inventory>().isShooting)
            {
                RaycastHit2D hit = Physics2D.Raycast((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, 4f, mask);
                try
                {
                    if (hit.collider.CompareTag("WireIO") && hit.collider.GetComponent<InputIO>().connects == 0)
                    {
                        if (hit.collider.gameObject != CurrentIO)
                        {
                            newWire.GetComponent<Wire>().InPut = hit.collider.gameObject;
                            newWire.GetComponent<Wire>().StopWire();
                            newWire = null;
                            isWireSet = false;
                            hit.collider.GetComponent<InputIO>().connects++;
                            hit.collider.GetComponent<InputIO>().connectClient = CurrentIO;
                            CurrentIO.GetComponent<InputIO>().connects++;
                            CurrentIO.GetComponent<InputIO>().connectClient = hit.collider.gameObject;
                        }
                    }
                }
                finally{}                
            }
        }
    }


    void Update()
    {
        ChekIO();
        Connect();
    }
}
