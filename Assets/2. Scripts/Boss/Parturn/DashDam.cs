using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDam : MonoBehaviour
{
    public int damageAmount = 1;
    private bool isDamaging = false;

    private void Start()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDamaging)
        {
            if (Boss.bossMove)
            {
                StartCoroutine(DealDamageToPlayer(collision.gameObject));
            }
        }
    }

    IEnumerator DealDamageToPlayer(GameObject player)
    {
        isDamaging = true;
        PlayerDie playerDie = player.GetComponent<PlayerDie>();
        playerDie.PlayerTakeDamage(damageAmount);

        yield return new WaitForSeconds(1f);

        isDamaging = false;
    }
}
