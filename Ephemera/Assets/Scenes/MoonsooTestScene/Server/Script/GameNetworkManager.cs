using Mirror;
using System.Runtime.Serialization;
using UnityEngine;
using static Mirror.Examples.CharacterSelection.NetworkManagerCharacterSelection;

public class GameNetworkManager : NetworkManager
{
    [SerializeField]
    GameObject playerObjectPrefab;
    

    [SerializeField]
    public PrefabReference prefabReference;
    [SerializeField]
    public ObjectReference objectReference;

    int playerCount = 0;

    public static GameNetworkManager Instance => NetworkManager.singleton as GameNetworkManager;

    //(�������� ȣ��)ȣ��Ʈ�� ���۵� ���� �����Ͽ� ������ ���۵� �� ȣ��˴ϴ�.
    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler<CreateCharacterMessage>(OnCreateCharacter);
    }
    //(�������� ȣ��)Ŭ���̾�Ʈ�� ����� �� ȣ��
    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        GameObject playerObject = Instantiate(playerObjectPrefab);

        // call this to use this gameobject as the primary controller
        NetworkServer.AddPlayerForConnection(conn, playerObject);
        playerCount++;
    }
    //(�������� ȣ��)Ŭ���̾�Ʈ�� �߰��� �� ȣ��
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        GameObject player = Instantiate(prefabReference.playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player);
        //objectReference.camera.SetActive(false);
    }
    //(�������� ȣ��)Ŭ���̾�Ʈ�� �������� ���� ������ �� ȣ��
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
    }
    //(Ŭ���̾�Ʈ���� ȣ��)������ ����Ǹ� Ŭ���̾�Ʈ���� ȣ��˴ϴ�.
    //�⺻������ Ŭ���̾�Ʈ�� �غ� ���·� �����ϰ� �÷��̾ �߰��մϴ�.
    public override void OnClientConnect()
    {
        base.OnClientConnect();

        // you can send the message here, or wherever else you want
        CreateCharacterMessage characterMessage = new CreateCharacterMessage
        {
            name = playerCount.ToString(),
        };

        NetworkClient.Send(characterMessage);

        //objectReference.camera.SetActive(false);
        //NetworkClient.localPlayer.gameObject.GetComponent<PlayerController>().enabled = true;
    }

    //������ ���� �Լ�
    void OnCreateCharacter(NetworkConnectionToClient conn, CreateCharacterMessage message)
    {
        // playerPrefab is the one assigned in the inspector in Network
        // Manager but you can use different prefabs per race for example
        GameObject gameobject = Instantiate(prefabReference.playerPrefab);

        // call this to use this gameobject as the primary controller
        NetworkServer.AddPlayerForConnection(conn, gameobject);
    }

    //�÷��̾� ������Ʈ ����
    public void ReplacePlayer(NetworkConnectionToClient conn, GameObject newPrefab)
    {
        // Cache a reference to the current player object
        GameObject oldPlayer = conn.identity.gameObject;

        // Instantiate the new player object and broadcast to clients
        // Include true for keepAuthority paramater to prevent ownership change
        NetworkServer.ReplacePlayerForConnection(conn, Instantiate(newPrefab), true);

        // Remove the previous player object that's now been replaced
        // Delay is required to allow replacement to complete.
        Destroy(oldPlayer, 0.1f);
    }
}
