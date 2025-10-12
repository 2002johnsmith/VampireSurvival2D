using UnityEngine;

public class ExperienceCollectible : MonoBehaviour
{
    public static System.Action<int> OnExpCollected;

    [SerializeField] private int expValue = 10;
    [SerializeField] private float attractionSpeed = 8f;

    private Transform player;
    private bool isAttracted = false;

    void Start()
    {
        player = FindObjectOfType<PlayerManager>().transform;
    }

    void Update()
    {
        if (isAttracted && player != null)
        {
            // Moverse hacia el player
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                attractionSpeed * Time.deltaTime
            );

            // Si está muy cerca, recolectar
            if (Vector3.Distance(transform.position, player.position) < 0.5f)
            {
                Collect();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttracted = true;
        }
    }

    private void Collect()
    {
        OnExpCollected?.Invoke(expValue);
        Destroy(gameObject);
    }
}
