using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HelpPlayer : MonoBehaviour {
    public int maxHealth = 100;
    public int maxEnergy = 100;
    public int energy = 100;
    public int health = 100;
    public int damage = 5;
    public AudioClip[] move;
    public AudioClip coins;
    public AudioClip[] attack;

    [SerializeField]
    private RectTransform barHp, barEn;

    bool fullEn = true;
    bool corotin = true;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
    }

    public void UpdateHpBar()
    {
        float with = barHp.rect.width;
        with -= with * 0.3f;

        float value = -with + (with / (float)maxHealth * (float)health);
        barHp.offsetMin = new Vector2(value, 0f);
        barHp.offsetMax = new Vector2(value, 0f);
    }
    public void UpdateEnBar()
    {
        float with = barEn.rect.width;
        with -= with * 0.3f;

        float value = -with + (with / (float)maxEnergy * (float)energy);
        barEn.offsetMin = new Vector2(value, 0f);
        barEn.offsetMax = new Vector2(value, 0f);
    }

    public void AttackDamage(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().hp = Mathf.Clamp(enemy.GetComponent<Enemy>().hp - damage, 0, enemy.GetComponent<Enemy>().maxhp);
    }


    public void EnergyAdd(int count)
    {
        energy = Mathf.Clamp(energy + count, 0, maxEnergy);
        UpdateEnBar();
    }

    void HpAdd(int count)
    {
        health = Mathf.Clamp(health + count, 0, maxHealth);
        UpdateHpBar();

    }

    IEnumerator plusEnergy()
    {
        while (energy < maxEnergy)
        {
            EnergyAdd(2);
            UpdateEnBar();
            yield return new WaitForSeconds(0.1f);
        }
        corotin = true;
    }

    void Die() {
        gameObject.GetComponent<Animator>().Play("RobotDie");    
    }
    void Respawn() {
        gameObject.transform.position = GameObject.Find("GlobalScripts").transform.GetChild(0).position;
        gameObject.GetComponent<Animator>().Play("RobotRespawn");
    }

    void SetMove() {
        gameObject.GetComponent<Move>().move = !gameObject.GetComponent<Move>().move;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {     
        if (collision.tag == "takeDamage")
        {
            Destroy(collision.gameObject);
            GetComponent<Animator>().Play("RobotDamage");
            HpAdd(-5);
        }
    }

    private void FixedUpdate()
    {
        if (energy < maxEnergy)
        {

            if (corotin)
            {
                fullEn = false;
                corotin = false;
                if (!fullEn)
                {
                    fullEn = true;
                    StartCoroutine(plusEnergy());
                }

            }

        }

        if (health==0)
        {
            Die();
        }
    }
}
