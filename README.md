# Mirror Split Screen

Uses the new input system for local split screen multiplayer with network players.

![Short demo](https://github.com/GartzenDeHaes/MirrorSplitScreen/blob/main/media/spltscreen01.gif?raw=true)

## Running The Demo

To run the Mirror Tanks split screen demo:

0. You'll need a supported gamepad such as an XBox or dual shock controller.

1. Clone or download the repository and open in Unity (currently 2019, but newer versions should work).

2. Open the demo scene in /ThirdParty/TanksSplitScreen/Scenes/SplitScreenTanksScene

3. Hit run. (The demo scene is setup so that only the keyboard and mouse are active until the networked game starts). 

4. Click Host (Server+Client)

5. Push a button on your controller to join the second local player.

6. To test joining network players:

- Go to "File>Build Settings" and remove any existing scenes in the build.

- Add the SplitScreenTanksScene.

- Build

- Note: To use a gamepad controller with two games running locally, you'll need to disable background input processing.  Otherwise, your controller will join both games.

![Unity Input Settings](https://github.com/GartzenDeHaes/MirrorSplitScreen/blob/main/media/inputsettings.png?raw=true)

## Security

This system works by allowing clients to spawn new players. Probably not what you want on a public facing service.

## Other Notes

A development version of Mirror is included for convience, but you should use their asset store builds in your system.
