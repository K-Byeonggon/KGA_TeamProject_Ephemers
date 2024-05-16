using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomNetworkManager : NetworkRoomManager
{
    [SerializeField]
    GameObject gamePlayerObjectPrefab;
    [SerializeField]
    NetworkRoomPlayer roomPlayerObjectPrefab;

    public override void Start()
    {
        base.Start();
        spawnPrefabs.Add(roomPlayerObjectPrefab.gameObject);
        spawnPrefabs.Add(gamePlayerObjectPrefab);
        playerPrefab = gamePlayerObjectPrefab;
        roomPlayerPrefab = roomPlayerObjectPrefab;
    }

    //(�������� ȣ��)ȣ��Ʈ�� ���۵� ���� �����Ͽ� ������ ���۵� �� ȣ��˴ϴ�.
    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<CreateCharacterMessage>(OnCreateCharacter);
        NetworkServer.RegisterHandler<CreateRoomCharacterMessage>(OnCreateRoomCharacter);

        //GameObject prefab = ResourceManager.Instance.GetPrefab("MoonsooTestScene/Server/Prefab/Player.prefab");
    }
    public override void OnRoomStartClient() 
    {
        Debug.Log("OnRoomStartClient");

        CreateRoomCharacterMessage characterRoomMessage = new CreateRoomCharacterMessage
        {
            name = numPlayers.ToString(),
        };

        NetworkClient.Send(characterRoomMessage);
    }
    public override void OnRoomClientEnter()
    {
        base.OnRoomClientEnter();
    }
    public override void OnClientConnect()
    {
        Debug.Log("OnClientConnect");
        base.OnClientConnect();

        CreateCharacterMessage characterMessage = new CreateCharacterMessage
        {
            name = numPlayers.ToString(),
        };

        NetworkClient.Send(characterMessage);
    }

    //�����÷��̾� ���� �Լ�
    void OnCreateCharacter(NetworkConnectionToClient conn, CreateCharacterMessage message)
    {
        GameObject gameobject = Instantiate(gamePlayerObjectPrefab);
        //gameobject�� ������Ʈ�� ������ message�� �ʱ�ȭ

        NetworkServer.AddPlayerForConnection(conn, gameobject);
    }
    //room�÷��̾� ���� �Լ�
    void OnCreateRoomCharacter(NetworkConnectionToClient conn, CreateRoomCharacterMessage message)
    {
        GameObject gameobject = Instantiate(roomPlayerPrefab.gameObject);
        //gameobject�� ������Ʈ�� ������ message�� �ʱ�ȭ

        NetworkServer.AddPlayerForConnection(conn, gameobject);
    }
}
