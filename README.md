# Silly Race Replica → Skyward Fold (Mach 1)
This project now contains the building blocks for **Skyward Fold (Mach 1)**, a hypercasual gate runner where a paper dart evolves into a hypersonic aircraft as it sprints down a bright minimalist track. Players choose between split-gate upgrades, auto-fire their weapons, and finish each level with an Overdrive assault on a cardboard fortress.

## Skyward Fold Gameplay Pillars
- **20-stage evolution ladder** across four eras (Desktop, Propeller, Jet, Stealth/Future) with tier-aware stage data and optional model swaps.
- **Gate-driven progression** for evolution jumps, firepower multipliers, and speed boosts, plus gravity gates and shredders for risk/reward decisions.
- **Auto-fire combat loop** that scales projectile count with firepower and switches to Overdrive at the finish line.
- **Final score system** based on evolution level × collected power-ups to reward aggressive gate choices.

## Scripts Added
The Unity scripts under `Project/Assets/Scripts/SkywardFold` provide the foundation for the new experience:
- `SkywardEvolutionSystem` maintains the 20 evolution stages and handles visual swaps.
- `SkywardGate` applies gate logic for evolution, speed, firepower, gravity, and shredders.
- `SkywardAutoFire` manages projectile firing and overdrive.
- `SkywardRunnerStats` tracks speed, firepower, and power-up counts for scoring and UI.
- `SkywardHUD` updates evolution and power-up UI text.
- `SkywardFinishLine` + `SkywardScoreReporter` trigger the destruction sequence scoring.

## Screenshots

<img src="https://user-images.githubusercontent.com/55920002/114303501-15d72700-9ad7-11eb-9a32-582c08c19d52.png"/>
<img src="https://user-images.githubusercontent.com/55920002/114303509-1a9bdb00-9ad7-11eb-8b5b-2cf1f7a5d29f.png"/>
<img src="https://user-images.githubusercontent.com/55920002/114303508-196aae00-9ad7-11eb-9703-6d793acf5c09.png"/>
<img src="https://user-images.githubusercontent.com/55920002/114303505-17a0ea80-9ad7-11eb-8ad5-146443dfa6f9.png"/>
<img src="https://user-images.githubusercontent.com/55920002/114303504-17a0ea80-9ad7-11eb-8ab6-33cd29538b9f.png"/>
<img src="https://user-images.githubusercontent.com/55920002/114303503-17085400-9ad7-11eb-9751-729229650780.png"/>
