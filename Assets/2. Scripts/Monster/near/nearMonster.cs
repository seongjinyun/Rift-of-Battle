using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class nearMonster : MonoBehaviour
{
    public float attackRange = 5f; // ������ ���� ����
    public float attackDelay = 2f; // ���� ������ �ð�
    Animator animator; // ������ �ִϸ����� ������Ʈ

    public Transform player; // �÷��̾��� ��ġ�� �����ϱ� ���� ����
    private bool isAttacking = false; // ���� ������ ���θ� ��Ÿ���� ����
    private NavMeshAgent agent; // ������ �׺���̼� ������Ʈ ������Ʈ

    private void Start()
    {
        // playerTag�� ���� ������Ʈ�� ã�Ƽ� player ������ �Ҵ�
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // NavMeshAgent Ȱ��ȭ
        agent.enabled = true;

        // ���� ������ stoppingDistance�� ����
        agent.stoppingDistance = attackRange;
    }


    private void Update()
    {
        // player�� �Ҵ���� ���� ���, playerTag�� ���� ������Ʈ�� ã�Ƽ� �Ҵ�
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            return;
        }


        // �÷��̾ �����ϰ� �̵��ϴ� ����
        Vector3 directionToPlayer = player.position - transform.position;

        // �÷��̾� ������ �ٶ󺸵��� ȸ��
        transform.rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ���� ���� ���� �ִ� ���
        if (distanceToPlayer <= attackRange)
        {
            animator.SetBool("Walk", false);
            // Ray�� ����Ͽ� �÷��̾���� ���� �Ÿ� �˻�
            Ray ray = new Ray(transform.position, directionToPlayer);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, attackRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // ���� ���� �ƴ� ��쿡�� ����
                    if (!isAttacking)
                    {
                        Attack();
                    }
                }
            }
        }
        else
        {
            // ���� ���� �ۿ� �ִ� ��� ����
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

        // ���� �ð� ���� ����� �Ŀ� isAttacking ���� ����
        StartCoroutine(ResetAttack());
    }


}
