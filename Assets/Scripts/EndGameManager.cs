using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel;

    private int kills = 0;
    private int totalKills = 100;

    private void Start()
    {
        endGamePanel.SetActive(false);
    }

    public void AddKill()
    {
        kills++;

        if (kills >= totalKills)
        {
            ShowEndGamePanel();
        }
    }

    private void ShowEndGamePanel()
    {
        endGamePanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.LoadScene("MenuScene");
    }
}
