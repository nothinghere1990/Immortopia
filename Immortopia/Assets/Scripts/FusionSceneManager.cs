using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class FusionSceneManager : MonoBehaviour, INetworkRunnerCallbacks
{
    public static FusionSceneManager Instance { get; private set; }

    [SerializeField] private NetworkRunner networkRunner;
    
    public string lobbyName;
    
    public int currentParentSceneIndex;
    public int currentSubSceneIndex;
    public List<MyScene> subScenes;
    
    public List<SessionInfo> sessionList;
    
    public Action onConnectedToLobby;
    public Action onSessionListUpdated, onPlayerJoined;
    public Action onLoadParentSceneStarted, onLoadParentSceneDone;
    public Action onShotdown;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        
        networkRunner = GetComponent<NetworkRunner>();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnParentSceneLoadDone;
        
        lobbyName = "Lobby";
        currentParentSceneIndex = 0;
        AddSubScenes();
        LoadSubScene(0);
    }

    public async void ConnectToLobby()
    {
        networkRunner.ProvideInput = true;
        var result = await networkRunner.JoinSessionLobby(SessionLobby.Custom, lobbyName);

        if (result.Ok)
        {
            onConnectedToLobby?.Invoke();
            LoadSubScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public async void ConnectToSession(string sessionName)
    {
        onLoadParentSceneStarted?.Invoke();
        await networkRunner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.AutoHostOrClient,
            CustomLobbyName = lobbyName,
            SessionName = sessionName,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
            Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex + 1)
        });
    }
    
    public void DisconnectFromSession()
    {
        networkRunner.Shutdown(destroyGameObject: false);
        Destroy(networkRunner);
        StartCoroutine(AddNewNetworkRunner());
        LoadParentScene(SceneManager.GetActiveScene().buildIndex - 1, false);
    }

    private IEnumerator AddNewNetworkRunner()
    {
        yield return new WaitForEndOfFrame();
        networkRunner = gameObject.AddComponent<NetworkRunner>();
    }

    private void LoadParentScene(int parentSceneIndex, bool isNetwork)
    {
        if (isNetwork)
            networkRunner.LoadScene(SceneRef.FromIndex(parentSceneIndex));
        else
        {
            onLoadParentSceneStarted?.Invoke();
            SceneManager.LoadSceneAsync(parentSceneIndex);
        }
    }
    
    private void AddSubScenes()
    {
        subScenes.Clear();
        currentSubSceneIndex = 0;
        
        switch (currentParentSceneIndex)
        {
            case 0:
                subScenes.Add(GameObject.Find("UI Main Menu").GetComponent<MyScene>());
                subScenes.Add(GameObject.Find("UI Create or Join Session").GetComponent<MyScene>());
                subScenes.Add(GameObject.Find("UI Create Session").GetComponent<MyScene>());
                Debug.Log(GameObject.Find("UI Create or Join Session"));
                break;
            case 1:
                subScenes.Add(GameObject.Find("UI Waiting Room").GetComponent<MyScene>());
                break;
        }
    }
    
    public void LoadSubScene(int inputIndex)
    {
        foreach (MyScene subScene in subScenes)
        {
            subScene.LeaveSubScene();
        }
        currentSubSceneIndex = inputIndex;
        subScenes[currentSubSceneIndex].LoadSubScene();
    }
    
    public void LoadLastSubScene()
    {
        subScenes[currentSubSceneIndex].LeaveSubScene();
        if (currentSubSceneIndex > 0) currentSubSceneIndex -= 1;
        subScenes[currentSubSceneIndex].LoadSubScene();
    }
    
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        this.sessionList = sessionList;
        onSessionListUpdated?.Invoke();
    }
    
    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }
    
    private void OnParentSceneLoadDone(Scene scene, LoadSceneMode mode)
    {
        currentParentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        onLoadParentSceneDone?.Invoke();
        AddSubScenes();
        
        if (subScenes.Count <= 0) return;
        
        switch (currentParentSceneIndex)
        {
            case 0:
                //Reconnect to Lobby
                ConnectToLobby();
                LoadSubScene(subScenes.IndexOf(GameObject.Find("UI Create or Join Session").GetComponent<MyScene>()));
                break;
            case 1:
                LoadSubScene(subScenes.IndexOf(GameObject.Find("UI Waiting Room").GetComponent<MyScene>()));
                break;
        }
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }
    
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        onPlayerJoined?.Invoke();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        onShotdown?.Invoke();
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }
}