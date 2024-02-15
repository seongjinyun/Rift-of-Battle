using UnityEngine;

public class Wizard : MonoBehaviour
{
    public GameObject objectToSummon; // 소환할 오브젝트
    public float summonInterval = 6f; // 소환 간격
    private Animator anim;

    private Transform playerTransform; // 플레이어의 위치

    private float timer = 0f; // 소환 타이머

    private void Start()
    {
        // 플레이어의 위치를 추적하기 위해 플레이어의 Transform 컴포넌트를 가져옴
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // 타이머를 업데이트
        timer += Time.deltaTime;

        // 일정 시간마다 오브젝트를 소환
        if (timer >= summonInterval)
        {
            SummonObject();
            timer = 0f;
        }

        // 플레이어를 바라보도록 회전
        transform.LookAt(playerTransform);
    }

    private void SummonObject()
    {
        // 플레이어의 위치를 기준으로 오브젝트를 소환
        anim.SetTrigger("Attack");
        Instantiate(objectToSummon, playerTransform.position, Quaternion.identity);
    }
}

