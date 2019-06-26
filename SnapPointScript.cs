using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPointScript : MonoBehaviour
{
    [SerializeField] private string snapPointTag;   //SNAP POINT TAG
    private bool containsBlock = false;

    void OnTriggerEnter(Collider collider)
    {
        BuildingBlockScript buildingBlockScript = collider.transform.GetComponent<BuildingBlockScript>();
        if (!containsBlock && buildingBlockScript)
        {
            foreach(string tag in buildingBlockScript.GetSnappablePoints())
            {
                if(tag == snapPointTag)
                {
                    if (buildingBlockScript.Snap(this.transform))
                    {
                        BuildingSystemScript.instance.PutInPauseMode();
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        BuildingBlockScript buildingBlockScript = collider.transform.GetComponent<BuildingBlockScript>();
        if (!containsBlock && buildingBlockScript)
        {
            foreach (string tag in buildingBlockScript.GetSnappablePoints())
            {
                if (tag == snapPointTag)
                {
                    buildingBlockScript.UnSnap(this.transform);
                }
            }
        }
    }

    //WHEN A BLOCK IS PLACED IN THE SNAPPOINT
    public void AttachBlockHere()
    {
        containsBlock = true;
    }

    //WHEN A BLOCK IS REMOVED FROM THE SNAP POINT
    public void UnattachBlockHere()
    {
        containsBlock = false;
    }

}
