using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneIndex); 
        }
    }
}
