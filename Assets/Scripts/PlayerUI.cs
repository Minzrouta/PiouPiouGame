using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private Transform _energyBar;
    private float _barScaleX;
    private float _barScaleY;
    private Button _hostButton;

    private void Start()
    {
        _energyBar = transform.Find("EnergyBar").Find("Energy");
        var localScale = _energyBar.localScale;
        _barScaleX = localScale.x;
        _barScaleY = localScale.y;
        _hostButton = transform.Find("Host").GetComponent<Button>();
        _hostButton.onClick.AddListener(HostGame);
    }

    public void UpdateEnergyBar(float newEnergy, float maxEnergy)
    {
        _energyBar.localScale = new Vector3((newEnergy / maxEnergy) * _barScaleX, _barScaleY, 1);
    }
    
    private void HostGame()
    {
        NetworkManager.Singleton.StartHost();
    }
}
