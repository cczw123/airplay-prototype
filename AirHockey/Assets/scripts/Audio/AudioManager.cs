using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    // Start is called before the first frame update
    public List<AudioClip> goalAudioList = new List<AudioClip>();
    public List<AudioClip> jubilianceAudioList = new List<AudioClip>();
    public List<AudioClip> boundaryAudioList = new List<AudioClip>();
    public List<AudioClip> kickAudioList = new List<AudioClip>();
    public List<AudioClip> openingAudioList = new List<AudioClip>();

    private enum AudioCondition
    {
        GOAL = 0,
        JUBILIANCE = 1,
        BOUNDARY = 2,
        KICK = 3,
        OPENING = 4
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayGoalAudio(Vector3 pos)
    {
        PlayAudio(AudioCondition.GOAL, pos);
    }

    public void PlayBoundaryAudio(Vector3 pos)
    {
        PlayAudio(AudioCondition.BOUNDARY, pos);
    }

    public void PlayKickAudio(Vector3 pos)
    {
        PlayAudio(AudioCondition.KICK, pos);
    }

    public void PlayJubilianceAudio(Vector3 pos)
    {
        PlayAudio(AudioCondition.JUBILIANCE, pos);
    }

    public void PlayOpeningAudio(Vector3 pos)
    {
        PlayAudio(AudioCondition.OPENING, pos);
    }

    private void PlayAudio(AudioCondition condition, Vector3 position)
    {
        List<AudioClip> playList;
        switch (condition)
        {
            case AudioCondition.BOUNDARY:
            default:
                playList = boundaryAudioList;
                break;
            case AudioCondition.GOAL:
                playList = goalAudioList;
                break;
            case AudioCondition.JUBILIANCE:
                playList = jubilianceAudioList;
                break;
            case AudioCondition.KICK:
                playList = kickAudioList;
                break;
            case AudioCondition.OPENING:
                playList = openingAudioList;
                break;
        }

        if (playList == null || playList.Count == 0)
        {
            return;
        }

        int choice = Random.Range(0, playList.Count);
        AudioSource.PlayClipAtPoint(playList[choice], position);
    }
}