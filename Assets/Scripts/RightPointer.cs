using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightPointer : MonoBehaviour
{
    public OVRHand rightHand;
    public GameObject syncAvatar;
    Transform indexTip;
    public AvatarController avatarController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (indexTip == null) indexTip = syncAvatar.transform.FindChildRecursive("FullBody_RightHandIndexTip").transform;
        transform.position = indexTip.position;

        if (rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "DelayedHandle")
        {
            other.GetComponent<Image>().color = Color.blue;
        }


    }


    private void OnTriggerExit(Collider other)
    {
        if (other.name == "DelayedHandle")
        {
            other.GetComponent<Image>().color = Color.white;
        }

    }

}
