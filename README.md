# PetRenamer

![Icon](https://raw.githubusercontent.com/direwolf420/PetRenamer/master/icon.png)

Terraria Forum link: https://forums.terraria.org/index.php?threads/pet-renamer-allows-you-to-name-your-pets.79293/

**If you wish to use this mod on a tModLoader version prior to 0.11, download [this](https://github.com/direwolf420/PetRenamer/releases/tag/v1.0.0.3)!**

Allows the player to name the summon item of any pet, so that the pet will show its name when hovered over it (similar to how NPCs show their name)

There are two ways to use this mod: UI and chat command

## UI
How to use:
* Press the hotkey (default: 'P')
* Place a pet summoning item in it
* Type a name into the text box
* (optional) Press 'Random' for a random name
* (optional) Press 'Clear' to delete the text
* Press 'Apply' to set the text from the text box as the name for the pet
* (optional) Take out the item
* Press 'X' to close. Item will be returned to you if it's still in the UI

Notes:
* If you hold a pet summoning item in your cursor and press the hotkey, the item will be automatically placed in the UI
* If you leave the UI with the item in it open and close the game, it will appear again next time you play

Demonstration:

![UI](https://raw.githubusercontent.com/direwolf420/PetRenamer/master/Images/ui.png)

[Showcase (gfycat video)](https://gfycat.com/totalformalindochinesetiger)

## Chat Command
How to use:
* Either open your chat (default: Enter), then left click on a summon item (so it sticks to your cursor), or hold an item in your cursor and then open your chat
* The chat will then say '/renamepet ', the following words you type in will be accepted as the new pet name
* It will also save the name of the player that gave the pet its last nickname
* To remove the name, simply type 'reset' as the name (yes, you can name your pet 'reset' aswell, but only if it doesn't have a name currently)

Notes:
* Pet names will be saved on the item, even when disabling the mod and enabling it again
* Multiplayer compatible
* Compatible with most modded pets if they do it the vanilla way:
    * If a summon item happens to summon multiple different pets at once, only one of them will be named
    * If the pet name hover text only shows up in a specific position, contact that other mods dev so he can fix the pets hitbox
    * (For example some Thorium Mod pets only show a name when hovered over the top left corner of the sprite)

Example:

`/renamepet Mr. Bones` >> sets or renames the name of the pet item you are holding to 'Mr. Bones'

`/renamepet reset` >> deletes the current name

Demonstration (gfycat videos):

[Setting a name](https://gfycat.com/unsteadysplendidannelid)

[Removing a name](https://gfycat.com/flickeringringediraniangroundjay)

Credits:
* jopojelly for UI
* darthmorf for UI help

Changelog:

v1.1.2.1: Possibly fix rare bug with mouseovering a pet in multiplayer

v1.1.2: Add a config + setting for disabling the chat autofill feature

v1.1.1.2: Fix rare bug with mouseovering a pet in multiplayer

v1.1.1.1: When opening the UI and the mouse item slots in, the text field now auto-focuses

v1.1.1: Using the UI allows you to randomize the name, over 10 million possible names

v1.1: An additional way to rename a pet through a UI accessed via hotkey

v1.0.0.5: tml 0.11.7 update

v1.0.0.4: tml 0.11 update, command says the correct thing now when not holding a pet item

v1.0.0.3: Buff tip of the summoned pet now also displays its name

v1.0.0.2: Narrowed down detection of pet summoning items, if you had any previous items set to a name that weren't a pet, they will be reset upon game quit

v1.0 and v1.0.0.1: Initial release and added icon
