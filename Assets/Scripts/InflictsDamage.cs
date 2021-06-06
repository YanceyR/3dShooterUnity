using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictsDamage : MonoBehaviour
{
    public float damageAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Health healthComponent = other.GetComponent<Health>();

        if (healthComponent != null)
        {
            healthComponent.dealDamage(damageAmount);
        }
    }
}
