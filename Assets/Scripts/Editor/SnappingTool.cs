using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

/// <summary>
/// Custom snap tool by Emerald Powder
/// https://www.youtube.com/@EmeraldPowder
/// </summary>

[EditorTool("Custom snap tool", typeof(CustomSnap))]
public class SnappingTool : EditorTool
{
    public Texture2D toolIcon;
    public float snapDistance = 0.5f;

    private Transform oldTarget;

    CustomSnapPoint[] allPoints;
    CustomSnapPoint[] targetPoinst;

    public override GUIContent toolbarIcon
    {
        get {
            return new GUIContent
            (
                "Custom Snap Tool",
                toolIcon,
                "Snapping objects with Snap Points"
            );
            }
    }

    public override void OnToolGUI(EditorWindow window)
    {
        Transform targetTransform = ((CustomSnap) target).transform;

        if(targetTransform != oldTarget)
        {
            allPoints = FindObjectsOfType<CustomSnapPoint>();
            targetPoinst = targetTransform.GetComponentsInChildren<CustomSnapPoint>();

            oldTarget = targetTransform;
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newPosition = Handles.PositionHandle(targetTransform.position, Quaternion.identity);

        if(EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(targetTransform, "Move with snap tool");
            targetTransform.position = newPosition;
            MoveWithSnapping(targetTransform, newPosition);
        }
    }

    private void MoveWithSnapping(Transform targetTransform, Vector3 newPosition)
    {
        Vector3 bestPosition = newPosition;
        Quaternion bestRotation = Quaternion.identity;
        float closestDistance = float.PositiveInfinity;

        foreach(CustomSnapPoint point in allPoints)
        {
            if (point.transform.parent == targetTransform) continue;

            foreach(CustomSnapPoint ownPoint in targetPoinst)
            {
                if(ownPoint.connectionType != point.connectionType) continue;

                Vector3 targetPos = point.transform.position - (ownPoint.transform.position - targetTransform.position);
                Quaternion targetRotation = point.transform.parent.rotation;
                float distance = Vector3.Distance(targetPos, newPosition);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    bestPosition = targetPos;
                    bestRotation = targetRotation;
                }
            }   
        }

        if (closestDistance < snapDistance)
        {
            targetTransform.position = bestPosition;
           // targetTransform.rotation = bestRotation;
        }
        else
        {
            targetTransform.position = newPosition;
            Debug.Log("Nah");
        }
    }
}
