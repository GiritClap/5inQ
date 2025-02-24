using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyType // 적 타입  선택
{
    RobotA,
    RobotB,
    RobotC,
}
public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool isKnockback = false; // 넉백 중인지 체크
    private float knockbackCooldown = 0.5f; // 넉백 후 쿨타임 (0.5초 동안 추가 넉백 방지)

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

    public float invincibilityDuration = 100f; // 무적 지속 시간
    public float blinkInterval = 0.05f; // 깜빡이는 간격

    private bool isDead = false; // 적이 이미 죽었는지 체크
    private bool isInvincible = false; // 무적 상태 체크
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
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer 가져오기
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
                // B 공격
                if (type == EnemyType.RobotB)
                {
                    animator.SetTrigger("Die");
                    StartCoroutine(DestroyAfterAnimation());  // 애니메이션이 끝난 후 객체 삭제
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

        // 플레이어와 적 사이 거리 계산
        distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange) // 공격 범위 안에 들어오면 공격
        {
            navMeshAgent.isStopped = true; // 이동 멈춤
            if (atkDelay <= 0)
            {
                Attack();
                atkDelay = atkCooltime; // 공격 쿨타임 적용
            }
        }
        else // 공격 범위 밖이면 이동
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(player.position);
        }

    }


    public void LaunchCRocket()
    {
        
        // 로켓 생성
        GameObject rocket = Instantiate(cRocket, transform.position, Quaternion.identity);

       
    }

    public void ObjectDestoy() //  신애리 추가
    {
        Destroy(animator.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack") && !isDead && !isInvincible) // 무적 아닐 때만 피격
        {
            OnDamaged(playerAttack.atkDmg);
        }
    }

    void OnDamaged(float atkDmg)
    {
        if (isKnockback) return; // 넉백 중이거나 쿨타임 동안 추가 넉백 방지

        Debug.Log("피격!");

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

            // 넉백 실행 (쿨타임 적용됨)
            StartCoroutine(KnockbackRoutine());

            StartCoroutine(InvincibilityCoroutine());
        }
    }



    private IEnumerator InvincibilityCoroutine()
    {

        isInvincible = true; // 무적 활성화

        float elapsed = 0f;
        while (elapsed < invincibilityDuration)
        {
            spriteRenderer.color = new Color(1f, 0f, 0f, 1f); // 빨강 색
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // 원래 색
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval * 2;
        }

        isInvincible = false; // 무적 해제
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(0.5f); // 애니메이션 길이만큼 기다림
        Destroy(gameObject);
    }

    

    private IEnumerator KnockbackRoutine()
    {
        if (isKnockback) yield break; // 이미 넉백 중이면 실행 X

        isKnockback = true; // 넉백 시작
        if (navMeshAgent != null)
            navMeshAgent.enabled = false; // 넉백 중 NavMesh 비활성화

        Vector2 knockbackDir = (transform.position - player.position).normalized;
        float knockbackForce = 5f;

        float knockbackTime = 0.2f; // 넉백 지속 시간
        float timer = 0f;

        while (timer < knockbackTime)
        {
            transform.position += (Vector3)(knockbackDir * knockbackForce * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        if (navMeshAgent != null)
            navMeshAgent.enabled = true; // 넉백 후 NavMesh 다시 활성화

        yield return new WaitForSeconds(knockbackCooldown); // 넉백 후 일정 시간 기다림
        isKnockback = false; // 넉백 종료
        }

    public void Setup(Transform target)
    {
        this.target = target;

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

    }
}
