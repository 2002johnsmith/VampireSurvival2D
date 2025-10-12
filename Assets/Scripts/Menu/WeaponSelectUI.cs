using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectUI : MonoBehaviour
{
    [SerializeField] private Button continueButton;

    private int selectedWeaponIndex = -1;

    private void Start()
    {
        if (continueButton != null) continueButton.interactable = false;
    }

    public void SelectWeapon(int weaponIndex)
    {
        selectedWeaponIndex = weaponIndex;
        if (continueButton != null) continueButton.interactable = true;
        Debug.Log($"WeaponSelectUI: seleccionaste arma {weaponIndex}");
    }

    public void ContinueGame()
    {
        if (selectedWeaponIndex == -1)
        {
            Debug.LogWarning("WeaponSelectUI: no seleccionaste arma.");
            return;
        }

        PlayerPrefs.SetInt("SelectedWeapon", selectedWeaponIndex);
        PlayerPrefs.Save();
        Debug.Log($"WeaponSelectUI: PlayerPrefs Saved SelectedWeapon={selectedWeaponIndex}");
/*
        if (GameManager.Instance != null)
            //GameManager.Instance.LoadScene("GameScene");
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
*/
    }
}
