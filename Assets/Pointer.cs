using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public GameObject syncAvatar;
    Transform indexTip;
    public AvatarController avatarController;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (indexTip == null) indexTip = syncAvatar.transform.FindChildRecursive("FullBody_LeftHandIndexTip").transform;
        transform.position = indexTip.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AvatarControlUI")
        {
            if (other.name == "Sync")
            {
                avatarController.avatarMovements = AvatarController.AvatarMovements.Sync;
            }
            else if (other.name == "Delayed")
            {
                avatarController.avatarMovements = AvatarController.AvatarMovements.Delayed;
            }
            else if (other.name == "BrownianMotion")
            {
                avatarController.avatarMovements = AvatarController.AvatarMovements.BrownianMotion;
            }
            else if (other.name == "Prerecorded")
            {
                avatarController.avatarMovements = AvatarController.AvatarMovements.Prerecorded;
            }
            AvatarController.switchAvatarMovement = true;
        }
    }
}
