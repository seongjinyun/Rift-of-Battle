using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMons : MonoBehaviour
{
    public GameObject objectToActivate; // Ȱ��ȭ�� ���� ������Ʈ

    private void Update()
    {

        // �� ���� ���� �±װ� ���� ��� ��ü�� Ȱ��ȭ
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        if (monsters.Length == 0)
        {
            objectToActivate.SetActive(true);
        }
    }
}
