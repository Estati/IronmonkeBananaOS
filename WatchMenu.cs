﻿using BananaOS;
using BananaOS.Pages;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR;
using Utilla;

namespace IronMonkeBananaOS
{
    internal class WatchMenu:WatchPage
    {
        public override void OnPostModSetup()
        {
            selectionHandler.maxIndex = 1;
            force = 7;

        }
        public override string Title => "<color=red>Iron</color><color=yellow>Monke</color>";
        public override bool DisplayOnMainMenu => true;
        public bool IsEnabled;
        
        
        public override string OnGetScreenContent()
        {
            var BuildMenuOptions = new StringBuilder();
            BuildMenuOptions.AppendLine("<color=yellow>========================</color>");
            BuildMenuOptions.AppendLine("                 <color=red>Iron Monke</color>");
            BuildMenuOptions.AppendLine("");
            BuildMenuOptions.AppendLine("                 By: <color=blue>Estatic</color>");
            BuildMenuOptions.AppendLine("<color=yellow>========================</color>");
            BuildMenuOptions.AppendLine("");
            BuildMenuOptions.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "[Enabled : " + IsEnabled + "]"));
            BuildMenuOptions.AppendLine("");
            BuildMenuOptions.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, "[Force : " + force + "]"));
            BuildMenuOptions.AppendLine("   [Both hands force : " + force * 2 + "]");

            return BuildMenuOptions.ToString();
        }
        public float force;
        
        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            switch (buttonType)
            {
                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;

                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;

                    case WatchButtonType.Enter:
                    if (selectionHandler.currentIndex == 0)
                    {
                        IsEnabled = !IsEnabled;
                    }
                    break;
                case WatchButtonType.Right:
                    if (selectionHandler.currentIndex == 1)
                    {
                        if (force > 57)
                        {
                            force -= 1f;
                        }
                        else
                        {
                            force += 1f;
                        }
                    }

                    break;
                case WatchButtonType.Left:
                    if (selectionHandler.currentIndex == 1)
                    {
                        if (force < 7)
                        {
                            force += 1f;
                        }
                        else
                        {
                            force -= 1f;
                        }
                    }
                    break;

                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
        void Update()
        {
            // thanks for the gamemode check dean!
            if (IsEnabled && PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Contains("MODDED_"))
            {
                if (ControllerInputPoller.instance.rightGrab)
                {
                    GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity += GorillaLocomotion.Player.Instance.rightControllerTransform.transform.right * force * Time.deltaTime;
                }
                if (ControllerInputPoller.instance.leftGrab)
                {
                    GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity += GorillaLocomotion.Player.Instance.leftControllerTransform.transform.right * -force * Time.deltaTime;
                }
            }
        }
    }
}
