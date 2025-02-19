# Order Service

Bu proje, modern e-ticaret uygulamaları için tasarlanmış bir sipariş yönetim mikroservisidir. Domain-Driven Design (DDD) ve Clean Architecture prensipleri kullanılarak geliştirilmiştir.

## Teknolojiler

### Backend
- **.NET 8.0**: Ana framework
- **ASP.NET Core**: Web API ve MVC uygulamaları için
- **Entity Framework Core 8.0**: ORM ve veritabanı işlemleri için
- **PostgreSQL**: Ana veritabanı
- **MediatR 12.2.0**: CQRS implementasyonu için
- **FluentValidation**: Veri doğrulama için
- **Serilog**: Loglama için
- **Swagger/OpenAPI**: API dokümantasyonu için

### Mesajlaşma ve Event Driven Architecture
- **Apache Kafka**: Event bus implementasyonu için
- **Confluent.Kafka**: Kafka client olarak

### Frontend
- **ASP.NET Core MVC**: Web arayüzü için
- **Bootstrap 5**: UI framework
- **Bootstrap Icons**: İkonlar için

## Proje Yapısı

Proje, Clean Architecture ve DDD prensiplerine uygun olarak katmanlı bir yapıda tasarlanmıştır:

```
src/
├── Services/
│   └── OrderService/
│       ├── OrderService.API/          # REST API katmanı
│       ├── OrderService.Application/  # Uygulama iş mantığı
│       ├── OrderService.Domain/       # Domain modeller ve iş kuralları
│       ├── OrderService.Infrastructure/ # Veritabanı ve dış servis entegrasyonları
│       └── OrderService.Web/          # Web UI (MVC)
```

### Katmanlar

- **Domain Layer**: İş domaininin çekirdek mantığını içerir
- **Application Layer**: Use-case'leri ve uygulama mantığını içerir
- **Infrastructure Layer**: Teknik detayları ve implementasyonları içerir
- **API Layer**: REST API endpoints'lerini içerir
- **Web Layer**: Kullanıcı arayüzünü içerir

## Özellikler

- ✅ CQRS pattern implementasyonu
- ✅ Event-driven architecture
- ✅ Domain events
- ✅ Validation pipeline
- ✅ Exception handling middleware
- ✅ Structured logging
- ✅ Modern UI tasarımı
- ✅ Responsive web arayüzü

## Veritabanı Şeması

### Orders Tablosu
```sql
CREATE TABLE "Orders" (
    "Id" uuid PRIMARY KEY,
    "OrderNumber" varchar(100),
    "UserId" uuid,
    "TotalAmount" decimal(18,2),
    "Status" integer,
    "OrderDate" timestamp,
    -- Shipping Address
    "ShippingAddress_Street" varchar(100),
    "ShippingAddress_City" varchar(50),
    "ShippingAddress_State" varchar(50),
    "ShippingAddress_Country" varchar(50),
    "ShippingAddress_ZipCode" varchar(10)
);
```

### OrderItems Tablosu
```sql
CREATE TABLE "OrderItems" (
    "Id" uuid PRIMARY KEY,
    "OrderId" uuid,
    "ProductId" uuid,
    "ProductName" varchar(100),
    "UnitPrice" decimal(18,2),
    "Quantity" integer,
    "TotalPrice" decimal(18,2),
    FOREIGN KEY ("OrderId") REFERENCES "Orders" ("Id")
);
```

## Kurulum

1. PostgreSQL'i yükleyin ve çalıştırın
2. Kafka'yı Docker ile başlatın:
```bash
docker-compose up -d
```

3. Veritabanını oluşturun:
```sql
CREATE DATABASE OrderDb;
```

4. Veritabanı tablolarını oluşturun (SQL scriptlerini çalıştırın)

5. API projesini başlatın:
```bash
cd src/Services/OrderService/OrderService.API
dotnet run
```

6. Web projesini başlatın:
```bash
cd src/Services/OrderService/OrderService.Web
dotnet run
```

## API Endpoints

- `GET /api/Orders/user/{userId}`: Kullanıcının siparişlerini listeler
- `GET /api/Orders/{id}`: Belirli bir siparişin detaylarını getirir
- `POST /api/Orders`: Yeni sipariş oluşturur

## Web Arayüzü

- `/Orders`: Sipariş listesi
- `/Orders/Create`: Yeni sipariş oluşturma
- `/Orders/Details/{id}`: Sipariş detayları

## Portlar

- API: https://localhost:5011
- Web: https://localhost:5021
- Kafka: localhost:9092
- Kafdrop UI: http://localhost:9000

