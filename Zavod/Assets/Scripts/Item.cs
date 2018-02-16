using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{   public int cost;
    public string type;
    public string sprite;
    public string prefab;
    public int damagePlus;
    public int GunDamage;
    public float handRadius;
    public int speed;
    public int bullets;
    public int healthPlus;
    public string descr;
    [HideInInspector]
    public int AllAmmo;
    [HideInInspector]
    public int AmmoCurOne;

    public int AmmoInOne;
    public Vector2 handPos;
    public Vector3 handAngle;
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
