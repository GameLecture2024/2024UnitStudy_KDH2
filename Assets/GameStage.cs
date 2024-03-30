using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStage : MonoBehaviour
{
    public static GameStage Instance;

    [Header("���� ���� ����")]
    public int spawnEnemyCount;
    public int waveCount = 1;
    public int maxWave;
    public bool stageClear;
    public GameObject enemyPrefab;
    public Transform[] spawnPositions;

    [Header("�������� Ŭ���� UI")]
    public GameObject clearGameUI;

    private void Awake() // �̱��� ����
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CreateEnemy(waveCount);
    }

    private void Update()
    {
        if(spawnEnemyCount <= 0 && !stageClear)
        {
            waveCount++;
            WaveProcess();
        }

        if (stageClear)
        {
            clearGameUI.SetActive(true);
        }
    }

    private void WaveProcess()
    {
        if(waveCount > maxWave)
        {
            stageClear = true;
            return;
        }
        else
        {
            CreateEnemy(waveCount);
        }
    }

    // ���� ���� �Լ�
    private void CreateEnemy(int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(enemyPrefab, SetCharacterPosition().position, Quaternion.identity);
            spawnEnemyCount++;
        }
    }

    private Transform SetCharacterPosition() // ������ ��ġ�� ��ȯ�ϴ� �Լ�
    {
        int selectPos = UnityEngine.Random.RandomRange(0, spawnPositions.Length);

        return spawnPositions[selectPos];
    }

    // �������� Ŭ���� UI ��ư
    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Intro");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
