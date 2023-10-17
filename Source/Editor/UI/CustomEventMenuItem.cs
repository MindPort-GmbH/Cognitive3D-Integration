using VRBuilder.Core.Behaviors;
using VRBuilder.Editor.UI.StepInspector.Menu;

namespace VRBuilder.Cognitive3DIntegration.Behaviours.UI
{
    /// <inheritdoc />
    public class CustomEventMenuItem : MenuItem<IBehavior>
    {
        /// <inheritdoc />
        public override string DisplayedName { get; } = "Cognitive3D/Send Custom Event";

        /// <inheritdoc />
        public override IBehavior GetNewItem()
        {
            return new CustomEventBehavior("New Event", "", BehaviorExecutionStages.Deactivation);
        }
    }
}
