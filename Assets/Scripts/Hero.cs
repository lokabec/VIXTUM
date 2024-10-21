using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    private SpriteRenderer _sprite;
    private bool _faceRight = true;
    private Animator _anim;
   // private bool _isRunning = false;
    private void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) Run();
        Reflect();
        AllAnims();
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, _speed * Time.deltaTime);
        
        
    }
    private void Reflect()
    {
        if (Input.GetAxis("Horizontal") > 0 && _faceRight || Input.GetAxis("Horizontal") < 0 && !_faceRight)
        {
            transform.localScale *= new Vector2(-1, 1);
            _faceRight = !_faceRight;
        }
    }

    private void AllAnims()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0) _anim.SetFloat("SpeedX", 1f);
        else _anim.SetFloat("SpeedX", 0f);
    }
}
