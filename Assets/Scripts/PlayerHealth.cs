using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider hpBar;
    public int hp = 100;


    private void Awake()
    {
        SetMaxHp(hp);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            GetDamage(10);
        }
    }

    public void SetMaxHp(int health)
    {
        hpBar.maxValue = health;
        hpBar.value = health;
    }

    public void GetDamage(int damage)
    {
        int getDamagedHp = hp - damage;
        if(getDamagedHp <= 0)
        {
            GetDie();
        }
        else
        {
            hp = getDamagedHp;
            hpBar.value = hp;
        }   
    }

    public void GetDie()
    {
        // end Game
    }

    private void Update()
    {
    }


}
