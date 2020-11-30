using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpManager : MonoBehaviour
{
    private Image hpImage;
    private float damage = 0;
    private float counter = 1;
    private void Start()
    {
        hpImage = this.GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        if (damage >= counter) {

            counter += Time.deltaTime;

            if(hpImage.fillAmount >= 0)
                hpImage.fillAmount -= Time.deltaTime;
        }

        //testDamageForDebug
        if (Input.GetKeyDown(KeyCode.O)) {
            takeDamage(0.2f);
        }
    }

    public void takeDamage(float damage) {
        this.damage = damage;
        counter = 0;
    }
}
