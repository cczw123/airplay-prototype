using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DodgeBallGameManager : MonoBehaviour
{
    public static DodgeBallGameManager Inst;
    public int[] scores = new int[2];
    private int playeTeamMatch = 0;
    private Coroutine currentOpeningCoroutine;

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
        }
        else
        {
            Destroy(this);
        }
    }

    [Tooltip("The prefab to use for initiating the player")]
    public GameObject teamOnePrefab;
    public GameObject teamTwoPrefab;

    //#region Photon Callbacks

    //public override void OnLeftRoom()
    //{
    //    SceneManager.LoadScene(0);
    //}

    //#endregion


    #region Public Methods

    //public void LeaveRoom()
    //{
    //    PhotonNetwork.LeaveRoom();
    //}

    public IEnumerator StartGameCoroutine(GameObject hockey = null)
    {
        AudioManager.Instance.PlayOpeningAudio(Vector3.zero);
        if (hockey)
        {
            yield return new WaitForSeconds(0.5f);
            hockey.transform.position = Vector3.zero;
            hockey.SetActive(true);
        }
    }

    public void StartGame(GameObject hockey = null)
    {
        if (currentOpeningCoroutine != null)
        {
            StopCoroutine(currentOpeningCoroutine);
        }

        currentOpeningCoroutine = StartCoroutine(StartGameCoroutine(hockey));
    }

    #endregion

    //#region Private Methods

    //void LoadArena()
    //{
    //    if (!PhotonNetwork.IsMasterClient)
    //    {
    //        Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
    //    }

    //    Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
    //    PhotonNetwork.LoadLevel("DodgeBall");
    //}

    //#endregion

    #region Photon Callbacks

    private void Start()
    {
        if (teamOnePrefab == null || teamTwoPrefab == null)
        {
            Debug.LogError(
                "<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",
                this
            );
            return;
        }

        //Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);
        //// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
        //if (PlayerManager.LocalPlayerInstance == null && playeTeamMatch == 0)
        //{
        //    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
        //    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
        //    PhotonNetwork.Instantiate(teamOnePrefab.name, new Vector3(Random.Range(-7f, 7f), Random.Range(-3f, 3f), 0f),
        //        Quaternion.identity, 0);
        //    playeTeamMatch = 1 - playeTeamMatch;
        //    StartGame();
        //}else if (PlayerManager.LocalPlayerInstance == null && playeTeamMatch == 1)
        //{
        //    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
        //    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
        //    PhotonNetwork.Instantiate(teamTwoPrefab.name, new Vector3(Random.Range(-7f, 7f), Random.Range(-3f, 3f), 0f),
        //        Quaternion.identity, 0);
        //    playeTeamMatch = 1 - playeTeamMatch;
        //    StartGame();
        //}
        //else
        //{
        //    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
        //}
    }

    //public override void OnPlayerEnteredRoom(Player other)
    //{
    //    Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}",
    //            PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


    //        LoadArena();
    //    }
    //}


    //public override void OnPlayerLeftRoom(Player other)
    //{
    //    Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}",
    //            PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


    //        LoadArena();
    //    }
    //}

    #endregion
}