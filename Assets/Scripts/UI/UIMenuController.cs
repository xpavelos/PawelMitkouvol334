using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TKOU.SimAI.UI
{
    /// <summary>
    /// Menu controller.
    /// </summary>
    public class UIMenuController : MonoBehaviour
    {
        #region Variables

        [SerializeField]
        private GameController gameController;

        [SerializeField]
        private Button playButton;

        [SerializeField]
        private Button quitButton;

        [SerializeField]
        private GameObject mainMenuObject;

        [SerializeField]
        private bool useKeyboard;
        MenuKeyboardController keyboardController;

        [SerializeField]
        [Tooltip("Set to TRUE to automatically start the game on play. Useful for testing.")]
        private bool autostart = false;

        #endregion Variables

        #region Unity methods

        private void Awake()
        {
            keyboardController = GetComponent<MenuKeyboardController>();

            playButton.onClick.AddListener(PlayButton_OnClick);
            quitButton.onClick.AddListener(QuitButton_OnClick);

            gameController.OnGameRun += GameController_OnGameRun;
            gameController.OnGameEnd += GameController_OnGameEnd;

            keyboardController.ActivateHighlighter(useKeyboard);
            keyboardController.Init(gameController);
        }

        private void Start()
        {
            if (autostart)
            {
                gameController.RunGame();
            }
        }

        void OnEnable()
        {
          //  GameController.
        }

        void OnDisable()
        {

        }

        private void Update()
        {
            if (!useKeyboard) return;
            if (keyboardController != null) keyboardController.HandleUpdate();

        }

        #endregion Unity methods

        #region Private methods

        private void UpdateVisibility()
        {
            mainMenuObject.SetActive(!gameController.IsGameRunning);
        }

        #endregion Private methods

        #region Event callbacks

        private void QuitButton_OnClick()
        {
            Application.Quit();
        }

        private void PlayButton_OnClick()
        {
            gameController.RunGame();
        }

        private void GameController_OnGameRun()
        {
            UpdateVisibility();
        }

        private void GameController_OnGameEnd()
        {
            UpdateVisibility();
        }

        #endregion Event callbacks
    }
}
