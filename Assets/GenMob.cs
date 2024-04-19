using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenMob : MonoBehaviour
{
    public float radius = 1f; // Radio del círculo
    public int segments = 36; // Número de segmentos del círculo
    public float raycastDistance = 5f; // Distancia máxima del raycast
    public LayerMask layerMask; // Máscara de capas para el raycast
    public GameObject brickPrefab; // Prefab del ladrillo
    public int numberOfBricks = 10; // Número de ladrillos a instanciar
    private GameObject bricksParent; // Referencia al GameObject que almacena los ladrillos


    void Start()
    {
        // Instancia los ladrillos
        SpawnBricks();
    }

    void SpawnBricks()
    {
        // Crea un GameObject para agrupar los ladrillos si no existe
        if (bricksParent == null)
        {
            bricksParent = new GameObject("Bricks");
        }

        // Itera a través de cada segmento del círculo
        for (int i = 0; i < segments; i++)
        {
            // Calcula el ángulo del segmento actual
            float angle = i * 2 * Mathf.PI / segments;

            // Calcula la dirección del rayo para el segmento actual
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);

            // Realiza el raycast en la dirección calculada
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, layerMask);

            // Si el raycast golpea algo, instancia ladrillos a lo largo del borde del círculo
            if (hit.collider != null)
            {
                // Itera a través del número de ladrillos a instanciar
                for (int j = 0; j < numberOfBricks; j++)
                {
                    // Calcula una posición aleatoria a lo largo del borde del círculo
                    float randomAngle = angle + Random.Range(-0.5f, 0.5f) * Mathf.PI;
                    Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f);
                    Vector3 randomPosition = transform.position + randomDirection * radius;

                    // Instancia el prefab del ladrillo como hijo del GameObject padre
                    GameObject brick = Instantiate(brickPrefab, randomPosition, Quaternion.identity, bricksParent.transform);
                }
            }
        }
    }

    void Update()
    {
        // Verifica si la cantidad de hijos de "Bricks" es igual a 2
        if (bricksParent != null && bricksParent.transform.childCount == 2)
        {
            // Vuelve a llamar a la función para crear más ladrillos
            SpawnBricks();
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja un círculo en la posición del objeto con el radio especificado
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}