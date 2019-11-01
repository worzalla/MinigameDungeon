# MinigameDungeon
Project for CS 506 Software Enginering at UW-Madison

## Git structure
Master is the version which is never broken and has good quality code. All code on master should be submitted via a pull request and have gone through peer code reviews when submitted to dev, integration testing on dev, and should be the final version of whatever feature you're working on.
Dev is our expected final version. All code on dev should be submitted via a pull request and be code reviewed before submitted. It should be the final version of whatever you're working on.
Feature branches can be made and deleted by the person who made it. Feature branches should be associated with an issue. If there isn't an issue already, create one and name the branch after it. 

## Build instructions
To build, first go to and install Unity Hub.
Then, go to https://unity3d.com/unity/beta/2019.3.0b7 and download Unity 2019.3b7.
Open Unity Hub, create an account, activate your license, and then open Unity.

To play the game, go to the resources window in Unity, and navigate to Assets > _Project > Scenes > Evans
To build for PC, go to File > Build Settings, select "PC, Mac and Linux Standalone" and click the "Build and Run" button.
To build for Android, go to Unity Hub and install Android Build Support, Android SDK & NDK Tools, and OpenJDK. Then, in Unity, go to File > Build Settings, select Android, plug in a suitable Android phone, enable USB Debugging on the android phone, and click Build and Run.
