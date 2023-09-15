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
    [SerializeField] private Animator anim;

    private float moving = 0f;
    private float speed = 5f;
    private float jumpForce = 10f;
    private bool isFacingRight = true;
    
    private enum MovementState { idel, running,jumping, falling}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moving = Input.GetAxisRaw("Horizontal");//nhan gia tri dau vao tu ban phim
        Move();
        UpdateMovementState();
    }
    
    private void UpdateMovementState()
    {
        MovementState state;
        //nếu moving khác 0 thì sẽ ở trạng thái running bằng 0 ở trạng thái Idle
        state =(moving!=0)?MovementState.running:MovementState.idel;
        Flip();

        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private void Move()
    {
        rb.velocity = new Vector2(moving * speed, rb.velocity.y);// su ly di chuyen theo chieu x

        //su ly nhay theo chieu y
        if (Input.GetButtonDown("Jump") && IsGrounded())// neu nut dc an thi se thay doi chieu cao nhan vat
        {
            rb.velocity =new Vector2(rb.velocity.x, jumpForce);
        }

        //nhảy cao hơn khi giữ 
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)// nếu mà nhả nút và nhân vật ở trên không
        {
            //tốc độ sẽ giảm tốc
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()// kiểm tra nhân vật có ở tren mặt đất hay không
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Flip()
    {
        ///<summary>
        ///Neu nhan vat (quay mat sang phai && nhan di chuyen sang trai) 
        ///             se thay doi Scale de Player quay mat sang trai
        ///Neu nhan vat ( quay mat sang trai && nhan di chuyern sang phai)
        ///             se thay doi Scale de Player quay mat sang phai
        /// </summary>
        if ((isFacingRight && moving < 0f) || (!isFacingRight && moving > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
