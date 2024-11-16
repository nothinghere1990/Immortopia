using UnityEngine;
using UnityEngine.UI;

public class NonRectangleButton : MonoBehaviour
{
    public float alphaThreshold = .1f;
    
    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
    }
}