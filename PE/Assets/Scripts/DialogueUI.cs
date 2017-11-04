﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using TMPro;
using System.Collections.Generic;

// Displays dialogue lines to the player, and sends
// user choices back to the dialogue system.

// Note that this is just one way of presenting the
// dialogue to the user. The only hard requirement
// is that you provide the RunLine, RunOptions, RunCommand
// and DialogueComplete coroutines; what they do is up to you.

namespace Yarn.Unity.Example
{
    public class DialogueUI : Yarn.Unity.DialogueUIBehaviour
    {

        // The object that contains the dialogue and the options.
        // This object will be enabled when conversation starts, and
        // disabled when it ends.
        public GameObject DialogueContainer;
        //GameObject DialogueContainer;

        // The UI element that displays lines
        //public Text LineText;
        public TextMeshProUGUI LineText;
        //Text LineText;

        // A UI element that appears after lines have finished appearing
        public GameObject continuePrompt;

        // A delegate (ie a function-stored-in-a-variable) that
        // we call to tell the dialogue system about what option
        // the user selected
        private Yarn.OptionChooser SetSelectedOption;

        [Tooltip("How quickly to show the text, in seconds per character")]
        public float textSpeed = 0.025f;

        [Tooltip("How long to wait until next line")]
        public float WaitTime = 2f;

        // The buttons that let the user choose an option
        public List<Button> optionButtons;

        public RectTransform gameControlsContainer;

        void Awake()
        {
            //DialogueContainer = DialogueContainer;
            //LineText = LineText;
            // Start by hiding the container, line and option buttons
            if (DialogueContainer != null)
                DialogueContainer.SetActive(false);

            LineText.gameObject.SetActive(false);

            foreach (var button in optionButtons)
            {
                button.gameObject.SetActive(false);
            }

            // Hide the continue prompt if it exists
            if (continuePrompt != null)
                continuePrompt.SetActive(false);
        }


        // Show a line of dialogue, gradually
        public override IEnumerator RunLine(Yarn.Line line)
        {
            // Show the text
            LineText.gameObject.SetActive(true);
            DialogueContainer.gameObject.SetActive(true);

            if (textSpeed > 0.0f)
            {
                // Display the line one character at a time
                var stringBuilder = new StringBuilder();

                foreach (char c in line.text)
                {
                    stringBuilder.Append(c);
                    LineText.text = stringBuilder.ToString();
                    yield return new WaitForSeconds(textSpeed);
                }
            }
            else
            {
                // Display the line immediately if textSpeed == 0
                LineText.text = line.text;
            }

            // Show the 'press any key' prompt when done, if we have one
            if (continuePrompt != null)
                continuePrompt.SetActive(true);


            // Wait for any user input
            //while (Input.anyKeyDown == false)
            //{
            //    yield return null;
            //}

            // Wait for user specified seconds
            yield return new WaitForSeconds(WaitTime);

            // Hide the text and prompt
            LineText.gameObject.SetActive(false);
            DialogueContainer.gameObject.SetActive(false);

            if (continuePrompt != null)
                continuePrompt.SetActive(false);

        }

        // Show a list of options, and wait for the player to make a selection.
        public override IEnumerator RunOptions(Yarn.Options optionsCollection,
                                                Yarn.OptionChooser optionChooser)
        {
            // Do a little bit of safety checking
            if (optionsCollection.options.Count > optionButtons.Count)
            {
                Debug.LogWarning("There are more options to present than there are" +
                                 "buttons to present them in. This will cause problems.");
            }

            // Display each option in a button, and make it visible
            int i = 0;
            foreach (var optionString in optionsCollection.options)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<Text>().text = optionString;
                i++;
            }

            // Record that we're using it
            SetSelectedOption = optionChooser;

            // Wait until the chooser has been used and then removed (see SetOption below)
            while (SetSelectedOption != null)
            {
                yield return null;
            }

            // Hide all the buttons
            foreach (var button in optionButtons)
            {
                button.gameObject.SetActive(false);
            }
        }

        // Called by buttons to make a selection.
        public void SetOption(int selectedOption)
        {

            // Call the delegate to tell the dialogue system that we've
            // selected an option.
            SetSelectedOption(selectedOption);

            // Now remove the delegate so that the loop in RunOptions will exit
            SetSelectedOption = null;
        }

        // Run an internal command.
        public override IEnumerator RunCommand(Yarn.Command command)
        {
            // "Perform" the command
            Debug.Log("Command: " + command.text);

            yield break;
        }

        public override IEnumerator DialogueStarted()
        {
            Debug.Log("Dialogue starting!");
            // Enable the dialogue controls.
            if (DialogueContainer != null)
                DialogueContainer.SetActive(true);

            // Hide the game controls.
            if (gameControlsContainer != null)
            {
                gameControlsContainer.gameObject.SetActive(false);
            }

            yield break;
        }

        // Yay we're done. Called when the dialogue system has finished running.
        public override IEnumerator DialogueComplete()
        {
            Debug.Log("Complete!");

            // Hide the dialogue interface.
            if (DialogueContainer != null)
                DialogueContainer.SetActive(false);

            // Show the game controls.
            if (gameControlsContainer != null)
            {
                gameControlsContainer.gameObject.SetActive(true);
            }

            yield break;
        }

    }

}