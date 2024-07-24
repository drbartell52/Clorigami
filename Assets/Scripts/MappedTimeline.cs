using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class MappedTimeline : MonoBehaviour
{
    //director component reference
    private PlayableDirector director;
    
    //temporary scrubbing variable for timeline
    [Range(0,1)]
    public float scrub;

    public Transform dialPos;
    public float dialZero;

    public Transform rightHandFirstFold;
    public Transform leftHandFirstFold;
    
    public Transform rightHandSecondFold;
    public Transform leftHandSecondFold;
    
    public Transform rightHandThirdFold;
    public Transform leftHandThirdFold;
    
    public float firstFoldLeftZero;
    public float firstFoldRightZero;
    
    


    // Start is called before the first frame update
    void Start()
    {
        //assigns attached PlayableDirector component
        director = GetComponent<PlayableDirector>();
        
        //necessary for director scrubbing
        director.RebuildGraph();
        director.playableGraph.SetTimeUpdateMode(DirectorUpdateMode.Manual);

        //assigns the default x position of the dial to associated variable
        dialZero = dialPos.position.x;

        firstFoldLeftZero = leftHandFirstFold.position.x;
        firstFoldRightZero = rightHandFirstFold.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        var dialDiff = dialPos.position.x - dialZero;
        //var dialDiff = Mathf.Clamp01(dialPos.position.x - dialZero);

        var firstLeftDif = leftHandFirstFold.position.x - firstFoldLeftZero;
        var firstRightDif = rightHandFirstFold.position.x - firstFoldRightZero;
        
        //director.time = Mathf.Clamp01(scrub) * director.duration;
        director.time = Mathf.Clamp01(firstLeftDif) * director.duration;
        director.time = Mathf.Clamp01(firstRightDif) * director.duration;
        director.DeferredEvaluate();
    }
}
