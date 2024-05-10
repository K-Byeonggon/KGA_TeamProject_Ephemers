using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Examples.CharacterSelection;
using static Mirror.Examples.CharacterSelection.NetworkManagerCharacterSelection;

public class EphemeraRoomManager : NetworkRoomManager
{
    public bool SpawnAsCharacter = true;

    //�������� ���� ������ Ŭ���̾�Ʈ�� �������� �� �����ϴ� �Լ�
    public override void OnRoomServerConnect(NetworkConnectionToClient conn)
    {
        base.OnRoomServerConnect(conn);

        //roomPlayerPrefab;

        //�÷��̾� ���ӿ�����Ʈ ����
        var player = Instantiate(spawnPrefabs[0]);

        //Ŭ���̾�Ʈ�鿡�� �� ���ӿ�����Ʈ�� ��ȯ�Ǿ��ٴ°��� �˸���
        //�ű� ������ �÷��̾��� ������ ��� conn�� �ѱ����ν� player�� conn�� �������� �˸�
        NetworkServer.Spawn(player, conn);
    }


    private void ReturnToRoomScene(NetworkConnectionToClient conn)
    {
        // increment the index before adding the player, so first player starts at 1
        clientIndex++;

        if (Utils.IsSceneActive(RoomScene))
        {
            allPlayersReady = false;

            //Debug.Log("NetworkRoomManager.OnServerAddPlayer playerPrefab: {roomPlayerPrefab.name}");

            GameObject newRoomGameObject = OnRoomServerCreateRoomPlayer(conn);
            if (newRoomGameObject == null)
                newRoomGameObject = Instantiate(roomPlayerPrefab.gameObject, Vector3.zero, Quaternion.identity);

            NetworkServer.AddPlayerForConnection(conn, newRoomGameObject);
        }
        else
        {
            // Late joiners not supported...should've been kicked by OnServerDisconnect
            Debug.Log($"Not in Room scene...disconnecting {conn}");
            conn.Disconnect();
        }
    }

}
