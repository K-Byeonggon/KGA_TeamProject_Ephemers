using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView1 : MonoBehaviour
{
    public float viewRadius = 10f;
    [Range(0, 360)]
    public float viewAngle = 90f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public LineRenderer lineRenderer;
    public int edgeResolveIterations = 4;
    public float edgeDstThreshold = 0.5f;

    protected virtual void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0; // �ʱ� ������ ī��Ʈ�� 0���� ����
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    private IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private void LateUpdate()
    {
        DrawFieldOfView();
    }

    public virtual void FindVisibleTargets()
    {
        // �⺻ Ÿ�� ���� ���� (�ʿ�� ����)
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * 2);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
        }

        lineRenderer.positionCount = viewPoints.Count + 1;

        // ù ��° ���� ������Ʈ�� ��ġ�� ����
        lineRenderer.SetPosition(0, transform.position);

        for (int i = 0; i < viewPoints.Count; i++)
        {
            lineRenderer.SetPosition(i + 1, viewPoints[i]);
        }

        // ������ ���� �ٽ� ������Ʈ�� ��ġ�� �����Ͽ� �þ߸� ����
        lineRenderer.SetPosition(viewPoints.Count, transform.position);
    }

    private ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirectionFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
}