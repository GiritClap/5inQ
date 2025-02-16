using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : StateMachineBehaviour
{
    private Enemy enemy;
    private NavMeshAgent agent;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        agent = enemy.GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.isStopped = true; // 대기 상태에서는 이동 중지
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

        if (distanceToPlayer <= enemy.distance)
        {
            animator.SetBool("isFollow", true); // 플레이어를 추적
        }
        else
        {
            animator.SetBool("isPattern", true); // 패턴 행동 시작
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null)
        {
            agent.isStopped = false; // 이동 가능 상태로 변경
        }
    }
}
