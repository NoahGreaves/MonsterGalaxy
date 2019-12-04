using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer_Controller : MonoBehaviour
{
    [Header("Gravity Check")]   
    [SerializeField] private float _groundCheckDistance = 20f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _heightCheck = 1f;

    [Header("PLayer Controls")]
    [SerializeField] private float _playerSpeed = 10f;
    float vertical;
    float horizontal;
    Vector3 _movement;

    private Rigidbody _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();    
    }


    private void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, _groundCheckDistance, _groundMask))
        {
            Vector3 _groundPosition = hit.point;
            Vector3 _groundNormal = hit.normal;
            Vector3 _elevatedLevel = _groundPosition + _groundNormal * _heightCheck;

            Vector3 _groundForwardCheck = Vector3.Cross(-_groundNormal, transform.right);
            transform.rotation = Quaternion.LookRotation(_groundForwardCheck, _groundNormal);
            Vector3 targetPosition = _elevatedLevel + _groundForwardCheck * _playerSpeed * Time.fixedDeltaTime;

            // _rigidBody.MovePosition(targetPosition);

            //gets the Axis for horizontal and vertical and assigns a Vector 3 to them with a movespeed
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            //_movement = new Vector3(vertical, 0, horizontal);

            // apply movement to local orientation
            _movement = transform.forward * vertical + transform.right * horizontal;
            // normalize movement input
            if (_movement.magnitude > 1f) _movement = _movement.normalized;

            _rigidBody.AddForce(_movement * _playerSpeed);
            Debug.DrawRay(transform.position, _movement * 3f, Color.green);


            if (_rigidBody.velocity.magnitude > 20)
            {
                _rigidBody.AddForce(-_movement * _playerSpeed);
            }


            Vector3 targetDir = transform.forward;
        }

        else
        {
            _rigidBody.velocity = transform.forward * _playerSpeed;
        }

   
    }
}
