using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenMob : MonoBehaviour
{
    public float radius = 1f; // Radio del c�rculo
    public int segments = 36; // N�mero de segmentos del c�rculo
    public float raycastDistance = 5f; // Distancia m�xima del raycast
    public LayerMask layerMask; // M�scara de capas para el raycast
    public GameObject brickPrefab; // Prefab del ladrillo
    public int numberOfBricks = 10; // N�mero de ladrillos a instanciar
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

        // Itera a trav�s de cada segmento del c�rculo
        for (int i = 0; i < segments; i++)
        {
            // Calcula el �ngulo del segmento actual
            float angle = i * 2 * Mathf.PI / segments;

            // Calcula la direcci�n del rayo para el segmento actual
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);

            // Realiza el raycast en la direcci�n calculada
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, layerMask);

            // Si el raycast golpea algo, instancia ladrillos a lo largo del borde del c�rculo
            if (hit.collider != null)
            {
                // Itera a trav�s del n�mero de ladrillos a instanciar
                for (int j = 0; j < numberOfBricks; j++)
                {
                    // Calcula una posici�n aleatoria a lo largo del borde del c�rculo
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
            // Vuelve a llamar a la funci�n para crear m�s ladrillos
            SpawnBricks();
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja un c�rculo en la posici�n del objeto con el radio especificado
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}