using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

namespace SpellCooldowns
{
    class SpellCooldowns
    {
        private MenuItem shouldDrawAnything;

        public SpellCooldowns()
        {
            bindEvents();
        }

        public void bindEvents()
        {
            CustomEvents.Game.OnGameLoad += onGameLoad;
            Drawing.OnDraw += onDraw;
        }

        private void onGameLoad(EventArgs args)
        {
            createMenu();
        }

        private void onDraw(EventArgs args)
        {
            // return method instantly if the user selected to not draw anything
            if (!shouldDrawAnything.GetValue<bool>()) return;

            int lineCounter = 0;
            int topMargin = 100;

            foreach (var hero in ObjectManager.Get<Obj_AI_Hero>()
                    .Where(hero => hero != null && hero.IsValid)
                    .OrderBy(hero => !hero.IsAlly))
            {
                int topMarginModifier = 20;
                lineCounter++;
            
                // make a bigger gap between the two teams
                if (lineCounter == 5) topMarginModifier = 50;

                var champ = new Champion(hero);
                champ.drawTableEntry(100, topMargin);

                topMargin += topMarginModifier;
            }
        }

        private void createMenu()
        {
            var rootMenu = new Menu("SpellCooldowns", "spellCooldowns", true);
            var drawingsMenu = new Menu("Drawings", "drawings");
            shouldDrawAnything = new MenuItem("drawTimers", "Draw Timers");

            // set default values
            shouldDrawAnything.SetValue(true);

            // link everything
            rootMenu.AddSubMenu(drawingsMenu);
            drawingsMenu.AddItem(shouldDrawAnything);
            
            // append it to the main menu
            rootMenu.AddToMainMenu();
        }
    }
}
