using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    Slider hpSlider;
    float hp = 1000f;
    // Start is called before the first frame update
    void Start()
    {
        hpSlider = GetComponent<Slider>();
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
        //aaaa
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
