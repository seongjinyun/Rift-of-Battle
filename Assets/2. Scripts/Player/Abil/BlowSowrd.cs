using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowSowrd : MonoBehaviour
{
    public GameObject sowrdEff;
    public AudioClip atkSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster") || other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("АјАн" + Unit.blowDamage);
            SoundManager.instance.PlaySound(atkSound);
            other.gameObject.GetComponent<MonsterDamage>().TakeDamage(Unit.blowDamage);
            GameObject eff = Instantiate(sowrdEff, other.transform.position, Quaternion.identity);
            Destroy(eff, 0.2f);
        }
    }
}
