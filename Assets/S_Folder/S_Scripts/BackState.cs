using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
        //
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector2.Distance(enemy.home, enemyTransform.position) < 1.0f || Vector2.Distance(enemyTransform.position, enemy.player.position)<= 10)
        {
            animator.SetBool("isBack", false);
        }
        else
        {
            enemy.DirectionEnemy(enemy.home.x, enemyTransform.position.x);
            //enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, enemy.home, Time.deltaTime * enemy.speed);
            enemy.Setup(enemy.home2.transform);
        }
    }


    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
}
