using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LaserPointerUI : MonoBehaviour
{
    public XRRayInteractor rayInteractor; // Referencia al XR Ray Interactor
    public InputActionProperty clickAction; // Acción para el clic (gatillo)

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            lineRenderer.SetPosition(0, rayInteractor.transform.position); // Inicio del láser
            lineRenderer.SetPosition(1, hit.point); // Punto de impacto
        }
        else
        {
            lineRenderer.SetPosition(0, rayInteractor.transform.position);
            lineRenderer.SetPosition(1, rayInteractor.transform.position + rayInteractor.transform.forward * 10f); // Extensión máxima
        }
    
        // Detectar si el gatillo fue presionado
        if (clickAction.action.WasPerformedThisFrame())
        {
            // Realizar el Raycast y buscar un botón en la UI
            if (rayInteractor.TryGetCurrentUIRaycastResult(out var hitResult))
            {
                Button button = hitResult.gameObject.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.Invoke(); // Simula el clic en el botón
                }
            }
        }
    }
}
