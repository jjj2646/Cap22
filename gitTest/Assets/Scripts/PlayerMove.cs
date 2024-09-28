using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    Rigidbody2D rigid;


    void Awake()
    {  
        rigid = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        // stop speed
        if(Input.GetButtonUp("Horizontal")) {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
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
