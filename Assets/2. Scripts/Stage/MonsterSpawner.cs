using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // 몬스터 소환 위치 배열
    public GameObject[] monsters; // 몬스터 프리팹 배열
    public GameObject bossMonster; // 보스 몬스터 프리팹
    public Transform bossSpawnPoint; // 보스 몬스터 소환 위치

    // Start is called before the first frame update
    void Start()
    {
        if (donStageCanvas.currentStage >= 1 || donStageCanvas.currentStage >= 8)
        {
            SpawnMonsters();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 8스테이지 이후에는 다시 일반 몬스터를 소환

    }

    void SpawnMonsters()
    {
        // 7스테이지 이상이면 보스 몬스터 소환
        if (donStageCanvas.currentStage == 7 || donStageCanvas.currentStage == 14)
        {
            SpawnBossMonsterAtSpecificPoint();
        }
        else
        {
            SpawnRandomMonsters();
        }
    }

    void SpawnRandomMonsters()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // 랜덤으로 몬스터 선택
            GameObject randomMonster = monsters[Random.Range(0, monsters.Length)];

            // 선택된 몬스터를 랜덤한 위치에 소환
            Instantiate(randomMonster, spawnPoints[i].position, Quaternion.identity);
        }
    }

    void SpawnBossMonsterAtSpecificPoint()
    {
        // 보스 몬스터를 특정 위치에 소환
        Instantiate(bossMonster, bossSpawnPoint.position, Quaternion.identity);
    }
}
