using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BackState : StateMachineBehaviour
{
    private Enemy enemy;
    private NavMeshAgent agent;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        agent = enemy.GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.isStopped = false;
            agent.SetDestination(enemy.home); // ���� �ڸ��� �̵�
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distanceToHome = Vector2.Distance(enemy.transform.position, enemy.home);

        if (distanceToHome < 1.0f) // Ȩ�� �����ϸ� ��� ���·� ����
        {
            animator.SetBool("isBack", false);
            animator.SetBool("isPattern", true);
        }

        enemy.DirectionEnemy(enemy.home.x, enemy.transform.position.x);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null)
        {
            agent.ResetPath(); // �̵� ��� �ʱ�ȭ
        }
    }
}
