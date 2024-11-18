using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class K_BossBasicAnimation : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
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
