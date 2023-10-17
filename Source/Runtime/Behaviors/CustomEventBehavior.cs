using System.Runtime.Serialization;
using UnityEngine;
using VRBuilder.Cognitive3DIntegration.Properties;
using VRBuilder.Core;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.Behaviors;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core.Utils;

namespace VRBuilder.Cognitive3DIntegration.Behaviours
{
    /// <summary>
    /// A behavior that records a event and sends it to the Cognitive3D API
    /// </summary>
    public class CustomEventBehavior : Behavior<CustomEventBehavior.EntityData>
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
                    string target = Target.IsEmpty() ? "" : $" on {Target.Value.SceneObject.GameObject.name}";
                    return $"Record Event{target}";
                }
            }

            [DataMember]
            [DisplayName("Event Name")]
            public string EventName { get; set; }

            [DataMember]
            [DisplayName("Dynamic Object (optional)")]
            [DisplayTooltip("The Dynamic Object id and position will be included in the Event")]
            public ScenePropertyReference<IDynamicObjectProperty> Target { get; set; }

            /// <inheritdoc />
            [DataMember]
            public BehaviorExecutionStages ExecutionStages { get; set; }
        }

        private class RecordCustomEventProcess : InstantProcess<EntityData>
        {
            private readonly BehaviorExecutionStages executionStages;
            public RecordCustomEventProcess(BehaviorExecutionStages executionStages, EntityData data) : base(data)
            {
                this.executionStages = executionStages;
            }

            /// <inheritdoc />
            public override void Start()
            {
                if ((Data.ExecutionStages & executionStages) > 0)
                {
                    SendEvent();
                }
            }

            void SendEvent()
            {
                Vector3 eventPosition = Cognitive3D.GameplayReferences.HMD.position;

                string dynamicId = "";
                Cognitive3D.DynamicObject dynamic;

                // We need this check because the DynamicObject is an optional parameter
                if (Data.Target.Value != null)
                {
                    dynamic = Data.Target.Value.DynamicObject;
                    dynamicId = dynamic.GetId();
                    eventPosition = dynamic.transform.position;
                }

                Cognitive3D.CustomEvent.SendCustomEvent(Data.EventName, eventPosition, dynamicId);
            }
        }

        protected CustomEventBehavior() : this("New Event", "", BehaviorExecutionStages.Deactivation)
        {
        }

        public CustomEventBehavior(string eventName, IDynamicObjectProperty targetObject, BehaviorExecutionStages executionStages) : this(eventName, ProcessReferenceUtils.GetNameFrom(targetObject), executionStages)
        {

        }

        public CustomEventBehavior(string eventName, string targetObject, BehaviorExecutionStages executionStages)
        {
            Data.Target = new ScenePropertyReference<IDynamicObjectProperty>(targetObject);
            Data.EventName = eventName;
            Data.ExecutionStages = executionStages;
        }

        /// <inheritdoc />
        public override IStageProcess GetActivatingProcess()
        {
            return new RecordCustomEventProcess(BehaviorExecutionStages.Activation, Data);
        }

        /// <inheritdoc />
        public override IStageProcess GetDeactivatingProcess()
        {
            return new RecordCustomEventProcess(BehaviorExecutionStages.Deactivation, Data);
        }
    }
}
