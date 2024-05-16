using UnityEngine;
using Mirror;

public class VoiceChatClient : NetworkBehaviour
{
    public string microphoneDevice; // ����ũ ��ġ�� �̸�
    public float voiceInterval = 0.1f; // ������ ���� ����

    private float voiceTimer = 0f;
    private AudioClip microphoneInput;
    public NetworkBehaviour[] NetworkBehaviours { get; private set; } = new NetworkBehaviour[] { };
    void Start()
    {
        microphoneInput = Microphone.Start(Microphone.devices[0].ToString(), true, 1, AudioSettings.outputSampleRate);
        Debug.Log(Microphone.devices[0].ToString());
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        voiceTimer += Time.deltaTime;
        if (voiceTimer >= voiceInterval)
        {
            SendVoiceData();
            voiceTimer = 0f;
        }
    }

    void SendVoiceData()
    {
        int microphoneLength = Microphone.GetPosition(microphoneDevice);
        float[] data = new float[microphoneLength];
        microphoneInput.GetData(data, 0);

        // ������ ���� �����͸� �����ϴ�.
        CmdSendVoiceData(data);
    }

    [Command]
    void CmdSendVoiceData(float[] data)
    {
        // �������� ��� Ŭ���̾�Ʈ���� ���� �����͸� �����մϴ�.
        RpcReceiveVoiceData(data);
    }

    [ClientRpc]
    void RpcReceiveVoiceData(float[] data)
    {
        // ���� ���� �����͸� ����մϴ�.
        AudioClip receivedAudio = AudioClip.Create("Voice", data.Length, 1, AudioSettings.outputSampleRate, false);
        receivedAudio.SetData(data, 0);
        AudioSource.PlayClipAtPoint(receivedAudio, transform.position);
    }
}
