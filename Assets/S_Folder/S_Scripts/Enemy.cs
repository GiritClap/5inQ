using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyType // �� Ÿ��  ����
{
    RobotA,
    RobotB,
    RobotC,
}
public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool isKnockback = false; // �˹� ������ üũ
    private float knockbackCooldown = 0.5f; // �˹� �� ��Ÿ�� (0.5�� ���� �߰� �˹� ����)

    private NavMeshAgent navMeshAgent;
    private Transform target;
    public GameObject home2;


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

    public float invincibilityDuration = 100f; // ���� ���� �ð�
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
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        rb = GetComponent<Rigidbody2D>();

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

                //Debug.Log("B Die launched!"); 

                /*
                // B ����
                if (type == EnemyType.RobotB)
                {
                    animator.SetTrigger("Die");
                    StartCoroutine(DestroyAfterAnimation());  // �ִϸ��̼��� ���� �� ��ü ����
                }
                */

                
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player == null || playerHp == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerHp = player.GetComponent<M_PlayerHealth>();
        }

        if (atkDelay >= 0)
            atkDelay -= Time.deltaTime;

        // �÷��̾�� �� ���� �Ÿ� ���
        distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange) // ���� ���� �ȿ� ������ ����
        {
            navMeshAgent.isStopped = true; // �̵� ����
            if (atkDelay <= 0)
            {
                Attack();
                atkDelay = atkCooltime; // ���� ��Ÿ�� ����
            }
        }
        else // ���� ���� ���̸� �̵�
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(player.position);
        }

    }


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
        if (isKnockback) return; // �˹� ���̰ų� ��Ÿ�� ���� �߰� �˹� ����

        Debug.Log("�ǰ�!");

        enemyHp -= atkDmg;

        if (enemyHp <= 0 && !isDead)
        {
            isDead = true;
            animator.SetTrigger("Die");
            StartCoroutine(DestroyAfterAnimation());
        }
        else if (enemyHp > 0)
        {
            animator.SetTrigger("Damage");

            // �˹� ���� (��Ÿ�� �����)
            StartCoroutine(KnockbackRoutine());

            StartCoroutine(InvincibilityCoroutine());
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

    

    private IEnumerator KnockbackRoutine()
    {
        if (isKnockback) yield break; // �̹� �˹� ���̸� ���� X

        isKnockback = true; // �˹� ����
        if (navMeshAgent != null)
            navMeshAgent.enabled = false; // �˹� �� NavMesh ��Ȱ��ȭ

        Vector2 knockbackDir = (transform.position - player.position).normalized;
        float knockbackForce = 5f;

        float knockbackTime = 0.2f; // �˹� ���� �ð�
        float timer = 0f;

        while (timer < knockbackTime)
        {
            transform.position += (Vector3)(knockbackDir * knockbackForce * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        if (navMeshAgent != null)
            navMeshAgent.enabled = true; // �˹� �� NavMesh �ٽ� Ȱ��ȭ

        yield return new WaitForSeconds(knockbackCooldown); // �˹� �� ���� �ð� ��ٸ�
        isKnockback = false; // �˹� ����
        }

    public void Setup(Transform target)
    {
        this.target = target;

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

    }
}
