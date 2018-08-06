# 2DMenus
An exploration into generating programmatic 2D menus in Unity

# Description
I've wanted to make a game involving VR for a while and I have zero experience in game dev. The game I want to do involves
procedurally generated objects and so it was necessary to look into generating objects programmatically in my game engine of choice. 
There were two main suggestions: Unity and Unreal Engine 4. Unity was chosen because most reviews suggested it for those new to game dev
and there is substantial documentation online.

All game objects are generated through code. The main scene calls a script which instantiates classes that load the game objects shown. 
There are no public GameObjects drug and dropped into the MonoBehaviour. While definitely not the easiest way to build a menu, it was 
necessary to explore how to programmatically generate GameObjects, attach them to other GameObjects, and attach functionality to the 
generated objects.

# Screenshots
![Main Menu](screenshots/MainMenu.png "Main Menu")

![Options Menu](screenshots/OptionsMenu.png "Options Menu")

# Current Capabilities
On the main menu you can go to the options menu or quit. From the options menu you can change any of the sliders and they affect the 
associated mixer. The back button works to take you back to the main menu. All buttons have a hover sound that plays as well (to test 
the SFX mixer). There is a background track that plays on loop which is attached to the Music mixer.

# Future Goals
Next is the Load window which would load a game. There is no game to save and therefore no game to load, so this is only going to be the 
window with buttons showing functionality through Debug.Log().
