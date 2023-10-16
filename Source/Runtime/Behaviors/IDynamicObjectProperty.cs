using Cognitive3D;
using VRBuilder.Core.Properties;

public interface IDynamicObjectProperty : ISceneObjectProperty
{
    DynamicObject DynamicObject { get; }
}
