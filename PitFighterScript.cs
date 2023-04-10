using DefaultNamespace;
using UnityEngine;

public class PitFightScript : MonoBehaviour
{
    private Fighter _fighter1;
    private Fighter _fighter2;
    private bool _gameModeSelected;
    
    private void Start()
    {
        _gameModeSelected = false;
        Debug.Log("Welcome to the fight club. You can choose both fighters!\n" +
                  "1) Gandalf 2) GLaDOS 3) VanillaPlayer 4) VanillaAi \n" +
                  "Choose twice before you choose the Game Mode.\n" +
                  "1) Both fighters are controlled by the ai\n" +
                  "2) You control the first fighter against the ai\n" +
                  "3) Both fighters are controlled by humans\n");
    }
    
    private void Update()
    {
        // GameSetUp 
        // should be its own script
        if (!_gameModeSelected)
        {
            if (_fighter1 == null || !_fighter1.IsReady)
            {
                _fighter1 = ChooseFighter();
            }
            else if (_fighter2 == null || !_fighter2.IsReady) // implicit _fighter1 is chosen.
            {
                _fighter2 = ChooseFighter();
            }
            else if (_fighter1 != null && _fighter2 != null && _fighter1.IsReady && _fighter2.IsReady) 
            {
                ChooseGameMode();
            }
        }
        else
        {
            // Updates during the game
            if (_fighter1 != null && _fighter1.IsPlayer && _fighter1.Initiative)
            {
                _fighter1.PlayerChooseAction();
            }
            else if (_fighter2 != null && _fighter2.IsPlayer && _fighter2.Initiative)
            {
                _fighter2.PlayerChooseAction();
            }
        }


    }

    private void ChooseGameMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Ai vs Ai");
            StartGame();
            _gameModeSelected = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Human vs Ai");
            _fighter1.SetPlayerName("Player1");
            StartGame();
            _gameModeSelected = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Human vs Human");
            _fighter1.SetPlayerName("Player1");
            _fighter2.SetPlayerName("Player2");
            StartGame();
            _gameModeSelected = true;
        }
    }

    private void StartGame()
    {
        _fighter2.Opponent = _fighter1;
        _fighter1.Opponent = _fighter2;

        _fighter1.RollInitiative();
    }

    private Fighter ChooseFighter()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return new Gandalf();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return new Glados();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return new VanillaPlayer();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return new VanillaAi();
        }
        
        return new Fighter(); // empty Fighter this could be slow but it is only for the setup
    }
}