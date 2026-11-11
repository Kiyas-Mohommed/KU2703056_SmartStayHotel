# SmartStay Hotel Booking System

A console-based hotel booking application for the fictional **SmartStay** hotel chain, built in **C# (.NET 8)** using object-oriented programming principles.

> **Academic project:** CI6115 Programming III, Coursework 1 (Kingston University)
> **Student ID:** KU2703056

---

## Table of contents

1. [Overview](#overview)
2. [Features](#features)
3. [Business rules](#business-rules)
4. [Design assumptions](#design-assumptions)
5. [Technology stack](#technology-stack)
6. [Getting started](#getting-started)
7. [How to use the application](#how-to-use-the-application)
8. [Billing calculation](#billing-calculation)
9. [Project structure](#project-structure)
10. [Design and OOP concepts](#design-and-oop-concepts)
11. [Data structures and algorithms](#data-structures-and-algorithms)

---

## Overview

SmartStay lets a hotel administrator manage rooms, and lets guests register, search for available rooms, make and cancel reservations, and receive an itemised invoice. The system runs entirely in the console and keeps its data in memory while the program is running.

## Features

### Administrator
- Add a new room (unique Room ID, room type, bed type)
- Update a room's type and bed type
- Remove a room (blocked if the room has an active booking)
- Change a room's availability status (Available, Reserved, Under Maintenance)
- View all rooms

### Guest
- Register with NIC/Passport, full name, contact number and email
- Search available rooms by room type and dates
- Make a reservation (a room is assigned automatically)
- Search reservations by guest name or booking ID
- View reservations ordered by check-in date
- Cancel an eligible reservation
- Receive an itemised invoice when a reservation is confirmed

## Business rules

These rules come from the coursework brief and are enforced by the system.

| Rule | Detail |
|---|---|
| Guest registration | NIC/Passport, full name, contact number and email are required |
| Advance payment | A refundable **LKR 10,000** advance is taken at reservation |
| Room availability | Only rooms with status **Available** can be reserved |
| Booking inputs | Room type, number of guests, check-in date, check-out date |
| Advance notice | Reservations must be made **at least 2 days** before check-in |
| Cancellation window | No cancellation or modification within 24 hours of check-in |
| Long-stay discount | **12%** off the base room charge for stays of **5 or more nights** |

### Room packages

| Room type | Daily rate (LKR) | Free breakfast | Extra guest charge (LKR) | Tax |
|---|---|---|---|---|
| Standard | 6,000 | No | 1,000 | 10% |
| Deluxe | 9,000 | Yes | 1,500 | 12% |
| Suite | 14,000 | Yes | 2,000 | 15% |
| Family Suite | 18,000 | Yes | 2,500 | 15% |

## Design assumptions

The brief leaves some details open. These assumptions were made and applied consistently.

| # | Topic | Assumption |
|---|---|---|
| 1 | Room capacity | Guests included in the daily rate: Standard 2, Deluxe 2, Suite 3, Family Suite 4. Guests above this number are charged as extra guests. |
| 2 | Extra guest charge | Charged **per extra guest, per night**. |
| 3 | Discount | The 12% discount applies **only to the base room charge** (daily rate x nights), never to extra guest charges or tax. |
| 4 | Cancellation | Allowed only if check-in is **2 or more days away**; the full LKR 10,000 advance is then refunded. Otherwise the request is rejected and the booking stays active. |
| 5 | Availability | Room status is the administrator-managed current state. Future dates are checked against existing bookings using a date-overlap rule. A new guest may check in on the day another guest checks out. |
| 6 | Room allocation | The guest chooses the room type; the system assigns the **first available room** (lowest Room ID) when the booking is confirmed. |

## Technology stack

| Item | Detail |
|---|---|
| Language | C# |
| Framework | .NET 8.0 |
| Interface | Console application |
| IDE | Visual Studio |
| Storage | In memory (no database or files) |

## Getting started

### Option: Run from Visual Studio

1. Clone the repository:
   ```
   git clone https://github.com/Kiyas-Mohommed/KU2703056_SmartStayHotel.git
   ```
2. Open `KU2703056_SmartStayHotel.sln` in Visual Studio.
3. Press **Ctrl + F5** (Start Without Debugging).

**Requirement:** Visual Studio with the **.NET desktop development** workload and the .NET 8 SDK.

## How to use the application

When the program starts, it loads six sample rooms and shows the main menu.

```
============== MAIN MENU ==============
1. Administrator
2. Guest
0. Exit
```

### Sample rooms loaded at startup

| Room ID | Type | Bed |
|---|---|---|
| STD101 | Standard | Single |
| STD102 | Standard | Double |
| DLX201 | Deluxe | Queen |
| DLX202 | Deluxe | King |
| STE301 | Suite | King |
| FAM401 | Family Suite | Queen |

### Typical guest journey

1. Choose **Guest**, then **Register as a guest** and enter your details.
2. (Optional) Choose **Search available rooms** to check a room type and dates.
3. Choose **Make a reservation**, enter your NIC/Passport, room type, number of guests and dates. The system assigns a room and displays the invoice.
4. Later, use **Search reservation by booking ID** to view the booking and its invoice again, or **Cancel a reservation** if you need to.

### Input formats

- Dates use the format **dd/MM/yyyy**, for example `25/12/2026`.
- Invalid input never crashes the program. The system shows a message and asks again or returns to the menu.

### Sample invoice

```
====================================================
              SMARTSTAY HOTEL - INVOICE
====================================================
Booking ID : BK001

GUEST DETAILS
Name       : Kiyas Mohommed
NIC/Passport: 200012345678

ROOM DETAILS
Room ID    : STD101
Room Type  : Standard
Bed Type   : Single
Daily Rate : LKR 6,000.00

STAY DETAILS
Check-in   : 20 Nov 2026
Check-out  : 25 Nov 2026
Nights     : 5
Guests     : 3 (included in rate: 2)
----------------------------------------------------
Base room charge                LKR        30,000.00
Extra guest charges             LKR         5,000.00
Less: Multi-night discount      LKR         3,600.00
----------------------------------------------------
Subtotal                        LKR        31,400.00
Tax (10%)                       LKR         3,140.00
Less: Advance paid              LKR        10,000.00
====================================================
FINAL PAYABLE AMOUNT            LKR        24,540.00
====================================================
```

## Billing calculation

The final bill follows this order:

```
base      = daily rate x nights
extra     = extra guests x extra guest rate x nights
discount  = 12% x base            (only if nights >= 5)
subtotal  = base + extra - discount
tax       = subtotal x tax rate
total     = subtotal + tax - 10,000 (advance)
```

**Worked example:** Standard room, 3 guests, 5 nights.

| Step | Calculation | Amount (LKR) |
|---|---|---|
| Base charge | 6,000 x 5 | 30,000.00 |
| Extra guest charge | 1 x 1,000 x 5 | 5,000.00 |
| Discount | 12% of 30,000 | -3,600.00 |
| Subtotal | 30,000 + 5,000 - 3,600 | 31,400.00 |
| Tax (10%) | 10% of 31,400 | 3,140.00 |
| Advance paid | | -10,000.00 |
| **Final payable** | | **24,540.00** |

If a short stay costs less than the advance already paid, the invoice shows **"Refund due to guest"** instead of a negative amount.

## Project structure

```
KU2703056_SmartStayHotel/
└── SmartStayHotel/
    ├── Program.cs                  Entry point: creates objects and starts the main menu
    ├── Config/                     Business rule constants
    ├── Enums/                      RoomType, BedType, AvailabilityStatus, BookingStatus
    ├── Models/
    │   ├── Guest, Booking, Invoice
    │   └── Rooms/                  Abstract Room, four room types, RoomFactory
    ├── Repositories/               In-memory storage for rooms, guests and bookings
    ├── Validation/                 Guest and booking input validators
    ├── Modules/
    │   ├── RoomManagement/         AddRoom, UpdateRoom, RemoveRoom, ChangeRoomStatus, ViewAllRooms
    │   ├── GuestManagement/        RegisterGuest
    │   ├── Reservations/           SearchAvailableRooms, MakeReservation, SearchReservationByGuestName,
    │   │                           SearchReservationByBookingId, ViewReservationsByCheckIn, CancelReservation
    │   └── Billing/                CalculateFinalBill, GenerateInvoice
    ├── UI/                         ConsoleInput, MainMenu, AdminMenu, GuestMenu
    └── Data/                       SeedData (sample rooms)
```

All class names are prefixed with the student ID (`KU2703056_`).

## Design and OOP concepts

| Concept | Where it is used |
|---|---|
| **Abstraction** | `KU2703056_Room` is abstract and defines the rates every room type must supply |
| **Inheritance** | Standard, Deluxe, Suite and Family Suite rooms extend `KU2703056_Room` |
| **Polymorphism** | Billing reads `DailyRate`, `ExtraGuestRate` and `TaxRate` from any room without checking its type |
| **Encapsulation** | Properties use private setters; a booking changes state only through its own methods (for example `Cancel()`) |
| **Single Responsibility Principle** | Each class has one job: models hold data, repositories store it, validators check it, operations do one task each, and menus handle only input and output |
| **Factory pattern** | `KU2703056_RoomFactory` is the one place that decides which room subclass to create |
| **Dependency passing** | Operations receive the repositories they need through their constructors |

Operation classes never print to the console. They return results and messages, and the menus display them, so the logic is separate from the user interface.

## Data structures and algorithms

**Data structures**

| Structure | Used for | Why |
|---|---|---|
| `Dictionary<string, Room>` | Rooms, keyed by Room ID | Fast lookup and unique IDs |
| `Dictionary<string, Guest>` | Guests, keyed by NIC/Passport | Prevents duplicate registration |
| `List<Booking>` | Bookings | Looped over for searching and sorting |

**Algorithms**

| Algorithm | Purpose |
|---|---|
| Date-overlap check | A stay conflicts with an existing booking when `newCheckIn < existingCheckOut` and `newCheckOut > existingCheckIn` |
| Linear search | Find bookings by booking ID or by part of a guest name (case insensitive) |
| Sorting | Order reservations by check-in date, then by booking ID |
| First-fit allocation | Assign the free room with the lowest Room ID |
| Bill calculation | The step-by-step formula in the billing section |
| Input validation | Email, contact number, NIC/Passport, dates and guest count |

---

*Developed by Kiyas Mohommed (KU2703056) for CI6115 Programming III.*
