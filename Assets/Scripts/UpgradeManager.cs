using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private Button[] upgradeButtons;

    private int kills = 0;
    private int upgradeStep = 30; 

    private void Start()
    {
        upgradePanel.SetActive(false);

        foreach (Button btn in upgradeButtons)
        {
            btn.onClick.AddListener(() => OnUpgradeSelected(btn));
        }
    }

    public void AddKill()
    {
        kills++;

        if (kills % upgradeStep == 0)
        {
            ShowUpgradePanel();
        }
    }

    private void ShowUpgradePanel()
    {
        upgradePanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    private void OnUpgradeSelected(Button clickedButton)
    {
        Debug.Log("Mejora seleccionada: " + clickedButton.name);

        // Aqu� aplicamos mejoras seg�n bot�n y arma seleccionada
        // Ejemplo:
        // if(clickedButton.name == "DamageButton") { subir da�o del arma }

        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
