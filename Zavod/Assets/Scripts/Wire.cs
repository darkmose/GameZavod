using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{

    LineRenderer line;
    int currentLine;
    [HideInInspector]
    public GameObject OutPut, InPut;
    [HideInInspector]
    public string side;

    private void Start()
    {
        StartLine();
    }


    void StartLine()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.startColor = Color.green;
        line.endColor = Color.green;
        line.enabled = false;
        line.positionCount++;
        line.SetPosition(0, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
        line.positionCount++;
        line.enabled = true;
    }

    public void StopWire()
    {
        Vector2 pos = line.GetPosition(line.positionCount - 1);
        line.positionCount = 4;
        line.SetPosition(3, pos);
        pos = line.GetPosition(1);
        pos.y = 13.62f;
        pos.x = OutPut.transform.position.x + ((side == "right") ? 0.3f : -0.3f);
        line.SetPosition(1, pos);
        pos = line.GetPosition(2);
        pos.y = 13.62f;
        pos.x = InPut.transform.position.x + ((side != "right") ? 0.3f : -0.3f);
        line.SetPosition(2, pos);

        line.startColor = Color.white;
        line.endColor = Color.white;

        this.gameObject.isStatic = true;
        Destroy(gameObject.GetComponent<Wire>());
    }


    void GotoCurrent()
    {
        if (GameObject.Find("Main Camera").GetComponent<MouseSystem>().isWireSet)
        {
            line.SetPosition(line.positionCount - 1, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void Update()
    {
        GotoCurrent();
        if (Input.GetMouseButtonUp(1) || Input.GetButtonUp("Inventory") || Input.GetButtonDown("CraftMenu"))
        {
            if (GameObject.Find("Main Camera").GetComponent<MouseSystem>().isWireSet)
            {
                Destroy(gameObject);
                GameObject.Find("Main Camera").GetComponent<MouseSystem>().isWireSet = false;
            }
        }
    }
}
