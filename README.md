# 🏦 Aplikacja bankowa

Prosta aplikacja mobilna symulująca podstawowe funkcje bankowości elektronicznej. Umożliwia założenie konta, zarządzanie danymi i wykonywanie operacji na rachunku. Projekt powstał w celach edukacyjnych.

## ✨ O projekcie

Aplikacja pozwala użytkownikowi zarządzać rachunkiem i sprawdzać historię operacji. Dane użytkowników oraz transakcje są przechowywane lokalnie w bazie SQLite.

### Najważniejsze funkcje

- 👤 rejestracja i logowanie
- 💳 podgląd rachunku, dostępnych środków i statusu karty
- 💸 przelewy krajowe oraz przelewy BLIK i na telefon
- 🏧 symulacja wpłat i wypłat gotówki
- 📜 historia transakcji z wyszukiwaniem
- 🔐 zmiana hasła i zarządzanie danymi użytkownika
- ⚙️ ustawianie limitu płatności

## 🧱 Technologie

- **C# i .NET 10** – logika aplikacji
- **.NET MAUI** – aplikacja wieloplatformowa
- **XAML** – interfejs użytkownika
- **SQLite** – lokalne przechowywanie danych

## 📋 Wymagania

- Git
- **.NET 10 SDK**
- Workload .NET MAUI oraz narzędzia dla wybranej platformy:
  - **Android** – Android SDK i emulator lub urządzenie
  - **iOS / Mac Catalyst** – macOS i Xcode
  - **Windows** – Windows 10/11 oraz wymagane narzędzia Windows

## 🚀 Uruchomienie lokalne

### 1. Sklonuj repozytorium

```bash
git clone https://github.com/Bartek-Piechnik/Projekt-aplikacji-mobilnej-banku.git
cd Projekt-aplikacji-mobilnej-banku
```

### 2. Przygotuj .NET MAUI

Jeśli workload MAUI nie jest jeszcze zainstalowany, wykonaj:

```bash
dotnet workload install maui
```

Następnie przywróć zależności projektu:

```bash
dotnet restore "Aplikacja bankowa.slnx"
```

### 3. Uruchom aplikację

Wybierz platformę, skonfiguruj jej wymagane narzędzia i uruchom projekt:

```bash
# Android (wymaga uruchomionego emulatora lub podłączonego urządzenia)
dotnet build "Aplikacja bankowa/Aplikacja bankowa.csproj" -t:Run -f net10.0-android

# Mac Catalyst (macOS)
dotnet build "Aplikacja bankowa/Aplikacja bankowa.csproj" -t:Run -f net10.0-maccatalyst

# iOS (macOS, wymaga Xcode i skonfigurowanego symulatora lub urządzenia)
dotnet build "Aplikacja bankowa/Aplikacja bankowa.csproj" -t:Run -f net10.0-ios

# Windows
dotnet build "Aplikacja bankowa/Aplikacja bankowa.csproj" -t:Run -f net10.0-windows10.0.19041.0
```

Możesz też otworzyć plik `Aplikacja bankowa.slnx` w JetBrains Rider lub Visual Studio, wybrać platformę docelową i uruchomić projekt z IDE.

### 💾 Baza danych

Baza SQLite jest tworzona automatycznie na urządzeniu przy pierwszym użyciu aplikacji. Nie trzeba importować pliku ani konfigurować osobnego serwera. Konto można utworzyć z poziomu ekranu rejestracji.

## 📁 Struktura projektu

```text
├── Aplikacja bankowa/
│   ├── Data/        # obsługa lokalnej bazy SQLite
│   ├── Helpers/     # walidacja, formatowanie i funkcje pomocnicze
│   ├── Models/      # modele użytkowników i transakcji
│   ├── Platforms/   # konfiguracja platform mobilnych i desktopowych
│   ├── Resources/   # obrazy, fonty, ikony i style
│   └── Views/       # widoki aplikacji
├── Aplikacja bankowa.slnx
└── README.md
```

## 🎓 Informacja

Projekt ma charakter edukacyjny i służy do demonstracji podstawowych operacji bankowych. Nie jest prawdziwą aplikacją bankową i nie należy używać go do przechowywania rzeczywistych danych ani wykonywania rzeczywistych transakcji.
