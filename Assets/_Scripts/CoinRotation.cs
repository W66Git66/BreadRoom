using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    // ��ת�ٶ�
    public float rotationSpeed = 100f;
    private float currentRotationY = 0f;

    void Update()
    {
        // �ۼ�Y�����ת�Ƕ�
        currentRotationY += rotationSpeed * Time.deltaTime;

        // ʹ�� Mathf.Repeat ���ֽǶ��� 0 �� 360 ֮��
        currentRotationY = Mathf.Repeat(currentRotationY, 360f);

        // �����������ת
        transform.rotation = Quaternion.Euler(0f, currentRotationY, 0f);
    }
}
