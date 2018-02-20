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
        try
        {
            if (Input.GetMouseButtonUp(0) && !isWireSet && GameObject.Find("GlobalScripts").GetComponent<Inventory>().armor[4].type == "instrument")
            {
                if (!GameObject.Find("GlobalScripts").GetComponent<Inventory>().isShooting)
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
                    if (hit.collider.CompareTag("WireIO") && hit.collider.GetComponent<InputIO>().connects == 0 && Vector2.Distance(GameObject.Find("Player").transform.position, hit.collider.transform.position) < 5f)
                    {
                        if (hit.collider.gameObject != CurrentIO)
                        {
                            newWire.GetComponent<Wire>().InPut = hit.collider.gameObject;
                            newWire.GetComponent<Wire>().StopWire();
                            hit.collider.GetComponent<InputIO>().connects++;
                            hit.collider.GetComponent<InputIO>().connectClient = CurrentIO;
                            CurrentIO.GetComponent<InputIO>().connects++;
                            CurrentIO.GetComponent<InputIO>().connectClient = hit.collider.gameObject;
                            hit.collider.transform.parent.GetComponent<Mechanism>().IsConnected = true;
                            CurrentIO.transform.parent.GetComponent<Mechanism>().IsConnected = true;
                            hit.collider.GetComponent<InputIO>().WireRef = newWire;
                            CurrentIO.GetComponent<InputIO>().WireRef = newWire;
                            newWire = null;
                            isWireSet = false;
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


    void Update()
    {
        ChekIO();
        Connect();
        ClimbCapsule();
    }
}
