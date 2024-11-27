using UnityEngine;
using UnityEngine.UI;

public class ColliderButton3D : MonoBehaviour
{
    private Button button; 
    
    private void Awake()
    {
        button = GetComponentInChildren<Button>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetMouseButtonUp(0)) button.onClick.Invoke();
    }
}
