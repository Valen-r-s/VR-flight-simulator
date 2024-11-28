using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour
{
    [Header("Referencias")]
    public Transform joystick; // Referencia al objeto del joystick
    public Transform airplane; // Referencia al objeto del avión

    [Header("Ajustes de Sensibilidad")]
    public float pitchSensitivity = 20f; // Sensibilidad para el movimiento hacia adelante y atrás (inclinación)
    public float rollSensitivity = 15f; // Sensibilidad para el movimiento de giro (izquierda y derecha)
    public float movementSpeed = 10f; // Velocidad de avance del avión
    public float maxAngle = 30f; // Ángulo máximo de inclinación permitido

    private Quaternion initialJoystickRotation; // Rotación inicial del joystick para referencia

    void Start()
    {
        // Guarda la rotación inicial del joystick
        if (joystick != null)
        {
            initialJoystickRotation = joystick.localRotation;
        }
    }

    void Update()
    {
        if (joystick != null && airplane != null)
        {
            // Calcula la desviación del joystick respecto a su rotación inicial
            Quaternion rotationDelta = joystick.localRotation * Quaternion.Inverse(initialJoystickRotation);

            // Convierte la rotación en ángulos
            Vector3 rotationAngles = rotationDelta.eulerAngles;

            // Normaliza los ángulos para que estén en el rango [-180, 180]
            float pitch = NormalizeAngle(rotationAngles.x); // Movimiento adelante/atrás
            float roll = NormalizeAngle(rotationAngles.z); // Movimiento izquierda/derecha

            // Limita los ángulos máximos de inclinación
            pitch = Mathf.Clamp(pitch, -maxAngle, maxAngle);
            roll = Mathf.Clamp(roll, -maxAngle, maxAngle);

            // Calcula los movimientos del avión basados en el joystick
            float pitchMovement = pitch / maxAngle * pitchSensitivity; // Movimiento vertical
            float rollMovement = roll / maxAngle * rollSensitivity; // Movimiento horizontal

            // Aplica las rotaciones al avión
            airplane.Rotate(Vector3.right, pitchMovement * Time.deltaTime); // Inclina adelante/atrás
            airplane.Rotate(Vector3.forward, rollMovement * Time.deltaTime); // Corrige el giro horizontal

            // Mueve el avión hacia adelante
            airplane.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
    }

    // Función para normalizar ángulos
    private float NormalizeAngle(float angle)
    {
        if (angle > 180) angle -= 360;
        return angle;
    }
}