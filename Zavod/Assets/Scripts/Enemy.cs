using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class Enemy : MonoBehaviour {
    Animator animator;
    public SpriteMesh face1, face2;
    public SpriteMeshInstance spritemesh;
    public int hp;
    public GameObject bullet;
    public Vector2 startPos;
    public Transform rifle;
    public GameObject player;


    public IEnumerator bulFly(GameObject bul, float scale)
    {
        Vector2 target = startPos;
        target.x = player.transform.position.x+(50*scale);

            if (scale > 0)
            {
                for (; bul.transform.position.x <= startPos.x + 50;)
                {
                    bul.transform.position = Vector2.MoveTowards(bul.transform.position, target, Time.deltaTime * 5);
                    yield return null;
                }
                Destroy(bul);
            }
            if (scale < 0)
            {
                for (; bul.transform.position.x >= startPos.x - 50;)
                {
                    bul.transform.position = Vector2.MoveTowards(bul.transform.position, target, Time.deltaTime * 5);
                    yield return null;
                }
                Destroy(bul);
            }        
    }

    void Start()
    {   
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }



    void BulletInstance() {
        Debug.Log("Shot!");
        startPos = startPos = rifle.GetChild(1).transform.position;
        float scale = transform.localScale.x;
        GameObject bull = Instantiate(bullet, startPos, Quaternion.identity, GameObject.Find("Bullets").transform);
        bull.transform.localScale = new Vector2(scale, 1);
        StartCoroutine(bulFly(bull, scale));
    }


    private void ToPlayer()
    {
        if (player.transform.position.x < transform.position.x)
        {

            transform.localScale = new Vector2(-1, 1);
        }
        else transform.localScale = new Vector2(1, 1);

    }


    




    void Update() {

    }


    private void FixedUpdate()
    {
        if (animator.GetInteger("Do") == 1)
        {
            spritemesh.spriteMesh = face2;
        }
        else spritemesh.spriteMesh = face1;
        ToPlayer();
    }

  



}
