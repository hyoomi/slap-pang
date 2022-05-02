using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesMove : MonoBehaviour
{

    public float moveSpeed = 10f;
    Vector2 moveVec;

    //[SerializeField]
    Rigidbody2D rigid;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //moveVec = Vector2.zero;
    }
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        moveVec = Vector2.zero;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            moveVec.x = -moveSpeed;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            moveVec.x = moveSpeed;

        rigid.velocity = moveVec;
    }
}
