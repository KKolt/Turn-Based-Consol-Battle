using UnityEngine;

namespace DefaultNamespace
{
    public class Glados : Fighter
    {
        private const int _baseMinDmg = 1;
        private const int _baseMaxDmg = 2;
        private const string _name = "GLaDOS";
        private bool _cakeIsAlive;

        public Glados() : base(_baseMinDmg, _baseMaxDmg, _name)
        {
            WeaponDmg = 1;
            Action1Weight = 1;
            Action2Weight = 1;
            Action3Weight = 1;
            _cakeIsAlive = true;
        }

        protected override void ActionSlot1()
        {
            Attack();
        }

        protected override void ActionSlot2()
        {
            Debug.Log("We are throwing a party in honor of your tremendous success! A party associate will arrive shortly to collect you.");
            if (!_cakeIsAlive)
            {
                Debug.Log(Opponent.Name + ": The cake is a lie!");
            }
            else
            {
                Debug.Log(Opponent.Name +" takes party position");
                Attack();
                Attack();
                _cakeIsAlive = false;
            }
        }

        protected override void ActionSlot3()
        {
            Disarm();
        }
        
        protected override void InitiativePrint()
        {
            Debug.Log("1) Test test subject 2) Invite to party  3) Burn companion cube");
        }

        protected override void AiChooseAction()
        {
            Action2Weight = _cakeIsAlive ? 1 : 0;

            Action3Weight = Opponent.WeaponDmg > 0 ? 1 : 0;

            base.AiChooseAction();
        }

    }
}