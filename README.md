EnChant-it is an immersive VR game developed with Unity, where players use voice commands to cast spells and navigate a magical world. Dive into an enchanting adventure and unleash your inner sorcerer!

## Table of Contents
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Gameplay Instructions](#gameplay-instructions)
- [Contact](#contact)

## Features
- **Voice Control**: Use natural language commands to interact with the game world.
- **Immersive VR Experience**: Fully integrated VR environment providing a captivating gaming experience.
- **Dynamic Spells**: Cast various spells by speaking incantations.

### Prerequisites
- Unity 2022.3.10f1 or later
- Oculus Integration Package
- VR headset (Oculus Quest 2)
	- Make sure the wit.ai (voice folder) is imported as well.
- Universal Render Pipeline


### Open in Unity
1. In the installations folder you will find a unity package.
2. Create a new 3D project in Unity with Universal Render Pipeline (URP)
3. Install the Oculus Integration Package on the Unity Store 
https://assetstore.unity.com/packages/tools/integration/oculus-integration-deprecated-82022
4. Under Assets -> Import Package -> Custom Package, Add the package in the Installations folder
or use the github url https://github.com/EHB-MCT/EnChant-It.git 
5. Set up your URP in projects settings, Under Edit -> Project Settings -> Graphics tab -> Scriptabke Render Pipeline Settings & Under Edit -> Project Settings -> Quality tab -> Rendering Pipeline Assset

## Usage/Development
### Starting the Game
1. Launch Unity: Open the Unity editor with the EnChant-it project.
2. Connect VR Headset: Ensure your Oculus Quest 2 is properly connected to your development PC.
3. Run the Game: Click on the play button in Unity to start the game in the editor or build and run on your Oculus Quest 2.
4. Voice Setup: Ensure your microphone is properly set up to allow voice command detection.

## Configuration
### Audio Settings
1. Microphone Setup: Ensure your microphone is set as the default input device on your PC or VR headset.
2. Voice Sensitivity: Adjust the voice command sensitivity in the game settings to ensure optimal spell detection.
2.1 This can be done by going to the Hierachy in the Unity scene -> Managers -> VoiceManagers -> App Voice Experience

### Graphics Settings
1. Quality Settings: Choose the appropriate quality settings for your system under Edit -> Project Settings -> Quality tab.
2. Render Pipeline: Ensure the Universal Render Pipeline is correctly configured under Edit -> Project Settings -> Graphics tab.

## Gameplay Instructions
### Casting Spells
1. Learn Spells: Throughout the game, you'll discover new spells. Pay attention to the incantations.
2. Speak Clearly: clearly say the spell incantation.
3. Spell Effects: Observe the effects of your spells and use them strategically to progress in the game.

### Navigating the World
1. Exploration: Explore the magical world, find hidden objects.
2. Objectives: Follow the quest objectives to advance the storyline/tutorial.

### Building the Game
1. Build Settings: Open File -> Build Settings and select the appropriate platform (e.g., Android for Oculus Quest).
1.1 Ensure that the Texture Compression in Build Settings is ETC2 (GLES 3.0) and ETC2 fallback is 32-bit.
2. Player Settings: Configure player settings to ensure compatibility with VR.
3. Build and Run: Click on Build and Run to deploy the game to your Oculus Quest 2.

## Contact
Questions or feedback contact me:
Mail adress: jens.willems@student.ehb.be
