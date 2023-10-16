using Cognitive3D;
using UnityEngine;
using VRBuilder.Core.Properties;

namespace VRBuilder.Cognitive3DIntegration.Properties
{
    /// <summary>
    /// Basic implementation of <see cref="IDynamicObjectProperty"/>.
    /// </summary>
    [RequireComponent(typeof(DynamicObject))]
    public class DynamicObjectProperty : ProcessSceneObjectProperty, IDynamicObjectProperty
    {
        private DynamicObject danymicObject;

        /// <summary>
        /// The dynamic object component on this game object.
        /// </summary>
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
}
