// Comment to silence
#define VERBOSE

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Core.StateMachine
{
    [Serializable]
    public abstract class State : ScriptableObject, IState
    {
        public IStateMachine Machine { get; private set; }

        public bool IsActive => Machine.IsCurrentState(this);

        [SerializeField]
        internal List<IState> _possibleTransitions;

        public virtual void Update()
        {
            Debug.Log("Updating");
        }

        public virtual void FixedUpdate() { }

        public virtual void LateUpdate() { }

        public virtual void OnCollisionEnter(Collision collision)
        {
#if (VERBOSE)
            Log(MethodBase.GetCurrentMethod().Name);
#endif
        }

        public virtual void OnCollisionStay(Collision collision) { }

        public virtual void OnCollisionExit(Collision collision)
        {
#if (VERBOSE)
            Log(MethodBase.GetCurrentMethod().Name);
#endif
        }

        public virtual void OnTriggerEnter(Collider collider)
        {
#if (VERBOSE)
            Log(MethodBase.GetCurrentMethod().Name);
#endif
        }

        public virtual void OnTriggerStay(Collider collider) { }

        public virtual void OnTriggerExit(Collider collider)
        {
#if (VERBOSE)
            Log(MethodBase.GetCurrentMethod().Name);
#endif
        }

        public virtual void OnAnimatorIK(int layerIndex) { }

        public virtual void Initialize(IStateMachine m)
        {
            Machine = m;
#if (VERBOSE)
            Log(MethodBase.GetCurrentMethod().Name);
#endif
        }

        public virtual void OnEnterState()
        {
#if (VERBOSE)
            Log(MethodBase.GetCurrentMethod().Name);
#endif
        }

        public virtual void OnExitState()
        {
#if (VERBOSE)
            Log(MethodBase.GetCurrentMethod().Name);
#endif
        }

        private void Log(string methodName)
        {
            Debug.Log(
                $"{Machine.GetType().Name}.{GetType().Name}::{methodName}"
            );
        }
    }
}
