using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ItemObservation : MonoBehaviour
{
    Player player;

    [SerializeField] float itemMaxDistance;
    [SerializeField] LayerMask itemLayer;
    [SerializeField] float observationDistance; // how far from camera object will be when observing

    ObserableItem obserableItem;
    Vector3 itemOriginalPosition;
    Quaternion itemOriginalRotation;

    private void Awake()
    {
        if (player == null) player = GetComponent<Player>();

        player.Input.OnInteractPress += OnInteractionStart;
        player.Input.OnInteractRelease += OnInteractionEnd;
    }

    private void OnDisable() 
    {
        player.Input.OnInteractPress -= OnInteractionStart;
        player.Input.OnInteractRelease -= OnInteractionEnd;
    }

    private void Update()
    {
        if (obserableItem != null)
        {

        }
    }


    private void OnInteractionStart()
    {
        if (obserableItem == null)
        {
            Ray ray = player.Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, itemMaxDistance, itemLayer))
            {
                if (hit.collider.TryGetComponent<ObserableItem>(out ObserableItem item))
                {
                    obserableItem = item;
                    itemOriginalPosition = obserableItem.transform.position;
                    itemOriginalRotation = obserableItem.transform.rotation;

                    obserableItem.transform.position = PositionInFrontOfCamera;
                    obserableItem.UseGravity(false);
                }
                //else Debug.LogError("No item hit but raycast hit something");
            }
            //else Debug.LogError("Raycast miss");
        }
    }

    private void OnInteractionEnd()
    {
        if (obserableItem != null)
        {
            obserableItem.transform.position = itemOriginalPosition;
            obserableItem.transform.rotation = itemOriginalRotation;
            obserableItem.UseGravity(true);
            obserableItem = null;
        }
    }

    private Vector3 PositionInFrontOfCamera
    {
        get
        {
            Vector3 position = player.Camera.transform.position + (player.Camera.transform.forward * observationDistance);
            return position;
        }
    }

    public bool IsObserving => obserableItem != null;
}
