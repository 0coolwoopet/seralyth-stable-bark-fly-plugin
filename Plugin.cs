using static Seralyth.Menu.Main;
using Seralyth.Classes.Menu;
using Seralyth.Mods;
using UnityEngine;
using Seralyth.Managers;
using Seralyth.Menu;
using GorillaLocomotion;

namespace SeralythPlugin
{
    public class Plugin
    {
        public static string Name = "barkfly";
        public static string Description = "fixed ver of barkfly.";
        private static Vector3 lastPosition;
        private static bool wasFlying = false;

        public static void OnEnable()
        {
            LogManager.Log("Plugin " + Name + " has been enabled!");

            int category = Buttons.AddCategory("Plugin Mods");
            Buttons.AddButton(Buttons.GetCategory("Main"), new ButtonInfo { buttonText = "Plugin Mods", method = () => Buttons.CurrentCategoryIndex = category, isTogglable = false, toolTip = "Brings you to a category for plugins." });

            Buttons.AddButtons(
                category,
                new ButtonInfo[]
                {
                    new ButtonInfo { buttonText = "Exit Plugin Mods", method =() => Buttons.CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns you back to the main page." },
                    new ButtonInfo { buttonText = "Barkfly <color=grey>[</color><color=green>J</color><color=grey>]</color>", method = () => BarkFly(), toolTip = "fly with your joysticks." },


                }
            );
        }

        public static void OnDisable()
        {
            LogManager.Log("Plugin " + Name + " has been disabled!");

            Buttons.RemoveCategory("Plugin Mods");
            Buttons.RemoveButton(Buttons.GetCategory("Main"), "Plugin Mods");

            if (wasFlying)
            {
                GorillaTagger.Instance.rigidbody.useGravity = true;
                wasFlying = false;
            }
        }



        private static void BarkFly()
        {
            if (!wasFlying)
            {
                GorillaTagger.Instance.rigidbody.useGravity = false;
                wasFlying = true;
            }

            Vector2 left = leftJoystick;
            Vector2 right = rightJoystick;

            if (!menu)
            {
                Transform headTransform = GorillaTagger.Instance.headCollider.transform;

                GorillaTagger.Instance.rigidbody.linearVelocity = Vector3.zero;

                GorillaTagger.Instance.rigidbody.transform.position += headTransform.forward * (Time.deltaTime * 10 * left.y);
                GorillaTagger.Instance.rigidbody.transform.position += headTransform.right * (Time.deltaTime * 10 * left.x);
                GorillaTagger.Instance.rigidbody.transform.position += headTransform.up * (Time.deltaTime * 10 * right.y);

                GorillaTagger.Instance.rigidbody.transform.position += Vector3.up * (Time.deltaTime * 0.135f);

                VRRig.LocalRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.headCollider.transform.rotation;
            }
        }


        public static void OnGUI() { }

    }
}