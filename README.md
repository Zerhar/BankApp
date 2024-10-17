# Bank Application - .NET 8 & MongoDB

## Overview
This project is a console-based bank application built in C# using .NET 8 and MongoDB. The application allows users to manage bank accounts, including deposits, withdrawals, and viewing transaction history. It ensures atomic operations and supports multi-user environments.

## Features
- **Account Management**: Each account has a name, balance, and a transaction list.
- **Deposit & Withdrawal**: Users can make deposits and withdrawals, with input validation for valid amounts.
- **Atomic Transactions**: Ensures safe and consistent operations in multi-user environments.
- **Total Balance**: Displays the sum of all bank accounts before exiting.
- **MongoDB**: Stores account details and transaction history.

## How It Works
1. Start the application and input a bank account name.
2. If the account exists, users can deposit or withdraw funds. If it doesn't exist, a new account is created.
3. The account balance updates after each transaction, and the transaction is added to the account's history.
4. The application loops back to the account input step or shows the total balance of all accounts before exit.

## Setup Instructions
1. **Clone the repository**:
   ```bash
   git clone https://github.com/Zerhar/BankApp.git
