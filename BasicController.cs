using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour
{
    float vertical;
    float horizontal;

    static Animator _anim;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Vector3 _movement;
    public Rigidbody _rigidbody;
    public Transform target;
  



    public void Start()
    {
        // gets the attached rigid body of the plagyer // 
        _rigidbody = GetComponent<Rigidbody>();

    }

 


       private  void FixedUpdate()
    {
        //gets the Axis for horizontal and vertical and assigns a Vector 3 to them with a movespeed
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        //_movement = new Vector3(vertical, 0, horizontal);

        // apply movement to local orientation
        _movement = transform.forward * vertical + transform.right * horizontal;
        // normalize movement input

        if(_moveSpeed != 0 )
        {
            _anim.SetBool("IsWalking", true);
        }
        else

        {
            _anim.SetBool("IsWalking", false);
        }

        if (_movement.magnitude > 1f) _movement = _movement.normalized;

        _rigidbody.AddForce(_movement * _moveSpeed);
        Debug.DrawRay(transform.position, _movement * 3f, Color.green);


        if (_rigidbody.velocity.magnitude > 20)
        {
            _rigidbody.AddForce(-_movement * _moveSpeed);
        }


        Vector3 targetDir = transform.forward;
    }
}
