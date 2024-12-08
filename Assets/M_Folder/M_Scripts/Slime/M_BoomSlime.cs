using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_BoomSlime : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float moveSpeed = 5f; // 이동 속도
    public float explosionRange = 2f; // 자폭 범위
    public float detectionRange = 15f; // 플레이어 탐지 범위
    //public int damage = 50; // 플레이어에게 입힐 데미지

    private Animator animator; // 애니메이터
    private bool isExploding = false; // 자폭 중인지 여부

    void Start()
    {
        animator = GetComponent<Animator>();
        FindPlayer();
    }

    void Update()
    {
        if (isExploding) return; // 자폭 중에는 이동 중단

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 플레이어가 탐지 범위 안에 있다면
        if (distanceToPlayer <= detectionRange)
        {
            MoveTowardsPlayer(); // 플레이어에게 다가가기
        }

        // 플레이어가 자폭 범위 안에 들어왔다면
        if (distanceToPlayer <= explosionRange)
        {
            StartSelfDestruct(); // 자폭 시작
        }
    }

    // 플레이어를 향해 이동
    void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        transform.position += directionToPlayer * moveSpeed * Time.deltaTime;

        // 이동 방향으로 회전
        //transform.LookAt(player);
    }

    // 자폭 동작 실행
    void StartSelfDestruct()
    {
        isExploding = true; // 자폭 상태 설정
        animator.SetTrigger("IsDie"); // 자폭 애니메이션 트리거 실행

        // 애니메이션 종료 후 폭발 처리 (1초 대기 예시)
        //Invoke(nameof(DealExplosionDamage), 1f); // 데미지 처리
    }

   /* // 폭발 범위 내의 플레이어에게 데미지 입히기
    void DealExplosionDamage()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= explosionRange)
        {
            // 플레이어 데미지 처리 (예: PlayerHealth 컴포넌트 접근)
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }*/

    // 에너미 삭제
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    // 디버그용 폭발 범위 표시
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
        Gizmos.color = Color.yellow;
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
