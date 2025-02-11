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


    public float invincibilityDuration = 0.2f; // 무적 지속 시간
    public float blinkInterval = 0.05f; // 깜빡이는 간격

    private bool isDead = false; // 적이 이미 죽었는지 체크
    private bool isInvincible = false; // 무적 상태 체크

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
            Debug.Log("데미지 받음 : " + damage);
            Debug.Log("남은 체력 : " + hp);


        }
    }

    public void GetDie()
    {
        // end Game
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true; // 무적 활성화

        float elapsed = 0f;
        while (elapsed < invincibilityDuration)
        {
            spriteRenderer.color = new Color(1f, 0f, 0f, 1f); // 빨강 색
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // 원래 색
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval * 2;
        }

        isInvincible = false; // 무적 해제
    }

}
