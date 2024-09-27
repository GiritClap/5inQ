using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class C_PlayerMove : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Animator anim;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Movement
        Vector2 nextVec = inputVec * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);

        // Adjust sortingOrder based on y-position
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 3);
    }

    private void FixedUpdate()
    {
        //flip
        /*if (inputVec.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (inputVec.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
*/
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        if (inputVec.magnitude > 0)
        {
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);

        }

        //flip
        if (inputVec.x != 0)
        {
            spriteRenderer.flipX = inputVec.x > 0;
        }
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
}
