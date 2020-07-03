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
```
  bool b1 = Dice.ToBoolean(true);   // == true
  bool b2 = Dice.ToBoolean(null);   // randomly true|false.
  bool b3 = Dice.Chance(75);        // 75% chance to result in true.
```
## `int` Extensions
* `d2 (int mod=0, int probability=100)`
* `d3 (...)`
* `d4 (...)`
* `d5 (...)`
* `d6 (...)`
* `d8 (...)`
* `d10 (...)`
* `d12 (...)`
* `d20 (...)`
* `d100 (...)`, all as per `d2` above.
```
  int r1 = 5.d6();
  int r2 = 5.d6(5);
  int r3 = 5.d6(5, 75);
  int r4 = 5.d6(mod: 5, probability: 75);
  int r5 = 5.d6(probability: 75);
  int r6 = (5.d6(5, 1.d100())).d6(probability: 25);
```
## `string` Extensions
```
  string s  = "string with 5d6+5 embedded".Evaluate();
  int i     = "5d6+5".Evaluate<int>();
  long l    = "5d6+5".Evaluate<long>();
  float f   = "5d6+5".Evaluate<float>();
  double d  = "5d6+5".Evaluate<double>();
  decimal m = "5d6+5".Evaluate<decimal>();
```
## `bool` Extensions
```
  bool b1 = true.Probability(75);       // 75% chance for b1==true, 25% for b1==false
  bool b2 = (b1==true).Probability(33); // ;-) ... yeah, go figure ...
```
