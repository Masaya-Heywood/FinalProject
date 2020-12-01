using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private float count = 0;
    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if (count > 5.0f)
            Destroy(this.gameObject);
    }
}
