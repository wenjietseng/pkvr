using Klak.Motion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public bool isAsyncAvatar;
    bool areBonesCreated;
    public GameObject bmEffect;

    
    void Start()
    {
        bmEffect.GetComponent<BrownianMotion>().enabled = false;


    }


    void Update()
    {
        if (!areBonesCreated)
        {
            GameObject bonesGO = GameObject.Find("Bones");
            bmEffect.transform.SetParent(bonesGO.transform);
            GameObject.Find("FullBody_LeftHandPalm").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandWrist").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandThumbMetacarpal").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandThumbProximal").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandThumbDistal").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandThumbTip").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandIndexMetacarpal").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandIndexProximal").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandIndexIntermediate").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandIndexDistal").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandIndexTip").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandMiddleMetacarpal").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandMiddleProximal").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandMiddleIntermediate").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandMiddleDistal").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandMiddleTip").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandRingMetacarpal").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandRingProximal").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandRingIntermediate").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandRingDistal").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandRingTip").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandLittleMetacarpal").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandLittleProximal").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandLittleIntermediate").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandLittleDistal").transform.SetParent(bmEffect.transform);
            GameObject.Find("FullBody_LeftHandLittleTip").transform.SetParent(bmEffect.transform);
            Debug.LogWarning("Bones are found");
            areBonesCreated = true;
        }
        //bool isIndexFingerPinching = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        //Debug.LogWarning(isIndexFingerPinching);
        //if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch) || rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index) );
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAsyncAvatar = isAsyncAvatar ? false : true;
            bmEffect.GetComponent<BrownianMotion>().enabled = isAsyncAvatar;
            if (isAsyncAvatar) Debug.LogWarning("Enable async avatar");
            else Debug.LogWarning("Enable sync avatar");
        }

    }
}
