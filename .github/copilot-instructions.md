# Copilot Instructions for RimWorld Modding Project

## Mod Overview and Purpose
This mod aims to enhance the agricultural mechanics in RimWorld by introducing a system that synchronizes plant growth within designated grow zones. It provides players with greater control and efficiency in managing their crops, ultimately leading to more strategic gameplay and resource management.

## Key Features and Systems
- **Plant Growth Synchronization**: The core feature of this mod is the synchronization of plant growth stages within specified groups, ensuring that plants within the same grow zone mature at the same time.
- **Efficient Resource Management**: By aligning plant growth cycles, players can harvest simultaneously, optimizing labor and time management in the colony.
- **Dynamic Grow Zone Management**: The mod intelligently groups plants based on existing grow zones, allowing for seamless integration with current colony layouts.

## Coding Patterns and Conventions
- **Project Structure**: The project adheres to RimWorld's modding standards, separating code into logical components based on functionality.
- **Class Naming**: Classes follow PascalCase naming, and a meaningful naming scheme is used to reflect their responsibilities (e.g., `PlantGrowerGrowthSyncer` for growth synchronization functions).
- **Method Naming**: Methods are named using camelCase and reflect their specific operations (e.g., `buildPlantGrowerGroups`).

## XML Integration
The mod likely interacts with XML files to define various game components, such as plant types, grow zones, and other XML-based configurations common in RimWorld mods. While XML specifics are not provided, ensure XML files are structured properly and that keys and attributes align with the game's modding API.

## Harmony Patching
- **Purpose of Harmony**: Harmony is used to patch RimWorld's base methods, allowing the mod to alter or extend existing game functionality without directly modifying the game's assembly.
- **Using Harmony**: Ensure that patches are non-destructive and revert correctly. Consider prefix and postfix patching methods where appropriate to maintain game stability.

## Suggestions for Copilot
- **Code Retrieval**: Copilot can assist in generating helper functions and repetitive code structures common in this mod, like getters and setters for plant properties or debugging output.
- **XML Parsing**: Use Copilot to help generate snippets for parsing and modifying XML data files, facilitating smoother integration with the game's configurations.
- **Harmony Patching Assistance**: Copilot can suggest standard as well as unique Harmony patch patterns required for modifying core game behavior to sync plant growth stages.

By ensuring these conventions and best practices are followed, contributions to the mod’s development can be more seamlessly integrated while maintaining code quality and game stability.
