using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class HubNetWork : MonoBehaviour
{
    [SerializeField] Button joinRoomButton;
    [SerializeField] Button leaveRoomButton;
    private void Start()
    {
       leaveRoomButton.gameObject.SetActive(false);
        joinRoomButton.gameObject.SetActive(false);
        
        if(PhotonNetwork.IsConnected)
        {
            Debug.Log("Connected to Photon Network");
        }
        else
        {
            Debug.Log("Not connected to Photon Network");
        }
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server");
        PhotonNetwork.JoinLobby();
        joinRoomButton.gameObject.SetActive(true);
        leaveRoomButton.gameObject.SetActive(true);
    }
    public override void OnjoinedLobby()
    {
        Debug.Log("Joined Lobby");
        joinRoomButton.gameObject.SetActive(true);
     
    }
    public override void joinRoom()
    {
        Debug.Log("Joining Room...");
        PhotonNetwork.JoinRandomRoom();
        joinRoomButton.gameObject.SetActive(true);
        leaveRoomButton.gameObject.SetActive(false);
    }
    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            leaveRoomButton.gameObject.SetActive(false);
            joinRoomButton.gameObject.SetActive(true);
            Debug.Log("Leaving room...");
        }
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
            Debug.Log("Leaving lobby...");
            }
        }
    }
