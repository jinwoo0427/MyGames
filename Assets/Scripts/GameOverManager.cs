using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private Text textScore = null;
    [SerializeField]
    private Text textBEST = null;

    private void Start()
    {

        textScore.text = string.Format("Score : {0}", PlayerPrefs.GetInt("Score"));
        textBEST.text = string.Format("BEST : {0}", PlayerPrefs.GetInt("BEST"));
    }

    public void OnClickYes()
    {
        SoundManager.instance.UiSound();
        SceneManager.LoadScene("Main");
    }
    public void OnClickNo()
    {
        SoundManager.instance.UiSound();
        SceneManager.LoadScene("StartScene");
    }
}
