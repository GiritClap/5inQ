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
        // 이동 타이머 감소
        moveTimer -= Time.deltaTime;

        // 적 이동
        enemyTransform.Translate(moveDirection * enemy.speed * Time.deltaTime);

        // 방향 설정에 따라 애니메이터에 전달
        enemy.DirectionEnemy(enemyTransform.position.x + moveDirection.x, enemyTransform.position.x);

        // 이동 시간이 끝났으면 새로운 방향 설정
        if (moveTimer <= 0)
        {
            ResetMovement();
        }

        // 플레이어가 가까워지면 추적 상태로 전환

        //patrol.nav.destination = patrol.targets[patrol.point].transform.position;
        //patrol.point = (patrol.point + 1) % patrol.targets.Length;
        //patrol.next();

        if (Vector2.Distance(enemyTransform.position, enemy.player.position) <= enemy.distance)
        {
            animator.SetBool("isPattern", false);
            animator.SetBool("isFollow", true);// 추적 상태로 전환
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    void ResetMovement()
    {
        moveTimer = moveDuration;

        // 패트롤 영역 안에서만 이동하도록 제한
        if (enemy.patrolArea != null)
        {
            Bounds bounds = enemy.patrolArea.bounds; // 패트롤 영역 크기 가져오기
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            moveDirection = (new Vector2(x, y) - (Vector2)enemyTransform.position).normalized; // 현재 위치에서 패트롤 영역 내 랜덤 위치로 이동
        }
        else
        {
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
    }
}

