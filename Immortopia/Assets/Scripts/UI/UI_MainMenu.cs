using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    private Button startBtn;

    private void Start()
    {
        startBtn = transform.Find("container/startButton").GetComponent<Button>();
        startBtn.onClick.AddListener(JoinSession);
    }

    private void JoinSession()
    {
        FusionConnection.instance.ConnectToSession("Test Room");
    }
}
