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

    private enum AudioCondition
    {
        GOAL = 0,
        JUBILIANCE = 1,
        BOUNDARY = 2,
        KICK = 3
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

    public void PlayGoalAudio(Transform goalTransform)
    {
        PlayAudio(AudioCondition.GOAL, goalTransform.position);
    }

    public void PlayBoundaryAudio(Transform goalTransform)
    {
        PlayAudio(AudioCondition.BOUNDARY, goalTransform.position);
    }

    public void PlayKickAudio(Transform targetTransform)
    {
        PlayAudio(AudioCondition.KICK, targetTransform.position);
    }

    public void PlayJubilianceAudio(Transform goalTransform)
    {
        PlayAudio(AudioCondition.JUBILIANCE, goalTransform.position);
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
        }

        if (playList == null || playList.Count == 0)
        {
            return;
        }

        int choice = Random.Range(0, playList.Count);
        AudioSource.PlayClipAtPoint(playList[choice], position);
    }
}