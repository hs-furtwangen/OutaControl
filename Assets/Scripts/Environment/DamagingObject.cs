using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    public int damageAmount = 1;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            collider.gameObject.GetComponent<Character>().DealDamage(damageAmount);
            Debug.Log("Ouch");
        }
    }
}
