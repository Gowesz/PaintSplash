﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using Assets.Scripts.GameScripts;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.PlayerScripts
{
    class PauseMenu : NetworkBehaviour
    {
        [Header("Player Settings")]
        [SerializeField] private Slider sensConfig;
        [SerializeField] private PlayerCameraController playerCameraController;

        [Header("Pause Settings")]
        public static bool GameIsPaused = false;
        public GameObject pauseMenuUI;
        
        [Client]
        void Start()
        {
            sensConfig.value = PlayerPrefsManager.PlayerSensitivity;
        }

        [Client]
        void Update()
        {
            if (!PlayerController.localPlayer) return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            GameIsPaused = false;
            Time.timeScale = 1f;
        }

        private void Pause()
        {
            pauseMenuUI.SetActive(true);
            GameIsPaused = true;
            Time.timeScale = 0f;
        }

        public void ChangeSens()
        {
            PlayerPrefsManager.PlayerSensitivity =  sensConfig.value;
            playerCameraController.mouseSensitivity = PlayerPrefsManager.PlayerSensitivity;
        }

        public void GoToMainMenu()
        {
            Scene active = SceneManager.GetActiveScene();
            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

        public void Quit() { GameManager gm = new GameManager(); gm.QuitGame();}

    }
}
