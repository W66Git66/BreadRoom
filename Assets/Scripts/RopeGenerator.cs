using UnityEngine;

public class RopeGenerator : MonoBehaviour
{
    public GameObject ropeSegmentPrefab; // 预制体，用于绳子段
    public int segmentCount = 10;        // 绳子的段数
    public float segmentLength = 0.5f;   // 每段绳子的长度
    public Transform ropeStart;          // 绳子的起始位置
    public Transform ropeEnd;            // 绳子的终点，选填

    private GameObject[] ropeSegments;   // 存储生成的绳子段

    void Start()
    {
        GenerateRope();
    }

    void GenerateRope()
    {
        ropeSegments = new GameObject[segmentCount];
        Vector3 segmentPosition = ropeStart.position;

        GameObject previousSegment = null;

        // 生成绳子的每一段
        for (int i = 0; i < segmentCount; i++)
        {
            GameObject newSegment = Instantiate(ropeSegmentPrefab, segmentPosition, Quaternion.identity);
            ropeSegments[i] = newSegment;

            // 如果是第一个段，固定到起始点
            if (i == 0)
            {
                newSegment.GetComponent<Rigidbody2D>().isKinematic = true; // 固定第一段
                newSegment.transform.position = ropeStart.position;
            }
            else
            {
                // 每一段都通过 DistanceJoint2D 连接到前一个段
                DistanceJoint2D joint = newSegment.GetComponent<DistanceJoint2D>();
                joint.connectedBody = previousSegment.GetComponent<Rigidbody2D>();
                joint.autoConfigureDistance = false;
                joint.distance = segmentLength;
            }

            // 更新下一段的位置
            segmentPosition -= new Vector3(0, segmentLength, 0); // 向下生成每段

            previousSegment = newSegment;
        }

        // 如果有终点，则将绳子最后一段与终点连接
        if (ropeEnd != null)
        {
            DistanceJoint2D endJoint = ropeSegments[segmentCount - 1].GetComponent<DistanceJoint2D>();
            endJoint.connectedBody = ropeEnd.GetComponent<Rigidbody2D>();
        }
    }
}
