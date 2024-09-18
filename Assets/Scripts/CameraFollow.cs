using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;            // 主角的Transform
    public float smoothTime = 0.3f;     // 相机平滑跟随的时间
    public Vector2 minPosition;         // 相机跟随的x轴和y轴的最小位置
    public Vector2 maxPosition;         // 相机跟随的x轴和y轴的最大位置
    public Vector3 offset;              // 相机相对于主角的偏移值

    private Vector3 velocity = Vector3.zero;  // 用于平滑移动的速度

    void LateUpdate()
    {
        // 计算相机的新位置
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z) + offset;

        // 限制相机在x轴和y轴的移动范围
        targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

        // 平滑地移动相机到目标位置
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
