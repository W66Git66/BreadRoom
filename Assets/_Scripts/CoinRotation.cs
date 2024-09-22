using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    // 旋转速度
    public float rotationSpeed = 100f;
    private float currentRotationY = 0f;

    void Update()
    {
        // 累加Y轴的旋转角度
        currentRotationY += rotationSpeed * Time.deltaTime;

        // 使用 Mathf.Repeat 保持角度在 0 到 360 之间
        currentRotationY = Mathf.Repeat(currentRotationY, 360f);

        // 设置物体的旋转
        transform.rotation = Quaternion.Euler(0f, currentRotationY, 0f);
    }
}
