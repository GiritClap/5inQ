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

    //������ ���� �ʾ�
    public GameObject laserPrefab; // ������ ������
    public Transform laserSpawnPoint; // ������ ���� ��ġ
    public float laserSpeed = 10f; // ������ �ӵ�
    public float laserDuration = 0.5f; // ������ ���� �ð�
    public float laserCooldown = 1f; // ������ ��Ÿ��
    private float laserTimer = 0f;

    private Animator anim;
    private bool canAttack = true;

  
    private SpriteRenderer spriteRenderer;
    
    private bool isFading = false;


    // 6���� ����
    private readonly Vector2[] directions = new Vector2[]
    {
        new Vector2(1, 0),   // ������
        new Vector2(-1, 0),  // ����
        new Vector2(1, 1),   // �밢�� ������ ��
        new Vector2(1, -1),   // �밢�� ������ �Ʒ�
        new Vector2(-1, 1),  // �밢�� ���� ��
        new Vector2(-1, -1)  // �밢�� ���� �Ʒ�
    };
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
                StartCoroutine(FireLaser());
                laserTimer = 0f; // ��Ÿ�� �ʱ�ȭ
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

    private IEnumerator FireLaser()
    {
        canAttack = false;
        anim.SetTrigger("FireLaser"); // ������ �߻� �ִϸ��̼� Ʈ����

        // ���콺 ��ġ�� �����ͼ� ������ ���
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized; // ĳ���Ϳ� ���콺 ������ ����

        // 6���� �� ���� ����� ������ ����
        Vector2 closestDirection = GetClosestDirection(direction);

        // �������� ���� ��ġ ���� (ĳ������ �߾ӿ��� �ణ �������� ����)
        Vector2 laserStartPosition = (Vector2)transform.position + closestDirection * 0.5f; // ĳ������ �߾ӿ��� �������� ����

        // ������ �ν��Ͻ� ����
        GameObject laser = Instantiate(laserPrefab, laserStartPosition, Quaternion.identity); // ������ �ʱ� ��ġ ����

        // �������� ȸ�� ����
        float angle = Mathf.Atan2(closestDirection.y, closestDirection.x) * Mathf.Rad2Deg;
        laser.transform.rotation = Quaternion.Euler(0, 0, angle);

        // ������ �̵� ó�� (Rigidbody2D�� ���)
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = closestDirection * laserSpeed; // ������ �ӵ� ����
        }

        Destroy(laser, laserDuration); // ���� �ð� ���� ������ ����

        yield return new WaitForSeconds(laserCooldown); // ��Ÿ��
        canAttack = true;
    }

    private Vector2 GetClosestDirection(Vector2 direction)
    {
        // ���� ����� 6������ �����ϴ� ����
        float minAngleDiff = Mathf.Infinity;
        Vector2 closestDirection = Vector2.zero;

        foreach (var dir in directions)
        {
            // ���� ����� 6���� ������ ������ ���
            float angleDiff = Mathf.Abs(Vector2.Angle(direction, dir));

            if (angleDiff < minAngleDiff)
            {
                minAngleDiff = angleDiff;
                closestDirection = dir;
            }
        }

        return closestDirection;
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

   
}
