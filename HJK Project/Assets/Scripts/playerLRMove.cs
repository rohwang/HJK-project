using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class playerLRMove : MonoBehaviour
{
    public playerDash dash;

    public float moveSpeed = 3f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Move()
    {

        float horiz = Input.GetAxisRaw("Horizontal"); // -1, 0, 1
        rb.linearVelocity = new Vector2(horiz * moveSpeed, rb.linearVelocity.y);

        // 걷기 애니메이션 재생
        
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
    }
}
