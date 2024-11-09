using System.Collections.Generic;
using UnityEngine;

public class CustomSceneManager : MonoBehaviour
{
    public static CustomSceneManager Instance {  get; private set; }
    
    public int currentSceneIndex;
    public List<Scene> scenes;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
    }

    public void LoadScene(int inputIndex)
    {
        scenes[currentSceneIndex].GetComponent<IScene>().LeaveScene();
        currentSceneIndex = inputIndex;
        scenes[currentSceneIndex].GetComponent<IScene>().LoadScene();
    }
    
    public void LoadLastScene()
    {
        scenes[currentSceneIndex].GetComponent<IScene>().LeaveScene();
        if (currentSceneIndex > 0) currentSceneIndex -= 1;
        scenes[currentSceneIndex].GetComponent<IScene>().LoadScene();
    }
}