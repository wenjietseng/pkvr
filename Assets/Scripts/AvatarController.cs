using Klak.Motion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public enum AvatarMovements { Sync = 0, Delayed = 1, BrownianMotion = 2, Prerecorded = 3};
    public AvatarMovements avatarMovements;
    public static bool switchAvatarMovement;
    public Material selectedBlue;
    public Material unselectedWhite;
    public GameObject syncBtn;
    public GameObject delayedBtn;
    public GameObject bmBtn;
    public GameObject prerecordedBtn;
    public GameObject syncAvatar;
    public GameObject asyncAvatar;

    [Header("Delayed")]
    public GameObject trackingAvatar;
    public GameObject delayedAvatar;
    Transform[] allChildrenTrackedAvatar;
    Transform[] allChildrenDelayedAvatar;
    SkinnedMeshRenderer[] syncAvatarSMRs;
    [Range(0.0f, 3f)]
    public float delayedTime = 0.5f; 

    [Header("Random Movement")]
    public bool isAsyncAvatar;
    bool areBonesCreated;
    public GameObject bmEffect;

    void Start()
    {
        bmEffect.GetComponent<BrownianMotion>().enabled = false;
        allChildrenTrackedAvatar = trackingAvatar.GetComponentsInChildren<Transform>();
        allChildrenDelayedAvatar = delayedAvatar.GetComponentsInChildren<Transform>();
        syncAvatarSMRs = syncAvatar.GetComponentsInChildren<SkinnedMeshRenderer>();
        asyncAvatar.SetActive(false);

    }


    void Update()
    {
        if (switchAvatarMovement)
        {
            switchAvatarMovement = false;
            if (avatarMovements == AvatarMovements.Sync)
            {
                syncBtn.GetComponent<Renderer>().material = selectedBlue;
                delayedBtn.GetComponent<Renderer>().material = unselectedWhite;
                bmBtn.GetComponent<Renderer>().material = unselectedWhite;
                prerecordedBtn.GetComponent<Renderer>().material = unselectedWhite;

                asyncAvatar.SetActive(false);

                foreach (var t in syncAvatarSMRs) t.enabled = true;
            }
            else if (avatarMovements == AvatarMovements.Delayed)
            {

                syncBtn.GetComponent<Renderer>().material = unselectedWhite;
                delayedBtn.GetComponent<Renderer>().material = selectedBlue;
                bmBtn.GetComponent<Renderer>().material = unselectedWhite;
                prerecordedBtn.GetComponent<Renderer>().material = unselectedWhite;
                asyncAvatar.SetActive(true);
                foreach (var t in syncAvatarSMRs) t.enabled = false;


            }
            else if (avatarMovements == AvatarMovements.BrownianMotion)
            {

                syncBtn.GetComponent<Renderer>().material = unselectedWhite;
                delayedBtn.GetComponent<Renderer>().material = unselectedWhite;
                bmBtn.GetComponent<Renderer>().material = selectedBlue;
                prerecordedBtn.GetComponent<Renderer>().material = unselectedWhite;
                asyncAvatar.SetActive(false);
                foreach (var t in syncAvatarSMRs) t.enabled = true;
            }
            else if (avatarMovements == AvatarMovements.Prerecorded)
            {

                syncBtn.GetComponent<Renderer>().material = unselectedWhite;
                delayedBtn.GetComponent<Renderer>().material = unselectedWhite;
                bmBtn.GetComponent<Renderer>().material = unselectedWhite;
                prerecordedBtn.GetComponent<Renderer>().material = selectedBlue;
                asyncAvatar.SetActive(false);
                foreach (var t in syncAvatarSMRs) t.enabled = true;
            }
        }

        /// <summary>
        /// below is for brownian movement, need to tweak the values and polish code
        /// </summary>
        //if (!areBonesCreated)
        //{
        //    GameObject bonesGO = GameObject.Find("Bones");
        //    bmEffect.transform.SetParent(bonesGO.transform);
        //    GameObject.Find("FullBody_LeftHandPalm").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandWrist").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandThumbMetacarpal").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandThumbProximal").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandThumbDistal").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandThumbTip").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandIndexMetacarpal").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandIndexProximal").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandIndexIntermediate").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandIndexDistal").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandIndexTip").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandMiddleMetacarpal").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandMiddleProximal").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandMiddleIntermediate").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandMiddleDistal").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandMiddleTip").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandRingMetacarpal").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandRingProximal").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandRingIntermediate").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandRingDistal").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandRingTip").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandLittleMetacarpal").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandLittleProximal").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandLittleIntermediate").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandLittleDistal").transform.SetParent(bmEffect.transform);
        //    GameObject.Find("FullBody_LeftHandLittleTip").transform.SetParent(bmEffect.transform);
        //    Debug.LogWarning("Bones are found");
        //    areBonesCreated = true;
        //}
    }

    private void LateUpdate()
    {
        if (avatarMovements == AvatarMovements.Delayed) StartCoroutine(DelayedAvatar(delayedTime));
    }

    private IEnumerator DelayedAvatar(float _duration=0f)
    {

        List<Vector3> thisFramePos = new List<Vector3>();
        List<Quaternion> thisFrameRot = new List<Quaternion>();
        foreach (var t in allChildrenTrackedAvatar)
        {
            thisFramePos.Add(t.position);
            thisFrameRot.Add(t.rotation);
        }

        yield return new WaitForSeconds(_duration);

        for (int i = 0; i < allChildrenTrackedAvatar.Length; i++)
        {
            allChildrenDelayedAvatar[i].position = thisFramePos[i];
            allChildrenDelayedAvatar[i].rotation = thisFrameRot[i];
            //allChildrenDelayedAvatar[i].localScale = allChildrenTrackedAvatar[i].localScale;
        }

        yield break;
    }
}
