using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType // 적 타입  선택
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

    public float invincibilityDuration = 0.2f; // 무적 지속 시간
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
        // 01/23 문승준 추가 
        if (player == null || playerHp == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerHp = player.GetComponent<M_PlayerHealth>();
        }
        //

        if (atkDelay >= 0)
            atkDelay -= Time.deltaTime;


        //Debug.Log(atkDamage);   01/23 문승준 추가 
    }

    /*
    IEnumerator DestroyAfterAnimation()
    {
        // 애니메이션 상태를 가져옴
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 애니메이션의 길이만큼 대기
        yield return new WaitForSeconds(stateInfo.length);

        // 객체 삭제
        Destroy(gameObject);
    }
    */


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
        Debug.Log("피격!");
        enemyHp -= atkDmg;

        if (enemyHp <= 0 && !isDead) // 중복 처리 방지
        {
            isDead = true;
            animator.SetTrigger("Die");
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine()); // 무적 & 깜빡이는 효과 시작
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
}