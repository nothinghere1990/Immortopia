using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_BlackScreen : Scene
{
    private Image blackScreenImage;
    
    public Action onFadeInComplete, onFadeOutComplete;

    protected override void Awake()
    {
        blackScreenImage = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        DontDestroyOnLoad(transform.parent);
        
        FusionConnection.Instance.onLoadSceneStarted += () => StartCoroutine(FadeIn(.3f));
    }
    
    public override void LoadScene()
    {
    }
    
    public IEnumerator FadeIn(float duration)
    {
        blackScreenImage.gameObject.SetActive(true);
        blackScreenImage.DOFade(1f, duration);
        yield return new WaitForSeconds(duration);
        onFadeInComplete?.Invoke();
    }
    
    public IEnumerator FadeOut(float duration)
    {
        blackScreenImage.gameObject.SetActive(true);
        blackScreenImage.DOFade(0f, duration);
        yield return new WaitForSeconds(duration);
        blackScreenImage.gameObject.SetActive(false);
        onFadeOutComplete?.Invoke();
    }
    
    public override void LeaveScene()
    {
    }
}
