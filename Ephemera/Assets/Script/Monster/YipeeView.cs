using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YipeeView : FieldOfView
{
    public override void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        //��ü�� �����ؼ�, �þ� ���� ��ŭ�� ��¥ ����.
        foreach (Collider target in targetsInViewRadius)
        {
            Transform targetTransform = target.transform;
            Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    //��������� �÷��̾ ���� ���� �� ���� ��ó�� ������������ �θ� �����? �÷��̾� ����.
                    Debug.Log("��������� " + targetTransform.name + " ���� ����.");

                }
            }
        }
    }
}
