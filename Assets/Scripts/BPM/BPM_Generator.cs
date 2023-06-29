using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM_Generator : MonoBehaviour
{
    enum BeatAccuracy {OFF_BEAT, CLOSE, ON_BEAT};
    SpriteRenderer sprite;
    float counter = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        sprite = transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        /*switch(GetAccuracy())
        {
            case BeatAccuracy.ON_BEAT:
                sprite.color = Color.green;
                break;
            case BeatAccuracy.CLOSE:
                sprite.color = Color.yellow;
                break;
            case BeatAccuracy.OFF_BEAT:
                sprite.color = Color.red;
                break;
        }*/
    }

    public void blink(BeatState state)
    {
        switch(state){
            case BeatState.ON:
                sprite.color = Color.green;
                break;
            case BeatState.OFF:
                sprite.color = Color.red;
                break;
            case BeatState.CLOSE:
                sprite.color = Color.yellow;
                break;
        }
    }

    BeatAccuracy GetAccuracy()
    {
        float fract = Mathf.Abs(counter - Mathf.Round(counter));
        return fract switch 
        {
            < 0.05f => BeatAccuracy.ON_BEAT,
            < 0.1f => BeatAccuracy.CLOSE,
            _ => BeatAccuracy.OFF_BEAT
        };
    }
}
