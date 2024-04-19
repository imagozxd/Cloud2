using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float rotationSpeed = 100f; // Velocidad de rotación del jugador
    public GameObject bulletPrefab; // Prefab del proyectil
    public float fireInterval = 0.5f; // Intervalo de tiempo entre disparos
    private float lastFireTime; // Tiempo del último disparo
    public float bulletSpeed = 10f; // Velocidad de la bala

    void Update()
    {
        // Captura la entrada del teclado
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calcula el ángulo de rotación basado en la entrada del teclado
        float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;

        // Invierte la dirección de rotación
        rotationAmount *= -1f;

        // Aplica la rotación al objeto del jugador
        transform.Rotate(Vector3.forward, rotationAmount);

        // Dispara un proyectil si se pulsa la tecla de disparo y ha pasado suficiente tiempo desde el último disparo
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastFireTime > fireInterval)
        {
            Shoot();
            lastFireTime = Time.time;
        }


    }
    void Shoot()
    {
        // Calcula la posición de inicio del proyectil al frente del jugador
        Vector3 spawnPosition = transform.position + transform.up;

        // Instancia un proyectil en la posición calculada y sin rotación
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // Obtiene el componente Rigidbody2D del proyectil
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        // Aplica una velocidad hacia adelante al proyectil basada en la velocidad de la bala
        bulletRigidbody.velocity = transform.up * bulletSpeed;
    }
    

}