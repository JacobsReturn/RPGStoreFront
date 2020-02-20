# RPGStoreFront
My roleplay store for AIE. Which includes many items to purchase and sell, you can visit the store owned by Garlvash.

## Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites
This console app requires .NET Core 3.1 Framework and Visual Studio.

### Installing
Download the [.zip](https://github.com/JacobsReturn/RPGStoreFront/archive/1.0.0.zip "Source Download Page") or download the current direct [repository](https://github.com/JacobsReturn/RPGStoreFront "Repository Page").

## Developers
This project is using .Net and is coded in C#.

### Accessing the namespace
There are a few ways to access the namespace (in order to use the classes, methods, variables and so fourth).

#### First way
```C#
namespace RPGStoreSimulator
{
}
```
#### Second way
```C#
using RPGStoreSimulator;
```
### Variables
Here are a list of variables that are available under RPGStoreSimulator.Program.
(unless you are using a Program derivative, you must use the Program. suffix on all of these variables below)

| Variable Name | Class/Type    | Output |
| ------- |:-------------:| ------- |
| commandList | Commands | Array |
| itemList | BaseItem | Array |
| user | Player | Player Class |
| shop | Store | Store Class |
| itemReference | BaseItem | BaseItem Class |
| repo | string | Directory String |
| RarityColours | string | 2D Array of 5,2 |

### Base Classes
| Class | File | Description |
| ------- |:---------:| ------- |
| Commands | Classes/Commands/Commands.cs | The base for creating commands. |
| BaseItem | Classes/Inventory/BaseItem.cs | The base for creating items. |
| InventoryBase | Classes/Inventory/InventoryBase.cs | Used for the creation of the player and stores inventory. |
| Table | Classes/NoList.cs | My own take on lists which follows the assignments criteria. |
| Library | Library.cs | A library I created for easy transfer of useful methods to other projects. |
| Program | Program.cs | The main class used as a parent for every other class made which inherits the library. |

### Creating items
#### Usage:
```C#
CreateItem(string name, string description, int cost, string category, int rarity, string[] stats)
```
#### Example:
```C#
CreateItem("Frost Staff", "Allows the wielder to shoot powerful ice bolts. The bolts apply frostbite.", 1300, "Magic Staff", 1, 
    new string[5]
    { 
        "- Direct Damage: 500",
        "- Splash Damage: 100",
        "- Projectile Speed: 63u/s.",
        "- Applies <[\x03FE] Frostbite> for 10s.",
        "            \x25B2 Deals 35 magic damage/s.",
    }
);
```

### Creating commands
Create a new .cs file and makesure it includes the namespace somehow.

Create a new class, make sure it inherits/derives from the "Commands" class.
Public the commands starter, and use 
```C# 
this.SetCommand(string command, string description)
``` 
inside of it.

Then to have it run when executed, override Execute(string arg), string arg is an argument that can be broken into other arguments by using the Split method.

After you have created your class, makesure to input a new ClassName() into the Program.commandList, you can either do it in the Program.cs main file or insert it into the array by resizing it etc.

#### Example
```C#
class HelpCommand : Commands
{
    public HelpCommand()
    {
        this.SetCommand("/help", "To get some help.");
    }

    public override void Execute(string arg)
    {
        Print("Here are a list of commands: ", ConsoleColor.Cyan);
        foreach (Commands command in commandList)
        {
            if (command.stringCommand != "/help")
            {
                Print(textSpacing + "- " + command.stringCommand, ConsoleColor.White);
                Print(textSpacing + "  " + command.commandHelp, ConsoleColor.Blue);
                Space();
            }
        }
    }
}
```

### If you would like any other developer help, please search through the files, the methods and so on should be well commented and easy to understand for even a beginner.
Thank you for reading my readme, and thank you for possibly using this application.
