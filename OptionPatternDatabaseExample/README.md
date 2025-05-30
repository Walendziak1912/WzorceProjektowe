Aplikacja konsolowa zbudowana w architekturze Clean Architecture z wykorzystaniem .NET 8, Entity Framework, Options Pattern i Dependency Injection.


Options Pattern w .NET to implementacja wzorca projektowego Builder Pattern (Wzorzec Budowniczy) oraz częściowo Strategy Pattern (Wzorzec Strategii).
Options Pattern to eleganckie połączenie kilku wzorców, które zapewnia czytelną i łatwo testowalną konfigurację aplikacji .NET.

Główne powiązania wzorcowe:
Builder Pattern - Options Pattern pozwala na stopniowe budowanie konfiguracji przez dodawanie kolejnych opcji:
services.Configure<MyOptions>(options =>
{
    options.Property1 = "value1";
    options.Property2 = 42;
    options.NestedOption = new NestedConfig { Setting = true };
});


Strategy Pattern - Umożliwia wybór różnych strategii konfiguracji w zależności od środowiska:
// Różne strategie dla różnych środowisk
services.Configure<DatabaseOptions>(Configuration.GetSection("Database"));
services.Configure<DatabaseOptions>("Production", prodConfig);
services.Configure<DatabaseOptions>("Development", devConfig);

## 🎯 Przegląd

Aplikacja służy do przetwarzania użytkowników z bazy danych i wysyłania powiadomień email. Implementuje wzorce Clean Architecture, Repository Pattern oraz Options Pattern dla konfiguracji.

### Główne funkcjonalności

- Pobieranie aktywnych użytkowników z bazy danych SQL Server
- Przetwarzanie użytkowników według reguł biznesowych
- Wysyłanie powiadomień email
- Logowanie operacji
- Konfiguracja przez pliki appsettings.json

## 🏗️ Architektura

Projekt został zorganizowany w Clean Architecture z następującymi warstwami:
┌─────────────────────────────────────┐
│ MyApp.ConsoleApp │ ← Entry Point + DI Configuration
├─────────────────────────────────────┤
│ MyApp.Application │ ← Business Logic + Use Cases
├─────────────────────────────────────┤
│ MyApp.Infrastructure │ ← External Concerns + Data Access
├─────────────────────────────────────┤
│ MyApp.Domain │ ← Core Business Rules + Entities
└─────────────────────────────────────┘

### Zasady architektury

- **Domain** - rdzeń biznesowy bez zależności zewnętrznych
- **Application** - logika aplikacji, implementuje use case'y
- **Infrastructure** - implementacje dostępu do danych i serwisów zewnętrznych
- **ConsoleApp** - punkt wejścia, konfiguracja DI i uruchomienie

Utwórz bazę danych i tabelę Users:

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

-- Przykładowe dane testowe
INSERT INTO Users (Name, Email, CreatedAt, IsActive) VALUES
('Jan Kowalski', 'jan.kowalski@example.com', GETDATE(), 1),
('Anna Nowak', 'anna.nowak@example.com', DATEADD(day, -10, GETDATE()), 1),
('Piotr Zalewski', 'piotr.zalewski@example.com', DATEADD(day, -50, GETDATE()), 0);
```

## ⚙️ Konfiguracja

### 1. Connection String

Edytuj plik `MyApp.ConsoleApp/appsettings.json`:

```json
{
  "Database": {
    "ConnectionString": "Server=localhost;Database=MyAppDb;Trusted_Connection=true;TrustServerCertificate=true;",
    "CommandTimeout": 30,
    "EnableRetryOnFailure": true
  }
}
```

### 2. Konfiguracja Email (opcjonalne)

Dla Gmail z App Password:

```json
{
  "Email": {
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "FromEmail": "noreply@myapp.com"
  }
}
```

### 3. Reguły biznesowe

```json
{
  "Business": {
    "MaxDaysOld": 30,
    "BatchSize": 100,
    "SendEmailNotifications": true
  }
}
```

### 4. Konfiguracja środowiskowa

**Development** (`appsettings.Development.json`):

```json
{
  "Database": {
    "ConnectionString": "Server=localhost;Database=MyAppDb_Dev;Trusted_Connection=true;"
  },
  "Business": {
    "SendEmailNotifications": false
  }
}

## 📁 Struktura projektu

```

MyApp.Solution/
├── README.md
├── MyApp.sln
│
├── MyApp.Domain/ # 🎯 Core Domain
│ ├── Entities/
│ │ └── User.cs # Encje biznesowe
│ ├── Interfaces/
│ │ └── IUserRepository.cs # Kontrakty dla repozytoriów
│ └── Services/
│ └── UserDomainService.cs # Czysta logika biznesowa
│
├── MyApp.Application/ # 💼 Business Logic
│ ├── Interfaces/
│ │ ├── IUserService.cs # Kontrakty serwisów
│ │ └── IEmailService.cs
│ ├── Services/
│ │ └── UserService.cs # Implementacja use case'ów
│ └── Configuration/
│ └── BusinessOptions.cs # Konfiguracja reguł biznesowych
│
├── MyApp.Infrastructure/ # 🔧 Technical Implementation
│ ├── Configuration/
│ │ ├── DatabaseOptions.cs # Konfiguracja bazy danych
│ │ └── EmailOptions.cs # Konfiguracja email
│ ├── Repositories/
│ │ └── UserRepository.cs # Implementacja dostępu do danych
│ └── Services/
│ └── EmailService.cs # Implementacja serwisów zewnętrznych
│
└── MyApp.ConsoleApp/ # 🚀 Entry Point
├── Program.cs # Konfiguracja DI + uruchomienie
├── appsettings.json # Konfiguracja główna
├── appsettings.Development.json # Konfiguracja dev
└── appsettings.Production.json # Konfiguracja prod

````

### Wzorce i praktyki

- **Clean Architecture** - separacja warstw
- **Dependency Injection** - odwrócenie zależności
- **Repository Pattern** - abstrakcja dostępu do danych
- **Options Pattern** - strongly-typed konfiguracja

### Testowanie

Struktura przygotowana pod testy jednostkowe:

// Przykład testu serwisu
[Test]
public async Task ProcessUsersAsync_ShouldProcessActiveUsers()
{
    // Arrange
    var mockRepository = new Mock<IUserRepository>();
    var mockEmailService = new Mock<IEmailService>();
    var options = Options.Create(new BusinessOptions { MaxDaysOld = 30 });

    var service = new UserService(mockRepository.Object, mockEmailService.Object,
        Mock.Of<ILogger<UserService>>(), options);

    // Act & Assert
    await service.ProcessUsersAsync();
    mockRepository.Verify(r => r.GetActiveUsersAsync(), Times.Once);
}

### Rozszerzenia

- **Entity Framework Core** - dla bardziej zaawansowanego ORM
- **MediatR** - dla CQRS pattern
- **FluentValidation** - dla zaawansowanej walidacji
- **Polly** - dla retry policies
- **Serilog** - dla structured logging

## 📄 Licencja

MIT License - zobacz plik LICENSE dla szczegółów.

**Autor:** [Walendziak1912]  
**Wersja:** 1.0.0
