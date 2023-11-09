using UnityEngine;

namespace Core.StateMachine
{
    public interface IState
    {
        bool IsActive { get; }

        IStateMachine Machine { get; }

        void Initialize(IStateMachine machine);

        void OnEnterState();

        void Update();
        void FixedUpdate();
        void LateUpdate();

        void OnExitState();

        void OnCollisionEnter(Collision collision);
        void OnCollisionStay(Collision collision);
        void OnCollisionExit(Collision collision);

        void OnTriggerEnter(Collider collider);
        void OnTriggerStay(Collider collider);
        void OnTriggerExit(Collider collider);

        void OnAnimatorIK(int layerIndex);
    }
}
