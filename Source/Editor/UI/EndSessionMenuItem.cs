using VRBuilder.Core.Behaviors;
using VRBuilder.Editor.UI.StepInspector.Menu;

namespace VRBuilder.Cogentive3D.Behaviours.UI
{
    /// <inheritdoc />
    public class EndSessionMenuItem : MenuItem<IBehavior>
    {
        /// <inheritdoc />
        public override string DisplayedName { get; } = "Cogentive3D/End Session";

        /// <inheritdoc />
        public override IBehavior GetNewItem()
        {
            return new EndSessionBehavior(BehaviorExecutionStages.Activation);
        }
    }
}


