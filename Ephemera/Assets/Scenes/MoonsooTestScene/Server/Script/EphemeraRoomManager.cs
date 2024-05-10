using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EphemeraRoomManager : NetworkRoomManager
{
    //�������� ���� ������ Ŭ���̾�Ʈ�� �������� �� �����ϴ� �Լ�
    public override void OnRoomServerConnect(NetworkConnectionToClient conn)
    {
        base.OnRoomServerConnect(conn);

        //�÷��̾� ���ӿ�����Ʈ ����
        var player = Instantiate(spawnPrefabs[0]);

        //player.transform.position = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0);

        //Ŭ���̾�Ʈ�鿡�� �� ���ӿ�����Ʈ�� ��ȯ�Ǿ��ٴ°��� �˸���
        //�ű� ������ �÷��̾��� ������ ��� conn�� �ѱ����ν� player�� conn�� �������� �˸�
        NetworkServer.Spawn(player, conn);
    }
}
