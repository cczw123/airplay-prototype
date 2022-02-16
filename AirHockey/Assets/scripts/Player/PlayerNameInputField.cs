using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;


[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    public const string defaultName = "NoName";

    #region Private Constants

    // Store the PlayerPref Key to avoid typos
    const string playerNamePrefKey = "PlayerName";

    #endregion


    #region MonoBehaviour CallBacks

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// </summary>
    void Start()
    {
        InputField inputField = GetComponent<InputField>();
        string playerName = defaultName;
        if (inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                playerName = PlayerPrefs.GetString(playerNamePrefKey);
                inputField.text = playerName;
            }
        }

        PhotonNetwork.NickName = playerName;
    }

    #endregion


    #region Public Methods

    /// <summary>
    /// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
    /// </summary>
    /// <param name="value">The name of the Player</param>
    public void SetPlayerName(string value)
    {
        // #Important
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }

        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNamePrefKey, value);
    }

    #endregion
}