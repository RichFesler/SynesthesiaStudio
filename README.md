# SynesthesiaStudio

**A real-time audiovisual environment engine driven by modular synthesis and MIDI.**

SynesthesiaStudio is an experimental Unity project exploring the relationship between generative electronic music and dynamic, continuously evolving 3D environments.

Rather than producing conventional audio-reactive visualizations, the system uses musical modulation signals to influence natural and artificial environments.

Wind, fog, lighting, vegetation, particles, wildlife, and procedural animations can respond to the underlying structure of music while maintaining autonomous behavior.

## Project Status

**v0.1 — MIDI-to-Environment Proof of Concept**

The first prototype, *LivingForest*, successfully demonstrates real-time control of a Unity/Gaia wind system using control signals generated in VCV Rack 2.

Verified working:

- VCV Rack generates continuous modulation using an LFO.
- MIDI CC messages travel through a Windows virtual MIDI port.
- Unity receives MIDI values through Minis.
- A modulation component processes normalized MIDI values.
- A separate output adapter controls Unity's WindZone.
- Gaia vegetation responds to the changing wind strength.

Currently, the implementation supports one configured MIDI CC input and one processed modulation channel.

## System Architecture

```text
VCV Rack 2
    |
    | MIDI CC
    v
loopMIDI / SynthLink
    |
    v
Unity / Minis
    |
    v
MidiMonitor
    |
    v
ModulationEngine
    |
    v
WindAdapter
    |
    v
Unity WindZone
    |
    v
Gaia Vegetation
```

The architecture separates signal acquisition, processing, and environmental output. This allows future scenes to share the same control infrastructure.

## Core Components

### MidiMonitor.cs

Receives MIDI Continuous Controller messages using the Minis library.

The initial implementation monitors MIDI CC 20 and exposes its normalized value.

### ModulationEngine.cs

Transforms the incoming control value using adjustable parameters:

- Offset
- Multiplier
- Inversion
- Minimum and maximum limits
- Rate-limited smoothing

These parameters can be adjusted in Unity's Inspector.

### WindAdapter.cs

Connects the processed modulation output to Unity's native `WindZone.windMain` property.

The adapter contains no MIDI-specific logic.

## Requirements

Development environment:

- Windows 11
- Unity 2022.3 LTS
- Built-in Render Pipeline
- Unity Input System
- Minis 1.3.2
- VCV Rack 2
- loopMIDI

The LivingForest demonstration also uses Gaia for Unity 2021 by Procedural Worlds.

The three included scripts do not require Gaia to compile; the wind adapter targets Unity's native WindZone component.

## Basic Setup

1. Create a Unity 2022.3 Built-in Render Pipeline project.
2. Install the Unity Input System and Minis.
3. Add the scripts from `Assets/SynesthesiaStudio/`.
4. Create a virtual MIDI port named `SynthLink` using loopMIDI.
5. Configure VCV Rack's CV-CC module to send MIDI CC 20 to that port.
6. Connect an LFO to the corresponding CV-CC input.
7. Create a Unity GameObject containing MidiMonitor, ModulationEngine, and WindAdapter.
8. Assign the component references and a WindZone.
9. Enter Play mode and verify that wind strength changes with the incoming modulation.

The repository does not currently include a complete demonstration scene.

## Development Roadmap

### Phase 1 — MIDI Control

- [x] Receive MIDI CC messages in Unity
- [x] Implement basic modulation processing
- [x] Control Unity WindZone
- [x] Demonstrate real-time vegetation response

### Phase 2 — Universal Modulation Matrix

- [ ] Multiple independent MIDI CC inputs
- [ ] Multiple output destinations
- [ ] Adjustable response curves
- [ ] Independent attack and release
- [ ] Multiple modulation sources per destination
- [ ] MIDI Learn
- [ ] Preset saving and loading
- [ ] Runtime configuration interface

### Phase 3 — Environmental Adapters

- [ ] Lighting
- [ ] Fog and atmospheric density
- [ ] Particle systems
- [ ] Material parameters
- [ ] Object transforms
- [ ] Animation parameters
- [ ] Procedural event triggers
- [ ] Probabilistic environmental behaviors

### Phase 4 — Demonstration Environments

**LivingForest**

Natural forest environment featuring animated vegetation, wind, fog, clouds, lighting, and wildlife.

**DarkLaboratory**

Electrical discharges, Jacob's ladders, ground fog, sparks, and atmospheric lighting.

**Underwater**

Water currents, bubbles, suspended particles, moving light patterns, and marine life.

## Design Philosophy

Music influences the environment; it does not dictate every movement.

Environmental systems should maintain autonomous motion and natural variation even when incoming musical signals remain constant.

The ultimate goal is a reusable visual instrument capable of interpreting generative music through entirely different virtual worlds.

## Third-Party Assets

This repository contains original integration scripts and does not distribute commercial Unity Asset Store packages.

Gaia, vegetation
