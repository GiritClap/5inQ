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

        agent.isStopped = true; // �غ� ���¿����� ���� �ֱ�
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector2.Distance(enemy.transform.position, enemy.player.position);

        // ���� ���� �ȿ� ������ ����
        if (distance <= enemy.attackRange)
        {
            if (enemy.atkDelay <= 0)
            {
                animator.SetTrigger("Attack");

                // RobotB�� �����ϸ� ��� ��� (�žָ� �߰� �κ� ����)
                if (enemy.type == EnemyType.RobotB)
                {
                    animator.SetTrigger("Die");
                }
            }
        }
        else
        {
            // ���� ������ ����� ���󰡱�
            animator.SetBool("isFollow", true);
            agent.isStopped = false; // �̵� ����
            agent.destination = enemy.player.position; // �÷��̾ ����
        }

        // �� ���� ����
        enemy.DirectionEnemy(enemy.player.position.x, enemy.transform.position.x);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.isStopped = false; // ���°� ����Ǹ� �ٽ� �̵� �����ϰ� ����
    }
}
