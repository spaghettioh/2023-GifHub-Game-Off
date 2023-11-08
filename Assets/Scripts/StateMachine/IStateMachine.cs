using System;
using UnityEngine;

namespace Core.StateMachine
{
    public interface IStateMachine
    {
        // <name> is a reference to MonoBehaviour.name
        string name { get; set; }

        void SetInitialState(IState T);

        void ChangeState(IState T);

        bool IsCurrentState(IState T);

        void AddState(IState T);

        void RemoveState(IState T);

        bool ContainsState(IState T);

        void OnCollisionEnter(Collision collision);
        void OnCollisionStay(Collision collision);
        void OnCollisionExit(Collision collision);

        void OnTriggerEnter(Collider collider);
        void OnTriggerStay(Collider collider);
        void OnTriggerExit(Collider collider);

        void RemoveAllStates();
    }
}
