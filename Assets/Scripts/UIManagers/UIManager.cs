using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pausePanel;
    
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        if (pausePanel)
            pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HitThePauseButtonFNC();
        }
    }

    public void HitThePauseButtonFNC()
    {
        if (_gameManager._gameOver)
            return;
        isPaused =!isPaused;
        if (pausePanel)
            pausePanel.SetActive(isPaused);
        if (SoundManager.instance) {
            SoundManager.instance.PlaySoundEffectFNC(0);
            Time.timeScale = (isPaused) ? 0 : 1;
        }
    }
    

    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
