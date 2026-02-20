using UnityEngine;

public class BossSpawnPointVisualizer : MonoBehaviour
{
    public Color gizmoColor = Color.yellow;
    public float gizmoRadius = 0.5f;

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);
        Gizmos.DrawLine(transform.position + Vector3.up * gizmoRadius, transform.position - Vector3.up * gizmoRadius);
        Gizmos.DrawLine(transform.position + Vector3.right * gizmoRadius, transform.position - Vector3.right * gizmoRadius);
    }
}
