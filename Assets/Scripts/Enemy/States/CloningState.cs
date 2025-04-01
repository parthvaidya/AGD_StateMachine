using StatePattern.Main;
using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class CloningState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public CloningState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            Debug.Log("CloningState: Entered.");
            if (Owner == null)
            {
                Debug.LogError("CloningState: Owner is null!");
                return;
            }
            CreateAClone();
            CreateAClone();
        }

        public void Update() { }

        public void OnStateExit() { }

        private void CreateAClone()
        {
            Debug.Log("CloningState: Attempting to create a clone...");
            CloneManController clonedRobot = GameService.Instance.EnemyService.CreateEnemy(Owner.Data) as CloneManController;
            if (clonedRobot == null)
            {
                Debug.LogError("CloningState: Failed to create a clone!");
                return;
            }
            clonedRobot.SetCloneCount((Owner as CloneManController).CloneCountLeft - 1);
            clonedRobot.Teleport();
            clonedRobot.SetDefaultColor(EnemyColorType.Clone);
            clonedRobot.ChangeColor(EnemyColorType.Clone);
            GameService.Instance.EnemyService.AddEnemy(clonedRobot);
        }
    }
}