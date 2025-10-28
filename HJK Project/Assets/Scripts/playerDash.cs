using System.Collections;
using UnityEngine;

public class playerDash : MonoBehaviour
{

    [Header("대쉬 설정")]
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

        tr.emitting = true;                      // 트레일 렌더러 활성화
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);    // 적 통과 기능 추가
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;                          //   대쉬 중에는 중력의 영향을 받지 않는다.
        float rot = transform.rotation.y;

        if(rot==0 || rot==-180) //  오른쪽 방향 대쉬
        {
            rb.linearVelocity = new Vector2(transform.localScale.x * dashForce, 0f);
            Debug.Log("대쉬 오른쪽");
        }
        else // 왼쪽 방향 대쉬
        {
            rb.linearVelocity = new Vector2(transform.localScale.x * -dashForce, 0f);
            Debug.Log("대쉬 왼쪽");
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
