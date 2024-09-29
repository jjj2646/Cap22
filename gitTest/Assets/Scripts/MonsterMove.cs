using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 5); // 5초마다 Think() 실행
    }

    void FixedUpdate()
    {
        // 몬스터 움직이기
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // 낭떠러지 막기
        Vector2 frontVec = new Vector2(rigid.position.x + (nextMove * 0.2f), rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0)); // 디버그 레이로 시각적으로 확인 가능
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

        // 낭떠러지 감지 시 방향 전환
        if (rayHit.collider == null) // 낭떠러지를 감지했을 때
        {
            nextMove *= -1; // 방향을 반대로 전환
            rigid.velocity = new Vector2(0, rigid.velocity.y); // 잠깐 멈춤
            Invoke("Think", 1); // 1초 후 다시 방향을 설정
        }
    }

    void Think()
    {
        // 0을 제외한 -1과 1만 반환하도록 수정
        nextMove = Random.Range(0, 2) == 0 ? -1 : 1; 

        Invoke("Think", 5); // 5초 후 다시 호출
    }
}
