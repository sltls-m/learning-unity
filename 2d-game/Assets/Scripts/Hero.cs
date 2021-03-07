using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour

{
    [SerializeField] private float speed = 3f; // Скорость движения
    [SerializeField] private int lives = 5; // Количество жизней
    [SerializeField] private float jumpForce = 15f; // Сила прыжка
    private bool isGrounded = false;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetButton("Horizontal"))
            Run();
        if (isGrounded && Input.GetButtonDown("Vertical"))
            Jump();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Run() 
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime); 
        sprite.flipX = dir.x < 0.0f;
    }
    private void Jump()
    {
       rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    // Почему не работают прыжки после добавления ограничений перестали работать.
    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
    }
    void Start()
    {
        
    }
}