using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fighter
{
    private readonly int _minDmg;
    private readonly int _maxDmg;
    protected const int _maxHealth = 10;
    private bool _preventDmg;
    public Fighter Opponent;
    private int AmorValue;
    public int WeaponDmg;
    public bool IsDead;
    public string Name;
    public bool Initiative;
    public bool IsCharged;
    public bool IsPlayer;
    protected int Action1Weight = 0;
    protected int Action2Weight = 0;
    protected int Action3Weight = 0;
    public readonly bool IsReady;

    private int _health;

    protected int Health
    {
        get => _health;
        private set
        {
            _health = value;

            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }

            CheckIfDead();
        }
    }

    protected Fighter(int minDmg, int maxDmg, string name)
    {
        Health = _maxHealth;
        _minDmg = minDmg;
        _maxDmg = maxDmg;
        _preventDmg = false;
        AmorValue = 0;
        WeaponDmg = 0;
        IsDead = false;
        Name = name;
        IsCharged = false;
        IsPlayer = false;
        IsReady = true;
        
        Debug.Log("You have chosen " + Name);
    }

    public Fighter()
    {
        IsReady = false;
    }

    public void SetPlayerName(string playerName)
    {
        Name += "(" + playerName + ")";
        IsPlayer = true;
    }

    protected void Attack()
    {
        int dmg = Random.Range(_minDmg, _maxDmg + 1) + WeaponDmg;
        Debug.Log(Name + " attacks");
        Opponent.TakeDamage(dmg);
    }

    private void CheckIfDead()
    {
        IsDead = Health <= 0;
    }

    private void TakeDamage(int damage)
    {
        if (_preventDmg)
        {
            Debug.Log(Name + " takes no damage.");
            return;
        }

        int realDamage = damage - AmorValue;

        if (realDamage < 0)
        {
            Debug.Log(Name + " absorbs damage.");
            return;
        }

        Health -= realDamage;
        Debug.Log(Name + " takes " + realDamage + " points of damage and has " + Health + " hit points.");
    }

    protected void Charge()
    {
        IsCharged = true;
        Debug.Log(Name + " is charging energy.");
    }

    protected void Disarm()
    {
        if (Opponent.WeaponDmg != 0)
        {
            Opponent.WeaponDmg = 0;
            Debug.Log(Name + " disarms " + Opponent.Name +".");
        }
        else
        {
            Debug.Log("Nothing happens.");
        }
        
    }

    protected void InstaKill()
    {
        Debug.Log(Name + " strikes a devastating blow.");
        Opponent.TakeDamage(Opponent.Health + Opponent.AmorValue);
        IsCharged = false;
    }

    protected void Defend()
    {
        _preventDmg = true;
        Debug.Log(Name + " takes a defensive stance.");
    }

    protected void Heal(int amount)
    {
        Health += amount;
        Debug.Log(Name + " is healed and has now " + Health + " hit points.");
    }
    
    protected virtual void ActionSlot1()
    {
    }

    protected virtual void ActionSlot2()
    {
    }

    protected virtual void ActionSlot3()
    {
    }

    protected void NextTurn()
    {
        if (Opponent.IsDead)
        {
            Debug.Log("This is a triumph. " + Name +" has won this fight.");

            return;
        }
        
        Opponent.GetInitiative();
    }

    private void GetInitiative()
    {
        Initiative = true;
        _preventDmg = false;
        
        if (IsPlayer)
        {
            Debug.Log("Choose your action " + Name + ": ");
            InitiativePrint();
        }
        else
        {
            Debug.Log("It's the " + Name + "'s turn");
            AiChooseAction();
        }
    }

    protected virtual void InitiativePrint()
    {
    }

    public void RollInitiative()
    {
        switch (Random.Range(1, 2 + 1))
        {
            case 1:
                GetInitiative();
                break;
            case 2:
                Opponent.GetInitiative();
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public virtual void PlayerChooseAction()
    {
        try
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Initiative = false;
                ActionSlot1();
                NextTurn();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Initiative = false;
                ActionSlot2();
                NextTurn();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Initiative = false;
                ActionSlot3();
                NextTurn();
            }
        }
        catch (NotImplementedException)
        {
            Debug.Log("The action slot you selected is not in use.");
            Initiative = true;
        }
       
    }

    protected virtual void AiChooseAction()
    {
        int cumulativeWeight = Action1Weight + Action2Weight + Action3Weight;
        int diceRoll = Random.Range(1, cumulativeWeight + 1);

        if (diceRoll <= Action1Weight)
        {
            ActionSlot1();
        }
        else if (diceRoll > Action1Weight && diceRoll <= Action1Weight + Action2Weight)
        {
            ActionSlot2();
        }
        else
        {
            ActionSlot3();
        }

        NextTurn();
    }
}