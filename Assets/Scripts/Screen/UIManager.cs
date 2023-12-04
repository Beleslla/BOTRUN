using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject countdownScreen;
    [SerializeField] private TextMeshProUGUI cuentaAtras;

    private int _initTime;

    [Header("Sounds")]
    [SerializeField] private AudioClip negativeClickSound;
    [SerializeField] private AudioClip positiveClickSound;
    [SerializeField] private AudioClip gameOverSound;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        countdownScreen.SetActive(true);
        pauseScreen.SetActive(false);

        _initTime = (int)Time.realtimeSinceStartup;

        stopTime(true);
    }
    private void Update()
    {
        int _time = (int)Time.realtimeSinceStartup -_initTime;
        cuentaAtras.text = "" + (4 - _time);
        if (_time == 4)
        {
            stopTime(false);
            countdownScreen.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }
        }
    }
    #region Game Over
    public void GameOver()
    {
        SoundManager.instance.Playsound(gameOverSound);
        gameOverScreen.SetActive(true);
        stopTime(true);
    }

    public void Restart()
    {
        StartCoroutine(delayedRestart());
    }

    public void MainMenu()
    {
        StartCoroutine(delayedMainMenu());
    }

    public void Quit()
    {
        StartCoroutine(delayedQuit());    
    }
    
    public IEnumerator delayedRestart()
    {
        SoundManager.instance.Playsound(positiveClickSound);
        yield return new WaitForSecondsRealtime(positiveClickSound.length);
        stopTime(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator delayedMainMenu()
    {
        SoundManager.instance.Playsound(negativeClickSound);
        yield return new WaitForSecondsRealtime(negativeClickSound.length);
        stopTime(false);
        SceneManager.LoadScene(1);
    }

    public IEnumerator delayedQuit()
    {
        SoundManager.instance.Playsound(negativeClickSound);
        yield return new WaitForSecondsRealtime(negativeClickSound.length);
        Application.Quit(); //Aplicacion

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Editor
#endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);
        stopTime(status);
    }

    public void stopTime(bool status)
    {
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    #endregion






}