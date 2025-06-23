# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

ReptiRealm is a multi-platform SaaS application for reptile owners to track their pets' health, feeding, and care data. The project consists of:

- **Backend API** (`/backend`): ASP.NET Core 8.0 API with PostgreSQL, deployable to AWS Lambda
- **Web Frontend** (`/frontend/source`): Next.js 15 with React 19, TypeScript, and Tailwind CSS
- **Mobile App** (`/app`): Flutter application for iOS/Android
- **Lynx App** (`/lynx-app`): Alternative UI using Lynx.js framework

## Essential Development Commands

### Backend (.NET API)
```bash
cd backend/ReptiRealm/src/ReptiRealm
dotnet restore
dotnet build
dotnet run  # Runs on https://localhost:63823
```

### Frontend (Next.js)
```bash
cd frontend/source
yarn install
yarn dev    # Development server with Turbopack
yarn build  # Production build
yarn lint   # Run ESLint
```

### Mobile App (Flutter)
```bash
cd app
flutter pub get
flutter run
flutter test
flutter build apk --release  # Android
flutter build ios --release  # iOS
```

### Database Setup
The backend requires PostgreSQL. Default connection string:
```
User Id=reptirealm;Password=reptirealm;Server=localhost;Port=5432;Database=postgres
```

Migrations are auto-applied on startup. The database is seeded with:
- Default admin user: `SuperAdmin` / `Password1!`
- Roles: Admin, User

## Architecture & Key Patterns

### Backend Architecture
- **Pattern**: Repository/Unit of Work with Generic Repository
- **Authentication**: JWT tokens with ASP.NET Identity
- **Background Jobs**: Hangfire with PostgreSQL storage
- **API Documentation**: Swagger (available in dev/staging at `/swagger`)
- **Deployment**: AWS Lambda with API Gateway

### Frontend Architecture
- **Rendering**: Server-side rendering with Next.js App Router
- **State**: React Context for authentication
- **API Client**: Mix of fetch and axios
- **Styling**: Tailwind CSS with Radix UI components
- **Authentication**: JWT tokens stored in cookies

### Data Models
Core entities track reptile care:
- `Reptile`: Pet information linked to users
- `Feed`, `Shed`, `Weight`, `Defecation`: Health tracking
- `Species`, `Morph`: Classification data
- `Subscription`: User subscription management with Stripe

### API Integration
- Frontend expects backend at `NEXT_PUBLIC_API_URL` environment variable
- Default local backend URL: `https://localhost:63823`
- CORS configured for `http://localhost:3000` and `http://127.0.0.1:5173`

## Testing

### Backend
No automated tests configured. Manual testing via Swagger UI.

### Frontend
```bash
yarn lint  # ESLint checks
```

### Mobile
```bash
flutter test
```

## Deployment

### Backend
```bash
cd backend/scripts/api
./build.sh   # Build Lambda package
./plan.sh    # Terraform plan
./apply.sh   # Deploy to AWS
```

### Frontend
```bash
cd frontend/scripts/web
./build.sh   # Build Next.js
./upload.sh  # Deploy to S3/CloudFront
```

Both use Terraform/Terragrunt for infrastructure management.