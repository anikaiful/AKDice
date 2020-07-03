# AKDice

Dice roll simulations. See the "Doc.md" for more details, but in short:

## Usage

`using Anikaiful.Dice;`

## Statics

### Ranges
* `int Dice.Range(int min, int max)`
* `double Dice.Range(double min, double max)`
```
   int ir    = Dice.Range(3,11);
   double dr = Dice.Range(3.0,11.0);
```
### Randoms
* `bool Dice.ToBoolean(bool? tf)`
* `bool Dice.Chance(int probability=50)`

## `int` Extensions
`d2`, `d3`, `d4`, `d5`, `d6`, `d8`, `d10`, `d12`, `d20`, `d100`, all as per (hypotethical) `dExt` below:

* `int `someInt`.dExt(int mod=0, int probability=100)`

Ergo, `2.d6()`, `18d20(3)`, `3.d4(probability: 75)`, `(5.d6()).d10(-3, probability: 4)`, and so forth.

## `string` Extensions
```
    string s  = "string with 5d6+5 embedded".Evaluate();
    int i     = "5d6+5".Evaluate<int>();
    long l    = "5d6+5".Evaluate<long>();
    float f   = "5d6+5".Evaluate<float>();
    double d  = "5d6+5".Evaluate<double>();
    decimal m = "5d6+5".Evaluate<decimal>();
```
