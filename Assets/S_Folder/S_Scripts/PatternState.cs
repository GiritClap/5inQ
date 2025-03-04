using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;
    //Patrol patrol;


    float moveTimer;       // �̵� �ð� Ÿ�̸�
    Vector2 moveDirection; // ���� �̵� ����
    float moveDuration = 2f; // �̵� ���� �ð� (�� ����)

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        //patrol = patrol.GetComponent<Patrol>();
        enemyTransform = animator.GetComponent<Transform>();
        
        //ResetMovement(); // �ʱ� ���� ���� ����
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �̵� Ÿ�̸� ����
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

        //patrol.nav.destination = patrol.targets[patrol.point].transform.position;
        //patrol.point = (patrol.point + 1) % patrol.targets.Length;
        //patrol.next();

        if (Vector2.Distance(enemyTransform.position, enemy.player.position) <= enemy.distance)
        {
            animator.SetBool("isPattern", false);
            animator.SetBool("isFollow", true);// ���� ���·� ��ȯ
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    void ResetMovement()
    {
        moveTimer = moveDuration;

        // ��Ʈ�� ���� �ȿ����� �̵��ϵ��� ����
        if (enemy.patrolArea != null)
        {
            Bounds bounds = enemy.patrolArea.bounds; // ��Ʈ�� ���� ũ�� ��������
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            moveDirection = (new Vector2(x, y) - (Vector2)enemyTransform.position).normalized; // ���� ��ġ���� ��Ʈ�� ���� �� ���� ��ġ�� �̵�
        }
        else
        {
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
    }
}

