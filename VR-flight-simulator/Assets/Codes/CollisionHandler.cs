using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    public Image[] lifeImages; 
    public Sprite explosionSprite; 
    public Image blackScreenImage; 
    public float transitionDuration = 2f; 
    private int lives = 3;
    private bool isTransitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && !isTransitioning)
        {
            LoseLife();
        }
    }

    void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            lifeImages[lives].sprite = explosionSprite; 
        }

        if (lives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        StartCoroutine(TransitionToNextScene());
    }

    private System.Collections.IEnumerator TransitionToNextScene()
    {
        isTransitioning = true;

        Time.timeScale = 0f;

        float elapsed = 0f;
        Color color = blackScreenImage.color;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsed / transitionDuration);
            blackScreenImage.color = color;
            yield return null;
        }

        color.a = 1f;
        blackScreenImage.color = color;

        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOver"); 
    }
}
