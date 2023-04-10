using UnityEngine;

namespace DefaultNamespace
{
    public class Gandalf : Fighter
    {
        private const int _baseMinDmg = 1;
        private const int _baseMaxDmg = 2;
        private const string _name = "Gandalf, the Grey";

        public Gandalf() : base(_baseMinDmg, _baseMaxDmg, _name)
        {
            WeaponDmg = 1;
            Action1Weight = 65;
            Action2Weight = 35;
            Action3Weight = 35;
        }

        protected override void ActionSlot1()
        {
            Attack();
        }

        protected override void ActionSlot2()
        {
            Defend();
        }

        protected override void ActionSlot3()
        {
            Disarm();
        }

        protected override void AiChooseAction()
        {
            if (Opponent.IsCharged)
            {
                ActionSlot2();
                NextTurn();
                return;
            }

            Action3Weight = Opponent.WeaponDmg > 0 ? 35 : 0;

            base.AiChooseAction();
        }

        protected override void InitiativePrint()
        {
            Debug.Log("1) Poke with staff 2) You Shall not pass 3) Your Staff is broken");
        }
        
    }
}