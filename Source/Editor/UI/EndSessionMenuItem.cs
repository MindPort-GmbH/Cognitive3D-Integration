using VRBuilder.Core.Behaviors;
using VRBuilder.Editor.UI.StepInspector.Menu;

namespace VRBuilder.Cognitive3DIntegration.Behaviours.UI
{
    /// <inheritdoc />
    public class EndSessionMenuItem : MenuItem<IBehavior>
    {
        /// <inheritdoc />
        public override string DisplayedName { get; } = "Cognitive3D/End Session";

        /// <inheritdoc />
        public override IBehavior GetNewItem()
        {
            return new EndSessionBehavior(BehaviorExecutionStages.Activation);
        }
    }
}


