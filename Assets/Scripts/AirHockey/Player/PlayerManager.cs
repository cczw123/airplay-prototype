using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using System.Collections;

/// <summary>
/// Player manager.
/// Handles fire Input and Beams.
/// </summary>
public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            // stream.SendNext(team1);
            // stream.SendNext(team2);
            stream.SendNext(GameManager.Inst.scores);
        }
        else
        {
            // Network player, receive data
            // team1 = (int) stream.ReceiveNext();
            // team2 = (int) stream.ReceiveNext();
            GameManager.Inst.scores = (int[]) stream.ReceiveNext();
        }
    }

    #endregion

    #region Public Fields

    public int team1;
    public int team2;

    #endregion

    //update team1 and team2

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    #region MonoBehaviour CallBacks

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    void Awake()
    {
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
            GetComponent<SpriteRenderer>().color = Color.cyan;
        }

        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    #region Custom

    #endregion
}