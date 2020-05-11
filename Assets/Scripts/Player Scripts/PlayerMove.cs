using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Animator anim;
    private CharacterController charController;
    private CollisionFlags collisionFlags = CollisionFlags.None;

    private float moveSpeed = 5f;
    private bool canMove;
    private bool finished_Movement = true;

    private Vector3 target_Pos = Vector3.zero;
    private Vector3 player_Move = Vector3.zero;

    private float player_ToPointDistance;

    private float gravity = 9.8f;
    private float height;

    void Awake()
    {
        anim = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();
        charController.Move(player_Move);
    }

    void MoveThePlayer()
    {
        //click on the left button
        if (Input.GetMouseButton(0))
        {
            //get click point on the screen
            //need to convert it to ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //difinition of hit
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                //Is ray hit point terrian?
                if(hit.collider is TerrainCollider)
                {
                    //contain distance between player and hit position
                    player_ToPointDistance = Vector3.Distance(transform.position, hit.point);


                    if(player_ToPointDistance >= 1.0f)
                    {
                        canMove = true;
                        target_Pos = hit.point;
                    }

                }
            }
        }// if mouse button down

        if (canMove)
        {
            //set parameter of animator
            anim.SetFloat("Walk", 1.0f);


            //move
            Vector3 target_Temp = new Vector3(target_Pos.x, transform.position.y, target_Pos.z);

            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(target_Temp - transform.position),
                15.0f * Time.deltaTime);

            player_Move = transform.forward * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target_Pos) <= 0.5f)
            {
                canMove = false;
            }

        }
        else
        {
            player_Move.Set(0f, 0f, 0f);
            anim.SetFloat("Walk", 0f);
        }
    }

} //class




















