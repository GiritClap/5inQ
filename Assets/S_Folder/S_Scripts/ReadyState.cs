using UnityEngine;
using UnityEngine.AI;

public class ReadyState : StateMachineBehaviour
{
    private Enemy enemy;
    private NavMeshAgent agent;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        agent = animator.GetComponent<NavMeshAgent>();

        agent.isStopped = true; // 준비 상태에서는 멈춰 있기
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector2.Distance(enemy.transform.position, enemy.player.position);

        // 공격 범위 안에 들어오면 공격
        if (distance <= enemy.attackRange)
        {
            if (enemy.atkDelay <= 0)
            {
                animator.SetTrigger("Attack");

                // RobotB는 공격하면 즉시 사망 (신애리 추가 부분 유지)
                if (enemy.type == EnemyType.RobotB)
                {
                    animator.SetTrigger("Die");
                }
            }
        }
        else
        {
            // 공격 범위를 벗어나면 따라가기
            animator.SetBool("isFollow", true);
            agent.isStopped = false; // 이동 시작
            agent.destination = enemy.player.position; // 플레이어를 따라감
        }

        // 적 방향 설정
        enemy.DirectionEnemy(enemy.player.position.x, enemy.transform.position.x);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.isStopped = false; // 상태가 변경되면 다시 이동 가능하게 설정
    }
}
