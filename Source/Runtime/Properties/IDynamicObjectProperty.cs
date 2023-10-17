using Cognitive3D;
using VRBuilder.Core.Properties;

namespace VRBuilder.Cognitive3DIntegration.Properties
{
    /// <summary>
    /// Property for a game object that will be tracked by Cognitive3D <see cref="Cognitive3D.DynamicObject"/>.
    /// </summary>
    public interface IDynamicObjectProperty : ISceneObjectProperty
    {
        /// <summary>
        /// The DynamicObject controlled by this property.
        /// </summary>
        DynamicObject DynamicObject { get; }
    }
}
