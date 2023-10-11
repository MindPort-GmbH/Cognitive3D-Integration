using System.Runtime.Serialization;
using UnityEngine;
using VRBuilder.Core;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.Behaviors;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core.Utils;

namespace VRBuilder.Cogentive3D.Behaviours
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
                    string target = Target.IsEmpty() ? "" : $" on {Target.Value.GameObject.name}";
                    return $"Record Event{target}";
                }
            }

            [DataMember]
            [DisplayName("Event Name")]
            public string EventName { get; set; }

            [DataMember]
            [DisplayName("Dynamic Object (optional)")]
            [DisplayTooltip("The Dynamic Object id and position will be includet in the Event")]
            public SceneObjectReference Target { get; set; }

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
                if (Data.Target != null)
                {
                    if (Data.Target.Value != null)
                    {
                        if (Data.Target.Value.GameObject != null)
                        {
                            dynamic = Data.Target.Value.GameObject.GetComponent<Cognitive3D.DynamicObject>();
                            dynamicId = dynamic.GetId();
                            eventPosition = dynamic.transform.position;
                        }
                    }
                }

                Cognitive3D.CustomEvent.SendCustomEvent(Data.EventName, eventPosition, dynamicId);
            }
        }

        protected CustomEventBehavior() : this("New Event", "", BehaviorExecutionStages.Deactivation)
        {
        }

        public CustomEventBehavior(string eventName, ISceneObject targetObject, BehaviorExecutionStages executionStages) : this(eventName, ProcessReferenceUtils.GetNameFrom(targetObject), executionStages)
        {

        }

        public CustomEventBehavior(string eventName, string targetObject, BehaviorExecutionStages executionStages)
        {
            Data.Target = new SceneObjectReference(targetObject);
            Data.EventName = eventName;
            Data.ExecutionStages = executionStages;
        }

        /// <inheritdoc />
        public override IStageProcess GetActivatingProcess()
        {
            return new RecordCustomEventProcess(BehaviorExecutionStages.Activation, Data);
        }

        public override IStageProcess GetDeactivatingProcess()
        {
            return new RecordCustomEventProcess(BehaviorExecutionStages.Deactivation, Data);
        }
    }
}
