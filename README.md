# Halloween Escape Room
A weekend-long escape room created for Halloween 2025.

## Overview
This project contains the Unity experience behind our escape room, integrating individual physical puzzles into a larger narrative. It was developed in Unity by members of CMU's Theme Park Engineering Group. 

## Features
- Finite State Machine to control game state based on Arduino inputs
- Synchronized UI and audio effects
- Serial communication with an Arduino RFID receiver
- Five RFID-powered interactive props

## System Design
Much of this system is inspired by a previous TPEG project, [Our 2025 Spring Carnival Booth Game](https://github.com/p0nk0/DinoGame). The RFID-scanning arduino code is the same as [the Arduino code](https://github.com/p0nk0/DinoArduinos) from that project. 

For guests to complete the room, they need to scan 5 objects using the cauldron's RFID reader to create a Hex Reversal Potion. Four of these objects are hidden around the room, and must be discovered via the recipe cards. The projection puzzle also has a few pieces hidden around the room, and results in the combination to unlock the final ingredient in the potion's recipe.
![Puzzle Diagram](Images/Puzzles.png)

These are the recipe cards from the room:
![Recipe Cards](Images/Recipes.png)

These are the 5 RFID-enabled props (only the moon tarot card contains an RFID tag):
![Props 1](Images/Props1.png)
![Props 2](Images/Props2.png)

## Setup
- Use Unity 6000.0.40f1
- Make sure the Cauldron's COM port and baud rate match the SerialController object
- To add/remove potion ingredients, edit validProps in GameStateManager.cs
- Press play!

## Media
![1](Images/1.png)
![2](Images/2.png)
![3](Images/3.png)
![4](Images/4.png)

## Credits
Unity Project Contributions
- Jacob Yakubisin: Arduino setup and testing
- Madhav Anugopal: Audio effects and narrative design

Escape Room Team Leads
- Carolyn Burback, Alva Huang: Theming
- Tay Padilla: Pepper's Ghost Puzzle
- Jacob Yakubisin: Projection Puzzle
- Mia Evans: Misc. Puzzles
- Carolyn Burback, Autumn Chan: Props
- Courtney Singleton: Recipe Cards
