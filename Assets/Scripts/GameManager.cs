using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerManager player;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeGame()
    {
        WeaponFactory.InitializeFactory();

        StartCoroutine(InitializePlayerWeapon());
    }

    private IEnumerator InitializePlayerWeapon()
    {
        // Esperar un frame para asegurar que PlayerManager esté listo
        yield return null;

        if (player != null)
        {
            // Forzar arma inicial para prueba
            WeaponData_So testWeapon = WeaponFactory.GetWeaponDataByName("BolaFuego");
            if (testWeapon != null)
            {
                player.AddWeapon(testWeapon);
                Debug.Log("GameManager: Arma inicial asignada al player");
            }
            else
            {
                Debug.LogError("GameManager: No se pudo cargar el arma inicial");
            }
        }
    }
}
