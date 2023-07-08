using DS.Elements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.Elements
{
    using Data.Save;
    using Enumerations;
    using UnityEditor;
    using UnityEditor.Experimental.GraphView;
    using UnityEditor.UIElements;
    using UnityEngine.UIElements;
    using Utilities;
    using Windows;

    /// <summary>
    /// Custom node for raising SOEvents
    /// </summary>
    public class DSEventNode : DSNode
    {
        public override void Initialize(string nodeName, DSGraphView dsGraphView, Vector2 position)
        {
            base.Initialize(nodeName, dsGraphView, position);

            DialogueType = DSDialogueType.Event;

            DSChoiceSaveData choiceData = new DSChoiceSaveData()
            {
                Text = "Next Dialogue"
            };

            Choices.Add(choiceData);
        }

        public override void Draw()
        {
            base.Draw();


            /* EXTENSION CONTAINER */

            ObjectField soEvent = new ObjectField("SO Event");

            soEvent.objectType = typeof(SOEvent);

            INotifyValueChangedExtensions.RegisterValueChangedCallback(soEvent, (e) => { this.SoEvent = (SOEvent)e.newValue; });

            soEvent.SetValueWithoutNotify(SoEvent);

            extensionContainer.Add(soEvent);

            ObjectField argForEvent = new ObjectField("SO object as argument");

            argForEvent.objectType = typeof(DSDialogEventArgSO);

            INotifyValueChangedExtensions.RegisterValueChangedCallback(argForEvent, (e) => { this.EventArgs = (DSDialogEventArgSO)e.newValue; });

            argForEvent.SetValueWithoutNotify(EventArgs);

            extensionContainer.Add(argForEvent);

            /* OUTPUT CONTAINER */


            foreach (DSChoiceSaveData choice in Choices)
            {
                Port choicePort = this.CreatePort(choice.Text);

                choicePort.userData = choice;

                outputContainer.Add(choicePort);
            }

            RefreshExpandedState();
        }

        private void tst(SOEvent e)
        {
            this.SoEvent = e;
        }
    }
}