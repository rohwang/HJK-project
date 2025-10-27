using UnityEngine;

public class playerJump : MonoBehaviour
{
    [Header("지면 체크")]
    public Transform groundCheck;        // 지면 체크용 위치 (플레이어 발 밑에 빈 GameObject)
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;        // 지면으로 인식할 레이어
    private bool isGrounded;

    [Header("점프 설정")]
    [SerializeField] float jumpForce = 7f;         // 점프 힘

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Jump()
    {
        // 1) 지면 충돌 체크
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 2) 점프 입력 처리
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Update()
    {
        Jump();
    }
}
