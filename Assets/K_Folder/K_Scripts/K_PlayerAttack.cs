using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class K_PlayerAttack : MonoBehaviour
{
    public Vector2 inputVec;
    public GameObject[] attackPos;

    public int level = 0;

    public float coolTime = 0.75f;
    private float timer1;
    private float timer2;
    private bool doSpecial1 = false;
    private bool doSpecial2 = false;

    public float fadeTime = 0.5f;
    public float reappearDelay = 0.1f; // 재등장 지연 시간
    public float moveDistance = 5f; // 이동 거리
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private bool isFading = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timer1 += Time.deltaTime;
        timer2 += Time.deltaTime;

        // 기본 공격 - 좌클릭으로 페이드 시작
        if (timer1 > coolTime && !doSpecial1 && !doSpecial2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Attack());
                timer1 = 0f;
                StartCoroutine(FadeAndMove()); // 페이드 및 이동 시작
            }
        }

        // 특수 공격 1
        if (timer2 > coolTime && level >= 1)
        {
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(SpecialAttack1());
                timer2 = 0f;
            }
        }

        // 애니메이션 제어
        anim.SetBool("isNAttack", doSpecial1);
        anim.SetBool("isSAttack", doSpecial2);
    }

    private IEnumerator FadeAndMove()
    {
        isFading = true;
        float startAlpha = spriteRenderer.color.a;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + new Vector3(moveDistance, 0f, 0f); // 목표 위치 설정

        float elapsed = 0f;

        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeTime;

            // 페이드 아웃 (투명도 감소)
            float alpha = Mathf.Lerp(startAlpha, 0f, t);
            spriteRenderer.color = new Color(1, 1, 1, alpha);

            // 천천히 목표 위치로 이동
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        // 완전 투명 상태에서 재등장 처리 시작
        spriteRenderer.color = new Color(1, 1, 1, 0f); // 완전 투명
        StartCoroutine(ReappearAfterDelay());
    }

    private IEnumerator Attack()
    {
        doSpecial1 = true;
        anim.SetBool("isNAttack", doSpecial1);

        // 공격 로직 처리
        yield return new WaitForSeconds(0.2f);

        doSpecial1 = false;
    }

    private IEnumerator SpecialAttack1()
    {
        doSpecial2 = true;
        anim.SetBool("isSAttack", doSpecial2);

        // 특수 공격 1 로직 처리
        yield return new WaitForSeconds(0.5f);

        doSpecial2 = false;
    }

    private IEnumerator ReappearAfterDelay()
    {
        yield return new WaitForSeconds(reappearDelay);

        // 다시 등장하면서 색상을 초기화하고 애니메이션 재개
        spriteRenderer.color = Color.white;
        anim.SetTrigger("Reappear");
    }

    public void resetAnim()
    {
        spriteRenderer.color = Color.white;
        gameObject.SetActive(true);
        isFading = false;
        timer1 = 0f;
    }
}
