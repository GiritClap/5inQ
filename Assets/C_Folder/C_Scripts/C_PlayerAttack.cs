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

    bool doSpecial1 = false;
    bool doSpecial2 = false;
    bool charging = false;
    bool canUseSpecialAttack = true; // SpecialAttack 상태

    SpriteRenderer spriteRenderer;
    Animator anim;
    C_PlayerMove playerMove;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerMove = GetComponent<C_PlayerMove>();
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
        if (timer1 > coolTime && !doSpecial1 && !doSpecial2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Attack());
            }
        }

        timer2 += Time.deltaTime;
        if (timer2 > coolTime && level == 2 && canUseSpecialAttack)
        {
            if (Input.GetMouseButton(1))
            {
                StartCoroutine("SpecialAttack");
            }
        }

        if (doSpecial2 == false)
        {
            powerGauge.value += Time.deltaTime;
            if (charging && powerGauge.value == maxGauge)
            {
                charging = false;
                fillColor.color = Color.yellow;
                canUseSpecialAttack = true; // 게이지가 최대치로 돌아왔을 때 SpecialAttack을 다시 사용
            }
        }

        if (level == 2 && !charging && canUseSpecialAttack)
        {
            if (Input.GetMouseButton(1))
            {
                SpecialAttack2();
            }
        }

        if (Input.GetMouseButtonUp(1) || charging)
        {
            doSpecial2 = false;
            for (int i = 0; i < attackPos.Length; i++)
            {
                attackPos[i].SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Level"))
        {
            level++;
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
        playerMove.enabled = false;

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

        playerMove.enabled = true;
    }

    void SpecialAttack2()
    {
        doSpecial2 = true;

        powerGauge.value -= Time.deltaTime;
        if (powerGauge.value <= 0f)
        {
            doSpecial2 = false;
            charging = true;
            fillColor.color = Color.black;
            canUseSpecialAttack = false;
            for (int i = 0; i < attackPos.Length; i++)
            {
                attackPos[i].SetActive(false);
            }
        }
        if (spriteRenderer.flipX)
        {
            attackPos[4].SetActive(true);
            attackPos[3].SetActive(true);
            attackPos[2].SetActive(true);
            attackPos[6].SetActive(false);
            attackPos[7].SetActive(false);
            attackPos[0].SetActive(false);
        }
        else if (!spriteRenderer.flipX)
        {
            attackPos[6].SetActive(true);
            attackPos[7].SetActive(true);
            attackPos[0].SetActive(true);
            attackPos[4].SetActive(false);
            attackPos[3].SetActive(false);
            attackPos[2].SetActive(false);
        }
    }
}
