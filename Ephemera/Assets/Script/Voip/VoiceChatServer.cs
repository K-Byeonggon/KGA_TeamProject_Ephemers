using UnityEngine;
using Mirror;

public class VoiceChatServer : NetworkBehaviour
{
    [Command]
    void CmdSendVoiceData(float[] data)
    {
        // ���� Ŭ���̾�Ʈ�� �����ϰ� ��� Ŭ���̾�Ʈ���� ���� ������ ����
        RpcReceiveVoiceData(data);
    }

    [ClientRpc]
    void RpcReceiveVoiceData(float[] data)
    {
        // �������� ���� ���� ������ ���
        AudioClip receivedAudio = AudioClip.Create("Voice", data.Length, 1, AudioSettings.outputSampleRate, false);
        receivedAudio.SetData(data, 0);
        AudioSource.PlayClipAtPoint(receivedAudio, transform.position);
    }
}
