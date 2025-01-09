using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType // �� Ÿ��  ����
{
    RobotA,
    RobotB,
    RobotC,
}
public class Enemy : MonoBehaviour
{

    public GameObject cRocket;


    public EnemyType type;

    Animator animator;
    public Transform player;
    public float speed;
    public Vector2 home;
    public float enemyHp;


    public float atkCooltime = 2;
    public float atkDelay;
    public int atkDamage;

    public float distance;

    public Transform boxpos;
    public Vector2 boxSize;

    public float attackRange;

    PlayerHealth playerHp;

    public void ChangeEnemy()
    {
        switch (type)
        {
            case EnemyType.RobotA:
                break;
            case EnemyType.RobotB:
                break;
            case EnemyType.RobotC:
                break;

            default:
                break;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHp = player.GetComponent<PlayerHealth>();
        home = transform.position;
        atkDamage = 3;
    }

    public void DirectionEnemy(float target, float baseobj)
    {
        if (target < baseobj)
        {
            animator.SetFloat("Direction", -1);

        }
        else
        {
            animator.SetFloat("Direction", 1);
        }
    }

    public void Attack()
    {
        if (animator.GetFloat("Direction") == -1)
        {
            if (boxpos.localPosition.x > 0)
                boxpos.localPosition = new Vector2(boxpos.localPosition.x * -1, boxpos.localPosition.y);
        }
        else
        {
            if (boxpos.localPosition.x < 0)
                boxpos.localPosition = new Vector2(Mathf.Abs(boxpos.localPosition.x), boxpos.localPosition.y);
        }

        



        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxpos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                Debug.Log("damage");
                playerHp.GetDamage(atkDamage);

                // B ����
                if (type == EnemyType.RobotB)
                {
                    animator.SetTrigger("Die");
                    StartCoroutine(DestroyAfterAnimation());  // �ִϸ��̼��� ���� �� ��ü ����
                }

                
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (atkDelay >= 0)
            atkDelay -= Time.deltaTime;

        if (enemyHp <= 0)
        {
            animator.SetTrigger("Die");
        }

        Debug.Log(atkDamage);
    }

    IEnumerator DestroyAfterAnimation()
    {
        // �ִϸ��̼� ���¸� ������
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // �ִϸ��̼��� ���̸�ŭ ���
        yield return new WaitForSeconds(stateInfo.length);

        // ��ü ����
        Destroy(gameObject);
    }


    public void LaunchCRocket()
    {
        // ���� ����
        GameObject rocket = Instantiate(cRocket, transform.position, Quaternion.identity);

        // ������ �÷��̾ �����ϵ��� ����
        /*
        C_Rocket rocketScript = rocket.GetComponent<C_Rocket>();
        if (rocketScript != null)
        {
            rocketScript.SetTarget(player);
        }
        */
        Debug.Log("C Rocket launched!");
    }

}