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
    class Champion
    {
        private Obj_AI_Hero champ;
        private int ultimateCooldown;
        private int summoner1Cooldown;
        private int summoner2Cooldown;
        private string summoner1Name;
        private string summoner2Name;

        public Champion(Obj_AI_Hero hero)
        {
            this.champ = hero;
            this.getCooldowns();
        }

        private void getCooldowns()
        {
            var ultimateSpell = this.champ.Spellbook.GetSpell(SpellSlot.R);
            this.ultimateCooldown = Convert.ToInt32(ultimateSpell.CooldownExpires - Game.Time);

            var summoner1Spell = this.champ.Spellbook.GetSpell(SpellSlot.Summoner1);
            this.summoner1Cooldown = Convert.ToInt32(summoner1Spell.CooldownExpires - Game.Time);
            this.summoner1Name = mapSummoner(summoner1Spell.Name);

            var summoner2Spell = this.champ.Spellbook.GetSpell(SpellSlot.Summoner2);
            this.summoner2Cooldown = Convert.ToInt32(summoner2Spell.CooldownExpires - Game.Time);
            this.summoner2Name = mapSummoner(summoner2Spell.Name);
        }

        public void drawTableEntry(int x, int y)
        {
            string outputTemplate = "{0}:   R{1}   {2}{3}   {4}{5}";
            string text = String.Format(outputTemplate, this.champ.ChampionName,
                                                        cooldownInBrackets(ultimateCooldown),
                                                        this.summoner1Name,
                                                        cooldownInBrackets(summoner1Cooldown),
                                                        this.summoner2Name,
                                                        cooldownInBrackets(summoner2Cooldown));

            Drawing.DrawText(x, y, decideTextColor(), text);
        }

        private Color decideTextColor()
        {
            int decider = (ultimateCooldown > 0 ? 1 : 0) + (summoner1Cooldown > 0 ? 1 : 0) + (summoner2Cooldown > 0 ? 1 : 0);
            switch (decider)
            {
                case 0:
                    return Color.Red;
                case 1:
                    return Color.Orange;
                case 2:
                    return Color.Yellow;
                case 3: 
                    return Color.LightGreen;
                default:
                    return Color.White;
            }
        }

        private string cooldownInBrackets(int time)
        {
            return (time > 0) ? " (" + time + "s)" : "";
        }

        private string mapSummoner(string name)
        {
            switch (name)
            {
                case "summonerodingarrison":
                    return "Garrison";
                case "summonerrevive":
                    return "Revive";
                case "summonerclairvoyance":
                    return "Clairvoyance";
                case "summonerboost":
                    return "Cleanse";
                case "summonermana":
                    return "Clarity";
                case "summonerteleport":
                    return "Teleport";
                case "summonerheal":
                    return "Heal";
                case "summonerexhaust":
                    return "Exhaust";
                case "summonersmite":
                    return "Smite";
                case "summonerdot":
                    return "Ignite";
                case "summonerhaste":
                    return "Ghost";
                case "summonerflash":
                    return "Flash";
                default:
                    return "Barrier";
            }
        }
    }
}
