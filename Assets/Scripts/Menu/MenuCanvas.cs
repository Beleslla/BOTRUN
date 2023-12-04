using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{

    [Header("Sounds")]
    [SerializeField] private AudioClip positiveClickSound;
    [SerializeField] private AudioClip negativeClickSound;
    public void StartGame()
    {
        StartCoroutine(delayedStartGame());
    }
    IEnumerator delayedStartGame()
    {
        SoundManager.instance.Playsound(positiveClickSound);
        yield return new WaitForSecondsRealtime(positiveClickSound.length);
        SceneManager.LoadScene(2);
    }
    public void Quit()
    {
        StartCoroutine(trueQuit());
    }
    IEnumerator trueQuit()
    {
        SoundManager.instance.Playsound(negativeClickSound);
        yield return new WaitForSecondsRealtime(negativeClickSound.length);
        Application.Quit();//Aplicacion
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Editor
#endif
    }
}