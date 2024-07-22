using System;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private Transform _energyBar;
    private float _barScaleX;
    private float _barScaleY;

    private void Start()
    {
        _energyBar = transform.Find("EnergyBar").Find("Energy");
        var localScale = _energyBar.localScale;
        _barScaleX = localScale.x;
        _barScaleY = localScale.y;
    }

    public void UpdateEnergyBar(float newEnergy, float maxEnergy)
    {
        _energyBar.localScale = new Vector3((newEnergy / maxEnergy) * _barScaleX, _barScaleY, 1);
    }
}
