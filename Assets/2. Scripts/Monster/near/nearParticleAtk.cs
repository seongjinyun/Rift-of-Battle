using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nearParticleAtk : MonoBehaviour
{
    public int nearDamage = 1;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerDie>().PlayerTakeDamage(nearDamage);

        }
    }
}
