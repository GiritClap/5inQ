using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class K_PlayerAttack : MonoBehaviour
{
    public Vector2 inputVec;

    public int level = 0;

    public float coolTime = 0.75f;
    private float timer1;
    private float timer2;
    private bool doSpecial1 = false;
    private bool doSpecial2 = false;

    // ������ ����
    public GameObject laserPrefab; // ������ ������
    public Transform laserSpawnPoint; // ������ ���� ��ġ
    public float laserSpeed = 10f; // ������ �ӵ�
    public float laserDuration = 0.5f; // ������ ���� �ð�
    public float laserCooldown = 1f; // ������ ��Ÿ��
    private float laserTimer = 0f;

    private Animator anim;
    private bool canAttack = true;

    private SpriteRenderer spriteRenderer;

    // C_Skill �̹��� ������Ʈ
    public GameObject C_Skill; // Canvas�� C_Skill �̹���

    private bool isCSkillActive = false; // �̹����� �̹� Ȱ��ȭ�Ǿ����� Ȯ���ϴ� �÷���

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // C_Skill �ʱ� ���� ��Ȱ��ȭ
        if (C_Skill != null)
        {
            C_Skill.SetActive(false);
        }
    }

    void Update()
    {
        timer1 += Time.deltaTime;
        timer2 += Time.deltaTime;

        laserTimer += Time.deltaTime;

        if (laserTimer > laserCooldown && canAttack)
        {
            if (Input.GetMouseButtonDown(0)) // ��Ŭ������ ������ �߻�
            {
                StartCoroutine(Attack());
                laserTimer = 0f; // ��Ÿ�� �ʱ�ȭ
            }
        }

        // Ư�� ���� 1 (���� 3 �̻󿡼��� ����)
        if (timer2 > coolTime && level >= 3)
        {
            if (Input.GetMouseButtonDown(1)) // ��Ŭ������ Ư�� ���� �ߵ�
            {
                StartCoroutine(SpecialAttack1());
                timer2 = 0f;
            }
        }

        // ���� 3�� �Ǿ��� �� C_Skill �̹��� Ȱ��ȭ
        if (level >= 3 && !isCSkillActive)
        {
            ActivateCSkillImage();
        }

        // �ִϸ��̼� ����
        anim.SetBool("isNAttack", doSpecial1);
        anim.SetBool("isSAttack", doSpecial2);
    }

    private void ActivateCSkillImage()
    {
        if (C_Skill != null)
        {
            C_Skill.SetActive(true); // �̹��� Ȱ��ȭ
            Debug.Log("C_Skill �̹����� Ȱ��ȭ�Ǿ����ϴ�!");
            isCSkillActive = true; // �÷��� ����
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Level"))
        {
            level++; // ���� ����
            Debug.Log("������! ���� ����: " + level);
            Destroy(collision.gameObject); // �浹�� ������Ʈ ����
        }
    }

}
