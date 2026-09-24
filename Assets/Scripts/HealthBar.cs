using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    //public float vidaActual;
    public float maxLife;

    void Update()
    {
        healthBar.fillAmount = maxLife - 0.01f;
    }
}