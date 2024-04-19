using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Bullet : MonoBehaviour
{
    bool touchingWall = false;
    public Score score;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("muro"))
        {
            touchingWall = true;
            Debug.Log("La bala est� tocando la pared");

        }
        if (collision.collider.CompareTag("meteor") && touchingWall)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject); // Tambi�n puedes destruir la bala si deseas
            Debug.Log("BALA METEOR");
            // Aumenta el puntaje al destruir un meteorito
            if (score != null)
            {
                score.AddToScore(1); // Ajusta la cantidad de puntos seg�n tu juego
                Debug.Log("NO ENTRA AQUII   ");
            }
        }

    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("muro"))
        {
            touchingWall = false;
            Debug.Log("La bala ya no est� tocando la pared");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (touchingWall)
        {
            // Aqu� puedes agregar cualquier otra l�gica que desees ejecutar mientras la bala est� tocando la pared
        }
    }
}
