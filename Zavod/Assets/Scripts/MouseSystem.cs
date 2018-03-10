using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSystem : MonoBehaviour
{

    GameObject wire, newWire;
    public LayerMask mask;
    public LayerMask mechMask;
    [HideInInspector]
    public bool isWireSet = false;
    GameObject CurrentIO;

    void Start()
    {
        wire = Resources.Load<GameObject>("prefabs/wire");
    }


    void ChekIO()
    {
        try
        {
            if (Input.GetMouseButtonUp(0) && !isWireSet && GameObject.Find("GlobalScripts").GetComponent<Inventory>().armor[4].type == "instrument")
            {
                if (GameObject.Find("GlobalScripts").GetComponent<Inventory>().items.Contains(Resources.Load<GameObject>("prefabs/Mach/WireItem").GetComponent<Item>()))
                {
                    RaycastHit2D hit = Physics2D.Raycast((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, 4f, mask);
                    {

                        if (hit.collider.CompareTag("WireIO") && hit.collider.GetComponent<InputIO>().connects == 0 && Vector2.Distance(GameObject.Find("Player").transform.position, hit.collider.transform.position) < 5f)
                        {
                            CurrentIO = hit.collider.gameObject;
                            newWire = Instantiate(wire, Vector2.zero, Quaternion.identity, GameObject.Find("Wires").transform);
                            newWire.GetComponent<Wire>().OutPut = hit.collider.gameObject;
                            newWire.GetComponent<Wire>().side = (hit.collider.gameObject.transform.localPosition.x > 0) ? "right" : "left";
                            isWireSet = true;
                        }
                        else if (hit.collider.transform.parent.name == "Capsule")
                        {
                            CurrentIO = hit.collider.gameObject;
                            newWire = Instantiate(wire, Vector2.zero, Quaternion.identity, GameObject.Find("Wires").transform);
                            newWire.GetComponent<Wire>().OutPut = hit.collider.gameObject;
                            newWire.GetComponent<Wire>().side = (hit.collider.gameObject.transform.localPosition.x > 0) ? "right" : "left";
                            isWireSet = true;
                        }

                    }

                }

            }

        }
        catch (System.NullReferenceException)
        {

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
                    if (hit.collider.CompareTag("WireIO") && Vector2.Distance(GameObject.Find("Player").transform.position, hit.collider.transform.position) < 5f)
                    {
                        if (hit.collider.gameObject != CurrentIO)
                        {

                            if (hit.collider.GetComponent<InputIO>().connects == 0 && hit.collider.transform.parent.name != "Capsule")
                            {
                                newWire.GetComponent<Wire>().InPut = hit.collider.gameObject;
                                newWire.GetComponent<Wire>().StopWire();
                                hit.collider.GetComponent<InputIO>().connects++;
                                hit.collider.GetComponent<InputIO>().connectClient.Add(CurrentIO);
                                CurrentIO.GetComponent<InputIO>().connects++;
                                CurrentIO.GetComponent<InputIO>().connectClient.Add(hit.collider.gameObject);
                                hit.collider.transform.parent.GetComponent<Mechanism>().IsConnected = true;
                                CurrentIO.transform.parent.GetComponent<Mechanism>().IsConnected = true;
                                hit.collider.GetComponent<InputIO>().WireRef.Add(newWire);
                                CurrentIO.GetComponent<InputIO>().WireRef.Add(newWire);
                                newWire = null;
                                isWireSet = false;
                                GameObject.Find("GlobalScripts").GetComponent<Inventory>().items.Remove(Resources.Load<GameObject>("prefabs/Mach/WireItem").GetComponent<Item>());
                            }
                            else if (hit.collider.transform.parent.name == "Capsule")
                            {
                                newWire.GetComponent<Wire>().InPut = hit.collider.gameObject;
                                newWire.GetComponent<Wire>().StopWire();
                                hit.collider.GetComponent<InputIO>().connects++;
                                int x = hit.collider.GetComponent<InputIO>().connects;
                                hit.collider.GetComponent<InputIO>().connectClient.Add(CurrentIO);
                                CurrentIO.GetComponent<InputIO>().connects++;
                                CurrentIO.GetComponent<InputIO>().connectClient.Add(hit.collider.gameObject);
                                hit.collider.transform.parent.GetComponent<Mechanism>().IsConnected = true;
                                CurrentIO.transform.parent.GetComponent<Mechanism>().IsConnected = true;
                                hit.collider.GetComponent<InputIO>().WireRef.Add(newWire);
                                CurrentIO.GetComponent<InputIO>().WireRef.Add(newWire);
                                newWire = null;
                                isWireSet = false;
                                GameObject.Find("GlobalScripts").GetComponent<Inventory>().items.Remove(Resources.Load<GameObject>("prefabs/Mach/WireItem").GetComponent<Item>());
                            }
                        }
                    }
                }
                catch (System.NullReferenceException)
                {
                }
            }
        }
    }


    void ClimbCapsule()
    {
        if (Input.GetMouseButtonUp(0))
        {
            try
            {
                if (Vector2.Distance(GameObject.Find("Player").transform.position, GameObject.Find("Capsule").transform.position) < 4f)
                {
                    RaycastHit2D hit = Physics2D.Raycast((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, 4, mask);

                    if (hit.collider.name == "CheckPlayerCapsule")
                    {
                        if (hit.collider.transform.parent.GetComponent<Capsule>().isOpen)
                        {
                            GameObject.Find("Player").transform.position = hit.collider.transform.parent.position;
                            GameObject.Find("Capsule").GetComponent<Capsule>().playerInside = true;
                            PlayerPrefs.SetInt("CapsulePlayerState", 1);
                            GameObject.Find("Player").GetComponent<Rigidbody2D>().isKinematic = true;
                            GameObject.Find("Player").GetComponent<Move>().flag = false;
                            hit.collider.transform.parent.GetComponent<Capsule>().SetLayerName("SoldierLayer");
                        }
                    }
                }

            }
            catch (System.NullReferenceException)
            {
            }
        }
    }

    void RemoveMech()
    {
        try
        {
            if (Input.GetMouseButtonUp(1) && !isWireSet && Input.GetButton("Run") && GameObject.Find("GlobalScripts").GetComponent<Inventory>().armor[4].type == "instrument")
            {
                RaycastHit2D hit = Physics2D.Raycast((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, 4f, mask);
                {
                    if ((hit.collider.CompareTag("Mech") || hit.collider.gameObject.layer == 13) && Vector2.Distance(GameObject.Find("Player").transform.position, hit.collider.transform.position) < 5f)
                    {
                        if (hit.collider.gameObject.GetComponent<Mechanism>().IsConnected)
                        {
                            hit.collider.transform.Find("Input").GetComponent<InputIO>().Delete();
                        }
                        GameObject.Find("GlobalScripts").GetComponent<Inventory>().items.Add(hit.collider.gameObject.GetComponent<Item>());
                        Destroy(hit.collider.gameObject);
                    }
                }

            }

        }
        catch (System.NullReferenceException)
        {

        }
    }



    void Update()
    {
        ChekIO();
        Connect();
        ClimbCapsule();
        RemoveMech();
    }
}
