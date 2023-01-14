using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShootProjectile))]
public class ShootProjectileEditor : Editor
{
    [DrawGizmo(GizmoType.Pickable | GizmoType.Selected)]
    static void DrawGizmosSelected(ShootProjectile cannon, GizmoType gizmoType) {
        float dashSize = 4.0f;

        // draw starting point
        var offsetPosition = cannon.Offset.position;
        Handles.DrawDottedLine(cannon.transform.position, offsetPosition, dashSize);
        Handles.Label(offsetPosition, "Offset");

        // ensure that we have a projectile prefab
        if (cannon.ProjectilePrefab == null) return;

        // generate a list of projected positions for the cannonball
        var velocity = cannon.transform.forward * cannon.LaunchSpeed;
        var position = offsetPosition;
        var positions = new List<Vector3>();
        var physicsStep = 0.1f;
        for (var i = 0f; i <= cannon.ProjectedLength; i += physicsStep) {
            positions.Add(position);
            position += velocity * physicsStep;
            velocity += Physics.gravity * physicsStep;
        }

        using (new Handles.DrawingScope(Color.yellow)) {
            // draw a line along that trajectory
            Handles.DrawAAPolyLine(positions.ToArray());

            // draw a small sphere at the end position
            Gizmos.DrawWireSphere(positions[positions.Count - 1], 0.125f);

            // label for the endpoint
            Handles.Label(positions[positions.Count - 1], "Estimated Position (1s)");
        }
    }

    private void OnSceneGUI() {
        var launcher = target as ShootProjectile; 
        var transform = launcher.transform;

        using (var cc = new EditorGUI.ChangeCheckScope()) {
            var newOffset = Handles.PositionHandle(
                    launcher.Offset.position,
                    transform.rotation);

            if (cc.changed) {
                Undo.RecordObject(launcher.Offset, "Offset Change");
                launcher.Offset.position = newOffset;

            }
        }

        Handles.BeginGUI();
        var rectMin = Camera.current.WorldToScreenPoint(launcher.transform.position + launcher.Offset.position);
        var rect = new Rect();
        rect.xMin = rectMin.x;
        rect.yMin = SceneView.currentDrawingSceneView.position.height - rectMin.y;
        rect.width = 64;
        rect.height = 18;
        GUILayout.BeginArea(rect);
        using (new EditorGUI.DisabledGroupScope(!Application.isPlaying)) {
            if (GUILayout.Button("Fire")) {
                launcher.Fire();
            }
        }
        GUILayout.EndArea();
        Handles.EndGUI();
    }
}
