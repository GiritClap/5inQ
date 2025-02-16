using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FollowState : StateMachineBehaviour
{
    private Enemy enemy;
    private NavMeshAgent agent;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        agent = enemy.GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.isStopped = false; // 이동 시작
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

        if (distanceToPlayer > enemy.distance + 5f) // 너무 멀어지면 돌아가기
        {
            animator.SetBool("isBack", true);
            animator.SetBool("isFollow", false);
        }
        else if (distanceToPlayer > enemy.attackRange)
        {
            agent.SetDestination(enemy.player.position); // 플레이어를 쫓아감
        }
        else
        {
            animator.SetBool("isFollow", false); // 공격 가능하면 멈춤
        }

        enemy.DirectionEnemy(enemy.player.position.x, enemy.transform.position.x);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null)
        {
            agent.ResetPath(); // 현재 경로 초기화
        }
    }
}
