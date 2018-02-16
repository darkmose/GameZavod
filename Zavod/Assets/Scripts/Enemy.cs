using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public SpriteMesh face1, face2;
    public SpriteMeshInstance spritemesh;
    public int maxhp = 100;
    public int hp = 100;
    public GameObject bullet;
    public Vector2 startPos;
    public Transform rifle;
    public GameObject player;
    bool attck = true;



    public IEnumerator bulFly(GameObject bul, float scale)
    {
        Vector2 target = startPos;
        target.x = player.transform.position.x + (50 * scale);

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



    void BulletInstance()
    {
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

    IEnumerator coins(int col)
    {
        for (int i = 0; i < col; i++)
        {
            Vector2 pos = transform.position;
            GameObject newCoin = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/CoinItem"), pos, Quaternion.identity, GameObject.Find("Coins").transform);
            newCoin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-3f, 3f), 7f), ForceMode2D.Impulse);
            yield return null;
        }
        col = Random.Range(0, 2);
        while (col != 0)
        {
            Vector2 pos = transform.position;
            GameObject newHelp = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/help"), pos, Quaternion.identity, GameObject.Find("Coins").transform);
            newHelp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-3f, 3f), 7f), ForceMode2D.Impulse);
            col--;
            yield return null;
        }
    }

    void Die()
    {

        Destroy(gameObject);
    }
    void Money()
    {
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
        if (hp == 0)
        {
            GetComponent<Animator>().Play("EnemyDie");
        }
    }

    void Damage(int damage)
    {
        hp = Mathf.Clamp(hp - damage, 0, maxhp);
    }

    IEnumerator TakeDamage(float time)
    {
        yield return new WaitForSeconds(time);

        Damage(GameObject.Find("Player").GetComponent<HelpPlayer>().damage);
        gameObject.GetComponent<Animator>().Play("EnemyDamage");
        float scale = (float)hp / (float)maxhp * 0.6f;
        int color = 200 / maxhp * hp;
        byte colorp = (byte)color;
        transform.GetChild(1).GetChild(0).transform.localScale = new Vector2(scale, 0.3f);
        transform.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(colorp, 0, 0, 194);
        transform.GetComponent<Rigidbody2D>().AddForce(new Vector2((transform.position.x > player.transform.position.x) ? 8f : -8f, 8f), ForceMode2D.Impulse);
    }

    public void TakeDamageFunc(float time)
    {
        StartCoroutine(TakeDamage(time));
    }



    IEnumerator timer()
    {
        attck = false;
        yield return new WaitForSeconds(0.55f);
        attck = true;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "EnemyDamage")
        {
            if (Input.GetMouseButtonDown(0) && !GameObject.Find("GlobalScripts").GetComponent<Inventory>().isShooting)
            {
                if (attck)
                {
                    if (transform.localScale.x != player.transform.localScale.x)
                    {
                        TakeDamageFunc(0.2f);
                        StartCoroutine(timer());
                    }
                }
            }
        }
    }




}
