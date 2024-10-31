using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) // Thay "Projectile" bằng tag của vật thể va chạm
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Tính toán hướng va chạm
                Vector3 forceDirection = (transform.position - collision.transform.position).normalized;

                // Tạo lực tác động
                rb.AddForce(forceDirection * 500f); // Thay đổi 500f theo nhu cầu
            }
        }
    }
}
