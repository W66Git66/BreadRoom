using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAndRespawn : MonoBehaviour
{
    private bool isDead = false;
    //public Transform spawnPoint;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Deadline"))
        {
            Die();
        }
    }

    void Update()
    {
        
        if (isDead)
        {
            Respawn();
        }

    }
    public void Die()
    {
        isDead = true;
    }

    // ��ɫ����������
    void Respawn()
    {
        // ���ý�ɫ��λ�õ�������
        transform.position = new Vector2(-38.2f,0.12f);
        isDead = false; // ��ɫ����
    }
}
