using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoilheadAI : MonoBehaviour
{
    private Node topNode;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    private DamageMessage damageMessage;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //ConstructBehaviorTree();

        damageMessage = new DamageMessage();
        damageMessage.damage = 100;
        damageMessage.damager = gameObject;
    }
    /*
    private void ConstructBehaviorTree()
    {
        //�������� óġ �Ұ��� ����. ü�µ� ������ ����.

        //���� ������
        ActionNode stop = new ActionNode(Stop);

        //���� �������� children Node��
        ActionNode attackWill = new ActionNode(AttackWill);
        ActionNode moveToPlayer = new ActionNode(MoveToPlayer);
        ActionNode attackPlayer = new ActionNode(AttackPlayer);

        //��ȸ �������� children Node��
        ActionNode setDest = new ActionNode(SetDest);
        ActionNode moveToDest = new ActionNode(MoveToDest);

    }

    void Update()
    {
        topNode.Evaluate();
    }

    //[���� ������] ����
    private Node.State Stop()
    {
        //�÷��̾ ���� ������ SUCCESS�� ����.
        //�ƴϸ� FAILURE
    }

    //[���� ������] ���� ����
    private Node.State AttackWill()
    {
        //�÷��̾ �߰������� �ӵ��� �������� ��� �÷��̾ ��ǥ�� ����.
    }

    //[���� ������] �÷��̾ ���� �̵�
    private Node.State MoveToPlayer()
    {

    }

    //[���� ������] �÷��̾� ����
    private Node.State AttackPlayer()
    {

    }
    */
}
