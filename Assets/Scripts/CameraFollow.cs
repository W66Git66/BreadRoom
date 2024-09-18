using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;            // ���ǵ�Transform
    public float smoothTime = 0.3f;     // ���ƽ�������ʱ��
    public Vector2 minPosition;         // ��������x���y�����Сλ��
    public Vector2 maxPosition;         // ��������x���y������λ��
    public Vector3 offset;              // �����������ǵ�ƫ��ֵ

    private Vector3 velocity = Vector3.zero;  // ����ƽ���ƶ����ٶ�

    void LateUpdate()
    {
        // �����������λ��
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z) + offset;

        // ���������x���y����ƶ���Χ
        targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

        // ƽ�����ƶ������Ŀ��λ��
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
