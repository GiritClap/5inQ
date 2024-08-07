using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class C_PlayerAttack : MonoBehaviour
{
    public Vector2 inputVec;
    public GameObject[] attackPos;

    public float coolTime = 0.75f;
    public float timer1;
    public float timer2;

    SpriteRenderer spriteRenderer;
    Animator anim;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        for (int i = 0; i < attackPos.Length; i++)
        {
            attackPos[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer1 += Time.deltaTime;
        if (timer1 > coolTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine("Attack");
                //Debug.Log("ぞし");
            }
        }

        timer2 += Time.deltaTime;
        if (timer2 > coolTime)
        {
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine("SpecialAttack");
                Debug.Log("ぞし2");
            }
        }
    }

    //check dir
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    IEnumerator Attack()
    {
        timer1 = 0;

        if ((inputVec.x < 1 && inputVec.x > 0) && (inputVec.y < 1 && inputVec.y > 0))
        {
            //up right 4
            anim.SetTrigger("tongueAttackUp");
            attackPos[4].SetActive(true);
        }
        else if ((inputVec.x < 1 && inputVec.x > 0) && (inputVec.y > -1 && inputVec.y < 0))
        {
            //down right 2
            anim.SetTrigger("tongueAttackDown");
            attackPos[2].SetActive(true);
        }
        else if ((inputVec.x > -1 && inputVec.x < 0) && (inputVec.y < 1 && inputVec.y > 0))
        {
            //up left 6
            anim.SetTrigger("tongueAttackUp");
            attackPos[6].SetActive(true);
        }
        else if ((inputVec.x > -1 && inputVec.x < 0) && (inputVec.y > -1 && inputVec.y < 0))
        {
            //down left 0
            anim.SetTrigger("tongueAttackDown");
            attackPos[0].SetActive(true);
        }
        else if ((inputVec.x == -1 && inputVec.y == 0) || !spriteRenderer.flipX)
        {
            //left 7
            anim.SetTrigger("tongueAttackMid");
            attackPos[7].SetActive(true);
        }
        else if ((inputVec.x == 1 && inputVec.y == 0) || spriteRenderer.flipX)
        {
            //right 3
            anim.SetTrigger("tongueAttackMid");
            attackPos[3].SetActive(true);
        }
        else if (inputVec.x == 0 && inputVec.y == 1)
        {
            //up 5
            anim.SetTrigger("tongueAttackUp");
            attackPos[5].SetActive(true);
        }
        else if (inputVec.x == 0 && inputVec.y == -1)
        {
            //down 1
            anim.SetTrigger("tongueAttackDown");
            attackPos[1].SetActive(true);
        }


        yield return new WaitForSeconds(0.7f);

        for (int i = 0; i < attackPos.Length; i++)
        {
            attackPos[i].SetActive(false);
        }

    }

    IEnumerator SpecialAttack()
    {
        timer2 = 0;

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
    }
}
