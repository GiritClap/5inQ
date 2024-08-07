using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class C_PlayerAttack : MonoBehaviour
{
    public Vector2 inputVec;
    public GameObject[] attackPos;

    public float coolTime = 0.75f;
    public float timer1;
    public float timer2;

    public int level = 0;

    public Slider powerGauge;
    public Image fillColor;
    float maxGauge = 3.0f;

    SpriteRenderer spriteRenderer;
    Animator anim;
    C_PlayerMove playerMove;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerMove = GetComponent<C_PlayerMove>(); // C_PlayerMove 스크립트 가져오기
        powerGauge.maxValue = maxGauge;
        powerGauge.value = maxGauge;
        for (int i = 0; i < attackPos.Length; i++)
        {
            attackPos[i].SetActive(false);
        }
    }

    void Update()
    {
        if (level == 2)
        {
            powerGauge.gameObject.SetActive(true);
        }
        else
        {
            powerGauge.gameObject.SetActive(false);
        }

        timer1 += Time.deltaTime;
        if (timer1 > coolTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Attack());
            }
        }

        timer2 += Time.deltaTime;
        if (timer2 > coolTime && level == 2)
        {
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(SpecialAttack());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Level"))
        {
            level = 2;
            collision.gameObject.SetActive(false);
        }
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    IEnumerator Attack()
    {
        timer1 = 0;
        playerMove.enabled = false; // 이동 스크립트 비활성화

        if ((inputVec.x < 1 && inputVec.x > 0) && (inputVec.y < 1 && inputVec.y > 0))
        {
            anim.SetTrigger("tongueAttackUp");
            attackPos[4].SetActive(true);
        }
        else if ((inputVec.x < 1 && inputVec.x > 0) && (inputVec.y > -1 && inputVec.y < 0))
        {
            anim.SetTrigger("tongueAttackDown");
            attackPos[2].SetActive(true);
        }
        else if ((inputVec.x > -1 && inputVec.x < 0) && (inputVec.y < 1 && inputVec.y > 0))
        {
            anim.SetTrigger("tongueAttackUp");
            attackPos[6].SetActive(true);
        }
        else if ((inputVec.x > -1 && inputVec.x < 0) && (inputVec.y > -1 && inputVec.y < 0))
        {
            anim.SetTrigger("tongueAttackDown");
            attackPos[0].SetActive(true);
        }
        else if ((inputVec.x == -1 && inputVec.y == 0) || !spriteRenderer.flipX)
        {
            anim.SetTrigger("tongueAttackMid");
            attackPos[7].SetActive(true);
        }
        else if ((inputVec.x == 1 && inputVec.y == 0) || spriteRenderer.flipX)
        {
            anim.SetTrigger("tongueAttackMid");
            attackPos[3].SetActive(true);
        }
        else if (inputVec.x == 0 && inputVec.y == 1)
        {
            anim.SetTrigger("tongueAttackUp");
            attackPos[5].SetActive(true);
        }
        else if (inputVec.x == 0 && inputVec.y == -1)
        {
            anim.SetTrigger("tongueAttackDown");
            attackPos[1].SetActive(true);
        }

        yield return new WaitForSeconds(0.7f);

        for (int i = 0; i < attackPos.Length; i++)
        {
            attackPos[i].SetActive(false);
        }

        playerMove.enabled = true; // 이동 스크립트 활성화
    }

    IEnumerator SpecialAttack()
    {
        timer2 = 0;
        playerMove.enabled = false; // 이동 스크립트 비활성화

        if (spriteRenderer.flipX)
        {
            anim.SetTrigger("teethAttack");
            attackPos[4].SetActive(true);
            attackPos[2].SetActive(true);
        }
        else if (!spriteRenderer.flipX)
        {
            anim.SetTrigger("teethAttack");
            attackPos[6].SetActive(true);
            attackPos[0].SetActive(true);
        }

        yield return new WaitForSeconds(0.7f);

        for (int i = 0; i < attackPos.Length; i++)
        {
            attackPos[i].SetActive(false);
        }

        playerMove.enabled = true; // 이동 스크립트 활성화
    }
}
