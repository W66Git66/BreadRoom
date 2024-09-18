using UnityEngine;

public class RopeGenerator : MonoBehaviour
{
    public GameObject ropeSegmentPrefab; // Ԥ���壬�������Ӷ�
    public int segmentCount = 10;        // ���ӵĶ���
    public float segmentLength = 0.5f;   // ÿ�����ӵĳ���
    public Transform ropeStart;          // ���ӵ���ʼλ��
    public Transform ropeEnd;            // ���ӵ��յ㣬ѡ��

    private GameObject[] ropeSegments;   // �洢���ɵ����Ӷ�

    void Start()
    {
        GenerateRope();
    }

    void GenerateRope()
    {
        ropeSegments = new GameObject[segmentCount];
        Vector3 segmentPosition = ropeStart.position;

        GameObject previousSegment = null;

        // �������ӵ�ÿһ��
        for (int i = 0; i < segmentCount; i++)
        {
            GameObject newSegment = Instantiate(ropeSegmentPrefab, segmentPosition, Quaternion.identity);
            ropeSegments[i] = newSegment;

            // ����ǵ�һ���Σ��̶�����ʼ��
            if (i == 0)
            {
                newSegment.GetComponent<Rigidbody2D>().isKinematic = true; // �̶���һ��
                newSegment.transform.position = ropeStart.position;
            }
            else
            {
                // ÿһ�ζ�ͨ�� DistanceJoint2D ���ӵ�ǰһ����
                DistanceJoint2D joint = newSegment.GetComponent<DistanceJoint2D>();
                joint.connectedBody = previousSegment.GetComponent<Rigidbody2D>();
                joint.autoConfigureDistance = false;
                joint.distance = segmentLength;
            }

            // ������һ�ε�λ��
            segmentPosition -= new Vector3(0, segmentLength, 0); // ��������ÿ��

            previousSegment = newSegment;
        }

        // ������յ㣬���������һ�����յ�����
        if (ropeEnd != null)
        {
            DistanceJoint2D endJoint = ropeSegments[segmentCount - 1].GetComponent<DistanceJoint2D>();
            endJoint.connectedBody = ropeEnd.GetComponent<Rigidbody2D>();
        }
    }
}
