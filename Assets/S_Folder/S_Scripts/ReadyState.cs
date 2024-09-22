using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyState : StateMachineBehaviour
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

        // ���� ���� Ȯ��
        if (distance <= enemy.attackRange) // 5���� �̳����� ����
        {
            if (enemy.atkDelay <= 0)
            {
                animator.SetTrigger("Attack"); // ���� Ʈ���� ���� ���Ⱑ �����̴ϱ� 
                // ����ٰ�  ����
                enemy.AttackStart();
            }
            else
            {
                enemy.AttackStop();

            }
        }
        else
        {
            animator.SetBool("isFollow", true); // 5���� �̻��� �� ���� ���·� ��ȯ
            enemy.AttackStop();

        }

        // ���� ���� ������Ʈ
        enemy.DirectionEnemy(enemy.player.position.x, enemyTransform.position.x);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���� ���� �� �ʿ��� �۾� �߰�
    }
}
