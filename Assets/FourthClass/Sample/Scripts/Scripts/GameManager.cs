using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region �̱��� ����
    // Single : Ŭ������ �Ѱ��� �����ϵ��� �����ϰ�, �̸� �����ؼ�
    // �ٸ� Ŭ�������� �� Ŭ������ �ҷ��ͼ� ����� �� �ְ� �Ѵ�.

    // �̱��� ������ ���� : �̱����� �ʹ� ���� ������� ����
    // �ϳ��� Ŭ������ �ʹ� ���� �����͸� ��� �Ǵ� ������ ������
    // static �̱����� ������ �ϴµ�, ������ ����� �� ���� �޸𸮰�
    // ��� ���� �ִ� �������� �ֽ��ϴ�.

    // [������ ����] Ŭ���� ���� ���� ���踦 �� �� �ִ� ����(��� ����)��
    // ���س��� �� ����� ���� ���鵵�� �ϰ� �� ��
    //�⺻ ����, �����ϴ� ���
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if( null == instance)
            {
                instance = new GameManager();
            }

            return instance;
        }
    }

    // void Awake() �Լ��� ��� Ŭ������ void Start()���� ���� ����˴ϴ�.

    private void Awake()
    {
        if(null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // static�� ������ ������ Ŭ���� �̸����� �ٷ� ������ �� �ִ� ������ �ֽ��ϴ�.
    // �� ��� ������ static�� ������ ���� �������?
    // GameManager �ȿ� �ִ� ��� static�� ����� ������ �������� �˰� �־�� �մϴ�.
    // static���� ������ ������ ���α׷��� ����� �� ���� ���� �ֽ��ϴ�.

    public bool IsPlayerDeath;

    // static Ŭ���� ȣ��, �ν��Ͻ� ȣ��

    #endregion

    public GameObject[] gameOverObjects;

    private void SetGameSetting()
    {
        IsPlayerDeath = false;

        gameOverObjects[0] = GameObject.Find("BackGround");
        gameOverObjects[1] = GameObject.Find("GameOver");

        // �ð� ���� ������ 0���� �ʱ�ȭ �ϴ� ����.
    }
    public void GameOver()
    {
        foreach(GameObject obj in gameOverObjects)
        {
            obj?.SetActive(true);
        }
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif 
        Application.Quit();
    }

    public void GameRestart()
    {
        // �̸��� ��Ȯ���� ������ ������ �߻��մϴ�.
        // ���� �ٿ� �ֱ�� �� �̸��� �����ɴϴ�.
        SetGameSetting();
        SceneManager.LoadScene("GameScene");
    }
}
