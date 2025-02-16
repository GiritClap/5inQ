using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PatternState : StateMachineBehaviour
{
    private Enemy enemy;
    private NavMeshAgent agent;
    private float moveTimer;
    private float moveDuration = 2f; // �̵� ���� �ð�
    private Vector2 randomTarget;    // �̵��� ���� ��ġ

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        agent = enemy.GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.isStopped = false; // �̵� ����
        }

        ResetMovement();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        moveTimer -= Time.deltaTime;

        // �̵� �ð��� ������ ���ο� ���� ������ ����
        if (moveTimer <= 0)
        {
            ResetMovement();
        }

        // �÷��̾ ��������� ���� ���·� ��ȯ
        if (Vector2.Distance(enemy.transform.position, enemy.player.position) <= enemy.distance)
        {
            animator.SetBool("isPattern", false);
            animator.SetBool("isFollow", true); // ���� ���·� ����
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null)
        {
            agent.isStopped = true; // �̵� ����
        }
    }

    private void ResetMovement()
    {
        moveTimer = moveDuration;

        // ���� ��ġ�� �������� ���� ���� ������ ���� ��ġ ����
        Vector2 randomOffset = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        randomTarget = (Vector2)enemy.transform.position + randomOffset;

        // NavMesh �̵� ����
        if (agent != null)
        {
            agent.SetDestination(randomTarget);
        }
    }
}
