using UnityEngine;

public class bobo: MonoBehaviour
{
    public float moveSpeed = 5f;           // �ƶ��ٶ�
    public float jumpForce = 10f;          // ��Ծ����
    public float slowFallDuration = 1f;    // ����������ʱ��
    public Vector2 slowFallScale = new Vector2(1.3f, 1.1f);  // ��������ʱ��ɫ�Ĵ�С
    public float fallMultiplier = 2.5f;    // ������׹�ı���

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isSlowFalling;
    private float slowFallTime;

    private Vector2 originalScale;          // ��¼ԭʼ��С
    private bool hasUsedSlowFall = false;   // ��Ծ�ڼ��Ƿ��Ѿ�ʹ�ù�m������

    public KeyCode SlowFallKey = KeyCode.LeftShift;

    private Vector3 spawnPoint;             // ��¼���ǵĳ�����
    private bool isDead = false;            // �����Ƿ������ı��

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // �洢��ɫ��ԭʼ��С
        spawnPoint = transform.position;      // ��¼���ǵĳ�����
    }

    void Update()
    {
        if (!isDead)
        {
            Move();
            Jump();
            SlowFall();
            AccelerateFall();
        }
        else
        {
            Respawn();
        }
    }

    // ���ƽ�ɫ�����ƶ�
    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // ����ɫ�ƶ�ʱ����������
        if (moveInput != 0)
        {
            if (!isSlowFalling)
            {
                transform.localScale = new Vector2(Mathf.Sign(moveInput) * originalScale.x, originalScale.y);  // �����ƶ������������
            }
            else
            {
                transform.localScale = new Vector2(Mathf.Sign(moveInput) * originalScale.x * slowFallScale.x, originalScale.y * slowFallScale.y);  // �����ƶ������������
            }
        }
    }

    // ������Ծ
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            hasUsedSlowFall = false; // ÿ����Ծ���û�������ʹ��״̬
        }
    }

    // ���ƻ�������
    void SlowFall()
    {
        if (Input.GetKeyDown(SlowFallKey) && !isGrounded && !hasUsedSlowFall)
        {
            isSlowFalling = true;
            slowFallTime = slowFallDuration;
            hasUsedSlowFall = true; // һ����Ծ�ڼ�ֻ����һ�λ�������

            // ���ֽ�ɫ��ǰ�ĳ��򣬷Ŵ�ʱ���ı䳯��
            float currentDirection = Mathf.Sign(transform.localScale.x); // ���ֳ��������ţ�
            transform.localScale = new Vector2(currentDirection * originalScale.x * slowFallScale.x, originalScale.y * slowFallScale.y);
        }

        if (isSlowFalling)
        {
            slowFallTime -= Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -1f)); // �����½��ٶ�

            if (slowFallTime <= 0 || Input.GetKeyUp(SlowFallKey))
            {
                isSlowFalling = false;
                transform.localScale = new Vector2(Mathf.Sign(transform.localScale.x) * originalScale.x, originalScale.y); // �ָ�ԭʼ��С�������ֳ���
            }
        }
    }

    // ������׹
    void AccelerateFall()
    {
        if (rb.velocity.y < 0 && !isSlowFalling) // ֻ�����½�����û�л�������ʱ����
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    // ����Ƿ��ڵ�����
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

    // ����Ƿ��ڵ�����
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

    // ��������״̬
    public void Die()
    {
        isDead = true;
    }

    // ��ɫ����������
    void Respawn()
    {
        // ���ý�ɫ��λ�õ�������
        transform.position = spawnPoint;
        rb.velocity = Vector2.zero; // �����ٶ�
        isDead = false; // ��ɫ����
        isGrounded = true; // ����ʱ�����ɫ�ڵ�����
        hasUsedSlowFall = false; // ���û�������ʹ��״̬
    }
}
