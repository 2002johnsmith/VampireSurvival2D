using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public List<Transform> enemigos = new List<Transform>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegistrarEnemigo(Transform enemigo)
    {
        if (!enemigos.Contains(enemigo))
            enemigos.Add(enemigo);
    }

    public void EliminarEnemigo(Transform enemigo)
    {
        enemigos.Remove(enemigo);
    }
}
