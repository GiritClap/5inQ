using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;
    //Patrol patrol;


    float moveTimer;       // 이동 시간 타이머
    Vector2 moveDirection; // 랜덤 이동 방향
    float moveDuration = 2f; // 이동 지속 시간 (초 단위)

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        //patrol = patrol.GetComponent<Patrol>();
        enemyTransform = animator.GetComponent<Transform>();

        //ResetMovement(); // 초기 랜덤 방향 설정
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        moveTimer -= Time.deltaTime;

        // ✅ 이동하기 전에 테두리 체크
        CheckBoundary();

        // 적 이동
        enemyTransform.Translate(moveDirection * enemy.speed * Time.deltaTime);

        enemy.DirectionEnemy(enemyTransform.position.x + moveDirection.x, enemyTransform.position.x);

        if (moveTimer <= 0)
        {
            ResetMovement();
        }

        if (Vector2.Distance(enemyTransform.position, enemy.player.position) <= enemy.distance)
        {
            animator.SetBool("isPattern", false);
            animator.SetBool("isFollow", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    void ResetMovement()
    {
        moveTimer = moveDuration;

        if (enemy.patrolArea != null)
        {
            Bounds bounds = enemy.patrolArea.bounds;

            // 랜덤 위치 설정 (패트롤 영역 내)
            Vector2 randomPoint = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );

            // 이동 방향 설정
            moveDirection = (randomPoint - (Vector2)enemyTransform.position).normalized;

            if (moveDirection == Vector2.zero)
            {
                moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            }
        }
        else
        {
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
    }

    // ✅ 패트롤 지역 벗어나면 방향 반대로 바꾸기
    void CheckBoundary()
    {
        if (enemy.patrolArea != null)
        {
            Bounds bounds = enemy.patrolArea.bounds;
            Vector2 nextPosition = (Vector2)enemyTransform.position + moveDirection * enemy.speed * Time.deltaTime;

            // X축 범위 체크
            if (nextPosition.x < bounds.min.x || nextPosition.x > bounds.max.x)
            {
                moveDirection.x *= -1; // 반대 방향 이동
            }

            // Y축 범위 체크
            if (nextPosition.y < bounds.min.y || nextPosition.y > bounds.max.y)
            {
                moveDirection.y *= -1; // 반대 방향 이동
            }
        }
    }

    
}