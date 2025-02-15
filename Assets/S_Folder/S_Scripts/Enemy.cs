using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType // �� Ÿ��  ����
{
    RobotA,
    RobotB,
    RobotC,
}
public class Enemy : MonoBehaviour
{

    public GameObject cRocket;


    public EnemyType type;

    Animator animator;
    public Transform player;
    public float speed;
    public Vector2 home;
    public float enemyHp;


    public float atkCooltime = 2;
    public float atkDelay;
    public int atkDamage;

    public float distance;

    public Transform boxpos;
    public Vector2 boxSize;

    public float attackRange;

    M_PlayerHealth playerHp;
    M_PlayerAttack playerAttack;

    public float invincibilityDuration = 0.2f; // ���� ���� �ð�
    public float blinkInterval = 0.05f; // �����̴� ����

    private bool isDead = false; // ���� �̹� �׾����� üũ
    private bool isInvincible = false; // ���� ���� üũ
    private SpriteRenderer spriteRenderer;
    public void ChangeEnemy()
    {
        switch (type)
        {
            case EnemyType.RobotA:
                break;
            case EnemyType.RobotB:
                break;
            case EnemyType.RobotC:
                break;

            default:
                break;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer ��������
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHp = player.GetComponent<M_PlayerHealth>();
        playerAttack = player.transform.GetComponent<M_PlayerAttack>();
        home = transform.position;
        atkDamage = 10;
    }

    public void DirectionEnemy(float target, float baseobj)
    {
        if (target < baseobj)
        {
            animator.SetFloat("Direction", -1);

        }
        else
        {
            animator.SetFloat("Direction", 1);
        }
    }

    public void Attack()
    {


        if (animator.GetFloat("Direction") == -1)
        {
            if (boxpos.localPosition.x > 0)
                boxpos.localPosition = new Vector2(boxpos.localPosition.x * -1, boxpos.localPosition.y);
        }
        else
        {
            if (boxpos.localPosition.x < 0)
                boxpos.localPosition = new Vector2(Mathf.Abs(boxpos.localPosition.x), boxpos.localPosition.y);
        }


        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxpos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                Debug.Log("damage" + atkDamage);
                playerHp.GetDamage(atkDamage);

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        // 01/23 ������ �߰� 
        if (player == null || playerHp == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerHp = player.GetComponent<M_PlayerHealth>();
        }
        //

        if (atkDelay >= 0)
            atkDelay -= Time.deltaTime;


        //Debug.Log(atkDamage);   01/23 ������ �߰� 
    }

    /*
    IEnumerator DestroyAfterAnimation()
    {
        // �ִϸ��̼� ���¸� ������
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // �ִϸ��̼��� ���̸�ŭ ���
        yield return new WaitForSeconds(stateInfo.length);

        // ��ü ����
        Destroy(gameObject);
    }
    */


    public void LaunchCRocket()
    {

        // ���� ����
        GameObject rocket = Instantiate(cRocket, transform.position, Quaternion.identity);


    }

    public void ObjectDestoy() //  �žָ� �߰�
    {
        Destroy(animator.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack") && !isDead && !isInvincible) // ���� �ƴ� ���� �ǰ�
        {
            OnDamaged(playerAttack.atkDmg);
        }
    }

    void OnDamaged(float atkDmg)
    {
        Debug.Log("�ǰ�!");
        enemyHp -= atkDmg;

        if (enemyHp <= 0 && !isDead) // �ߺ� ó�� ����
        {
            isDead = true;
            animator.SetTrigger("Die");
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine()); // ���� & �����̴� ȿ�� ����
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true; // ���� Ȱ��ȭ

        float elapsed = 0f;
        while (elapsed < invincibilityDuration)
        {
            spriteRenderer.color = new Color(1f, 0f, 0f, 1f); // ���� ��
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // ���� ��
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval * 2;
        }

        isInvincible = false; // ���� ����
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(0.5f); // �ִϸ��̼� ���̸�ŭ ��ٸ�
        Destroy(gameObject);
    }
}