# AKDice 3.1.x
# Using
`using Anikaiful.Dice;`
# Notes
All "probability values" are automatically clamped into (0 .. 100, inclusive) range unless `Dice.ThrowProbabilityOutOfRange = true` is set.
# Static Functions
## `double Dice.Range(double d1, double d2)`
Returns a `double` in range (`d1` .. `d2`, inclusive).
## `int Dice.Range(int i1, int i2)`
Returns an `int` in range (`i1` .. `i2`, inclusive).
## `bool Dice.ToBoolean(bool? b)`
Returns either `b` (if not `null`) or a random `true`|`false` value.
## `bool Dice.Chance(int probability = 50)`
Returns `true` if a random black-box roll with given `probability` so dictates, otherwise `false`.
# Extensions to `int`
`#` represents any valid `int` value (constant or otherwise), e.g. in form `5.d6()` or `(x+y*z).d20()`. Result of these extensions depends on `probability`; return is either the result of the roll or 0 (zero). `mod` value is applied to final result (if `probability` doesn't clamp the result to zero).
* `int #.d2(int mod=0, int probability=100)`
... toss an imaginary D2 (or basically a coin?).
* `int #.d3(int mod=0, int probability=100)`
... toss an imaginary D3 (more or less equivalent to D6/2 with real dice).
* `int #.d4(int mod=0, int probability=100)`
... toss a D4.
* `int #.d5(int mod=0, int probability=100)`
... toss an imaginary D5 (more or less equivalent to D10/2 with real dice).
* `int #.d6(int mod=0, int probability=100)`
... toss a D6.
* `int #.d8(int mod=0, int probability=100)`
... toss a D8.
* `int #.d10(int mod=0, int probability=100)`
... toss a D10.
* `int #.d12(int mod=0, int probability=100)`
... toss a D12.
* `int #.d20(int mod=0, int probability=100)`
... toss a D20.
* `int #.d100(int mod=0, int probability=100)`
... toss a D100 (yea, these dice do exist, too).

Internally the above extensions refer to...
* `int #.Probability(int probability)`
... which returns either `#` itself or zero, depending.

## Extensions to `bool`
`#` refers to any valid `bool` value.
* `bool #.Probability(int probability)`
... return `#` if `probability` so dictates, otherwise `!#`.

## Extensions to `string`
`#` refers to any valid `string` value.
* `string #.Evaluate(Dice.RollEvaluateMethod method = Dice.RollEvaluateMethod.Default)`
Evaluates all dice roll expressions and/or simple calculation ops (plus, minus, multiply, divide) embedded in the free-form string `#`, returning a new string. By default `Dice.RollEvaluateMethod.Default` is used as `method`. At times you might need minimum/maximum value of the expression(s), and for that purpose there is `Dice.RollEvaluateMethod.Minimize` and `Dice.RollEvaluateMethod.Maximize` respectively. Note that multiply and divide work internally with `double` values and thus YMMV with their result precision.
* `T #.Evaluate`<`T`>`(Dice.RollEvaluateMethod method = Dice.RollEvaluateMethod.Default)`
As per `#.Evaluate()` but with result represented as `T`. Note that this will work (only) when `T` is:
        `int`, `long`, `float`, `double`, `decimal`.

# Examples
    var a = 3.d6(); // throw 3d6
    var b = 4.d4(3); // throw 4d4+3
    var c = true.Probability(35); // 35% probability to result in "true", 65% "false"
    var d = (a+b).d8(probability: 75); // 75% probability to result in "(a+b)d8", 25% in zero.
    var s = "This string contains dice expression 18d7+8 and so on.".Evaluate();
    //  s = "This string contains dice expression # and so on.", where # is the result of 18d7+8.

# TODO, etc.
* Get `#.Evaluate()` to work with various forms of probability expressions.
