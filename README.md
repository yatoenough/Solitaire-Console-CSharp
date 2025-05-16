# 🃏 Solitaire CLI Game (C#)

A fully-functional, cross-platform **Solitaire card game** that runs in the terminal. Built in C# with clean architecture, internationalization support, and minimal dependencies.

---

## 📦 Requirements

- [.NET 8.0 or later](https://dotnet.microsoft.com/)
- 1 NuGet package (see below)

---

## 🔧 Setup & Run

```bash
# Build the project
dotnet build

# Run the game
dotnet run
```
---

## 🎮 Controls

Use the keyboard to navigate and interact with the game:

- **Up/Down arrow key**(in menu) - Move selection pointer
- **Enter**(in menu) - Select option in menu


- **Left/Right arrow key** - Move selection pointer
- **Enter** - Select card or confirm move
- **Backspace** - Cancel selection
- **B** - Undo last move
- **R** - Restart game
- **Q** - Quit the game
- **D** - Draw card from stock pile
- **P** - Pick card from waste pile
- **F** - Put card to foundation

---

## 🧪 NuGet Dependencies
Only one NuGet dependency is used:
 - Figgle 0.5.1 for displaying text banners

---

## 🗂️ Project Structure & Class Overview

- ### 📁 Config/
  - Settings.cs - Stores configuration for the game

- ### 📁 Core/
    - #### 📁 Engine/
      - DeckManager.cs — Manages deck initialization, shuffling, etc.
      - MoveManager.cs — Stores card movements and game logic steps. 
      - MoveValidator.cs — Validates the legality of a move based on Solitaire rules. 
      - Pointer.cs — Tracks the cursor on selected column in the terminal UI. 
      - ScoreboardStore.cs — Saves and retrieves game scores

    - #### MenuCore/
      - (Add class descriptions here if any files exist inside this folder.)

    - #### 📁 Models/
      - Card.cs — Represents a single playing card (suit, rank, etc.). 
      - Column.cs — Represents a column (pile) of cards in the game layout. 
      - Deck.cs — Represents the full deck of cards. 
      - GameResult.cs — Represents game result (date, moves count). 
      - Move.cs — Represents a single player move, including source and destination. 
      - MoveType.cs — Enum defining different move types (e.g., draw, transfer). 
      - Suit.cs — Enum defining suits (Hearts, Spades, etc.).

    - #### 📁 Rendering/
      - GameRenderer.cs — Responsible for drawing the game state in the terminal.

    - #### 📁 Utils/
        - ListExt.cs — Contains extension methods for List<T> (e.g., shuffle).
        - 
    - #### Game.cs — The main game logic loop; orchestrates gameplay flow.
    - #### GameState.cs — Maintains the current state of the game (board layout, score, moves, etc.).

- ### 📁 I18n/

  - GameStrings.resx — Default English strings for UI and messages.

  - GameStrings.pl.resx — Polish translation of UI strings (localized resources).

- ### Program.cs
  - Entry point of the application

---

## 🌐 Features

- CLI-based Solitaire gameplay
- Full keyboard interaction
- Scoreboard
- Multilingual support (English + Polish)
- Easily extensible architecture
