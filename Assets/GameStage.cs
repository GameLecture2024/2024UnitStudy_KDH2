using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStage : MonoBehaviour
{
    public static GameStage Instance;

    [Header("몬스터 생성 관리")]
    public int spawnEnemyCount;
    public int waveCount = 1;
    public int maxWave;
    public bool stageClear;
    public GameObject enemyPrefab;
    public Transform[] spawnPositions;

    [Header("스테이지 클리어 UI")]
    public GameObject clearGameUI;

    private void Awake() // 싱글톤 선언
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

    // 몬스터 생성 함수
    private void CreateEnemy(int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(enemyPrefab, SetCharacterPosition().position, Quaternion.identity);
            spawnEnemyCount++;
        }
    }

    private Transform SetCharacterPosition() // 랜덤한 위치를 반환하는 함수
    {
        int selectPos = UnityEngine.Random.RandomRange(0, spawnPositions.Length);

        return spawnPositions[selectPos];
    }

    // 스테이지 클리어 UI 버튼
    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Intro");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
