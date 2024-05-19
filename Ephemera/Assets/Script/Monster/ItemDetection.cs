using UnityEngine;

public class ItemDetection : MonoBehaviour
{
    YipeeAI yipee;
    float nestIgnoreRadius = 0.5f;
    private void Start()
    {
        yipee = transform.parent.GetComponent<YipeeAI>();
    }
    // ������ Ž�� ������ ������ �� ȣ��Ǵ� �Լ�
    
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("���� ����");
       

        if (other.gameObject.tag == "ObtainableItem")
        {
            Debug.Log("������ ��Ҿ�");
            float distanceToNest = Vector3.Distance(other.transform.position, yipee.nest.position);
            //���� ��ó�� �ִ� �������� Ž������ ����.
            if (distanceToNest > nestIgnoreRadius)
            {   //�̹� �÷��̾��� ���̳� �ڽ��� �տ� ����ִ� �������� Ž������ ����.
                if (other.transform.parent == null)
                {
                    yipee.itemFind = true;
                    yipee.detectedItem = other.gameObject;
                }
            }
        }
    }
}