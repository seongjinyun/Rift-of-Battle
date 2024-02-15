using UnityEngine;

public class FarAttackChild : MonoBehaviour
{
    public GameObject projectilePrefab; // 원거리 투사체 프리팹
    public float attackInterval = 1f; // 투사체 발사 간격

    private float timer; // 타이머 변수

    private void Update()
    {
        if (PlayerAbility.Skill4)
        {
            // 타이머를 감소시킴
            timer -= Time.deltaTime;

            // 타이머가 0 이하일 때 투사체 발사
            if (timer <= 0f)
            {
                FireProjectile();
                timer = attackInterval; // 타이머를 재설정
            }
        }
    }

    private void FireProjectile()
    {
        // Monster 태그로 적들을 찾음
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("MonsterBody");

        // 가장 가까운 적을 찾음
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

        // 가장 가까운 적이 있을 경우 투사체 발사
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
