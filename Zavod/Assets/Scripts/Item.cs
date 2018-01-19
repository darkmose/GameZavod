using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    public string type;
    public string sprite;
    public string prefab;
    Animator animato;
    public Rigidbody2D rigi;
    GameObject sprite1;
	void Start () {
        if(gameObject.tag != "Item")    gameObject.tag = "Item";
        if (gameObject.layer != 8) gameObject.layer = 8;
        sprite1 = this.transform.GetChild(0).gameObject;
        animato = sprite1.AddComponent<Animator>();
        animato.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("animation/drop");
        rigi = this.gameObject.AddComponent<Rigidbody2D>();
        rigi.gravityScale = 5f;       
	}

}
