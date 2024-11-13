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
    public float reappearDelay = 0.1f; // ����� ���� �ð�
    public float moveDistance = 5f; // �̵� �Ÿ�
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

        // �⺻ ���� - ��Ŭ������ ���̵� ����
        if (timer1 > coolTime && !doSpecial1 && !doSpecial2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Attack());
                timer1 = 0f;
                StartCoroutine(FadeAndMove()); // ���̵� �� �̵� ����
            }
        }

        // Ư�� ���� 1
        if (timer2 > coolTime && level >= 1)
        {
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(SpecialAttack1());
                timer2 = 0f;
            }
        }

        // �ִϸ��̼� ����
        anim.SetBool("isNAttack", doSpecial1);
        anim.SetBool("isSAttack", doSpecial2);
    }

    private IEnumerator FadeAndMove()
    {
        isFading = true;
        float startAlpha = spriteRenderer.color.a;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + new Vector3(moveDistance, 0f, 0f); // ��ǥ ��ġ ����

        float elapsed = 0f;

        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeTime;

            // ���̵� �ƿ� (���� ����)
            float alpha = Mathf.Lerp(startAlpha, 0f, t);
            spriteRenderer.color = new Color(1, 1, 1, alpha);

            // õõ�� ��ǥ ��ġ�� �̵�
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        // ���� ���� ���¿��� ����� ó�� ����
        spriteRenderer.color = new Color(1, 1, 1, 0f); // ���� ����
        StartCoroutine(ReappearAfterDelay());
    }

    private IEnumerator Attack()
    {
        doSpecial1 = true;
        anim.SetBool("isNAttack", doSpecial1);

        // ���� ���� ó��
        yield return new WaitForSeconds(0.2f);

        doSpecial1 = false;
    }

    private IEnumerator SpecialAttack1()
    {
        doSpecial2 = true;
        anim.SetBool("isSAttack", doSpecial2);

        // Ư�� ���� 1 ���� ó��
        yield return new WaitForSeconds(0.5f);

        doSpecial2 = false;
    }

    private IEnumerator ReappearAfterDelay()
    {
        yield return new WaitForSeconds(reappearDelay);

        // �ٽ� �����ϸ鼭 ������ �ʱ�ȭ�ϰ� �ִϸ��̼� �簳
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
