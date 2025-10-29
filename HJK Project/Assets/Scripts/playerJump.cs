using UnityEngine;

public class playerJump : MonoBehaviour
{
    Rigidbody2D rb;
    public playerDash dash;
    public SliderBar stamina;

    [Header("���� üũ")]
    public Transform groundCheck;        // ���� üũ�� ��ġ (�÷��̾� �� �ؿ� �� GameObject)
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;        // �������� �ν��� ���̾�
    private bool isGrounded;

    [Header("���� ����")]
    [SerializeField] float jumpForce = 7f;         // ���� ��
    public bool canJump2 = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Jump()
    {
        // 1) ���� �浹 üũ
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 2) ���� �Է� ó��
        if (Input.GetButtonDown("Jump") && isGrounded && canJump2)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            stamina.Jump();                     //  ���� �� ���¹̳� ����
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
    }
}
