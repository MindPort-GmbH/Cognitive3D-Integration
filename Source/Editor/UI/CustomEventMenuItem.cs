using VRBuilder.Core.Behaviors;
using VRBuilder.Editor.UI.StepInspector.Menu;

namespace VRBuilder.Cogentive3D.Behaviours.UI
{
    /// <inheritdoc />
    public class CustomEventMenuItem : MenuItem<IBehavior>
    {
        /// <inheritdoc />
        public override string DisplayedName { get; } = "Cogentive3D/Custom Event";

        /// <inheritdoc />
        public override IBehavior GetNewItem()
        {
            return new CustomEventBehavior("New Event", "", BehaviorExecutionStages.Deactivation);
        }
    }
}
