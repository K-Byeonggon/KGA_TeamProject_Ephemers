using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomUI : MonoBehaviour
{
    public void CreateRoom()
    {
        var manager = EphemeraRoomManager.singleton;

        //�� ���� �۾� ó��
        //TODO

        //������ ���� ���ÿ� Ŭ���̾�Ʈ�ν� ���ӿ� ������ �� �ֵ��� ������ִ� �Լ�
        manager.StartHost();

        //Ŭ���̾�Ʈ�ν� ���ӿ� ������ �� �ֵ��� ������ִ� �Լ�
        //manager.StartClient();
    }
}
