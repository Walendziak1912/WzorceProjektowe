# 🎯 Wzorce Projektowe w C# (.NET)

Kompletna kolekcja implementacji wzorców projektowych w C# z wykorzystaniem najnowszych technologii .NET 8, Entity Framework Core, Dependency Injection i Clean Architecture.

## 📋 Spis treści

- [Przegląd](#-przegląd)
- [Technologie](#️-technologie)
- [Wzorce projektowe](#-wzorce-projektowe)
- [Struktura projektu](#-struktura-projektu)
- [Instalacja i uruchomienie](#-instalacja-i-uruchomienie)
- [Przykłady użycia](#-przykłady-użycia)
- [Testy](#-testy)
- [Licencja](#-licencja)

## 🌟 Przegląd

To repozytorium zawiera praktyczne implementacje najpopularniejszych wzorców projektowych (Design Patterns) w ekosystemie .NET. Każdy wzorzec jest zaimplementowany jako osobny projekt z kompletnymi przykładami, testami jednostkowymi i dokumentacją.

### Główne założenia projektu:
- **Praktyczne przykłady** - każdy wzorzec ma realne zastosowanie biznesowe
- **Nowoczesne technologie** - .NET 8, EF Core, Dependency Injection
- **Clean Architecture** - separacja warstw i odpowiedzialności
- **Testowanie** - kompletne pokrycie testami jednostkowymi
- **Dokumentacja** - szczegółowe opisy implementacji

## 🛠️ Technologie

- **Framework**: .NET 8.0
- **Język**: C# 12
- **Baza danych**: SQL Server + Dapper/Entity Framework Core
- **Dependency Injection**: Microsoft.Extensions.DependencyInjection
- **Testy**: xUnit + Moq
- **Logowanie**: Microsoft.Extensions.Logging
- **Konfiguracja**: Options Pattern + appsettings.json

## 🏗️ Wzorce projektowe

### 1. 👁️ Observer (Obserwator)
**Lokalizacja**: `Obserwator_YouTube/`, `Obserwator_Suchy/`

Implementuje mechanizm powiadomień typu "publisher-subscriber". Przykład symuluje system powiadomień YouTube o nowych filmach.

**Kluczowe komponenty**:
- `IMailObserver` - interfejs obserwatora
- `Blog` - klasa subject zarządzająca obserwatorami
- `User` - konkretny obserwator

```csharp
public interface IMailObserver
{
    void Update();
}

public class Blog
{
    private List<IMailObserver> observers = new();
    
    public void Subscribe(IMailObserver observer) => observers.Add(observer);
    public void StartWork() => observers.ForEach(obs => obs.Update());
}
```

### 2. 🧩 Strategy (Strategia)
**Lokalizacja**: `Strategia_Kaczki/`, `Strategia_Sortowanie/`

Pozwala na dynamiczną zmianę algorytmu w czasie wykonania. Zawiera przykłady z symulacją zachowań kaczek oraz różnych algorytmów sortowania.

**Przykład sortowania**:
```csharp
public interface ISort
{
    int[] Sort(int[] array);
}

public class QuickSort : ISort { /* implementacja */ }
public class BubbleSort : ISort { /* implementacja */ }

public class Sortownia
{
    private ISort _sortStrategy;
    
    public void SetStrategy(ISort strategy) => _sortStrategy = strategy;
    public void SortElements(int[] array) => _sortStrategy.Sort(array);
}
```

### 3. 🎨 Decorator (Dekorator)
**Lokalizacja**: `DecoratorLoggingExample/`

Dodaje dodatkową funkcjonalność do istniejących obiektów bez modyfikacji ich struktury. Implementacja z wykorzystaniem Scrutor do automatycznego dekorowania.

**Kluczowe komponenty**:
```csharp
public interface IOrderProcessor
{
    void ProcessOrder(Order order);
}

public class LoggingOrderProcessorDecorator : IOrderProcessor
{
    private readonly IOrderProcessor _inner;
    private readonly ILogger _logger;
    
    public void ProcessOrder(Order order)
    {
        _logger.LogInformation("Rozpoczynam przetwarzanie...");
        _inner.ProcessOrder(order);
        _logger.LogInformation("Zakończono przetwarzanie");
    }
}
```

### 4. 🏭 Factory (Fabryka)
**Lokalizacja**: `PaymentGatewayFactory/`

Tworzy obiekty bez określania ich konkretnej klasy. Implementacja fabryki bramek płatności z pełną integracją z Dependency Injection.

**Architektura**:
```csharp
public interface IPaymentGatewayFactory
{
    IPaymentGateway GetGateway(CountryCode countryCode);
}

public class PaymentGatewayFactory : IPaymentGatewayFactory
{
    public IPaymentGateway GetGateway(CountryCode countryCode) => countryCode switch
    {
        CountryCode.PL => _serviceProvider.GetService<PayUGateway>(),
        CountryCode.US => _serviceProvider.GetService<PayPalGateway>(),
        _ => throw new NotSupportedException($"Kraj {countryCode} nie jest obsługiwany")
    };
}
```

### 5. ⚙️ Options Pattern
**Lokalizacja**: `OptionPatternDatabaseExample/`

Wzorzec konfiguracji aplikacji .NET z wykorzystaniem strongly-typed obiektów i walidacji. Implementuje Clean Architecture z pełnym cyklem CRUD.

**Konfiguracja**:
```csharp
public class DatabaseOptions
{
    public const string SectionName = "Database";
    
    [Required, MinLength(10)]
    public string ConnectionString { get; set; }
    
    [Range(1, 3600)]
    public int CommandTimeout { get; set; } = 30;
}

// Rejestracja w DI
services.Configure<DatabaseOptions>(config.GetSection(DatabaseOptions.SectionName));
services.AddOptions<DatabaseOptions>().ValidateDataAnnotations().ValidateOnStart();
```

## 📁 Struktura projektu

```
WzorceProjektowe/
├── 📂 Obserwator/                 # Wzorzec Observer
│   ├── Obserwator_YouTube/        # Przykład z powiadomieniami
│   └── Obserwator_Suchy/          # Klasyczna implementacja
├── 📂 Strategie/                  # Wzorzec Strategy  
│   ├── Strategia_Kaczki/          # Zachowania kaczek
│   └── Strategia_Sortowanie/      # Algorytmy sortowania
├── 📂 Dekorator/                  # Wzorzec Decorator
│   └── DecoratorLoggingExample/   # Dekorowanie z logowaniem
├── 📂 Factory/                    # Wzorzec Factory
│   ├── PaymentGatewayFactory/     # Fabryka bramek płatności
│   └── PaymentGatewayFactory.Tests/ # Testy jednostkowe
├── 📂 OptionPattern/              # Options Pattern
│   └── OptionPatternDatabaseExample/ # Clean Architecture + EF
├── 📄 README.md                   # Ten plik
└── 📄 WzorceProjektowe.sln       # Solution file
```

## 🚀 Instalacja i uruchomienie

### Wymagania
- .NET 8.0 SDK
- SQL Server (LocalDB lub pełna wersja)
- Visual Studio 2022 lub VS Code

### Kroki instalacji

1. **Klonowanie repozytorium**
```bash
git clone https://github.com/twoje-username/WzorceProjektowe.git
cd WzorceProjektowe
```

2. **Przywrócenie pakietów NuGet**
```bash
dotnet restore
```

3. **Budowanie rozwiązania**
```bash
dotnet build
```

4. **Konfiguracja bazy danych** (dla OptionPattern przykładu)
```sql
CREATE DATABASE MyAppDb;
USE MyAppDb;

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1
);
```

5. **Uruchomienie przykładów**
```bash
# Wzorzec Observer
cd Obserwator_YouTube
dotnet run

# Wzorzec Factory
cd PaymentGatewayFactory  
dotnet run

# Options Pattern
cd OptionPatternDatabaseExample
dotnet run
```

## 💡 Przykłady użycia

### Observer Pattern - System powiadomień
```csharp
var blog = new Blog();
blog.Subscribe(new User("Jan"));
blog.Subscribe(new User("Anna"));
blog.StartWork(); // Powiadamia wszystkich subskrybentów
```

### Strategy Pattern - Dynamiczna zmiana algorytmu
```csharp
var sortownia = new Sortownia(new QuickSort());
sortownia.SortujElementy(array);

sortownia.ZmienMetodeSortujaca(new BubbleSort());
sortownia.SortujElementy(array);
```

### Factory Pattern - Tworzenie bramek płatności
```csharp
var paymentService = serviceProvider.GetService<PaymentService>();
paymentService.ProcessPayment(100.0m, CountryCode.PL, paymentDetails);
// Automatycznie używa PayU dla Polski
```

### Decorator Pattern - Dodawanie logowania
```csharp
services.AddTransient<IOrderProcessor, CoreOrderProcessor>();
services.Decorate<IOrderProcessor, LoggingOrderProcessorDecorator>();
// Automatyczne owijanie w decorator z logowaniem
```

## 🧪 Testy

Projekty zawierają kompletne testy jednostkowe z wykorzystaniem xUnit i Moq.

### Uruchomienie testów
```bash
# Wszystkie testy
dotnet test

# Konkretny projekt
cd PaymentGatewayFactory.Tests
dotnet test --verbosity normal

# Z pokryciem kodu
dotnet test --collect:"XPlat Code Coverage"
```

### Przykład testu Factory Pattern
```csharp
[Fact]
public void GetGateway_ForPL_ReturnsPayUGateway()
{
    // Arrange
    var factory = _serviceProvider.GetRequiredService<IPaymentGatewayFactory>();
    
    // Act
    var gateway = factory.GetGateway(CountryCode.PL);
    
    // Assert
    Assert.IsType<PayUGateway>(gateway);
}
```

## 📚 Dodatkowe zasoby

- [Microsoft Docs - Design Patterns](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/)
- [Clean Architecture by Robert Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [.NET Dependency Injection](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)

## 🤝 Wkład w projekt

Zachęcamy do współtworzenia projektu! Jeśli chcesz dodać nowy wzorzec lub poprawić istniejący:

1. Utwórz fork repozytorium
2. Stwórz nową gałąź (`git checkout -b feature/nowy-wzorzec`)
3. Dodaj swoje zmiany z testami
4. Utwórz Pull Request

## 📄 Licencja

Projekt jest dostępny na licencji MIT. Zobacz plik [LICENSE](LICENSE) po szczegóły.

---

**Autor**: Walendziak1912
**Wersja**: 1.0.0

⭐ **Jeśli ten projekt Ci pomógł, zostaw gwiazdkę!** ⭐
