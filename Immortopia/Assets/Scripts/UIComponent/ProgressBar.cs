using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public int minimum = 0;
    public int maximum = 100;
    public int current = 0;
    
    private Image mask;

    private void Start()
    {
        mask = transform.Find("Mask").GetComponent<Image>();
        mask.fillAmount = 0;
    }

    private void Update()
    {
        //Animation
        mask.fillAmount = Mathf.Lerp(mask.fillAmount, GetCurrentFill(), Time.deltaTime);
    }

    private float GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        return currentOffset / maximumOffset;
    }
}