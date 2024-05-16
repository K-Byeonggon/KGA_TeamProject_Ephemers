using UnityEngine;
using Mirror;

public class ServerPlayerMove : NetworkBehaviour
{
    Vector3 _moveDirection = Vector3.zero;
    [Client]
    private void Update()
    {
        if(isLocalPlayer)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 playerMovement = new Vector3(h * 0.25f, v * 0.25f, 0);

            _moveDirection = playerMovement;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                CmdMove();
            }
        }
    }

    //Command, Ŭ���̾�Ʈ���� ������ ����� �����ϴ� ��Ʈ����Ʈ
    [Command]
    private void CmdMove()
    {
        //���� ��ȿ�� �˻�
        //TODO

        RpcMove();
    }
    //ClientRpc, �������� Ŭ���̾�Ʈ�鿡�� 
    [ClientRpc]
    private void RpcMove()
    {
        //transform.Translate(_moveDirection);
        transform.Translate(Vector3.up);
    }
}
