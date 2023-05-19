using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    PlayerData playerData;

    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        playerData.OnHealthChange += PlayerData_OnHealthChange;
    }

    private void PlayerData_OnHealthChange(object sender, PlayerData.OnHealthChangeEventArgs e)
    {
        image.fillAmount = (float)e.health / 100;
    }
}
