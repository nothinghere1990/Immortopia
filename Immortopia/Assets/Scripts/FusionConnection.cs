using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System;

public class FusionConnection : MonoBehaviour, INetworkRunnerCallbacks
{
    public static FusionConnection Instance { get; private set; }

    private NetworkRunner networkRunner;

    private string lobbyName;
    public SessionInfo sessionInfo;
    
    public Action OnCreateSession;
    public Action OnJoinSession;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
    }

    private void Start()
    {
        networkRunner = GetComponent<NetworkRunner>();
        networkRunner.ProvideInput = true;
    }

    public void ConnectToLobby(string lobbyName)
    {
        this.lobbyName = lobbyName;
        networkRunner.JoinSessionLobby(SessionLobby.Custom, this.lobbyName);
    }

    public async void ConnectToSession(string sessionName)
    {
        var startGameArgs = networkRunner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.AutoHostOrClient,
            CustomLobbyName = lobbyName,
            SessionName = sessionName,
            Scene = SceneRef.FromPath("Scenes/Session"),
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });

        var result = await startGameArgs;

        if (result.Ok)
        {
            sessionInfo = networkRunner.SessionInfo;
            
            if (networkRunner.IsServer)
                OnCreateSession.Invoke();
            if (networkRunner.IsClient)
                OnJoinSession.Invoke();
        }
        else
            Debug.LogError($"Failed to start game: {result.ErrorMessage}");
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

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer) Debug.Log("Server joined");
        else Debug.Log("Client joined");
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }
}