using System.Collections;
using UnityEngine;

public class Disppear : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // ľ����Ӿ���ʾ
    private Collider2D platformCollider;   // ľ�����ײ��
    private Vector2 original_Position;
    public float disappearTime = 3f;       // ľ����ʧ��ʱ��
    public float reappearTime = 3f;        // ľ�����³��ֵ�ʱ��

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
        yield return new WaitForSeconds(disappearTime); // �ȴ�3�����ʧ

        spriteRenderer.enabled = false;    // ����ľ����Ӿ�
        platformCollider.enabled = false;  // ����ľ�����ײ

        yield return new WaitForSeconds(reappearTime); // �ȴ�3������³���

        transform.localPosition = original_Position;
        spriteRenderer.enabled = true;     // ��ʾľ��
        platformCollider.enabled = true;   // ����ľ�����ײ
    }
}
