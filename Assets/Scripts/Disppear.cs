using System.Collections;
using UnityEngine;

public class Disppear : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // 木板的视觉表示
    private Collider2D platformCollider;   // 木板的碰撞体
    private Vector2 original_Position;
    public float disappearTime = 3f;       // 木板消失的时间
    public float reappearTime = 3f;        // 木板重新出现的时间

    public string CollisionName;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        platformCollider = GetComponent<Collider2D>();
        original_Position = transform.localPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name==CollisionName)
        {
            StartCoroutine(DisappearAndReappear());
        }
    }

    IEnumerator DisappearAndReappear()
    {
        yield return new WaitForSeconds(disappearTime); // 等待3秒后消失

        spriteRenderer.enabled = false;    // 隐藏木板的视觉
        platformCollider.enabled = false;  // 禁用木板的碰撞

        yield return new WaitForSeconds(reappearTime); // 等待3秒后重新出现

        transform.localPosition = original_Position;
        spriteRenderer.enabled = true;     // 显示木板
        platformCollider.enabled = true;   // 启用木板的碰撞
    }
}
