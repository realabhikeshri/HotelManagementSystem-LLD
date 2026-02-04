# 🏨 Hotel Management System – C#  LLD

This repository contains a **Hotel Management / Booking System** implemented in C#, designed for **machine coding** and **Low Level Design (LLD)** 

The focus is on:

- Correct hotel & room modeling
- Robust booking flow with availability & concurrency
- Clean separation of concerns and testability

---

## 🎯 Problem

Design a core backend service for a hotel management system that supports:

- Managing hotels and rooms (room types, status, pricing).
- Searching available rooms by **date range**, **hotel**, and **room type**.
- Creating bookings with **check-in** and **check-out** dates.
- Preventing **double-booking** under concurrency.
- Handling payments and booking life cycle:
  - `PENDING → CONFIRMED → CHECKED_IN → CHECKED_OUT / CANCELLED`.

This implementation is **in-memory** and framework-agnostic

---

## 🧱 Design Overview

### Core Entities

- **Hotel**
  - `Id`, `Name`, `Location`, collection of `Room`s.
- **Room**
  - `Id`, `HotelId`, `RoomNumber`, `Type`, `Status`, `BasePricePerNight`.
- **Guest**
  - `Id`, `Name`, `Email`, `Phone`.
- **Booking**
  - `Id`, `HotelId`, `RoomId`, `Guest`, `CheckIn`, `CheckOut`, `Status`, `TotalAmount`.
- **Payment**
  - `Id`, `BookingId`, `Amount`, `Status`, `CreatedAt`.

### Enums

- `RoomType`: `Single`, `Double`, `Suite`, `Deluxe`.
- `RoomStatus`: `Available`, `Occupied`, `OutOfService`.
- `BookingStatus`: `Pending`, `Confirmed`, `CheckedIn`, `CheckedOut`, `Cancelled`.
- `PaymentStatus`: `Pending`, `Completed`, `Failed`, `Refunded`.

---

## 🔑 Patterns & Concepts

- **Repository Pattern**
  - `IHotelRepository`, `IRoomRepository`, `IBookingRepository` abstract data access.
- **Service Layer**
  - `BookingService` coordinates search + booking + concurrency checks.
  - `PricingService` encapsulates pricing logic (e.g., weekend markup).
  - `PaymentService` simulates payment capture.
- **Concurrency**
  - Uses per-room locking (`lock` around room bookings) and date overlap checks to prevent double-booking.
- **Extensibility**
  - Easy to add:
    - Additional room types.
    - Seasonal pricing rules.
    - Loyalty discounts & taxes.

You can draw a simple class diagram:

`BookingService → (HotelRepository, RoomRepository, BookingRepository, PricingService, PaymentService)`  
`Hotel` aggregates `Room`s; `Booking` refers to `Room` and `Guest`

---

## Author
Abhishek Keshri
