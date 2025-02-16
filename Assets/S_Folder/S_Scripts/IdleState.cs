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
            agent.isStopped = true; // ��� ���¿����� �̵� ����
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

        if (distanceToPlayer <= enemy.distance)
        {
            animator.SetBool("isFollow", true); // �÷��̾ ����
        }
        else
        {
            animator.SetBool("isPattern", true); // ���� �ൿ ����
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null)
        {
            agent.isStopped = false; // �̵� ���� ���·� ����
        }
    }
}
