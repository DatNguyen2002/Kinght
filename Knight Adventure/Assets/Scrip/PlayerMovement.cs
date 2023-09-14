using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float moving = 0f;
    private float speed = 10f;
    private float jumpForce = 10f;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Flip();
    }

    private void Flip()
    {
        ///<summary>
        ///Neu nhan vat (quay mat sang phai && nhan di chuyen sang trai) 
        ///             se thay doi Scale de Player quay mat sang trai
        ///Neu nhan vat ( quay mat sang trai && nhan di chuyern sang phai)
        ///             se thay doi Scale de Player quay mat sang phai
        /// </summary>
        if((isFacingRight && moving< 0f)||(!isFacingRight && moving > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale=localScale;
        }
    }

    private void Move()
    {
        moving = Input.GetAxisRaw("Horizontal");//nhan gia tri dau vao tu ban phim
        rb.velocity = new Vector2(moving * speed, rb.velocity.y);// su ly di chuyen theo chieu x

        //su ly nhay theo chieu y
        if (Input.GetButtonDown("Jump") && IsGrounded())// neu nut dc an thi se thay doi chieu cao nhan vat
        {
            rb.velocity =new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
