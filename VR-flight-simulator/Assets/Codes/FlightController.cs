using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour
{
    [Header("Referencias")]
    public Transform joystick; // Referencia al objeto del joystick
    public Transform airplane; // Referencia al objeto del avi�n

    [Header("Ajustes de Sensibilidad")]
    public float pitchSensitivity = 20f; // Sensibilidad para el movimiento hacia adelante y atr�s (inclinaci�n)
    public float rollSensitivity = 15f; // Sensibilidad para el movimiento de giro (izquierda y derecha)
    public float movementSpeed = 10f; // Velocidad de avance del avi�n
    public float maxAngle = 30f; // �ngulo m�ximo de inclinaci�n permitido

    private Quaternion initialJoystickRotation; // Rotaci�n inicial del joystick para referencia

    void Start()
    {
        // Guarda la rotaci�n inicial del joystick
        if (joystick != null)
        {
            initialJoystickRotation = joystick.localRotation;
        }
    }

    void Update()
    {
        if (joystick != null && airplane != null)
        {
            // Calcula la desviaci�n del joystick respecto a su rotaci�n inicial
            Quaternion rotationDelta = joystick.localRotation * Quaternion.Inverse(initialJoystickRotation);

            // Convierte la rotaci�n en �ngulos
            Vector3 rotationAngles = rotationDelta.eulerAngles;

            // Normaliza los �ngulos para que est�n en el rango [-180, 180]
            float pitch = NormalizeAngle(rotationAngles.x); // Movimiento adelante/atr�s
            float roll = NormalizeAngle(rotationAngles.z); // Movimiento izquierda/derecha

            // Limita los �ngulos m�ximos de inclinaci�n
            pitch = Mathf.Clamp(pitch, -maxAngle, maxAngle);
            roll = Mathf.Clamp(roll, -maxAngle, maxAngle);

            // Calcula los movimientos del avi�n basados en el joystick
            float pitchMovement = pitch / maxAngle * pitchSensitivity; // Movimiento vertical
            float rollMovement = roll / maxAngle * rollSensitivity; // Movimiento horizontal

            // Aplica las rotaciones al avi�n
            airplane.Rotate(Vector3.right, pitchMovement * Time.deltaTime); // Inclina adelante/atr�s
            airplane.Rotate(Vector3.forward, rollMovement * Time.deltaTime); // Corrige el giro horizontal

            // Mueve el avi�n hacia adelante
            airplane.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
    }

    // Funci�n para normalizar �ngulos
    private float NormalizeAngle(float angle)
    {
        if (angle > 180) angle -= 360;
        return angle;
    }
}