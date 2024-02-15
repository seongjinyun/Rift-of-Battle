using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // ���� ��ȯ ��ġ �迭
    public GameObject[] monsters; // ���� ������ �迭
    public GameObject bossMonster; // ���� ���� ������
    public Transform bossSpawnPoint; // ���� ���� ��ȯ ��ġ

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
        // 8�������� ���Ŀ��� �ٽ� �Ϲ� ���͸� ��ȯ

    }

    void SpawnMonsters()
    {
        // 7�������� �̻��̸� ���� ���� ��ȯ
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
            // �������� ���� ����
            GameObject randomMonster = monsters[Random.Range(0, monsters.Length)];

            // ���õ� ���͸� ������ ��ġ�� ��ȯ
            Instantiate(randomMonster, spawnPoints[i].position, Quaternion.identity);
        }
    }

    void SpawnBossMonsterAtSpecificPoint()
    {
        // ���� ���͸� Ư�� ��ġ�� ��ȯ
        Instantiate(bossMonster, bossSpawnPoint.position, Quaternion.identity);
    }
}
