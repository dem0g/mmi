using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum BeatState {CLOSE, OFF, ON};

public class BeatManager : MonoBehaviour
{
    [SerializeField] private float _bpm;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Interval[] _intervals;
    [SerializeField] private Transform noteSpawn;
    [SerializeField] private Transform referenceSpawn;
    [SerializeField] private GameObject note;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach ( Interval interval in _intervals ) 
        {
            float sampleTime = (_audioSource.timeSamples) / (_audioSource.clip.frequency * interval.GetBeatLength(_bpm));
            interval.CheckForNewInterval(sampleTime);
        }

        foreach ( Transform t in transform )
        {
            float duration = ( 60f / _bpm ) * 8f;
            float speed = 10.0f / duration;
            t.transform.position -= 
                Vector3.right * ( speed * Time.deltaTime );
        }
    }

    public void Spawn(BeatState state)
    {
        if(state != BeatState.ON) return;

        GameObject newNote = Instantiate(
            note, noteSpawn.position, Quaternion.identity
        );

        GameObject refNote = Instantiate(
            note, referenceSpawn.position, Quaternion.identity
        );
        newNote.transform.parent = transform;
        refNote.transform.parent = transform;
    }
}

[System.Serializable]
public class Interval {

    [SerializeField] private float _steps;
    [SerializeField] private UnityEvent<BeatState> _trigger;
    private BeatState currentState = BeatState.OFF;
    private int _lastInterval;

    public float GetBeatLength(float bpm) {
        return 60f / (bpm * _steps);
    }

    public void CheckForNewInterval (float interval) {

        float fract = Mathf.Abs(interval - Mathf.Round(interval));
        BeatState nextState = fract switch 
        {
            < 0.05f => BeatState.ON,
            < 0.1f => BeatState.CLOSE,
            _ => BeatState.OFF
        };
        if( nextState != currentState )
        {
            currentState = nextState;
            _trigger.Invoke(currentState);
        }
    }
}