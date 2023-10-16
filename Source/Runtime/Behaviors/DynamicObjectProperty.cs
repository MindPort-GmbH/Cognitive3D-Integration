using Cognitive3D;
using UnityEngine;
using VRBuilder.Core.Properties;

[RequireComponent(typeof(DynamicObject))]
public class DynamicObjectProperty : ProcessSceneObjectProperty, IDynamicObjectProperty
{
    private DynamicObject danymicObject;

    public DynamicObject DynamicObject
    {
        get
        {
            if (danymicObject == null)
            {
                danymicObject = GetComponent<DynamicObject>();
            }

            return danymicObject;
        }
    }

}
