using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class nearMonster2 : MonoBehaviour
{
    public float attackRange = 5f; // ������ ���� ����
    public float attackDelay = 2f; // ���� ������ �ð�
    Animator animator; // ������ �ִϸ����� ������Ʈ

    public Transform player; // �÷��̾��� ��ġ�� �����ϱ� ���� ����
    private NavMeshAgent agent; // ������ �׺���̼� ������Ʈ ������Ʈ

    public int nearDamage = 1;
    private float timer = 0f;
    private float healthIncreaseInterval = 1f; // ���� ����

    private bool isPlayerInRange = false;

    private void Start()
    {
        // playerTag�� ���� ������Ʈ�� ã�Ƽ� player ������ �Ҵ�
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // NavMeshAgent Ȱ��ȭ
        agent.enabled = true;

        // ���� ������ stoppingDistance�� ����
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
                // ���� ����
            }
        }
    }

    private void Update()
    {
        // player�� �Ҵ���� ���� ���, playerTag�� ���� ������Ʈ�� ã�Ƽ� �Ҵ�
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            return;
        }

        animator.SetBool("Walk", true);

        // �÷��̾ �����ϰ� �̵��ϴ� ����
        Vector3 directionToPlayer = player.position - transform.position;

        // �÷��̾� ������ �ٶ󺸵��� ȸ��
        transform.rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        Delay();

        // �÷��̾ �����ϴ� �������� �̵�
        agent.SetDestination(player.position);
    }
}
