using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlockScript : MonoBehaviour
{
    [SerializeField] private Collider[] snapPoints;        //CHILDREN SNAP POINTS ATTACHED TO THIS BLOCK 
    [SerializeField] private List<string> snappablePoints;     //SNAPPABLE POINTS THIS BLOCK CAN SNAP TO
    [SerializeField] private int numberOfSnapPointsToStick = 1;
    [Space]
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material canplaceMaterial;
    [SerializeField] Material cannotPlaceMaterial;
    [Space]
    [SerializeField] private bool isAlreadyPlaced = false;

    private MeshRenderer meshRenderer;
    private Transform pointSnappedTo;

    private int currentNumberOfSnapPoints = 0;
    private bool canBePlaced = false;

    private void Start()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
        UpdateMesh();
    }

    public List<string> GetSnappablePoints()
    {
        return snappablePoints;
    }

    public void TurnOnSnapPoints()
    {
        for (int i = 0; i < snapPoints.Length; ++i)
        {
            snapPoints[i].transform.gameObject.SetActive(true);
        }
    }

    public void TurnOffSnapPoints()
    {
        for (int i = 0; i < snapPoints.Length; ++i)
        {
            snapPoints[i].transform.gameObject.SetActive(false);
        }
    }

    public bool Place()
    {
        if (canBePlaced)
        {
            isAlreadyPlaced = true;
            pointSnappedTo.GetComponent<SnapPointScript>().AttachBlockHere();
            meshRenderer.material = defaultMaterial;
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public bool Snap(Transform snapLocation)
    {
        currentNumberOfSnapPoints++;
        if (!isAlreadyPlaced && currentNumberOfSnapPoints >= numberOfSnapPointsToStick) {
            this.transform.position = snapLocation.position;
            this.transform.rotation = snapLocation.rotation;
            Debug.Log("snapppoints: " + currentNumberOfSnapPoints);
            pointSnappedTo = snapLocation;
            canBePlaced = true;
            UpdateMesh();
            return true;
        }
        return false;
    }

    public void UnSnap(Transform snapLocation = null)
    {
        currentNumberOfSnapPoints = 0;
        canBePlaced = false;
        snapLocation = null;
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        if (canBePlaced)
        {
            meshRenderer.material = canplaceMaterial;
        }
        else
        {
            meshRenderer.material = cannotPlaceMaterial;
        }
    }

}
