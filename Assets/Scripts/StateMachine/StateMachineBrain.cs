using UnityEngine;

namespace Core.StateMachine
{
    public class StateMachineBrain : MonoBehaviour
    {
        public StateMachine stateMachine;

        private void Awake()
        {
            stateMachine.Initialize(transform);
        }

        private void Update()
        {
            stateMachine.Update();
        }

        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }

        private void LateUpdate()
        {
            stateMachine.LateUpdate();
        }
    }
}
