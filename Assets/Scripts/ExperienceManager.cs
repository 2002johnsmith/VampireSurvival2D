using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ExperienceManager : MonoBehaviour
{
    [SerializeField] private AnimationCurve expCurve = AnimationCurve.Linear(0, 100, 50, 10000);
    [SerializeField] private int maxLevel = 50;

    [Header("UI References")]
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;

    private PlayerManager player;

    void Start()
    {
        UpdateUI();
    }

    public int CalculateExpForLevel(int level)
    {
        float normalizedLevel = (float)level / maxLevel;
        return Mathf.RoundToInt(expCurve.Evaluate(normalizedLevel));
    }

    public void UpdateExpBar(int currentExp, int expToNextLevel)
    {
        if (expSlider != null)
        {
            expSlider.value = (float)currentExp / expToNextLevel;
        }

        if (expText != null)
        {
            expText.text = $"{currentExp} / {expToNextLevel}";
        }
    }

    public void UpdateLevelText(int level)
    {
        if (levelText != null)
        {
            levelText.text = $"Lv. {level}";
        }
    }

    private void UpdateUI()
    {
        // Llamar desde PlayerManager cuando cambien los valores
    }
}
