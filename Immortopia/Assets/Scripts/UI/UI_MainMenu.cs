using DG.Tweening;
using TMPro;
using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    private GameObject startBtn;
    private GameObject quitBtn;
    private GameObject backBtn;
    
    private void Start()
    {
        startBtn = transform.Find("startButton").gameObject;
        quitBtn = transform.Find("quitButton").gameObject;
        backBtn = GameObject.Find("backButton");
        
        CustomSceneManager.OnSceneLoaded += StartGameTextAni;
        CustomSceneManager.OnSceneLoaded += ActiveMainMenuBtn;
        
        backBtn.SetActive(false);
        
        StartGameTextAni();
    }
    
    private void ActiveMainMenuBtn()
    {
        if (CustomSceneManager.Instance.camPosRotIndex == 0)
        {
            startBtn.SetActive(true);
            quitBtn.SetActive(true);
            backBtn.SetActive(false);
        }
        else
        {
            startBtn.SetActive(false);
            quitBtn.SetActive(false);
        }
    }

    private void StartGameTextAni()
    {
        if (CustomSceneManager.Instance.camPosRotIndex == 0)
        {
            TMP_Text startBtnText = startBtn.GetComponentInChildren<TMP_Text>();
            var startBtnSequence = DOTween.Sequence();
            startBtnSequence.Append(startBtnText.DOFade(0, .7f));
            startBtnSequence.Append(startBtnText.DOFade(1, .7f)).OnComplete(StartGameTextAni);
        }
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
