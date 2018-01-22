using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {
   public Camera main;
   public GameObject obj;
   public GameObject backgr;
   float razn,xn=-2.74f,xt;
   public float space = 3f;

	void Start ()
    {
        main.orthographicSize = 6.03f;
	}





    void BackMoveScene(){
        xt = obj.transform.position.x;
        razn = xn - xt;
        Vector2 a = backgr.transform.position;
        Vector2 b = obj.transform.position;
        b.x += (Input.GetAxisRaw("Horizontal") * -0.1f + razn/space);
        b.y = backgr.transform.position.y;
        backgr.transform.position = Vector2.Lerp(a, b, Time.deltaTime * 8);
    }

    void CameraMove() {
        Vector3 a = main.transform.position;
        Vector3 b = obj.transform.position;
        b.z = -3.7f;
        if (b.y < 18.9f)
        {
            b.y = 18.9f;
        }
        main.transform.position = Vector3.Lerp(a, b, Time.deltaTime * 8);
    }




    void Update()
    {
        BackMoveScene();
        CameraMove();

    }
}
