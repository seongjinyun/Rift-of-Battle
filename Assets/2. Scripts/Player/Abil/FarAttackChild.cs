using UnityEngine;

public class FarAttackChild : MonoBehaviour
{
    public GameObject projectilePrefab; // ���Ÿ� ����ü ������
    public float attackInterval = 1f; // ����ü �߻� ����

    private float timer; // Ÿ�̸� ����

    private void Update()
    {
        if (PlayerAbility.Skill4)
        {
            // Ÿ�̸Ӹ� ���ҽ�Ŵ
            timer -= Time.deltaTime;

            // Ÿ�̸Ӱ� 0 ������ �� ����ü �߻�
            if (timer <= 0f)
            {
                FireProjectile();
                timer = attackInterval; // Ÿ�̸Ӹ� �缳��
            }
        }
    }

    private void FireProjectile()
    {
        // Monster �±׷� ������ ã��
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("MonsterBody");

        // ���� ����� ���� ã��
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {

                closestEnemy = enemy.transform;
                closestDistance = distance;
                Debug.Log(closestEnemy);

            }
        }

        // ���� ����� ���� ���� ��� ����ü �߻�
        if (closestEnemy != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            if (projectile != null)
            {
                Projectile projectileComponent = projectile.GetComponent<Projectile>();
                if (projectileComponent != null)
                {
                    projectileComponent.Launch(closestEnemy.position);
                }
            }
        }

    }
}
