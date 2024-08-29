using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class C_PlayerAttack : MonoBehaviour
{
    public Vector2 inputVec;
    public GameObject[] attackPos;
    public float coolTime = 0.75f;

    public int level = 0;
    public Image[] specialAttackImages;
    private int StarcurrentIndex;
    public Text SpecialAttackCount;
    public Image SpecialAImg;

    private int c=0;

    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private C_PlayerMove playerMove;

    private float timer1;
    private float timer2;

    private bool canUseSpecialAttack = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerMove = GetComponent<C_PlayerMove>();

        foreach (var pos in attackPos)
        {
            pos.SetActive(false);
        }

        foreach (Image img in specialAttackImages)
        {
            img.enabled = false;
        }

        StarcurrentIndex = specialAttackImages.Length - 1;
    }

    void Update()
    {
        if (level == 2)
        {
            foreach (var image in specialAttackImages)
            {
                image.enabled = true;
            }
            SpecialAImg.gameObject.SetActive(true);
        }

        timer1 += Time.deltaTime;
        if (timer1 > coolTime && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }

        timer2 += Time.deltaTime;
        if (timer2 > coolTime && level == 2 && canUseSpecialAttack && Input.GetMouseButtonDown(1))
        {
            ++c;
            if (StarcurrentIndex >= 0)
            {
                specialAttackImages[StarcurrentIndex].color = Color.white;
                StartCoroutine(SpecialAttack());
                StarcurrentIndex--;
            }
            if(c==3)
            {
                StartCoroutine(ResetStarcurrentIndex());
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

        if (inputVec.x < 1 && inputVec.x > 0)
        {
            if (inputVec.y < 1 && inputVec.y > 0)
            {
                anim.SetTrigger("tongueAttackUp");
                attackPos[4].SetActive(true);
            }
            else if (inputVec.y > -1 && inputVec.y < 0)
            {
                anim.SetTrigger("tongueAttackDown");
                attackPos[2].SetActive(true);
            }
        }
        else if (inputVec.x > -1 && inputVec.x < 0)
        {
            if (inputVec.y < 1 && inputVec.y > 0)
            {
                anim.SetTrigger("tongueAttackUp");
                attackPos[6].SetActive(true);
            }
            else if (inputVec.y > -1 && inputVec.y < 0)
            {
                anim.SetTrigger("tongueAttackDown");
                attackPos[0].SetActive(true);
            }
        }
        else if (inputVec.x == -1 || !spriteRenderer.flipX)
        {
            anim.SetTrigger("tongueAttackMid");
            attackPos[7].SetActive(true);
        }
        else if (inputVec.x == 1 || spriteRenderer.flipX)
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

        foreach (var pos in attackPos)
        {
            pos.SetActive(false);
        }

        playerMove.enabled = true;
    }

    IEnumerator SpecialAttack()
    {
        timer2 = 0;
        playerMove.enabled = false;

        if (spriteRenderer.flipX)
        {
            anim.SetTrigger("teethAttack");
            attackPos[4].SetActive(true);
            attackPos[2].SetActive(true);
        }
        else
        {
            anim.SetTrigger("teethAttack");
            attackPos[6].SetActive(true);
            attackPos[0].SetActive(true);
        }

        yield return new WaitForSeconds(0.7f);

        foreach (var pos in attackPos)
        {
            pos.SetActive(false);
        }

        playerMove.enabled = true;
    }

    IEnumerator ResetStarcurrentIndex()
    {
        float waitTime = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;
            float remainingTime = Mathf.Max(0.1f, waitTime - elapsedTime);
            SpecialAttackCount.text = Mathf.CeilToInt(remainingTime).ToString();
            yield return null;
        }

        SpecialAttackCount.text = "";

        StarcurrentIndex = 2;
        foreach (Image img in specialAttackImages)
        {
            img.color = Color.yellow;
        }
        c = 0;
    }
}
