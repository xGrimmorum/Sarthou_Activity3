using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform Player; // Reference to the player
    public GameObject ProjectilePrefab; // Prefab of the projectile
    public Transform FirePoint; // Point where projectiles spawn
    public float RotationSpeed = 5f; // Speed of turret rotation
    public float FireRange = 10f; // Range to fire
    public float FireAngleThreshold = 5f; // Angle tolerance for firing
    public float FireCooldown = 2f; // Cooldown duration

    private float CDTimer = 0f;

    void Update()
    {
        // Rotate turret towards player
        Vector2 DirectionToPlayer = (Player.position - transform.position).normalized;
        float TargetAngle = Mathf.Atan2(DirectionToPlayer.y, DirectionToPlayer.x) * Mathf.Rad2Deg;
        float Angle = Mathf.LerpAngle(transform.eulerAngles.z, TargetAngle, RotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, Angle);

        // Check distance and angle to player
        float DistanceToPlayer = Vector2.Distance(transform.position, Player.position);
        float AngleToPlayer = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, TargetAngle));

        if (DistanceToPlayer <= FireRange && AngleToPlayer <= FireAngleThreshold && CDTimer <= 0f)
        {
            Fire();
        }

        // Reduce cooldown timer
        if (CDTimer > 0f)
        {
            CDTimer -= Time.deltaTime;
        }
    }

    void Fire()
    {
        // Spawn projectile
        Instantiate(ProjectilePrefab, FirePoint.position, FirePoint.rotation);
        CDTimer = FireCooldown;
    }
}
