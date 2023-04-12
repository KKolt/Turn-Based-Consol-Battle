using UnityEngine;

namespace DefaultNamespace
{
    public class VanillaAi : Fighter
    {
        private const int _baseMinDmg = 0;
        private const int _baseMaxDmg = 2;
        private const string _name = "VanillaAi";
        
        public VanillaAi() : base(_baseMinDmg, _baseMaxDmg, _name)
        {
            WeaponDmg = 2;
            Action1Weight = 65;
            Action2Weight = 35;
            Action3Weight = 0;
        }

        protected override void AiChooseAction()
        {
            Initiative = false;
            if (IsCharged)
            {
                InstaKill();
                NextTurn();
            }
            else
            {
                base.AiChooseAction();
            }
        }

        public override void PlayerChooseAction()
        {
            if (IsCharged)
            {
                Initiative = false;
                InstaKill();
                NextTurn();
            }
            else
            {
                base.PlayerChooseAction();
            }
        }

        protected override void ActionSlot1()
        {
            Attack();
        }

        protected override void ActionSlot2()
        {
            Charge();
        }
        
        protected override void ActionSlot3()
        {
        }
        
        protected override void InitiativePrint()
        {
            if (!IsCharged)
            {
                Debug.Log("1) Attack 2) Charge");
            }
        }
    }
}