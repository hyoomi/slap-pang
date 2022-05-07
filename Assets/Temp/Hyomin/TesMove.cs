using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesMove : MonoBehaviour
{

    public float moveSpeed = 3f;
    Vector2 moveVec;

    //[SerializeField]
    Rigidbody2D rigid;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(this.gameObject.name +" "+ collision.gameObject.name);
        moveVec = Vector2.zero;
    }

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        moveVec = Vector2.zero;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveVec.y = 0;
            moveVec.x = -moveSpeed;
        }            
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveVec.y = 0;
            moveVec.x = moveSpeed;
        }           
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveVec.x = 0;
            moveVec.y = moveSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveVec.x = 0;
            moveVec.y = -moveSpeed;
        }

        rigid.velocity = moveVec;
    }
}
