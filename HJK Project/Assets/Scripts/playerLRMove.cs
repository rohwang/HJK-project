using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class playerLRMove : MonoBehaviour
{
    public playerDash dash;

    public float moveSpeed = 3f;
    Rigidbody2D rb;
    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Move()
    {

        float horiz = Input.GetAxisRaw("Horizontal"); // -1, 0, 1
        rb.linearVelocity = new Vector2(horiz * moveSpeed, rb.linearVelocity.y);

        
        Vector3 rot = transform.rotation.eulerAngles;

        if (horiz < 0)
        {
            transform.rotation = Quaternion.Euler(rot.x, 180 * (horiz < 0 ? 1 : 0), rot.z);
        }
        else if (horiz > 0)
        {
            transform.rotation = Quaternion.Euler(rot.x, 0, rot.z);
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
        Move();

        float horiz = Input.GetAxisRaw("Horizontal"); // -1, 0, 1
        // 걷기 및 IDLE 애니메이션 재생

        if (horiz != 0)
        {
                anim.SetBool("Idle", false);
                anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Idle", true);
        }
    }
}
