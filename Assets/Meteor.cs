using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float minSpeed = 2f; // Velocidad mínima del ladrillo
    public float maxSpeed = 5f; // Velocidad máxima del ladrillo
    public Transform[] targetPositions; // Posiciones objetivo para los ladrillos

    void Start()
    {
        // Elige una posición objetivo aleatoria para el ladrillo
        if (targetPositions.Length > 0)
        {
            int randomIndex = Random.Range(0, targetPositions.Length);
            Transform target = targetPositions[randomIndex];

            // Calcula la dirección hacia la posición objetivo
            Vector3 targetDirection = (target.position - transform.position).normalized;

            // Asigna una velocidad aleatoria al ladrillo
            float randomSpeed = Random.Range(minSpeed, maxSpeed);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = targetDirection * randomSpeed;
            }

            // Apunta al ladrillo hacia la posición objetivo
            transform.up = targetDirection;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("destroyMeteor"))
        {
            // Destruye el objeto cuando colisiona con un objeto con el tag "destroyMeteor"
            Destroy(gameObject);
        }
    }
}

