using System.Runtime.Serialization;
using VRBuilder.Core;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.Behaviors;

namespace VRBuilder.Cogentive3D.Behaviours
{
    /// <summary>
    /// A behavior that ends the Cognitive3D session. After this no other events can be send.
    /// </summary>
    [DataContract(IsReference = true)]
    public class EndSessionBehavior : Behavior<EndSessionBehavior.EntityData>
    {
        [DisplayName("Cognitive3D Event")]
        [DataContract(IsReference = true)]
        public class EntityData : IBehaviorData, IBehaviorExecutionStages
        {
            /// <inheritdoc />
            public Metadata Metadata { get; set; }

            /// <inheritdoc />
            [IgnoreDataMember]
            public string Name
            {
                get
                {
                    return "End Cognitive3D Session";
                }
            }

            /// <inheritdoc />
            [DataMember]
            public BehaviorExecutionStages ExecutionStages { get; set; }
        }

        private class EndSessionProcess : InstantProcess<EntityData>
        {
            private readonly BehaviorExecutionStages executionStages;
            public EndSessionProcess(BehaviorExecutionStages executionStages, EntityData data) : base(data)
            {
                this.executionStages = executionStages;
            }

            /// <inheritdoc />
            public override void Start()
            {
                if ((Data.ExecutionStages & executionStages) > 0)
                {
                    Cognitive3D.Cognitive3D_Manager.Instance.EndSession();
                }
            }
        }

        protected EndSessionBehavior() : this(BehaviorExecutionStages.Activation)
        {

        }

        public EndSessionBehavior(BehaviorExecutionStages executionStages)
        {
            Data.ExecutionStages = executionStages;
        }

        public override IStageProcess GetActivatingProcess()
        {
            return new EndSessionProcess(BehaviorExecutionStages.Activation, Data);
        }

        public override IStageProcess GetDeactivatingProcess()
        {
            return new EndSessionProcess(BehaviorExecutionStages.Deactivation, Data);
        }
    }
}
