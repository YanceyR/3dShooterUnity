using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{

    private float deathBleedoutDuration = 2f;
    private float deathSpinSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReactToHit()
    {
        StartCoroutine(Spin());
    }

    private IEnumerator Spin()
    {
        float counter = 0f;

        while (counter < deathBleedoutDuration)
        {
            counter += Time.deltaTime;

            transform.Rotate(0, deathSpinSpeed, 0);

            yield return null;
        }

        Destroy(this.gameObject);
    }

}
