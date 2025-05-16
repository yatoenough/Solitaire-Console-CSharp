# 🃏 Pasjans (C#)

W pełni funkcjonalna, wieloplatformowa gra karciana Solitaire, działająca w terminalu. Zbudowana w C# z czystą architekturą, wsparciem dla dwóch języków i minimalnymi zależnościami

---

## 📦 Wymagania

- [.NET 8.0 lub nowszy](https://dotnet.microsoft.com/)
- 1 zależność NuGet (patrz poniżej)

---

## 🔧 Instalacja i Uruchomienie

```bash
# Zbuduj projekt
dotnet build

# Uruchom grę
dotnet run
```
---

## 🎮 Sterowanie

- Strzałki góra/dół (w menu) - Przesuń wskaźnik wyboru
- Enter (w menu) - Wybierz opcję z menu


- Strzałki lewo/prawo - Przesuń wskaźnik wyboru
- Enter - Wybierz kartę lub potwierdź ruch
- Backspace - Anuluj wybór
- B - Cofnij ostatni ruch
- R - Restartuj grę
- Q - Wyjdź z gry
- D - Dobierz kartę z talii
- P - Podnieś kartę ze stosu rezerwowego
- F - Umieść kartę na stosie końcowym

---

## 🧪 Zależności NuGet
Używana jest tylko jedna paczka NuGet:
 - Figgle 0.5.1 do wyświetlania banerów tekstowych
 
---

## 🗂️ Struktura Projektu i Opis Klas

- ### 📁 Config/
  - Settings.cs - Przechowuje konfigurację gry

- ### 📁 Core/
    - #### 📁 Engine/
      - DeckManager.cs — Zarządza inicjalizacją i tasowaniem talii
      - MoveManager.cs — Przechowuje ruchy gracza i logikę gry
      - MoveValidator.cs — Sprawdza legalność ruchów zgodnie z zasadami pasjansu
      - Pointer.cs — Śledzi kursor w wybranej kolumnie w terminalu
      - ScoreboardStore.cs — Zapisuje i pobiera wyniki gier

    - #### MenuCore/
      - Menu.cs - Klasa abstrakcyjna dla implementowania dowolnych menu
      - MenuOptionPicker - Śledzi wybraną opcję w menu
      - #### Options/
		- MenuOption.cs - Klasa abstrakcyjna dla implementowania opcji menu
		- Pozostałe elementy w tym folderze są implementacjami MenuOption
	  - #### Implementations/
		- Implementacje klasy Menu

    - #### 📁 Models/
      - Card.cs — Reprezentuje pojedynczą kartę (kolor, ranga, itd.)
      - Column.cs — Reprezentuje kolumnę (stos) kart na planszy
      - Deck.cs — Reprezentuje pełną talię kart
      - GameResult.cs — Reprezentuje wynik gry (data, liczba ruchów)
      - Move.cs — Reprezentuje pojedynczy ruch gracza, w tym źródło i cel
      - MoveType.cs — Enum definiujący typy ruchów (np. dobieranie, przenoszenie)
      - Suit.cs — Enum definiujący kolory kart (Kier, Pik, itd.)

    - #### 📁 Rendering/
      - GameRenderer.cs — Odpowiada za rysowanie stanu gry w terminalu

    - #### 📁 Utils/
        - ListExt.cs — Zawiera metody rozszerzające dla List (np. tasowanie)
        - 
    - #### Game.cs — Główna pętla logiki gry; zarządza przebiegiem gry
    - #### GameState.cs — Przechowuje aktualny stan gry (układ kart, wynik, ruchy, itd.)

- ### 📁 I18n/

  - GameStrings.resx — Domyślne angielskie napisy

  - GameStrings.pl.resx — Polska wersja językowa interfejsu

- ### Program.cs
  - Punkt wejścia aplikacji

## Have Fun!
