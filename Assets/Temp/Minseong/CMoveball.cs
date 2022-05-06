using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMoveball : MonoBehaviour
{
    [SerializeField]
    GameObject []object_ball;
    Rigidbody2D[] rigid = new Rigidbody2D[2];
    float ballspeed = 3f;
    Vector2[] moveVec = new Vector2[2];
    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveVec[0] = Vector3.zero;
        moveVec[1] = Vector3.zero;
    }
    void Start()
    {
        rigid[0] = object_ball[0].GetComponent<Rigidbody2D>();
        rigid[1] = object_ball[1].GetComponent<Rigidbody2D>();
        moveVec[0] = Vector2.zero;
        moveVec[1] = Vector2.zero;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                moveVec[0].x = 0;
                moveVec[0].y = ballspeed;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                moveVec[0].x = 0;
                moveVec[0].y = -ballspeed;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveVec[0].x = -ballspeed;
                moveVec[0].y = 0;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveVec[0].x = ballspeed;
                moveVec[0].y = 0;

            }
        }

        else if (Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                moveVec[1].x = 0;
                moveVec[1].y = ballspeed;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                moveVec[1].x = 0;
                moveVec[1].y = -ballspeed;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveVec[1].x = -ballspeed;
                moveVec[1].y = 0;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveVec[1].x = ballspeed;
                moveVec[1].y = 0;
            }

        }
        rigid[0].velocity = moveVec[0];
        rigid[1].velocity = moveVec[1];
    }
}
