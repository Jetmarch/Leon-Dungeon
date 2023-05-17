using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSnapPoint : MonoBehaviour
{
    public enum ConnectionType
    {
        Type1,
        Type2,
        Type3
    }

    public ConnectionType connectionType;

    private void OnDrawGizmos()
    {
        switch (connectionType)
        {
            case ConnectionType.Type1:
                Gizmos.color = Color.green;
                break;
            case ConnectionType.Type2:
                Gizmos.color= Color.blue;
                break;
            case ConnectionType.Type3:
                Gizmos.color = Color.red;
                break;
        }
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
