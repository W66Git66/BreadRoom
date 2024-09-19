using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;           // 移动速度
    //public float jumpForce = 10f;          // 跳跃力量
    public float slowFallDuration = 1f;    // 缓慢降落的最长时间
    public Vector2 slowFallScale = new Vector2(1.3f, 1.1f);  // 缓慢降落时角色的大小
    //public float fallMultiplier = 2.5f;    // 加速下坠的倍率

    public float jumpVelocity = 7f;          // 跳跃的初始速度
    //public LayerMask groundLayer;            // 地面层的图层掩码
    //public Transform groundCheck;            // 检查地面接触的位置
    //public float groundCheckRadius = 0.2f;   // 地面检测的小圆圈半径
    public float fallMultiplier = 2.5f;      // 下降时的速度乘数，用于加快下落
    public float lowJumpMultiplier = 2f;     // 轻按跳跃时的速度乘数，用于减少跳跃高度

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isSlowFalling;
    private float slowFallTime;

    private Vector2 originalScale;          // 记录原始大小
    private bool hasUsedSlowFall = false;   // 跳跃期间是否已经使用过m键降落

    public KeyCode SlowFallKey = KeyCode.LeftShift;

    private Vector3 spawnPoint;             // 记录主角的出生点
    private bool isDead = false;            // 主角是否死亡的标记

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // 存储角色的原始大小
        spawnPoint = transform.position;      // 记录主角的出生点
    }

    void Update()
    {
        Debug.Log("isGrounded:" + isGrounded);
        if (!isDead)
        {
            Move();
            SlowFall();
            //AccelerateFall();
            // 执行跳跃逻辑
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
                //isGrounded = false; // 重置跳跃输入
                hasUsedSlowFall = false; // 每次跳跃重置缓慢降落使用状态
            }

            // 当角色处于上升或下降状态时，调整跳跃曲线
            if (rb.velocity.y < 0)
            {
                // 下降时加速下落，模拟重力加速度
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                // 轻按空格键时减少上升速度，模拟小跳跃
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
        }
        else
        {
            Respawn();
        }
    }

    // 控制角色左右移动
    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // 当角色移动时，调整朝向
        if (moveInput != 0)
        {
            if (!isSlowFalling)
            {
                transform.localScale = new Vector2(Mathf.Sign(moveInput) * originalScale.x, originalScale.y);  // 根据移动方向调整朝向
            }
            else
            {
                transform.localScale = new Vector2(Mathf.Sign(moveInput) * originalScale.x * slowFallScale.x, originalScale.y * slowFallScale.y);  // 根据移动方向调整朝向
            }
        }
    }

    void FixedUpdate()
    {

    }
    // 控制跳跃
    //void Jump()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    //    {
    //        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    //        isGrounded = false;
    //        hasUsedSlowFall = false; // 每次跳跃重置缓慢降落使用状态
    //    }
    //}

    // 控制缓慢降落
    void SlowFall()
    {
        if (Input.GetKeyDown(SlowFallKey) && !isGrounded && !hasUsedSlowFall)
        {
            isSlowFalling = true;
            slowFallTime = slowFallDuration;
            hasUsedSlowFall = true; // 一次跳跃期间只允许一次缓慢降落

            // 保持角色当前的朝向，放大时不改变朝向
            float currentDirection = Mathf.Sign(transform.localScale.x); // 保持朝向（正负号）
            transform.localScale = new Vector2(currentDirection * originalScale.x * slowFallScale.x, originalScale.y * slowFallScale.y);
        }

        if (isSlowFalling)
        {
            slowFallTime -= Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -1f)); // 限制下降速度

            if (slowFallTime <= 0 || Input.GetKeyUp(SlowFallKey))
            {
                isSlowFalling = false;
                transform.localScale = new Vector2(Mathf.Sign(transform.localScale.x) * originalScale.x, originalScale.y); // 恢复原始大小，并保持朝向
            }
        }
    }

    // 加速下坠
    //void AccelerateFall()
    //{
    //    if (rb.velocity.y < 0 && !isSlowFalling) // 只有在下降并且没有缓慢降落时加速
    //    {
    //        rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    //    }
    //}

    // 检测是否在地面上
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Deadline"))
        {
            Die();
        }
    }

    // 检测是否在地面上
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    void Jump()
    {
        // 直接设置角色的垂直速度，使其跳跃
        rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
    }
    // 处理死亡状态
    public void Die()
    {
        isDead = true;
    }

    // 角色死亡后重生
    void Respawn()
    {
        // 重置角色的位置到出生点
        transform.position = spawnPoint;
        rb.velocity = Vector2.zero; // 重置速度
        isDead = false; // 角色复活
        isGrounded = true; // 复活时假设角色在地面上
        hasUsedSlowFall = false; // 重置缓慢降落使用状态
    }
}
