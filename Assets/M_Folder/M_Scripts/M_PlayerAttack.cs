using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class M_PlayerAttack : MonoBehaviour
{
    public Vector2 inputVec;
    public GameObject[] attackPos;

    public float coolTime = 0.75f;
    public float timer;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        for (int i = 0; i < attackPos.Length; i++)
        {
            attackPos[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > coolTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine("Attack");
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

        if ((inputVec.x < 1 && inputVec.x > 0) && (inputVec.y < 1 && inputVec.y > 0))
        {
            //up right 4
            attackPos[4].SetActive(true);
        }
        else if ((inputVec.x < 1 && inputVec.x > 0) && (inputVec.y > -1 && inputVec.y < 0))
        {
            //down right 2
            attackPos[2].SetActive(true);
        }
        else if ((inputVec.x > -1 && inputVec.x < 0) && (inputVec.y < 1 && inputVec.y > 0))
        {
            //up left 6
            attackPos[6].SetActive(true);
        }
        else if ((inputVec.x > -1 && inputVec.x < 0) && (inputVec.y > -1 && inputVec.y < 0))
        {
            //down left 0
            attackPos[0].SetActive(true);
        }
        else if ((inputVec.x == -1 && inputVec.y == 0) || !spriteRenderer.flipX)
        {
            //left 7
            attackPos[7].SetActive(true);
        }
        else if ((inputVec.x == 1 && inputVec.y == 0) || spriteRenderer.flipX)
        {
            //right 3
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


        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < attackPos.Length; i++)
        {
            attackPos[i].SetActive(false);
        }
        timer = 0;

    }
}
