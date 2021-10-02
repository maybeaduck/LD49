using System.Collections.Generic;
using Zlodey;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace Zlodey
{
    public class InitializeSystem : Injects, IEcsInitSystem
    {
        public void Init()
        {
            // AudioSource Instantiate
            var spawnedAudioSource = GameObject.Find("AudioSource")?.GetComponent<AudioSource>();
            if (spawnedAudioSource == null)
            {
                var audioSource = Object.Instantiate(_staticData.AudioSourcePrefab);
                audioSource.name = "AudioSource";
                Object.DontDestroyOnLoad(audioSource);
                base._runtimeData.AudioSource = audioSource;
            }
            else
            {
                base._runtimeData.AudioSource = spawnedAudioSource;
            }
           
            if (Progress.CurentSound == 0)
            {
                _ui.MenuScreen.SoundButton.SwitchImage();    
            }
            if (Progress.CurentHaptic == 0)
            {
                _ui.MenuScreen.HapticButton.SwitchImage();    
            }
            if(Progress.CurentHaptic == 0){
                base._runtimeData.HapticOn = false;
            }
            else if(Progress.CurentHaptic == 1){
                base._runtimeData.HapticOn = true;
            }
            
            if(Progress.CurentSound == 0){
                base._runtimeData.SoundOn = false;
            }
            else if(Progress.CurentSound == 1){
                base._runtimeData.SoundOn = true;
            }


            //camera
            var cameraRig = Object.FindObjectOfType<CameraRig>();
            if (cameraRig)
            {
                _sceneData.CameraRig = cameraRig;
                _runtimeData.MainCamera = _sceneData.CameraRig.Camera;
            }

            //monitorUI
            var monitorUI = Object.FindObjectOfType<MonitorUI>();
            if (monitorUI)
            {
                _sceneData.MonitorUI = monitorUI;
                _world.NewEntity().Get<MonitorScreenSwitchEvent>().State = DistructionState.Start;
            }

            var studio = Object.FindObjectOfType<Studio>();
            if (studio)
            {
                _sceneData.Studio = studio;
            }

            //cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}