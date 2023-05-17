using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarUI : MonoBehaviour
{
    [SerializeField]
    BossData bossData;

    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        bossData.OnHealthChange += BossData_OnHealthChange;
    }

    private void BossData_OnHealthChange(object sender, BossData.OnHealthChangeEventArgs e)
    {
        Debug.Log("Health changed");
        image.fillAmount = (float)e.health / 320;
    }
}
