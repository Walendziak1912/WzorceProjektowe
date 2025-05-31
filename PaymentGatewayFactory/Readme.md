# PaymentGatewayFactory

## Opis projektu

PaymentGatewayFactory to przykładowa implementacja wzorca projektowego Factory (Fabryka) w C#. Projekt demonstruje, jak można wykorzystać wzorzec fabryki do tworzenia różnych implementacji bramek płatności (PayU, PayPal) w zależności od kodu kraju.

Projekt zawiera również kompletny zestaw testów jednostkowych, które pokazują, jak testować implementacje wzorca Factory oraz jak używać mocków do testowania zależności.

## Struktura projektu

Projekt składa się z dwóch części:
1. **PaymentGatewayFactory** - główny projekt implementujący wzorzec Factory
2. **PaymentGatewayFactory.Tests** - projekt testowy zawierający testy jednostkowe

### Główny projekt

Projekt jest zorganizowany zgodnie z architekturą warstwową:

- **Domain** - zawiera interfejsy i modele
  - `IPaymentGateway` - interfejs, który muszą implementować wszystkie bramki płatności
  - `IPaymentGatewayFactory` - interfejs fabryki bramek płatności
  - `PaymentDetails` - model zawierający szczegóły płatności

- **Application** - zawiera logikę biznesową
  - `PaymentGatewayFactory` - implementacja fabryki bramek płatności
  - `PaymentService` - serwis odpowiedzialny za operacje płatności

- **Infrastructure** - zawiera implementacje infrastruktury
  - `PayUGateway` - implementacja bramki płatności PayU
  - `PayPalGateway` - implementacja bramki płatności PayPal
  - `ServiceCollectionExtensions` - rozszerzenia do rejestracji usług w kontenerze DI

## Wzorce projektowe

### Factory (Fabryka)

Głównym wzorcem projektowym użytym w tym projekcie jest **Factory** (Fabryka). Wzorzec ten pozwala na tworzenie obiektów bez konieczności określania dokładnej klasy obiektu, który ma zostać utworzony.

W naszym przypadku, `PaymentGatewayFactory` tworzy odpowiednią implementację `IPaymentGateway` na podstawie kodu kraju (enum `CountryCode`):
- Dla kodu kraju `CountryCode.PL` tworzy `PayUGateway`
- Dla kodu kraju `CountryCode.US` tworzy `PayPalGateway`

Użycie enuma `CountryCode` zamiast stringów zapewnia typowanie i eliminuje możliwość błędów literówek w stringach.

### Dependency Injection (Wstrzykiwanie zależności)

Projekt wykorzystuje również wzorzec **Dependency Injection** (Wstrzykiwanie zależności) do zarządzania zależnościami między komponentami. Używamy wbudowanego kontenera DI z Microsoft.Extensions.DependencyInjection.

## Jak uruchomić

1. Upewnij się, że masz zainstalowany .NET 8.0 SDK
2. Sklonuj repozytorium
3. Otwórz terminal w katalogu projektu
4. Uruchom polecenie `dotnet build`, aby skompilować projekt
5. Uruchom polecenie `dotnet run`, aby uruchomić aplikację

## Przykład użycia

Program demonstracyjny pokazuje:
1. Konfigurację kontenera DI i rejestrację usług
2. Wykonanie płatności w USA przez PayPal
3. Wykonanie płatności w Polsce przez PayU
4. Zwrot płatności w USA przez PayPal
5. Pobranie statusu transakcji w Polsce przez PayU
6. Obsługę błędów dla nieobsługiwanych kodów krajów

## Rozszerzanie projektu

Aby dodać nową bramkę płatności:
1. Utwórz nową klasę implementującą interfejs `IPaymentGateway`
2. Zarejestruj nową bramkę w metodzie `AddPaymentGateways()` w klasie `ServiceCollectionExtensions`
3. Dodaj nowy przypadek w metodzie `GetGateway()` w klasie `PaymentGatewayFactory`
4. Dodaj testy jednostkowe dla nowej bramki płatności

## Testy jednostkowe

Projekt zawiera kompletny zestaw testów jednostkowych, które testują:

### PaymentGatewayFactoryTests
- Testy dla fabryki bramek płatności
- Sprawdzanie, czy fabryka zwraca odpowiednie implementacje dla różnych kodów krajów
- Testowanie obsługi błędów dla nieobsługiwanych kodów krajów, pustych stringów i wartości null

### PaymentServiceTests
- Testy dla serwisu płatności
- Używanie mocków do testowania zależności
- Weryfikacja, czy serwis wywołuje odpowiednie metody na bramkach płatności

### PaymentGatewayTests
- Testy dla konkretnych implementacji bramek płatności (PayU, PayPal)
- Sprawdzanie, czy metody wykonują się bez błędów
- Testowanie outputu konsolowego

### Jak uruchomić testy

1. Upewnij się, że masz zainstalowany .NET 8.0 SDK
2. Otwórz terminal w katalogu projektu testowego
3. Uruchom polecenie `dotnet test`, aby uruchomić wszystkie testy
4. Możesz również uruchomić testy z Visual Studio lub Visual Studio Code
