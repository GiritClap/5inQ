using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class K_PlayerMove : MonoBehaviour
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

    private void FixedUpdate()
    {  
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        if (inputVec.magnitude > 0)
        {
            anim.SetBool("isFly", true);
        }
        else
        {
            anim.SetBool("isFly", false);

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
