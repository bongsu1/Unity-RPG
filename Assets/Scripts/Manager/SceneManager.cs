using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    [SerializeField] Image fade;
    [SerializeField] Slider loadingBar;

    private BaseScene curScene;

    public BaseScene GetCurScene()
    {
        if (curScene == null)
        {
            curScene = FindObjectOfType<BaseScene>();
        }

        return curScene;
    }

    // �Ϲ�ȭ
    public T GetCurScene<T>() where T : BaseScene
    {
        if (curScene == null)
        {
            curScene = FindObjectOfType<BaseScene>();
        }

        return curScene as T;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingRoutine(sceneName));
    }

    IEnumerator LoadingRoutine(string sceneName)
    {
        // Fade Out
        float time = 0;
        while (time < 0.5f)
        {
            time += Time.unscaledDeltaTime;
            fade.color = new Color(0, 0, 0, time * 2);
            yield return null;
        }

        loadingBar.gameObject.SetActive(true);
        Time.timeScale = 0f;
        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
        oper.allowSceneActivation = true;
        while (oper.isDone == false)
        {
            loadingBar.value = Mathf.Lerp(0f, 0.5f, oper.progress);
            yield return null;
        }

        BaseScene curScene = GetCurScene();
        yield return curScene.LoadingRoutine();
        Time.timeScale = 1f;
        loadingBar.value = 1f;
        loadingBar.gameObject.SetActive(false);

        // Fade In
        time = 0.5f;
        while (time > 0f)
        {
            time -= Time.unscaledDeltaTime;
            fade.color = new Color(0, 0, 0, time * 2);
            yield return null;
        }
    }

    public void SetLoadingBarValue(float value)
    {
        loadingBar.value = value;
    }
}
