using System;
using System.Text.RegularExpressions;

namespace Anikaiful.Dice
{
    /// <summary>
    /// Dice rolls and randomness for the masses.
    /// </summary>
    static public class Dice
    {
        /// <summary>
        /// Evaluate methods:
        /// * Default
        /// * Minimize - minimize roll values.
        /// * Maximize - maximize roll values.
        /// </summary>
        public enum RollEvaluateMethod { Default, Minimize, Maximize }

        /// <summary>
        /// Have no fear, RNG is here!
        /// </summary>
        static private readonly Random _rng_ = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Throw an exception instead of silently clamping out-of-bounds probability values?
        /// </summary>
        static public bool ThrowProbabilityOutOfRange = false;

#pragma warning disable IDE1006
        /// <summary>
        /// Roll a <paramref name="n">number</paramref> of dice with a defined number of <paramref name="s">sides</paramref>.
        /// </summary>
        /// <param name="n">Number of rolls.</param>
        /// <param name="s">Sides per die.</param>
        /// <returns>Some <see langword="int"/>.</returns>
        /// <remarks>If <paramref name="s"/> is <c>&lt;1</c>, return will always be <c>0</c>.</remarks>
        static internal int d_(int n, int s)
        {
            // no dice? No sides?
            if (n == 0 || s <= 0)
                return 0;
            
            if (s == 1)// this's very one-sided...
                return n;

            bool sign = n < 0;
            if (sign)
                n = -n;

            int result = 0;
            for (int i = 0; i < n; i++)
                result += Range(1, s);

            return sign ? (-result) : (+result);
        }
#pragma warning restore IDE1006

        /// <summary>
        /// Get a random number in the given range.
        /// </summary>
        /// <param name="d1">Minimum value returned.</param>
        /// <param name="d2">Maximum value returned.</param>
        /// <returns></returns>
        static public double Range(double d1, double d2)
        {
            if (d1 == d2) return d1;

            // swap if needed
            if (d1 > d2)
            {
                double t = d1;
                d1 = d2;
                d2 = t;
            }

            return d1 + (_rng_.NextDouble() * (d2 - d1));
        }

        /// <summary>
        /// Get a random number in the given range.
        /// </summary>
        /// <param name="i1">Minimum value returned.</param>
        /// <param name="i2">Maximum value returned.</param>
        /// <returns>Some <see langword="int"/>.</returns>
        static public int Range(int i1, int i2)
        {
            if (i1 == i2) return i1;

            // swap if needed
            if (i1 > i2)
            {
                int t = i1;
                i1 = i2;
                i2 = t;
            }

            return _rng_.Next(i1, i2);
        }

        /// <summary>
        /// 
        /// </summary>
        static public bool high { get => _rng_.Next(2) > 0; }
        static public bool low { get => !high; }

        /// <summary>
        /// Convert a <see langword="bool?"/> into <see langword="bool"/>ean value.
        /// </summary>
        /// <remarks><c>null</c> is randomly converted to either <c>true</c> or <c>false</c>.</remarks>
        /// <param name="b"></param>
        /// <returns><c>true</c>|<c>false</c></returns>
        static public bool ToBoolean(bool? b) => b ?? high;

        /// <summary>
        /// Roll versus a percentage chance.
        /// </summary>
        /// <param name="thisOrBelow">Success if roll is below-or-equal-to this number</param>
        /// <returns>Success</returns>
        static public bool Chance(int probability = 50) => 1.d100() <= probability;

        /// <summary>
        /// Roll (all, if any) dice representations within given string.
        /// </summary>
        /// <param name="s">String</param>
        /// <returns>A (possibly modified) string.</returns>
        static internal string Evaluate(string s, RollEvaluateMethod evaluateMethod = RollEvaluateMethod.Default)
            => Regex.Replace(s, @"([1-9][0-9]*)?[dD]([1-9][0-9]*)", delegate (Match match)
            {
                int v = (match.Groups[1].Length > 0)
                    ? int.Parse(match.Groups[1].ToString())
                    : 1;
                return evaluateMethod switch
                {
                    RollEvaluateMethod.Minimize => v.ToString(),
                    RollEvaluateMethod.Maximize => (v * int.Parse(match.Groups[2].ToString())).ToString(),
                    _ => d_(v, int.Parse(match.Groups[2].ToString())).ToString(),
                };
            });

        /// <summary>
        /// Validate a probability value.
        /// </summary>
        /// <param name="v">Some integer.</param>
        /// <returns>An integer, <paramref name="v"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <see cref="ThrowProbabilityOutOfRange"/>
        /// is set to <code>true</code> and <paramref name="v"/> is outside 0..100 range.</exception>
        static private int ValidateProbability(int v)
        {
            if (ThrowProbabilityOutOfRange && (v < 0 || v > 100))
                throw new ArgumentOutOfRangeException($"Probability value {v} is outside of 0..100 range!");
            return v;
        }

#pragma warning disable IDE1006
        static public int d2(this int c, int mod = 0, int probability = 100) => (d_(c, 2) + mod).Probability(probability);
        static public int d3(this int c, int mod = 0, int probability = 100) => (d_(c, 3) + mod).Probability(probability);
        static public int d4(this int c, int mod = 0, int probability = 100) => (d_(c, 4) + mod).Probability(probability);
        static public int d5(this int c, int mod = 0, int probability = 100) => (d_(c, 5) + mod).Probability(probability);
        static public int d6(this int c, int mod = 0, int probability = 100) => (d_(c, 6) + mod).Probability(probability);
        static public int d8(this int c, int mod = 0, int probability = 100) => (d_(c, 8) + mod).Probability(probability);
        static public int d10(this int c, int mod = 0, int probability = 100) => (d_(c, 10) + mod).Probability(probability);
        static public int d12(this int c, int mod = 0, int probability = 100) => (d_(c, 12) + mod).Probability(probability);
        static public int d20(this int c, int mod = 0, int probability = 100) => (d_(c, 20) + mod).Probability(probability);
        static public int d100(this int c, int mod = 0, int probability = 100) => (d_(c, 100) + mod).Probability(probability);
        static public int d_(this int c, int s, int mod = 0, int probability = 100) => (d_(c, s) + mod).Probability(probability);
#pragma warning restore IDE1006

        /// <summary>
        /// Value or <c>0</c>.
        /// </summary>
        /// <param name="n">Some value.</param>
        /// <param name="probability">Probability of value staying non-<c>0</c>.</param>
        /// <returns><paramref name="n"/> or <c>0</c></returns>
        static public int Probability(this int n, int probability) => (d_(1, 100) <= ValidateProbability(probability)) ? n : 0;

        /// <summary>
        /// Value or negated value.
        /// </summary>
        /// <param name="b">Some <see langword="bool"/>.</param>
        /// <param name="probability">Probability of <paramref name="b"/> staying what it is.</param>
        /// <returns><paramref name="b"/> or its opposite value.</returns>
        static public bool Probability(this bool b, int probability) => (d_(1, 100) <= ValidateProbability(probability)) ? b : !b;
    }
}
