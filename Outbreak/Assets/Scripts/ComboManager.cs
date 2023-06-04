using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
   
    public bool isFull;
    public bool isEmpty;
    public float combo = 50f;
    public float maxCombo = 100f;
    float timeSinceGainCombo;
    Color baseColor;
    [SerializeField] float comboDecreaseCooldown;
    [SerializeField] float comboDecreaseAmount;
    [SerializeField] Image comboBar;

    private void Awake()
    {
     
        baseColor = comboBar.color;
    }

    public void AddCombo(int comboPoints)
    {
        combo += comboPoints;
        timeSinceGainCombo = 0;
        isEmpty = false;

        if (combo > maxCombo)
        {
            combo = maxCombo;
            isFull = true;
        }
    }

    public void Empty()
    {
        combo = 0;
        isFull = false;
        isEmpty = true;
    }

    private void Update()
    {
        comboBar.fillAmount = (float)(combo / maxCombo);
        if (isFull)
            comboBar.color = Color.white;
        else
            comboBar.color = baseColor;
    }

    private void FixedUpdate()
    {
        timeSinceGainCombo += Time.deltaTime;

        if (timeSinceGainCombo >= comboDecreaseCooldown && !isFull && !isEmpty)
        {
            combo -= comboDecreaseAmount;
            if (combo < 0)
            {
                combo = 0;
                isEmpty = true;
            }
        }
    }
}
