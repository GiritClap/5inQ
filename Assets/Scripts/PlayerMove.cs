using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        rigid.MovePosition(rigid.position+ nextVec);
    }

    private void LateUpdate()
    {
        //flip
        if(inputVec.x != 0)
        {
            spriteRenderer.flipX = inputVec.x > 0;
        }
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
}
