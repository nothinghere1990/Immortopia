using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_BlackScreen : MyScene
{
    public static UI_BlackScreen Instance { get; private set; }
    
    private Image blackScreenImage;

    protected override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(transform.parent.gameObject);
        
        blackScreenImage = GetComponentInChildren<Image>();
        DontDestroyOnLoad(transform.parent);
    }

    private async void Start()
    {
        FusionSceneManager.Instance.onLoadParentSceneStarted += async () => await fadeIn(.3f);
        FusionSceneManager.Instance.onLoadParentSceneDone += async () => await fadeOut(.3f);
        await fadeOut(.3f);
    }
    
    public override void LoadSubScene()
    {
    }
    
    private async Task fadeInAndOut(float fadeInDuration, float fadeOutDuration)
    {
        await fadeIn(fadeInDuration);
        await fadeOut(fadeOutDuration);
    }
    
    private async Task fadeIn(float duration)
    {
        int durationMs = (int) (duration * 1000);
        blackScreenImage.gameObject.SetActive(true);
        blackScreenImage.DOFade(1f, duration);
        await Task.Delay(durationMs);
    }
    
    private async Task fadeOut(float duration)
    {
        int durationMs = (int) (duration * 1000);
        
        blackScreenImage.gameObject.SetActive(true);
        blackScreenImage.DOFade(0f, duration);
        await Task.Delay(durationMs);
        blackScreenImage.gameObject.SetActive(false);
    }
    
    public override void LeaveSubScene()
    {
    }
}
