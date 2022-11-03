using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float Gravity = -9.81f;
    public float GroundDistance = 0.2f;
    // public float DashDistance = 5f;
    public LayerMask Ground;
    public Vector3 Drag;
    public Animator characterAnime;

    private CharacterController _controller;
    private Vector3 _velocity;
    private bool _isGrounded = true;
    private Transform _groundChecker;
    Vector3 move;


    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _groundChecker = transform.GetChild(0);
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        if (_isGrounded && _velocity.y < 0)
            _velocity.y = 0f;

        if(MenuController.instance.stage==0){
            move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        }
        else{
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        
        _controller.Move(move * Time.deltaTime * Speed);
        if (move != Vector3.zero){
            transform.forward = move;
            characterAnime.SetBool("isRun",true);
        }else{
            characterAnime.SetBool("isRun",false);
        }

        if (Input.GetButtonDown("Jump") && _isGrounded){
            _velocity.y += Mathf.Sqrt(JumpHeight * -2f * Gravity);  
            characterAnime.SetTrigger("jump");            
        }

        if(Input.GetButtonDown("Action")){
            MenuController.instance.OpenPuzzle();
        }

        if(Input.GetButtonDown("Cancel")){
            MenuController.instance.OnPause();
        }

        _velocity.y += Gravity * Time.deltaTime;

        _velocity.x /= 1 + Drag.x * Time.deltaTime;
        _velocity.y /= 1 + Drag.y * Time.deltaTime;
        _velocity.z /= 1 + Drag.z * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

}
