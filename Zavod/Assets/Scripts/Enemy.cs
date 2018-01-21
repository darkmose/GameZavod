using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class Enemy : MonoBehaviour {
    Animator animator;
    public SpriteMesh face1, face2;
    public SpriteMeshInstance spritemesh;
    public int maxhp = 100;
    public int hp=100;
    public GameObject bullet;
    public Vector2 startPos;
    public Transform rifle;
    public GameObject player;

    

    public IEnumerator bulFly(GameObject bul, float scale)
    {   Vector2 target = startPos;
        target.x = player.transform.position.x+(50*scale);

         if (scale > 0)
         {  
            float x = bul.transform.position.x;
            for (; x <= startPos.x + 15;)
            {
                try
                {
                    bul.transform.position = Vector2.MoveTowards(bul.transform.position, target, Time.deltaTime * 5);
                    x = bul.transform.position.x;
                }
                catch (MissingReferenceException exc)
                {
                    Debug.Log(exc.Message);
                    break;
                }
                yield return null;
            }
            Destroy(bul);
                                      
         }
         else if (scale < 0)
         {
            float x = bul.transform.position.x;
            for (; x >= startPos.x - 15;)
            {
                try
                {
                    bul.transform.position = Vector2.MoveTowards(bul.transform.position, target, Time.deltaTime * 5);
                    x = bul.transform.position.x;
                }
                catch (MissingReferenceException exc)
                {
                    Debug.Log(exc.Message);
                    break;
                }
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

    IEnumerator coins(int col) {
        for (int i = 0; i < col; i++)
        {
            Vector2 pos = transform.position;
            GameObject newCoin = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/CoinItem"),pos,Quaternion.identity,GameObject.Find("Coins").transform);
            newCoin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-3f, 3f), 7f),ForceMode2D.Impulse);
            yield return null;
        }
    }

    void Die() {       
            
            Destroy(gameObject);       
    }
    void Money() {
        int k = Random.Range(5, 10);
        StartCoroutine(coins(k));
    }

    private void FixedUpdate()
    {
        if (animator.GetInteger("Do") == 1)
        {
            spritemesh.spriteMesh = face2;
        }
        else spritemesh.spriteMesh = face1;
        ToPlayer();
        if (hp==0)
        {
            GetComponent<Animator>().Play("EnemyDie");
            
        }
    }

  



}
