using UnityEngine;

public class playerJump : MonoBehaviour
{
    [Header("���� üũ")]
    public Transform groundCheck;        // ���� üũ�� ��ġ (�÷��̾� �� �ؿ� �� GameObject)
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;        // �������� �ν��� ���̾�
    private bool isGrounded;

    [Header("���� ����")]
    [SerializeField] float jumpForce = 7f;         // ���� ��

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Jump()
    {
        // 1) ���� �浹 üũ
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 2) ���� �Է� ó��
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
