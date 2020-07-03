# AKDice

Dice roll simulations. See the "Doc.md" for more details, but in short:

## Usage

`using Anikaiful.Dice;`

## Statics

### Ranges
`int Dice.Range(int min, int max)`

`double Dice.Range(double min, double max)`

### Randoms
`bool Dice.ToBoolean(bool? tf)`

`bool Dice.Chance(int probability=50)`

## `int` Extensions
* `d2`, `d3`, `d4`, `d5`, `d6`, `d8`, `d10`, `d12`, `d20`, `d100`, colloquially `dExt`:

`int `someInt`.dExt(int mod=0, int probability=100)`
