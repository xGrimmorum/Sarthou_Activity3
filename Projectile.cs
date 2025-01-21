using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 10f; // Speed of the projectile
    public float DetectionRadius = 1f; // Distance to detect the player
    public Transform Player; // Reference to the player
    public float Lifetime = 5f; // Time before the projectile is destroyed

    private float Timer = 0f;

    void Update()
    {
        // Move projectile forward
        transform.position += transform.right * Speed * Time.deltaTime;

        // Check if close to player
        float DistanceToPlayer = Vector2.Distance(transform.position, Player.position);
        if (DistanceToPlayer <= DetectionRadius)
        {
            RestartScene();
        }

        // Destroy after lifetime expires
        Timer += Time.deltaTime;
        if (Timer >= Lifetime)
        {
            Destroy(gameObject);
        }
    }

    void RestartScene()
    {
        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
