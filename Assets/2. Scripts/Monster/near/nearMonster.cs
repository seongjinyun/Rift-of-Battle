using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class nearMonster : MonoBehaviour
{
    public float attackRange = 5f; // 몬스터의 공격 범위
    public float attackDelay = 2f; // 공격 딜레이 시간
    Animator animator; // 몬스터의 애니메이터 컴포넌트

    public Transform player; // 플레이어의 위치를 추적하기 위한 변수
    private bool isAttacking = false; // 공격 중인지 여부를 나타내는 변수
    private NavMeshAgent agent; // 몬스터의 네비게이션 에이전트 컴포넌트

    private void Start()
    {
        // playerTag를 가진 오브젝트를 찾아서 player 변수에 할당
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // NavMeshAgent 활성화
        agent.enabled = true;

        // 공격 범위를 stoppingDistance로 설정
        agent.stoppingDistance = attackRange;
    }


    private void Update()
    {
        // player가 할당되지 않은 경우, playerTag를 가진 오브젝트를 찾아서 할당
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            return;
        }


        // 플레이어를 추적하고 이동하는 로직
        Vector3 directionToPlayer = player.position - transform.position;

        // 플레이어 방향을 바라보도록 회전
        transform.rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 공격 범위 내에 있는 경우
        if (distanceToPlayer <= attackRange)
        {
            animator.SetBool("Walk", false);
            // Ray를 사용하여 플레이어와의 가시 거리 검사
            Ray ray = new Ray(transform.position, directionToPlayer);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, attackRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // 공격 중이 아닌 경우에만 공격
                    if (!isAttacking)
                    {
                        Attack();
                    }
                }
            }
        }
        else
        {
            // 공격 범위 밖에 있는 경우 추적
            isAttacking = false;
            agent.SetDestination(player.position);
            animator.SetBool("Walk", true);
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
    }

    private void Attack()
    {

        animator.SetTrigger("Attack");

        // 일정 시간 동안 대기한 후에 isAttacking 값을 리셋
        StartCoroutine(ResetAttack());
    }


}
