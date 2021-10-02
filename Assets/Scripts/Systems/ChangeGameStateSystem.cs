using System;
using Zlodey;
using Leopotam.Ecs;
using UnityEngine;
namespace Zlodey
{
    public class ChangeGameStateSystem : Injects, IEcsRunSystem
    {
        private EcsFilter<ChangeGameStateEvent> _eventFilter;
        public void Run()
        {
            foreach (var index in _eventFilter)
            {
                GameState newState = _eventFilter.Get1(index).State;

                switch (newState)
                {
                    case GameState.BeforePlay:
                        Debug.Log("BeforePlay");
                        _ui.WinScreen.Hide();
                        _ui.LoseScreen.Hide();
                        _ui.GameScreen.Hide();
                        _ui.MenuScreen.Show();
                        break;

                    case GameState.Play:
                        Debug.Log("Play");
                        _ui.WinScreen.Hide();
                        _ui.LoseScreen.Hide();
                        _ui.GameScreen.Show();
                        _ui.MenuScreen.Hide();
                        _runtimeData.StartLevelTime = Time.time;
                        _world.NewEntity().Get<StartGameEvent>();
                        break;

                    case GameState.Win:
                        Debug.Log("win");
                        _ui.WinScreen.Show();
                        _ui.LoseScreen.Hide();
                        _ui.GameScreen.Hide();
                        _ui.MenuScreen.Hide();
                        _runtimeData.EndLevelTime = Time.time;
                        break;

                    case GameState.Lose:
                        Debug.Log("lose");
                        _ui.WinScreen.Hide();
                        _ui.LoseScreen.Show();
                        _ui.GameScreen.Hide();
                        _ui.MenuScreen.Hide();
                        _runtimeData.EndLevelTime = Time.time;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _runtimeData.GameState = newState;
                _eventFilter.GetEntity(index).Destroy();
            }
        }
    }
}