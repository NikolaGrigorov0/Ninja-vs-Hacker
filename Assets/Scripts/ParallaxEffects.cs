using UnityEngine;
using UnityEngine.UI;

public class ParallaxEffects : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    Vector2 startingPosition;

    float startingZ;

    Vector2 camMoveSinceStart => (Vector2) cam.transform.position - startingPosition;

    float zDistanceFromTarget => followTarget != null ? transform.position.z - followTarget.transform.position.z : 0f;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    void Update()
    {
        if (followTarget == null || cam == null) return;

        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ );
    }
}
