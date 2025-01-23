using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_PlayerHealth : MonoBehaviour
{
    public Slider hpBar;
    public int hp = 100;

    Animator anim;


    private void Awake()
    {
        SetMaxHp(hp);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
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
        if (getDamagedHp <= 0)
        {
            GetDie();
        }
        else
        {

            anim.SetTrigger("Damage");
            hp = getDamagedHp;
            hpBar.value = hp;
            Debug.Log("데미지 받음 : " + damage);
            Debug.Log("남은 체력 : " + hp);


        }
    }

    public void GetDie()
    {
        // end Game
    }


}
