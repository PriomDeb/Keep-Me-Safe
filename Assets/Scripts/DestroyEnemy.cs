using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    void Update()
    {
        Invoke(nameof(OnBecameInvisible), 10f);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
