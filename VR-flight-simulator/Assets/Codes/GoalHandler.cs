using UnityEngine;

public class GoalHandler : MonoBehaviour
{
    // Referencias a los objetos de los controladores
    public GameObject leftController;
    public GameObject rightController;
    public GameObject leftControllerRay;
    public GameObject rightControllerRay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Llama al método de alcanzar la meta
            FindObjectOfType<CollisionHandler>().ReachGoal();

            // Activa los Ray Controllers y desactiva los Hand Controllers
            ToggleControllers(false);
        }
    }

    public void ResumeGame()
    {
        // Activa los Hand Controllers y desactiva los Ray Controllers
        ToggleControllers(true);
    }

    private void ToggleControllers(bool showHandControllers)
    {
        // Cambia el estado de visibilidad de los controladores
        if (leftController != null) leftController.SetActive(showHandControllers);
        if (rightController != null) rightController.SetActive(showHandControllers);
        if (leftControllerRay != null) leftControllerRay.SetActive(!showHandControllers);
        if (rightControllerRay != null) rightControllerRay.SetActive(!showHandControllers);
    }
}
