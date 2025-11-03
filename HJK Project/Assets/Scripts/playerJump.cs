using UnityEngine;

public class playerJump : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    public playerDash dash;
    public SliderBar stamina;

    [Header("지면 체크")]
    public Transform groundCheck;        // 지면 체크용 위치 (플레이어 발 밑에 빈 GameObject)
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;        // 지면으로 인식할 레이어
    private bool isGrounded;

    [Header("점프 설정")]
    [SerializeField] float jumpForce = 7f;         // 점프 힘
    public bool canJump2 = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Jump()
    {
        // 1) 지면 충돌 체크
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 2) 점프 입력 처리
        if (Input.GetButtonDown("Jump") && isGrounded && canJump2)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Run", false);

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            stamina.Jump();                     //  점프 시 스태미나 감소

            
        }
    }
    private void FixedUpdate()
    {
        if (dash.isDash)
        {
            return;
        }
    }

    void Update()
    {
        if (dash.isDash)
        {
            return;
        }
        Jump();
        
        if (rb.linearVelocity.y > 0.3f)    // 점프 애니메이션 실행
            {
                anim.ResetTrigger("Fall");
                anim.SetTrigger("Jump");
            }
            else if (rb.linearVelocity.y < 0.3f)
            {
                anim.ResetTrigger("Jump");
                anim.SetTrigger("Fall");
            }
    }
}
