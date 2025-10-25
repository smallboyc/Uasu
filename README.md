# Uasu

A Casca Studio video game.

## Scene Structure

### Main.unity:

Contains the main menu, options, and persistent systems (audio, saves, scene management).
Does not contain gameplay or level-specific elements.


### Level_X_Main.unity:

Empty scene that loads additively : 
- Level_X_Gameplay.unity
- Level_X_UI.unity
- Level_X_Environment.unity

#### Level_X_Gameplay.unity:

Contains characters, core mechanics, and interactions.
Use prefabs for enemies, interactive objects, etc.

#### Level_X_UI.unity:

Contains the HUD, inventory, and level-specific UI elements.

#### Level_X_Environment.unity:

Contains decor, platforms, visual effects, and manually placed enemies.