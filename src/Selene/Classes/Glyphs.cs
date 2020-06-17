using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selene.Glyphs
{
    public enum BatteryLevels
    {
        Battery0 = '\uE850',
        Battery1 = '\uE851',
        Battery2 = '\uE852',
        Battery3 = '\uE853',
        Battery4 = '\uE854',
        Battery5 = '\uE855',
        Battery6 = '\uE856',
        Battery7 = '\uE857',
        Battery8 = '\uE858',
        Battery9 = '\uE859',
        Battery10 = '\uE83F',
        BatteryCharging0 = '\uE85A',
        BatteryCharging1 = '\uE85B',
        BatteryCharging2 = '\uE85C',
        BatteryCharging3 = '\uE85D',
        BatteryCharging4 = '\uE85E',
        BatteryCharging5 = '\uE85F',
        BatteryCharging6 = '\uE860',
        BatteryCharging7 = '\uE861',
        BatteryCharging8 = '\uE862',
        BatteryCharging9 = '\uE83E',
        BatteryCharging10 = '\uEA93',
        BatterySaver0 = '\uE863',
        BatterySaver1 = '\uE864',
        BatterySaver2 = '\uE865',
        BatterySaver3 = '\uE866',
        BatterySaver4 = '\uE867',
        BatterySaver5 = '\uE868',
        BatterySaver6 = '\uE869',
        BatterySaver7 = '\uE86A',
        BatterySaver8 = '\uE86B',
        BatterySaver9 = '\uEA94',
        BatterySaver10 = '\uEA95',
        BatteryUnknown = '\uE996',
        VerticalBattery0 = '\uF5F2',
        VerticalBattery1 = '\uF5F3',
        VerticalBattery2 = '\uF5F4',
        VerticalBattery3 = '\uF5F5',
        VerticalBattery4 = '\uF5F6',
        VerticalBattery5 = '\uF5F7',
        VerticalBattery6 = '\uF5F8',
        VerticalBattery7 = '\uF5F9',
        VerticalBattery8 = '\uF5FA',
        VerticalBattery9 = '\uF5FB',
        VerticalBattery10 = '\uF5FC',
        VerticalBatteryCharging0 = '\uF5FD',
        VerticalBatteryCharging1 = '\uF5FE',
        VerticalBatteryCharging2 = '\uF5FF',
        VerticalBatteryCharging3 = '\uF600',
        VerticalBatteryCharging4 = '\uF601',
        VerticalBatteryCharging5 = '\uF602',
        VerticalBatteryCharging6 = '\uF603',
        VerticalBatteryCharging7 = '\uF604',
        VerticalBatteryCharging8 = '\uF605',
        VerticalBatteryCharging9 = '\uF606',
        VerticalBatteryCharging10 = '\uF607',
        VerticalBatteryUnknown = '\uF608'
    }

    public enum Audio
    {
        MusicNote = '\uEC4F',
        MusicInfo = '\uE90B',
        Play = '\uE768',
        Pause = '\uE769',
        Previous = '\uE892',
        Next = '\uE893',
        Volume = '\uE767',
        Volume0 = '\uE992',
        Volume1 = '\uE993',
        Volume2 = '\uE994',
        Volume3 = '\uE995',
    }

    public class GlyphMethods
    {
        /// <summary>
        /// Determines the glyph number for the given battery percentage. 
        /// </summary>
        /// <param name="percent">Current battery percentage.</param>
        /// <returns>A battery glyph number.</returns>
        public static int DetermineGlyphNumber(double percent)
        {
            return (int)Math.Floor((percent + 4) / 10);
        }

        /// <summary>
        /// Gives the glyph that represents the current battery charge.
        /// </summary>
        /// <param name="percent">Current battery percentage</param>
        /// <returns>A string containing the glyph.</returns>
        public static string BatteryNormalGlyph(double percent)
        {
            int percentRange = DetermineGlyphNumber(percent);
            BatteryLevels level;

            switch (percentRange)
            {
                case 0:
                    level = BatteryLevels.VerticalBattery0;
                    break;
                case 1:
                    level = BatteryLevels.VerticalBattery1;
                    break;
                case 2:
                    level = BatteryLevels.VerticalBattery2;
                    break;
                case 3:
                    level = BatteryLevels.VerticalBattery3;
                    break;
                case 4:
                    level = BatteryLevels.VerticalBattery4;
                    break;
                case 5:
                    level = BatteryLevels.VerticalBattery5;
                    break;
                case 6:
                    level = BatteryLevels.VerticalBattery6;
                    break;
                case 7:
                    level = BatteryLevels.VerticalBattery7;
                    break;
                case 8:
                    level = BatteryLevels.VerticalBattery8;
                    break;
                case 9:
                    level = BatteryLevels.VerticalBattery9;
                    break;
                case 10:
                    level = BatteryLevels.VerticalBattery10;
                    break;
                default:
                    level = BatteryLevels.VerticalBattery0;
                    break;
            }

            return char.ConvertFromUtf32((int)level);
        }

        /// <summary>
        /// Gives the glyph that represents the current battery charge 
        /// and that the battery is currently charging.
        /// </summary>
        /// <param name="percent">Current battery percentage</param>
        /// <returns>A string containing the glyph.</returns>
        public static string BatteryChargingGlyph(double percent)
        {
            int percentRange = DetermineGlyphNumber(percent);
            BatteryLevels level;

            switch (percentRange)
            {
                case 0:
                    level = BatteryLevels.VerticalBatteryCharging0;
                    break;
                case 1:
                    level = BatteryLevels.VerticalBatteryCharging1;
                    break;
                case 2:
                    level = BatteryLevels.VerticalBatteryCharging2;
                    break;
                case 3:
                    level = BatteryLevels.VerticalBatteryCharging3;
                    break;
                case 4:
                    level = BatteryLevels.VerticalBatteryCharging4;
                    break;
                case 5:
                    level = BatteryLevels.VerticalBatteryCharging5;
                    break;
                case 6:
                    level = BatteryLevels.VerticalBatteryCharging6;
                    break;
                case 7:
                    level = BatteryLevels.VerticalBatteryCharging7;
                    break;
                case 8:
                    level = BatteryLevels.VerticalBatteryCharging8;
                    break;
                case 9:
                    level = BatteryLevels.VerticalBatteryCharging9;
                    break;
                case 10:
                    level = BatteryLevels.VerticalBatteryCharging10;
                    break;
                default:
                    level = BatteryLevels.VerticalBatteryCharging0;
                    break;
            }

            return char.ConvertFromUtf32((int)level);
        }
    }
}
