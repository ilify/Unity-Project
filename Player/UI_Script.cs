using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    public GameObject Root;
    private Slider Slider;
    void Start()
    {
        Slider = GetComponent<Slider>();
        Slider.maxValue = Root.GetComponent<Mouvment>().StamenaMax;
    }

    // Update is called once per frame
    void Update()
    {
        Slider.value = Root.GetComponent<Mouvment>().Stamena;
    }
}
