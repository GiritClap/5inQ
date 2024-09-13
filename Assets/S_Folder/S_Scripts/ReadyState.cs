using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;

    C_Rayser rayser;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
        rayser = animator.GetComponent<C_Rayser>();

        if (rayser == null)
        {
            Debug.LogError("C_Rayser component not found!");
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���� �÷��̾� ���� �Ÿ� ���
        float distance = Vector2.Distance(enemyTransform.position, enemy.player.position);

        // ���� ���� Ȯ��
        if (distance <= enemy.attackRange) // 5���� �̳����� ����
        {
            if (enemy.atkDelay <= 0)
            {
                animator.SetTrigger("Attack"); // ���� Ʈ���� ����
                rayser.Rayser();
            }
        }
        else
        {
            animator.SetBool("isFollow", true); // 5���� �̻��� �� ���� ���·� ��ȯ
        }

        // ���� ���� ������Ʈ
        enemy.DirectionEnemy(enemy.player.position.x, enemyTransform.position.x);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���� ���� �� �ʿ��� �۾� �߰�
    }
}
