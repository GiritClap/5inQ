using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PatternState : StateMachineBehaviour
{
    private Enemy enemy;
    private NavMeshAgent agent;
    private float moveTimer;
    private float moveDuration = 2f; // 이동 지속 시간
    private Vector2 randomTarget;    // 이동할 랜덤 위치

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        agent = enemy.GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.isStopped = false; // 이동 시작
        }

        ResetMovement();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        moveTimer -= Time.deltaTime;

        // 이동 시간이 끝나면 새로운 랜덤 목적지 설정
        if (moveTimer <= 0)
        {
            ResetMovement();
        }

        // 플레이어가 가까워지면 추적 상태로 전환
        if (Vector2.Distance(enemy.transform.position, enemy.player.position) <= enemy.distance)
        {
            animator.SetBool("isPattern", false);
            animator.SetBool("isFollow", true); // 추적 상태로 변경
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null)
        {
            agent.isStopped = true; // 이동 중지
        }
    }

    private void ResetMovement()
    {
        moveTimer = moveDuration;

        // 현재 위치를 기준으로 일정 범위 내에서 랜덤 위치 선정
        Vector2 randomOffset = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        randomTarget = (Vector2)enemy.transform.position + randomOffset;

        // NavMesh 이동 설정
        if (agent != null)
        {
            agent.SetDestination(randomTarget);
        }
    }
}
