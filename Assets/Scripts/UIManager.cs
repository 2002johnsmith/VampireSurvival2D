using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("HUD")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider expBar;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI timeText;

    [Header("Level Up Screen")]
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private Button[] upgradeButtons;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;

    private PlayerManager player;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        levelUpPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;
        }
    }

    public void UpdateExpBar(int currentExp, int expToNextLevel)
    {
        if (expBar != null)
        {
            expBar.value = (float)currentExp / expToNextLevel;
        }
    }

    public void UpdateLevelText(int level)
    {
        if (levelText != null)
        {
            levelText.text = $"Nivel {level}";
        }
    }

    public void UpdateTimeText(float seconds)
    {
        if (timeText != null)
        {
            int minutes = Mathf.FloorToInt(seconds / 60);
            int secs = Mathf.FloorToInt(seconds % 60);
            timeText.text = $"{minutes:00}:{secs:00}";
        }
    }

    public void ShowLevelUpOptions(PlayerManager player)
    {
        Time.timeScale = 0f; // Pausar juego
        levelUpPanel.SetActive(true);

        // Generar 3-4 opciones aleatorias de mejora
        List<UpgradeOption> options = GenerateUpgradeOptions(player);

        for (int i = 0; i < upgradeButtons.Length && i < options.Count; i++)
        {
            int index = i; // Para closure
            UpgradeOption option = options[i];

            upgradeButtons[i].GetComponentInChildren<Text>().text = option.description;
            upgradeButtons[i].onClick.RemoveAllListeners();
            upgradeButtons[i].onClick.AddListener(() => SelectUpgrade(option, index));
        }
    }

    private List<UpgradeOption> GenerateUpgradeOptions(PlayerManager player)
    {
        List<UpgradeOption> options = new List<UpgradeOption>();

        // Ejemplos de mejoras
        options.Add(new UpgradeOption
        {
            type = TypeUpgrade.Damage,
            value = 5,
            description = "+5 Daño"
        });

        options.Add(new UpgradeOption
        {
            type = TypeUpgrade.ProyectileCount,
            value = 1,
            description = "+1 Proyectil"
        });

        options.Add(new UpgradeOption
        {
            type = TypeUpgrade.Cooldown,
            value = 0.1f,
            description = "-0.1s Cooldown"
        });

        // Mezclar y devolver 3 aleatorias
        return options.OrderBy(x => Random.value).Take(3).ToList();
    }

    private void SelectUpgrade(UpgradeOption option, int buttonIndex)
    {
        // Aplicar mejora al player
        player.UpgradeWeapon(0, option.type, option.value); // Por ahora a la primera arma

        // Cerrar panel
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f; // Reanudar juego
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
