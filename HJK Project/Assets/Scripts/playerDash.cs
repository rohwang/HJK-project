using System.Collections;
using UnityEngine;

public class playerDash : MonoBehaviour
{

    [Header("�뽬 ����")]
    [SerializeField] float dashForce = 15f;
    [SerializeField] float dashCooldown = 1f;
    [SerializeField] float dashTime = 0.3f;
    bool canDash = true;

    public bool isDash = false;

    TrailRenderer tr;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
    }

    void ShiftDash()
    {
        KeyCode dashKey = KeyCode.LeftShift;
        if (Input.GetKeyDown(dashKey) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    private void FixedUpdate()
    {
        if (isDash)
        {
            return;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDash = true;

        tr.emitting = true;                      // Ʈ���� ������ Ȱ��ȭ
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);    // �� ��� ��� �߰�
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;                          //   �뽬 �߿��� �߷��� ������ ���� �ʴ´�.
        float rot = transform.rotation.y;

        if(rot==0 || rot==-180) //  ������ ���� �뽬
        {
            rb.linearVelocity = new Vector2(transform.localScale.x * dashForce, 0f);
            Debug.Log("�뽬 ������");
        }
        else // ���� ���� �뽬
        {
            rb.linearVelocity = new Vector2(transform.localScale.x * -dashForce, 0f);
            Debug.Log("�뽬 ����");
        }

        yield return new WaitForSeconds(dashTime);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);    //  �� ��� ����
        rb.gravityScale = originalGravity;              // �߷� ���� ����
        tr.emitting = false;                     // Ʈ���� ������ ��Ȱ��ȭ
        isDash = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }

    void Update()
    {
        if (isDash)
        {
            return;
        }
        ShiftDash();
    }
}
