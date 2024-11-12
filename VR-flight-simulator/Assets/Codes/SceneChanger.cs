using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Índice de la escena a la que quieres cambiar en el Build Settings
    public int sceneIndex;

    // Método para cambiar a la escena asignada
    public void ChangeScene()
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogWarning("El índice de escena especificado está fuera de rango.");
        }
    }
}
