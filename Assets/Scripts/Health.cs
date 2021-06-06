using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float healthAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dealDamage(float damageAmount)
    {
        healthAmount -= damageAmount;

        if(healthAmount <= 0)
        {
            die();
        }
    }

    void die()
    {

        Destroy(gameObject);
    }
}
