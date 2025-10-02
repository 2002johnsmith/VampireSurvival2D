using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject Weapon;
    private void Start()
    {
        Weapon.SetActive(false);
        optionsPanel.SetActive(false);
    }
    public void PlayGame()
    {
        Weapon.SetActive(true);
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
}
