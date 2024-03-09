using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Loading : MonoBehaviour
{
    static string nextScene;

    public Image proGressBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    proGressBar.fillAmount += 0.1f;
        //}
    }

    // 호출할 씬의 이름을 매개변수로 받아와서 nextScene에 저장한다.
    public static void LoadScene(string SceneName)
    {
        nextScene = SceneName;
        // 이 로딩 씬을 실행한다.
        SceneManager.LoadScene("NewLoadingScene");
    }

    IEnumerator LoadSceneProcess()
    {
        // yield return 위의 코드를 실행하고 나서 0.3초 기다려라 
        yield return new WaitForSeconds(0.3f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false; // 씬이 끝날 때 자동으로 해당 씬으로 이동시킬려면 true, 아니면 false

        float timer = 0f;
        while (!operation.isDone)
        {
            yield return null;

            if(operation.progress < 0.9f)
            {
                proGressBar.fillAmount = operation.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                proGressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(proGressBar.fillAmount >= 1f)
                {
                    yield return new WaitForSeconds(1.7f);
                    operation.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}
