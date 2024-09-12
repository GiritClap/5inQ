using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : StateMachineBehaviour
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
        if (Vector2.Distance(enemy.player.position, enemyTransform.position) > enemy.distance)
        {
            animator.SetBool("isBack", true);
            animator.SetBool("isFollow", false);
        }
        else if (Vector2.Distance(enemy.player.position, enemyTransform.position) > enemy.attackRange)
        {
            enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, enemy.player.position, Time.deltaTime * enemy.speed);
        }
        else
        {
            animator.SetBool("isBack", false);
            animator.SetBool("isFollow", false);
        }
        enemy.DirectionEnemy(enemy.player.position.x, enemyTransform.position.x);
    }

   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
}
