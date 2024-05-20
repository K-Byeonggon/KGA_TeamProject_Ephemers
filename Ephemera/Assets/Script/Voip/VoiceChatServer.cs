using UnityEngine;
using Mirror;
using System;

public class VoiceChatServer : NetworkBehaviour
{
    [Command]
    void CmdSendVoiceDataToServer(float[] data)
    {
        RpcReceiveVoiceData(data);
    }
    [ClientRpc]
    void RpcReceiveVoiceData(float[] data)
    {
        using (NetworkWriterPooled writer = new NetworkWriterPooled())
        {
            SerializeFloatArray(writer, data);
            SendDataToAllClients(writer.ToArray());
        }
    }

    void SerializeFloatArray(NetworkWriter writer, float[] array)
    {
        writer.WriteArray(array);
    }

    void SendDataToAllClients(byte[] data)
    {
        foreach (NetworkConnectionToClient conn in NetworkServer.connections.Values)
        {
            if (conn != null && conn != connectionToClient)
            {
                // VoiceDataMessage ����
                VoiceDataMessage message = new VoiceDataMessage();
                message.data = data;
                // �޽����� ���� (�⺻������ Reliable ä�� ���)
               // conn.Send<VoiceDataMessage>(message, 0);
            }
        }
    }

    // Send �޼��� �߰�
    public void Send<T>(T message, int channelId = Channels.Reliable) where T : struct, NetworkMessage
    {
        NetworkServer.SendToAll(message, channelId);
    }
}

public class VoiceDataMessage : NetworkMessage
{
    public byte[] data;
}
