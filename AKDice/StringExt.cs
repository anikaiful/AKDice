using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Anikaiful.Dice
{
    /// <summary>
    /// Some <see langword="string"/> extensions.
    /// </summary>
    static public class StringExt
    {
        /// <summary>
        /// Calculation modes for <see cref="CalcDo(string, CalcMode)"/>.
        /// </summary>
        private enum CalcMode { Add, Sub, Mul, Div }

        /// <summary>
        /// Evaluate basic math portion(s) of the given string.
        /// </summary>
        /// <param name="s">String.</param>
        /// <param name="evaluateMethod">Some <see cref="Dice.RollEvaluateMethod">roll evaluate method</see>.</param>
        /// <returns>Math-solved string.</returns>
        static public string Evaluate(this string s, Dice.RollEvaluateMethod evaluateMethod = Dice.RollEvaluateMethod.Default)
        {
            string res;

            // replace dice roll occurances
            res = Dice.Evaluate(s, evaluateMethod);

            // subeval within braces
            res = Regex.Replace(res, @"\(([^)(]*)\)", delegate (Match m)
            {
                return m.Groups[1].ToString().Evaluate();
            });

            // add, sub, mul, div, etc. in some sort of priority order.
            res = res.CalcDo(CalcMode.Mul);
            res = res.CalcDo(CalcMode.Div);
            res = res.CalcDo(CalcMode.Add);
            res = res.CalcDo(CalcMode.Sub);

            return res;
        }

        /// <summary>
        /// Evaluate basic math portion(s) of the given string.
        /// </summary>
        /// <typeparam name="T">Return type; supported:
        /// <see langword="int"/>,
        /// <see langword="long"/>,
        /// <see langword="float"/>,
        /// <see langword="double"/>,
        /// <see langword="decimal"/>.
        /// </typeparam>
        /// <param name="s">Some string w/ or w/o basic mathy things.</param>
        /// <param name="evaluateMethod">Some <see cref="Dice.RollEvaluateMethod">roll evaluate method</see>.</param>
        /// <returns>Something sensible?</returns>
        static public T Evaluate<T>(this string s, Dice.RollEvaluateMethod evaluateMethod = Dice.RollEvaluateMethod.Default)
        {
            if (typeof(T) == typeof(int))
                return (T)(object)int.Parse(s.Evaluate());
            if (typeof(T) == typeof(double))
                return (T)(object)double.Parse(s.Evaluate());
            if (typeof(T) == typeof(long))
                return (T)(object)long.Parse(s.Evaluate());
            if (typeof(T) == typeof(float))
                return (T)(object)float.Parse(s.Evaluate());
            if (typeof(T) == typeof(decimal))
                return (T)(object)decimal.Parse(s.Evaluate());

            throw new TypeAccessException("At the moment only these types are supported: int, long, float, double, decimal");
        }

        /// <summary>
        /// Do one mode of math within a string.
        /// </summary>
        /// <param name="s">Some string.</param>
        /// <param name="cm">Math mode to do.</param>
        /// <returns>Math-done string.</returns>
        static private string CalcDo(this string s, CalcMode cm)
        {
            return Regex.Replace(s,// and here, see the horrorific beauty of regex...
                $@"(?<digit1>([0-9]+[.])?[0-9]+)\s*[{cm switch{
                    CalcMode.Add => @"+",
                    CalcMode.Sub => @"-",
                    CalcMode.Div => @"/",
                    _ => @"*"
                }}]\s*(?<digit2>([0-9]+[.])?[0-9]+)",
                delegate (Match m)
                {
                    string sd1 = m.Groups[@"digit1"].ToString();
                    string sd2 = m.Groups[@"digit2"].ToString();

                    if (sd1.Contains('.') || sd2.Contains('.'))
                    {// solve decimal numbers
                        decimal d1 = decimal.Parse(sd1);
                        decimal d2 = decimal.Parse(sd2);
                        return cm switch
                        {
                            CalcMode.Mul => (d1 * d2).ToString(),
                            CalcMode.Div => (d1 / d2).ToString(),
                            CalcMode.Add => (d1 + d2).ToString(),
                            _ => (d1 - d2).ToString()
                        };
                    }
                    else
                    {// solve integers
                        long d1 = long.Parse(sd1);
                        long d2 = long.Parse(sd2);
                        return cm switch
                        {
                            CalcMode.Mul => (d1 * d2).ToString(),
                            CalcMode.Div => (d1 / d2).ToString(),
                            CalcMode.Add => (d1 + d2).ToString(),
                            _ => (d1 - d2).ToString()
                        };
                    }
                });
        }
    }
}
