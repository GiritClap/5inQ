using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Slime : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float detectionRange = 10f; // 플레이어 탐지 거리
    public float moveSpeed = 3f; // 에너미 이동 속도
    public float attackInterval = 3f; // 공격 간격
    public GameObject boomSlimePrefab;


    private Animator animator; // 애니메이터
    private float attackTimer; // 공격 타이머

    void Start()
    {
        animator = GetComponent<Animator>();
        attackTimer = 0f; // 타이머 초기화
        FindPlayer();
    }

    void Update()
    {
        HandleMovement(); // 이동 처리
        HandleAttack();   // 공격 처리
    }

    // 플레이어와의 거리를 계산하고 도망 이동 처리
    void HandleMovement()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            MoveAwayFromPlayer(); // 플레이어를 피해 도망
        }
    }

    // 플레이어 반대 방향으로 이동
    void MoveAwayFromPlayer()
    {
        Vector3 directionAwayFromPlayer = (transform.position - player.position).normalized;

        // 위치 업데이트
        transform.position += directionAwayFromPlayer * moveSpeed * Time.deltaTime;

        // 회전 설정: 플레이어와 반대 방향으로 보도록 회전
        //transform.rotation = Quaternion.LookRotation(-directionAwayFromPlayer);
    }

    // 공격 타이머를 관리하고 애니메이션 트리거 실행
    void HandleAttack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            TriggerAttack();  // 공격 실행
            attackTimer = 0f; // 타이머 리셋
        }
    }

    // Attack 애니메이션 트리거 실행
    void TriggerAttack()
    {
        if (animator != null)
        {
            animator.SetTrigger("IsAttack");
        }
        GameObject boomSlime = Instantiate(boomSlimePrefab, transform.position, transform.rotation);
    }

    // 디버그용 탐지 범위 표시
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    void FindPlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning($"태그 '{"Player"}'를 가진 플레이어를 찾을 수 없습니다!");
        }
    }
}
