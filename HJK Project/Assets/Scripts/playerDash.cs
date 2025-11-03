using System.Collections;
using UnityEngine;

public class playerDash : MonoBehaviour
{

    [Header("대쉬 설정")]
    [SerializeField] float dashForce = 2f;
    [SerializeField] float dashCooldown = 1f;
    [SerializeField] float dashTime = 0.3f;
    public bool canDash = true;

    [Header("스태미나 바")]
    [SerializeField] SliderBar stamina;

    public bool isDash = false;
    public bool canDash2 = true;

    TrailRenderer tr;
    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        anim = GetComponent<Animator>();
        tr.emitting = false;
    }

    void ShiftDash()
    {
        KeyCode dashKey = KeyCode.LeftShift;
        if (Input.GetKeyDown(dashKey) && canDash && canDash2)
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

        stamina.Dash();                     //  대쉬 시 스태미나 감소

        anim.SetBool("Idle", false);
        anim.SetBool("Run", false);
        anim.SetTrigger("Dash");

        tr.emitting = true;                      // 트레일 렌더러 활성화
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);    // 적 통과 기능 추가
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;                          //   대쉬 중에는 중력의 영향을 받지 않는다.
        float rot = transform.rotation.y;

        if(rot==0 || rot==-180) //  오른쪽 방향 대쉬
        {
            rb.linearVelocity = new Vector2(transform.localScale.x * dashForce, 0f);
        }
        else // 왼쪽 방향 대쉬
        {
            rb.linearVelocity = new Vector2(transform.localScale.x * -dashForce, 0f);
        }

        yield return new WaitForSeconds(dashTime);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);    //  적 통과 해제
        rb.gravityScale = originalGravity;              // 중력 영향 시작
        tr.emitting = false;                     // 트레일 렌더러 비활성화
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
