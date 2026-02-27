
using System;
using Newtonsoft.Json;
using TicTacTockGame;
using SocketIOClient;


public class RoomData
{
    [JsonProperty("roomId")]
    public string roomId {get; set;}
}

public class UserData
{
    [JsonProperty("usedId")]
    public string userId{get;set;}
}

public class Movedata
{
    [JsonProperty("position")]
    public int position {get;set;}
}
public enum MultiPlayMangerState
{
    CreateRoom, // 방 생성 
    JoinRoom, // 방 참가
    StartGame, //두 유저가 방에 모두 들어와서 게임을 시작할 떼
    ExitRoom, // 자신이 방을 빠져 나왔을 때
    EndGame //상대방이 접속을 끊거나 방을 나갔을 때
}


public class MultiplayManager : IDisposable
{
    private SocketIOUnity _socket;
    private event Action<MultiPlayMangerState , string> _onMultiplayStateChanged;
    public Action onReceiveMessage;
    public Action<Movedata> onOpponentMove;
    public Action doOppoonentMove;
    public MultiplayManager(Action<MultiPlayMangerState , string> onMultiPlayStateChage)
    {
        _onMultiplayStateChanged = onMultiPlayStateChage;

        var uri = new Uri(Constans.SocketURL);
        _socket = new SocketIOUnity(uri,new SocketIOOptions
        {
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });

        _socket.On("createRoom",CreateRoom);
        _socket.On("joinRoom",JoinRoom);
        _socket.On("startGame",StartGame);
        _socket.On("exitRoom",ExitRoom);
        _socket.On("endGame",EndGame);
        _socket.On("doOpponent",DoOpponent);
        

        //서버 접속
        _socket.Connect();
    }

    #region 서버에서 이벤트에 대한 처리 함수

    //클라이언트가 서버에 접속 했더니 아무도 없어서 방을 새롭게 만들었을 때 서버가 호출해 주는 함수
    private void CreateRoom(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
        _onMultiplayStateChanged?.Invoke(MultiPlayMangerState.CreateRoom,data.roomId);
    }

     //클라이언트가 서버에 접속했더니 대기 중인 방이 있어서 그 방에 참가 했을 때 서버가 호출해주는 함수

    private void JoinRoom(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
        _onMultiplayStateChanged?.Invoke(MultiPlayMangerState.JoinRoom, data.roomId);
    }

    //방에 참가한 유저가 게임을 시작할 때 서버가 호출해 주는 함수
    private void StartGame(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
        _onMultiplayStateChanged?.Invoke(MultiPlayMangerState.StartGame,data.roomId);
    }

    // 방에 참가한 유저가 방을 나갔을 때 서버가 호출해 주는 합수
    private void ExitRoom(SocketIOResponse response)
    {
        _onMultiplayStateChanged?.Invoke(MultiPlayMangerState.ExitRoom,null);
    }

    // 방에 참가한 유저가 접속을 끊었을 때 서버가 호출해주는 함수
    private void EndGame(SocketIOResponse response)
    {
        _onMultiplayStateChanged?.Invoke(MultiPlayMangerState.EndGame,null);
    }

    private void DoOpponent(SocketIOResponse response)
    {
        var data = response.GetValue<Movedata>();
        onOpponentMove?.Invoke(data);
    }

    #endregion

    #region  서버로 이벤트를 보내는 함수
    //플레이어가 마커를 놓았을 때 서버에 이동 정보 전송

    public void SendPlayerMove(string roomId,int position)
    {
        _socket.Emit("doPlayer", new { roomId, position });
    }

    // 클라이언트가 방을 나갈 때 호출하는 함수
    public void LeaveRoom(string roomId)
    {
        _socket.Emit("leaveRoom", new { roomId });
    }
    #endregion

    public void Dispose()
    {
        if(_socket != null)
        {
            _socket.Disconnect();
            _socket.Dispose();
        }
    }
}


