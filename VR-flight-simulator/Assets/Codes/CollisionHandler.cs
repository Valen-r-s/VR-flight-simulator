using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CollisionHandler : MonoBehaviour
{
    public Image[] lifeImages;
    public Sprite explosionSprite;
    public Image blackScreenImage;
    public float transitionDuration = 2f;
    public Canvas scoreCanvas;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    private int lives = 3;
    private bool isTransitioning = false;

    private float startTime;
    private float totalTime = 60f;

    private void Start()
    {
        startTime = Time.time;
        isTransitioning = false;
        lives = 3;
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        float timeRemaining = Mathf.Max(0, totalTime - (Time.time - startTime));
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = $"{minutes:00}:{seconds:00}"; // Formato MM:SS

        if (timeRemaining <= 0 && !isTransitioning)
        {
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && !isTransitioning)
        {
            LoseLife();

            // Ocultar el obstáculo desactivando el Renderer
            Renderer obstacleRenderer = other.GetComponent<Renderer>();
            if (obstacleRenderer != null)
            {
                obstacleRenderer.enabled = false; // Oculta el objeto
            }

            // Opcional: Desactivar todos los Renderers hijos, si el obstáculo tiene varios componentes visibles
            foreach (Renderer childRenderer in other.GetComponentsInChildren<Renderer>())
            {
                childRenderer.enabled = false;
            }

            // Desactiva completamente el objeto después
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Border") && !isTransitioning)
        {
            LoseAllLives();
        }
    }

    private void LoseAllLives()
    {
        // Poner todas las vidas a cero
        for (int i = lives - 1; i >= 0; i--)
        {
            lifeImages[i].sprite = explosionSprite; // Actualizar las imágenes de vida
        }
        lives = 0;

        // Activar el Game Over
        GameOver();
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
        StartCoroutine(TransitionToGameOver());
    }

    private System.Collections.IEnumerator TransitionToGameOver()
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

    public void ReachGoal()
    {
        StartCoroutine(ShowScore());
    }

    private System.Collections.IEnumerator ShowScore()
    {
        isTransitioning = true;

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

        int timeBonus = Mathf.Max(0, Mathf.FloorToInt((totalTime - (Time.time - startTime)) * 10));
        int score = (lives * 100) + timeBonus;
        scoreText.text = $"{score}";

        scoreCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}