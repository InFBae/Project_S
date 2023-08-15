using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    private LoadingUI loadingUI;

    private BaseScene curScene;
    public BaseScene CurScene
    {
        get
        {
            if (curScene == null)
                curScene = GameObject.FindObjectOfType<BaseScene>();

            return curScene;
        }
    }

    private void Awake()
    {
        LoadingUI loadingUI = Resources.Load<LoadingUI>("UI/LoadingUI");
        this.loadingUI = Instantiate(loadingUI);
        this.loadingUI.transform.SetParent(transform);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingRoutine(sceneName));
    }

    IEnumerator LoadingRoutine(string sceneName)
    {
        loadingUI.SetProgress(0f);
        loadingUI.FadeOut();
        yield return new WaitForSecondsRealtime(0.5f);

        //AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);

        PhotonNetwork.LoadLevel(sceneName);
        while (PhotonNetwork.LevelLoadingProgress >= 0.9)
        {
            loadingUI.SetProgress(Mathf.Lerp(0f, 1f, PhotonNetwork.LevelLoadingProgress));
            yield return null;
        }

        loadingUI.FadeIn();
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(0.5f);
    }
}
