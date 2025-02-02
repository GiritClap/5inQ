using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class M_PlayerAttack : MonoBehaviour
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
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        //powerGauge = GetComponent<Slider>();
        powerGauge.maxValue = maxGauge;
        powerGauge.value = maxGauge;
        for (int i = 0; i < attackPos.Length; i++)
        {
            attackPos[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(level == 2)
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
                StartCoroutine("Attack");
            }
        }

        timer2 += Time.deltaTime;
        if (timer2 > coolTime)
        {
            if (level == 1)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    StartCoroutine("SpecialAttack1");
                }
            }        
        }
        if(doSpecial2)
        {
            anim.SetBool("S_Attack_2", true);
        }
        else
        {
            anim.SetBool("S_Attack_2", false);
        }
        if (doSpecial2 == false)
        {
            powerGauge.value += Time.deltaTime;
            if(charging && powerGauge.value == maxGauge)
            {
                charging = false;
                fillColor.color = Color.yellow;

            }
        }
        if (level == 2 && !charging)
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
            //anim.SetBool("S_Attack_2", false);
            //StopCoroutine("SpecialAttack2");
            //Debug.Log("¤¾¤·4");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Level"))
        {
            level++;
            collision.gameObject.SetActive(false);
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
            anim.SetTrigger("Attack1");
            attackPos[4].SetActive(true);
        }
        else if ((inputVec.x < 1 && inputVec.x > 0) && (inputVec.y > -1 && inputVec.y < 0))
        {
            //down right 2
            anim.SetTrigger("Attack3");
            attackPos[2].SetActive(true);
        }
        else if ((inputVec.x > -1 && inputVec.x < 0) && (inputVec.y < 1 && inputVec.y > 0))
        {
            //up left 6
            anim.SetTrigger("Attack1");
            attackPos[6].SetActive(true);
        }
        else if ((inputVec.x > -1 && inputVec.x < 0) && (inputVec.y > -1 && inputVec.y < 0))
        {
            //down left 0
            anim.SetTrigger("Attack3");
            attackPos[0].SetActive(true);
        }
        else if ((inputVec.x == -1 && inputVec.y == 0) || !spriteRenderer.flipX)
        {
            //left 7
            anim.SetTrigger("Attack2");
            attackPos[7].SetActive(true);
        }
        else if ((inputVec.x == 1 && inputVec.y == 0) || spriteRenderer.flipX)
        {
            //right 3
            anim.SetTrigger("Attack2");
            attackPos[3].SetActive(true);
        }
        /*else if (inputVec.x == 0 && inputVec.y == 1)
        {
            //up 5
            attackPos[5].SetActive(true);
        }*/
        /*else if (inputVec.x == 0 && inputVec.y == -1)
        {
            //down 1
            attackPos[1].SetActive(true);
        }*/
        /*else if (inputVec.x == 0 && inputVec.y == 0)
        {
            attackPos[1].SetActive(true);
        }*/


        yield return new WaitForSeconds(0.7f);

        for (int i = 0; i < attackPos.Length; i++)
        {
            attackPos[i].SetActive(false);
        }

    }

    IEnumerator SpecialAttack1()
    {
        doSpecial1 = true;
        timer2 = 0;

        if (spriteRenderer.flipX)
        {
            anim.SetTrigger("S_Attack1");
            attackPos[4].SetActive(true);
            attackPos[2].SetActive(true);
        }
        else if (!spriteRenderer.flipX)
        {
            anim.SetTrigger("S_Attack1");
            attackPos[6].SetActive(true);
            attackPos[0].SetActive(true);
        }

        yield return new WaitForSeconds(0.7f);

        for (int i = 0; i < attackPos.Length; i++)
        {
            attackPos[i].SetActive(false);
        }
        doSpecial1 = false;
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
