using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : SingleTon<ScenesManager>
{
    public void LoadScene(string sceneName)
    {
        /*if(GameManager.Instance.UserData != null)
            GameManager.Instance.UserData.Save();
        SceneManager.LoadScene(sceneName);*/
        //StartCoroutine(LoadAsyncScene(sceneName));
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        //�񵿱������� ���� �ε�
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        //�ε尡 �Ϸ�� ������ ���
        while (!asyncLoad.isDone)
        {
            Debug.Log("Loading progress: " + (asyncLoad.progress * 100) + "%");

            yield return null;
        }
        PoolManager.Instance.SceneRig = GameObject.Find("SceneInstaller");
    }
}
