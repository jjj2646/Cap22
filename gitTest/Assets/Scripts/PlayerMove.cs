using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Awake()
    {  
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        // stop speed
        if(Input.GetButtonUp("Horizontal")) {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        // 좌우 이미지 변경
        if(Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        // 달리는 애니메이션. x축의 절대값이 0.3이하면
        if(Mathf.Abs(rigid.velocity.x) < 0.3)
            animator.SetBool("isRun", false);
        else
            animator.SetBool("isRun", true);
        
        // jump
        if(Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        // 점프 애니메이션
        if(Mathf.Abs(rigid.velocity.y) < 0.3)
            animator.SetBool("isJumping", false);
        else
            animator.SetBool("isJumping", true);




    }

    void FixedUpdate()
    {   // Move speed
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // velocity = AddForce의 현재속도
        if(rigid.velocity.x > maxSpeed) // 오른쪽 속도제한
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);

        if(rigid.velocity.x < maxSpeed*(-1)) // 왼쪽 속도제한
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);    

            
    }
}
