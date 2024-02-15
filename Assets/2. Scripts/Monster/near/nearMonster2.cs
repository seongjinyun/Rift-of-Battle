using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class nearMonster2 : MonoBehaviour
{
    public float attackRange = 5f; // 몬스터의 공격 범위
    public float attackDelay = 2f; // 공격 딜레이 시간
    Animator animator; // 몬스터의 애니메이터 컴포넌트

    public Transform player; // 플레이어의 위치를 추적하기 위한 변수
    private NavMeshAgent agent; // 몬스터의 네비게이션 에이전트 컴포넌트

    public int nearDamage = 1;
    private float timer = 0f;
    private float healthIncreaseInterval = 1f; // 공격 간격

    private bool isPlayerInRange = false;

    private void Start()
    {
        // playerTag를 가진 오브젝트를 찾아서 player 변수에 할당
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // NavMeshAgent 활성화
        agent.enabled = true;

        // 공격 범위를 stoppingDistance로 설정
        agent.stoppingDistance = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            timer += Time.deltaTime;
            if (timer >= healthIncreaseInterval)
            {
                timer = 0f;
                other.gameObject.GetComponent<PlayerDie>().PlayerTakeDamage(nearDamage);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void Delay()
    {
        if (!isPlayerInRange)
        {
            timer += Time.deltaTime;
            if (timer >= healthIncreaseInterval)
            {
                timer = 0f;
                // 공격 실행
            }
        }
    }

    private void Update()
    {
        // player가 할당되지 않은 경우, playerTag를 가진 오브젝트를 찾아서 할당
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            return;
        }

        animator.SetBool("Walk", true);

        // 플레이어를 추적하고 이동하는 로직
        Vector3 directionToPlayer = player.position - transform.position;

        // 플레이어 방향을 바라보도록 회전
        transform.rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        Delay();

        // 플레이어를 추적하는 방향으로 이동
        agent.SetDestination(player.position);
    }
}
