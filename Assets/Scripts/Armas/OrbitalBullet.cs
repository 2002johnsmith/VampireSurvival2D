using UnityEngine;

public class OrbitalBullet : BulletBase
{
    protected Transform center;
    protected int orbitalIndex;
    protected int totalOrbitals;
    protected float radius;
    protected float currentAngle;
    public void InitializeOrbital(Transform centerPoint, int index, int total,float orbitRadius, float dmg, float sz,float speed)
    {
        center = centerPoint;
        orbitalIndex = index;
        totalOrbitals = total;
        radius = orbitRadius;
        Initialize(dmg, speed, sz, 0);

        currentAngle = orbitalIndex * (360f / totalOrbitals);
    }

    protected override void Move()
    {
        if (center == null)
        {
            DestroyBullet();
            return;
        }
        currentAngle += speed * Time.deltaTime;
        Vector2 orbitPos = CalculateOrbitPosition(currentAngle, radius);
        transform.position = orbitPos;
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }

    private Vector2 CalculateOrbitPosition(float angle, float radius)
    {
        float radians = angle * Mathf.Deg2Rad;
        float x = Mathf.Cos(radians) * radius;
        float y = Mathf.Sin(radians) * radius;
        return (Vector2)center.position + new Vector2(x, y);
    }

    protected override void Update()
    {
        Move();
    }
}
