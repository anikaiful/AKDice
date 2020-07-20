# AKDice

Dice roll simulations. See the more in depth [documentation](AKDice/Doc.md) and/or [usage primer](AKDice/README.md).

A ready-to-roll version of AKDice is present at [NuGet.org](https://nuget.org/packages/AKDice).

Bugs may or may not exist, and thus this library comes without any sort of warranties - YMMV.

# TL;DR
* See [usage primer](AKDice/README.md).
* Visit [NuGet.org](https://nuget.org/packages/AKDice).

# Change Log?
## 3.1.5.5 to 3.1.5.8
* Added `Maximize` and `Minimize` string extensions.
* Fixed a semi-critical oversight in `Evaluate` method...
## 3.1.5.4 to 3.1.5.5
No API changes, just minor alterations.
## 3.1.5.3 to 3.1.5.4
* `string` extension `Evaluate()` now handles negative roll expressions properly, f.ex. in `"foobarbaz -1d10 bazbarfoo".Evaluate()`.
