using UnityEngine;

public class FlightController : MonoBehaviour
{
    public Transform joystick; // Asigna el joystick desde el inspector
    public float sensitivity = 10f; // Sensibilidad del control
    public float forwardSpeed = 20f; // Velocidad constante hacia adelante

    private void Update()
    {
        // Captura la rotaci�n del joystick
        float pitch = joystick.localRotation.eulerAngles.x;
        float roll = joystick.localRotation.eulerAngles.z;

        // Ajusta el pitch y roll para que vayan en el rango de -180 a 180
        if (pitch > 180) pitch -= 360;
        if (roll > 180) roll -= 360;

        // Invierte el movimiento de pitch (subir/bajar)
        pitch = -pitch;

        // Aplica la rotaci�n al avi�n (direcci�n de vuelo)
        transform.Rotate(pitch * sensitivity * Time.deltaTime, 0, -roll * sensitivity * Time.deltaTime);

        // Mueve el avi�n hacia adelante seg�n su orientaci�n
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;
    }
}
