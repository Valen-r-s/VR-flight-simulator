using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public int scene;

    // Referencias a los objetos
    public GameObject leftController;
    public GameObject rightController;
    public GameObject leftControllerRay;
    public GameObject rightControllerRay;

    private bool isPaused = false;
    private bool buttonPressed = false; // Para evitar múltiples activaciones al mantener el botón presionado

    void Start()
    {
        // Garantiza que el tiempo se inicie correctamente
        Time.timeScale = 1f;

        // Asegura que el menú de pausa esté desactivado al inicio
        pauseMenuCanvas.SetActive(false);

        // Activa los controladores estándar y desactiva los rayos
        SetControllerVisibility(true);
    }

    void Update()
    {
        // Detectar tecla Escape (para pruebas en PC)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Detectar botón de pausa en los controladores VR
        bool isButtonCurrentlyPressed =
            IsButtonPressed(XRNode.LeftHand, CommonUsages.secondaryButton) ||
            IsButtonPressed(XRNode.RightHand, CommonUsages.secondaryButton);

        if (isButtonCurrentlyPressed && !buttonPressed)
        {
            TogglePause();
            buttonPressed = true;
        }
        else if (!isButtonCurrentlyPressed)
        {
            buttonPressed = false;
        }
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f; // Detiene el tiempo
        isPaused = true;

        // Activar rayos y ocultar controladores
        SetControllerVisibility(false);
    }

    public void ResumeGame()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f; // Restaura el tiempo
        isPaused = false;

        // Activar controladores y ocultar rayos
        SetControllerVisibility(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }

    private bool IsButtonPressed(XRNode hand, InputFeatureUsage<bool> button)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(hand);
        if (device.TryGetFeatureValue(button, out bool isPressed))
        {
            return isPressed;
        }
        return false;
    }

    private void SetControllerVisibility(bool showControllers)
    {
        // Mostrar u ocultar controladores
        leftController.SetActive(showControllers);
        rightController.SetActive(showControllers);

        // Mostrar u ocultar rayos
        leftControllerRay.SetActive(!showControllers);
        rightControllerRay.SetActive(!showControllers);
    }
}
