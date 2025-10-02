using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectUI : MonoBehaviour
{
    [SerializeField] private Button continueButton;

    private int selectedWeaponIndex = -1;

    private void Start()
    {
        continueButton.interactable = false;
    }

    public void SelectWeapon(int weaponIndex)
    {
        selectedWeaponIndex = weaponIndex;
        continueButton.interactable = true;

        Debug.Log("Arma seleccionada: " + weaponIndex);
    }

    public void ContinueGame()
    {
        if (selectedWeaponIndex == -1) return;

        PlayerPrefs.SetInt("SelectedWeapon", selectedWeaponIndex);
        PlayerPrefs.Save();

        GameManager.Instance.LoadScene("GameScene");
    }
}
