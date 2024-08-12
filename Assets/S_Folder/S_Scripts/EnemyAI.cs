using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float chaseDistance;  // 플레이어를 쫓아가기 시작하는 거리
    public Rigidbody2D target;

    bool isLive = true; // 초기화

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator animator; // 애니메이터 컴포넌트

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 초기화
    }

    private void FixedUpdate()
    {
        if (!isLive || target == null)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        float distanceToTarget = Vector2.Distance(target.position, rigid.position);

        if (distanceToTarget < chaseDistance)
        {
            Vector2 dirVec = target.position - rigid.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;

            animator.SetBool("isWalking", true); // 걷는 애니메이션 실행

            if (target.position.x < rigid.position.x)
            {
                spriter.flipX = true; // 플레이어가 왼쪽에 있으면 뒤집기
            }
            else
            {
                spriter.flipX = false; // 플레이어가 오른쪽에 있으면 뒤집지 않기
            }

        }
        else
        {
            animator.SetBool("isWalking", false); // 걷는 애니메이션 중지
        }
    }

    // 적이 활성화/비활성화 될 때 사용할 메서드
    public void SetActive(bool active)
    {
        isLive = active;
    }
}
