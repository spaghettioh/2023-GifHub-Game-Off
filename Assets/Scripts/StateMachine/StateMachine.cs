using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.StateMachine
{
    public class StateMachine : ScriptableObject, IStateMachine
    {
        [field: SerializeField]
        public List<IState> States { get; private set; }

        [field: SerializeField]
        public IState InitialState { get; private set; }

        [Header("Debug ==========")]
        [SerializeField]
        private IState _currentState;

        [SerializeField]
        private IState _nextState;

        internal bool _onEnter;
        internal bool _onExit;

        internal Transform Transform;

        internal virtual void AddStates()
        {
            States.ForEach(state =>
            {
                if (!ContainsState(state))
                {
                    States.Add(state);
                }
            });
        }

        internal virtual void Initialize(Transform t)
        {
            Transform = t;
            AddStates();

            foreach (var state in States)
            {
                state.Initialize(this);
            }

            _currentState = InitialState;
            if (_currentState == null)
            {
                throw new Exception(
                    $"\n{name}.nextState is null on Initialize()!\tDid you forget to call SetInitialState()?"
                );
            }

            _onEnter = true;
            _onExit = false;
        }

        internal virtual void Update()
        {
            if (_onExit)
            {
                _currentState.OnExitState();
                _currentState = _nextState;
                _nextState = null;

                _onEnter = true;
                _onExit = false;
            }

            if (_onEnter)
            {
                _currentState.OnEnterState();

                _onEnter = false;
            }

            try
            {
                _currentState.Update();
            }
            catch (NullReferenceException e)
            {
                if (null == InitialState)
                {
                    throw new Exception(
                        $"\n{name}.currentState is null when calling Execute()!\tDid you set initial state?\n{e.Message}"
                    );
                }
                else
                {
                    throw new Exception(
                        $"\n{name}.currentState is null when calling Execute()!\tDid you change state to a valid state?\n{e.Message}"
                    );
                }
            }
        }

        internal virtual void FixedUpdate()
        {
            if (!(_onEnter && _onExit))
            {
                try
                {
                    _currentState.FixedUpdate();
                }
                catch (NullReferenceException e)
                {
                    if (null == InitialState)
                    {
                        throw new Exception(
                            $"\n{name}.currentState is null when calling Execute()!\tDid you set initial state?\n{e.Message}"
                        );
                    }
                    else
                    {
                        throw new Exception(
                            $"\n{name}.currentState is null when calling Execute()!\tDid you change state to a valid state?\n{e.Message}"
                        );
                    }
                }
            }
        }

        internal virtual void LateUpdate()
        {
            if (!(_onEnter && _onExit))
            {
                try
                {
                    _currentState.LateUpdate();
                }
                catch (NullReferenceException e)
                {
                    if (null == InitialState)
                    {
                        throw new Exception(
                            $"\n{name}.currentState is null when calling Execute()!\tDid you set initial state?\n{e.Message}"
                        );
                    }
                    else
                    {
                        throw new Exception(
                            $"\n{name}.currentState is null when calling Execute()!\tDid you change state to a valid state?\n{e.Message}"
                        );
                    }
                }
            }
        }

        public virtual void OnCollisionEnter(Collision collision) =>
            _currentState.OnCollisionEnter(collision);

        public virtual void OnCollisionStay(Collision collision) =>
            _currentState.OnCollisionStay(collision);

        public virtual void OnCollisionExit(Collision collision) =>
            _currentState.OnCollisionExit(collision);

        public virtual void OnTriggerEnter(Collider collider) =>
            _currentState.OnTriggerEnter(collider);

        public virtual void OnTriggerStay(Collider collider) =>
            _currentState.OnTriggerStay(collider);

        public virtual void OnTriggerExit(Collider collider) =>
            _currentState.OnTriggerExit(collider);

        public void OnAnimatorIK(int layerIndex)
        {
            if (!(_onEnter && _onExit))
            {
                try
                {
                    _currentState.OnAnimatorIK(layerIndex);
                }
                catch (NullReferenceException e)
                {
                    if (null == InitialState)
                    {
                        throw new Exception(
                            $"\n{name}.currentState is null when calling Execute()!\tDid you set initial state?\n{e.Message}"
                        );
                    }
                    else
                    {
                        throw new Exception(
                            $"\n{name}.currentState is null when calling Execute()!\tDid you change state to a valid state?\n{e.Message}"
                        );
                    }
                }
            }
        }

        public void ChangeState(IState nextState)
        {
            if (_nextState != null)
            {
                throw new Exception(
                    $"{name} is already changing states, you must wait to call ChangeState()!\n"
                );
            }

            try
            {
                _nextState = States.Find(state => state == nextState);
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception(
                    $"\n{name}.ChangeState() cannot find the state in the machine!\tDid you add the state you are trying to change to?\n{e.Message}"
                );
            }

            _onExit = true;
        }

        public bool IsCurrentState(IState state)
        {
            return _currentState == state;
        }

        public void RemoveState(IState state)
        {
            States.Remove(state);
        }

        public bool ContainsState(IState state)
        {
            return States.Contains(state);
        }

        public void RemoveAllStates()
        {
            States.Clear();
        }

        public void SetInitialState(IState T)
        {
            throw new NotImplementedException();
        }

        public void AddState(IState T)
        {
            States.Add(T);
        }
    }
}
