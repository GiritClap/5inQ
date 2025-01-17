using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ReadyState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���� �÷��̾� ���� �Ÿ� ���
        float distance = Vector2.Distance(enemyTransform.position, enemy.player.position);

        // ���� ���� �� �÷��̾ �ִ��� Ȯ��
        if (distance <= enemy.attackRange)
        {
            // ���� ��Ÿ���� �Ϸ�� ��쿡�� ���� ���� �� ���� ����
            if (enemy.atkDelay <= 0)
            {
                // ���� ����
                //if (enemy.type == EnemyType.RobotC)
                //{
                    //enemy.LaunchCRocket();
                //}

                // ���� �ִϸ��̼� ����
                animator.SetTrigger("Attack");

                // ��Ÿ�� �ʱ�ȭ
                enemy.atkDelay = enemy.atkCooltime;
            }
        }
        else
        {
            // 5���� �̻��� �� ���� ���·� ��ȯ
            animator.SetBool("isFollow", true);
        }

        // ���� ���� ������Ʈ
        enemy.DirectionEnemy(enemy.player.position.x, enemyTransform.position.x);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���¸� ������ �� �ʿ��� �۾�
    }
}
