using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ItemObservation : MonoBehaviour
{
    Player player;

    [SerializeField] float itemMaxDistance;
    [SerializeField] LayerMask itemLayer;
    [SerializeField] float observationDistance; // how far from camera object will be when observing
    [SerializeField] float rotationRate = 1;

    ObserableItem obserableItem;
    /*Vector3 itemOriginalPosition;
    Quaternion itemOriginalRotation;*/

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
            Vector3 rotation = obserableItem.transform.eulerAngles;

            rotation.x += player.Input.Look.y * rotationRate * Time.deltaTime;
            rotation.y += player.Input.Look.x * rotationRate * Time.deltaTime;

            obserableItem.transform.eulerAngles = rotation;
        }
    }


    private void OnInteractionStart()
    {
        if (obserableItem != null) return;

        Ray ray = player.Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, itemMaxDistance, itemLayer) &&
            hit.collider.TryGetComponent<ObserableItem>(out ObserableItem item))
        {
            obserableItem = item;
            obserableItem.PickUp(PositionInFrontOfCamera);
        }
    }

    private void OnInteractionEnd()
    {
        if (obserableItem != null)
        {
            obserableItem.LetGo();
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
