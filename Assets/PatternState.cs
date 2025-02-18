using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;

    float moveTimer;       // �̵� �ð� Ÿ�̸�
    Vector2 moveDirection; // ���� �̵� ����
    float moveDuration = 2f; // �̵� ���� �ð� (�� ����)

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
        ResetMovement(); // �ʱ� ���� ���� ����
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       /* // �̵� Ÿ�̸� ����
        moveTimer -= Time.deltaTime;

        // �� �̵�
        enemyTransform.Translate(moveDirection * enemy.speed * Time.deltaTime);

        // ���� ������ ���� �ִϸ����Ϳ� ����
        enemy.DirectionEnemy(enemyTransform.position.x + moveDirection.x, enemyTransform.position.x);

        // �̵� �ð��� �������� ���ο� ���� ����
        if (moveTimer <= 0)
        {
            ResetMovement();
        }

        // �÷��̾ ��������� ���� ���·� ��ȯ
        if (Vector2.Distance(enemyTransform.position, enemy.player.position) <= enemy.distance)
        {
            animator.SetBool("isPattern", false);
            animator.SetBool("isFollow", true);// ���� ���·� ��ȯ
        }
       */
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    void ResetMovement()
    {
        moveTimer = moveDuration;
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized; // ���� ���� ����
    }
}

