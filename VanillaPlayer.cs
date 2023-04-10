using UnityEngine;

namespace DefaultNamespace
{
    public class VanillaPlayer : Fighter
    {
        private const int _baseMinDmg = 1;
        private const int _baseMaxDmg = 2;
        private const string _name = "VanillaPlayer";
        private const int _healAmount = 2;

        public VanillaPlayer() : base(_baseMinDmg, _baseMaxDmg, _name)
        {
            Action1Weight = 90;
            Action2Weight = 10;
            Action3Weight = 30;
        }

        protected override void ActionSlot1()
        {
            Attack();
        }

        protected override void ActionSlot2()
        {
            Heal(_healAmount);
        }

        protected override void ActionSlot3()
        {
            Defend();
        }

        protected override void AiChooseAction()
        {
            if (Opponent.IsCharged)
            {
                ActionSlot3();
                NextTurn();
                return;
            }

            Action2Weight = _maxHealth - Health >= _healAmount ? 10 : 0;
            
            base.AiChooseAction();
        }

        protected override void InitiativePrint()
        {
            Debug.Log("1) Attack 2) Heal 3) Defend");
        }
    }
}