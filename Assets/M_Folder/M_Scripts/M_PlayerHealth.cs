using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_PlayerHealth : MonoBehaviour
{
    public Slider hpBar;
    public int hp = 100;

    Animator anim;

    private SpriteRenderer spriteRenderer;


    public float invincibilityDuration = 0.2f; // ���� ���� �ð�
    public float blinkInterval = 0.05f; // �����̴� ����

    private bool isDead = false; // ���� �̹� �׾����� üũ
    private bool isInvincible = false; // ���� ���� üũ

    private void Awake()
    {
        SetMaxHp(hp);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        if(anim == null)
        {
            return;
        }
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
            if(anim == null)
            {

            } else
            {
                //anim.SetTrigger("Damage");
                StartCoroutine(InvincibilityCoroutine());
            }
            hp = getDamagedHp;
            hpBar.value = hp;
            Debug.Log("������ ���� : " + damage);
            Debug.Log("���� ü�� : " + hp);


        }
    }

    public void GetDie()
    {
        // end Game
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true; // ���� Ȱ��ȭ

        float elapsed = 0f;
        while (elapsed < invincibilityDuration)
        {
            spriteRenderer.color = new Color(1f, 0f, 0f, 1f); // ���� ��
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // ���� ��
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval * 2;
        }

        isInvincible = false; // ���� ����
    }

}
