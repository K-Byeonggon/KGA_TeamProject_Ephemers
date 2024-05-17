using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class YipeeAI : MonoBehaviour
{
    private Node topNode;
    public Transform player;
    public float detectionRange = 10f;
    public float stoppingDistance = 2f;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;

    [SerializeField] float attackDistance = 1f;
    public Transform nest;
    [SerializeField] Transform Item;
    public bool setDesti = false;
    public float wanderRadius = 10f;
    public Vector3 itemPos;
    public GameObject detectedItem;
    public bool itemFind = false;
    public bool itemHave = false;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        ConstructBehaviorTree();
    }

    void Update()
    {
        topNode.Evaluate();
    }

    private void ConstructBehaviorTree()
    {
        //���� �������� children Node��
        ActionNode attackWill = new ActionNode(AttackWill);
        ActionNode moveToPlayer = new ActionNode(MoveToPlayer);
        ActionNode attackPlayer = new ActionNode(AttackPlayer);

        //��ȸ �������� children Node��
        ActionNode setDest = new ActionNode(SetDest);
        ActionNode moveToDest = new ActionNode(MoveToDest);

        //Ž�� �������� children Node��
        ActionNode setDestToScrap = new ActionNode(SetDestToScrap);
        ActionNode moveToScrap = new ActionNode(MoveToScrap);
        ActionNode getScrap = new ActionNode(GetScrap);
        ActionNode moveToNest = new ActionNode(MoveToNest);

        SequenceNode attackSequence = new SequenceNode(new List<Node> { attackWill, moveToPlayer, attackPlayer });
        SequenceNode wanderSequence = new SequenceNode(new List<Node> { setDest, moveToDest });
        SequenceNode detectSequence = new SequenceNode(new List<Node> { setDestToScrap, moveToScrap, getScrap, moveToNest });
        topNode = new SelectorNode(new List<Node> { attackSequence, wanderSequence, detectSequence });
    }

    //�������� Ȱ��ȭ(���� ������)
    private Node.State AttackWill()
    {
        //1. �÷��̾�� ���� �޾Ҵ��� bool���� Ȯ��
        bool isAttacked = false;    //�̰� ���߿� �ٸ� bool������ �ٲ��ֱ�.
        if (isAttacked) { /*�÷��̾� Transform ����*/ return Node.State.SUCCESS; }
        //2. �÷��̾�� ��ó�� ���� �־����� Ȯ��
        else if (false) { /*�÷��̾� Transform ����*/ return Node.State.SUCCESS; }
        //3. �÷��̾ ������ ��ǰ ���İ����� ��.
        else if(false) { /*�÷��̾� Transform ����*/ return Node.State.SUCCESS; }
        else return Node.State.FAILURE;
    }

    //�÷��̾�� ����(���� ������)
    private Node.State MoveToPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > attackDistance)
        {
            navMeshAgent.SetDestination(player.position);
            return Node.State.RUNNING;
        }
        else
        {
            return Node.State.SUCCESS;
        }
    }

    //�÷��̾ ����(���� ������)
    private Node.State AttackPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackDistance)
        {
            // ���� ���� ����
            return Node.State.SUCCESS;
        }
        return Node.State.FAILURE;
    }


    //���� ������ ����(�ѹ��� ����)(���ƴٴϱ� ������)
    private Node.State SetDest()
    {
        Debug.Log("SetDest");
        if (itemFind) return Node.State.FAILURE;        //�������� ã�Ƽ� �������� �����ϴ� ���¸�,
        else if (setDesti) return Node.State.SUCCESS;   //�̹� ������ ������ �Ǿ�������,
        else
        {
            Vector3 newPos = RandomNavMeshMovement.RandomNavSphere(nest.position, wanderRadius, -1);
            navMeshAgent.SetDestination(newPos);
            setDesti = true;
            return Node.State.SUCCESS;
        }
    }


    //�������� �̵�(���ƴٴϱ� ������)
    private Node.State MoveToDest()
    {
        Debug.Log("MoveToDest");

        if (itemFind) return Node.State.FAILURE;

        //�������� ������.
        else if (Vector3.Distance(transform.position, navMeshAgent.destination) <= 1f)
        {
            setDesti = false;
            return Node.State.SUCCESS;
        }
        else return Node.State.RUNNING;
    }

    //��ǰ���� ������ ����(Ž�� ������)
    private Node.State SetDestToScrap()
    {
        Debug.Log("SetDestToScrap");
        if (itemFind)
        {
            navMeshAgent.SetDestination(detectedItem.transform.position);
            return Node.State.SUCCESS;
        }
        else return Node.State.FAILURE;
    }

    //��ǰ���� �̵�(Ž�� ������)
    private Node.State MoveToScrap()
    {
        Debug.Log("MoveToScrap");
        if (itemFind)
        {
            if (Vector3.Distance(transform.position, navMeshAgent.destination) <= 0.5f)
            {
                Debug.Log("�Ÿ��� ���� �����.");
                return Node.State.SUCCESS;
            }
            else
            {
                Debug.Log("�ƴ� RUNNING�ǰ� �ִ°ž�?");

                //���߿� �������� ���� �ƴ� ���������� �ֿ����� FAILURE.
                if(detectedItem.transform.parent != null && detectedItem.transform.parent != transform) return Node.State.FAILURE;

                return Node.State.RUNNING;
            }
        }
        else return Node.State.FAILURE;
    }


    //��ǰ ����(Ž�� ������)
    private Node.State GetScrap()
    {
        Debug.Log("GetScrap");

        if (!itemHave)
        {
            //��ǰ ���

            //���߿� �������� ���� �ƴ� ���������� �ֿ����� FAILURE.
            if (detectedItem.transform.parent != null && detectedItem.transform.parent != transform) return Node.State.FAILURE;

            Debug.Log("���ø�");
            detectedItem.transform.position = transform.GetChild(1).position;
            detectedItem.transform.SetParent(transform.GetChild(1));
            itemHave = true;

            navMeshAgent.SetDestination(nest.position);
            setDesti = true;
            return Node.State.SUCCESS;
        }
        else if (itemHave) return Node.State.SUCCESS;
        else return Node.State.FAILURE;

    }

    //��ǰ ��� ������(Ž�� ������)
    private Node.State MoveToNest()
    {
        Debug.Log("MoveToNest");
        if (Vector3.Distance(transform.position, navMeshAgent.destination) <= 0.5f)
        {
            //��ǰ ��������
            Debug.Log("��������");
            detectedItem.transform.parent = null;
            setDesti = false;
            itemFind = false;
            itemHave = false;
            return Node.State.SUCCESS;
        }
        else { return Node.State.RUNNING; }
    }
}