using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f; // ����ü �ӵ�

    private Vector3 targetPosition; // ��ǥ ��ġ

    public GameObject farEff;

    public void Launch(Vector3 target)
    {
        targetPosition = target;
    }

    private void Update()
    {
        if (targetPosition != Vector3.zero)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }

        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster") || other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("����" + Unit.farDamage);
            other.gameObject.GetComponent<MonsterDamage>().TakeDamage(Unit.farDamage);
            GameObject eff = Instantiate(farEff, other.transform.position, Quaternion.identity);
            Destroy(eff, 0.2f);
            Destroy(gameObject);
        }
    }
}
