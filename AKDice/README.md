# AKDice

Dice roll simulations. See the [Doc.md](Doc.md) for more details, but in short:

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
* `Probability (int|string probability, int otherwise=0)`
```
  int r1 = 5.d6();
  int r2 = 5.d6(5);
  int r3 = 5.d6(5, 75);
  int r4 = 5.d6(mod: 5, probability: 75);
  int r5 = 5.d6(probability: 75);
  int r6 = (5.d6(5, 1.d100())).d6(probability: 25);
  int p1 = 10.Probability(50);
  int p2 = 25.Probability(75, /*otherwise:*/ 10);
  // lambda arg:
  int f1 = 85.Probability(() => 1.d20(), /* otherwise: */ 0); // 85% chance for 1.d20(), otherwise 0
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
`Evaluate` methods have their corresponding `Maximize` and `Minimize` variants, e.g.
```
  int mi = "5d10+5".Minimize<int>();
  int mx = "5d10+5".Maximize<int>();
```
## `bool` Extensions
```
  bool b1 = true.Probability(75);       // 75% chance for b1==true, 25% for b1==false
  bool b2 = (b1==true).Probability(33); // ;-) ... yeah, go figure ...
  bool b3 = true.Probability("50");	// 50% chance for b3 to be true
```
