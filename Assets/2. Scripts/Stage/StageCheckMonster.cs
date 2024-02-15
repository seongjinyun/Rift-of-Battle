using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCheckMonster : MonoBehaviour
{
    public GameObject objectToActivate; // 활성화할 게임 오브젝트

    private void Update()
    {
        // 씬 내에 몬스터 태그가 없을 경우 객체를 활성화
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        if (monsters.Length == 0)
        {
            objectToActivate.SetActive(true);
        }
    }
}
