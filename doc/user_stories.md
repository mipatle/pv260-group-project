# User Stories

---

## Planning Poker Legend

To help our team of five students estimate these tasks consistently, we use the following point scale:

- **1 Point**: Trivial task (**~1-2 hours**).
- **2 Points**: Small task (**~0.5 day**).
- **3 Points**: Medium task (**~1 day**).
- **5 Points**: Large task (**~2-3 days**).
- **8 Points**: Very large/Complex (**~5 days / 1 week**).
- **13+ Points**: Epic (**>1 week**).

---

## User Story 1: Architectural Discussion & Tech Stack Alignment

> As the **team**, we want to align on the chosen tech stack and architectural patterns, so that we have a consistent approach to development.

### Acceptance Criteria

- Formal agreement on the chosen web framework and architectural patterns.
- Agreement on the core data entities and their mapping to the database schema.
- Definition of standards for data types to ensure financial accuracy.
- Agreement on the audit strategy for tracking data origins (automated vs. manual).

### Out of Scope

- Implementing the actual code (this story is for alignment and documentation of the design).

### Estimation

**3 Points**: Medium task (**~1 day**).

---

## User Story 2: Requirement Refinement & Customer Data Analysis

> As a **developer**, I want to refine the input data requirements with the customer, so that we build a robust ingestion system that handles all edge cases.

### Acceptance Criteria

- Analysis and refinement of the customer's raw input data format (e.g., CSV, JSON, or API response).
- Definition of validation rules for mandatory fields (Date, Ticker, Shares, etc.).
- Identification of handling strategies for missing or malformed data.

### Out of Scope

- Building the actual UI or API for ingestion (this is for requirement analysis).

### Estimation

**2 Points**: Small task (**~0.5 day**).

---

## User Story 3: Project Setup & MVC Infrastructure

> As a **developer**, I want to initialize the ASP.NET Core MVC project, so that the team has a working software skeleton.

### Acceptance Criteria

- A new **ASP.NET Core MVC** project is initialized with a **Vertical Slice** structure.
- The project structure is set up to support slices (e.g., a `Features` or `Slices` folder).
- Dependency injection and basic cross-cutting concerns (logging, error handling) are configured.
- Environment variables and application settings (e.g., `appsettings.json`) are managed for different environments.

### Out of Scope

- Implementing the database schema (handled in US4).

### Estimation

**2 Points**: Small task (**~0.5 day**).

---

## User Story 4: Database Schema & PostgreSQL Configuration

> As a **developer**, I want to configure the PostgreSQL database and define the core schema, so that the application has persistent storage.

### Acceptance Criteria

- PostgreSQL database is configured with the `fund_positions` table:
    - Primary Key: `(date, ticker)`.
    - Columns: `date`, `fund` (default 'ARKK', check 'ARKK'), `company`, `ticker`, `cusip`, `shares` (NUMERIC), `market_value` (NUMERIC), `weight_percentage` (NUMERIC).
    - **Audit Column**: A nullable column to store the ID of the admin who performed the manual refresh.
- An index `idx_fund_positions_ticker` is created on the `ticker` column.
- Database connection string is managed securely.
- Entity Framework Core (or a similar ORM) is configured for database access.

### Out of Scope

- Implementing business logic features.

### Estimation

**2 Points**: Small task (**~0.5 day**).

---

## User Story 5: Project Documentation Foundation (Markdown)

> As a **developer**, I want to establish standard Markdown documentation for the repository, so that all team members understand how to set up, run, and contribute to the project.

### Acceptance Criteria

- A comprehensive `README.md` is created at the repository root using standard Markdown formatting.
- `README.md` includes local setup instructions, prerequisites (e.g., .NET SDK version), and commands to build and run the C# project.
- A basic `CONTRIBUTING.md` is created detailing branch naming and pull request guidelines.

### Out of Scope

- Detailed API endpoint documentation (e.g., Swagger/OpenAPI setup) or end-user manuals.

### Estimation

**2 Points**: Small task (**~0.5 day**).

---

## User Story 6: Continuous Integration (CI) for Automation

> As the **team**, we want to automate the build process, so that we ensure code quality from the beginning of the project.

### Acceptance Criteria

- A CI pipeline configuration file (e.g., GitHub Actions `.yaml`) is added to the repository.
- The pipeline automatically triggers on every Pull Request targeting the main branch.
- The pipeline restores dependencies and builds the C# solution.
- The pipeline executes all Unit Tests and E2E Tests.
- The pipeline fails and blocks the PR merge if any build or test step fails.

### Out of Scope

- Deployment (CD) or tests (tests will be added to the pipeline in stories US12 to US16).

### Estimation

**3 Points**: Medium task (**~1 day**).

---

## User Story 7: User Registration & Authentication

> As an **unknown user**, I want to register, log in, and log out, so that I can securely access the platform and maintain a session.

### Acceptance Criteria

- Unknown user can submit registration data to register.
- Unknown user can submit login data to log in.
- Successful login creates an active user session.
- Logged-in user can log out, which successfully terminates the session.

### Out of Scope

- Password recovery flows or multi-factor authentication (MFA).
- Third-party OAuth logins (e.g., Google, Apple).

### Estimation

**5 Points**: Large task (**~2-3 days**).

---

## User Story 8: Automated Data Ingestion (Cron)

> As a **system**, I want to automatically update data on a weekly schedule, so that the database has fresh, validated records without manual intervention.

### Acceptance Criteria

- Cron triggers automatically every Sunday at exactly 23:59:32.
- System downloads and validates the incoming data against the `FundPosition` record schema.
- System saves the data into `fund_positions`; the **audit column is left empty (NULL)** to indicate it was a cron job.

### Out of Scope

- A user interface to modify the cron schedule (it remains hardcoded for now).

### Notes

- Ensure robust error logging if validation fails during the automated run so admins are alerted.

### Estimation

**3 Points**: Medium task (**~1 day**).

---

## User Story 9: Manual Data Ingestion (Admin)

> As an **Admin**, I want to manually request a data update from Ark-funds, so that I can force a refresh outside the automated schedule.

### Acceptance Criteria

- Admin can trigger an "Update data" command.
- System requests a data update specifically from Ark-funds.
- System correctly identifies and handles API "Rate limits" gracefully.
- System saves the downloaded data into `fund_positions`, **storing the Admin's ID in the audit column** to mark it as a manual refresh.

### Out of Scope

- Fetching data from funds other than Ark-funds
- Rate limiting rules for the Ark-funds API

### Esimation

**3 Points**: Medium task (**~1 day**).

---

## User Story 10: Timestamp Navigation

> As a **logged-in user**, I want to select and compare a timestamp (snapshot) to the latest one, so that I can understand what changed in the portfolio over time and easily navigate between historical data.

### Acceptance Criteria

- User can request to list unique timestamps present in the `fund_positions` table.
- System successfully fetches and returns a collection of available timestamps.
- User can select a timestamp for comparison.
- System displays differences between the selected snapshots:
	- New positions
	- Increased positions (including percentage change)
	- Reduced positions (including percentage change)
- Differences are calculated correctly based on shares and/or weight.
- User can clearly identify which snapshot is older/newer.

### Out of Scope

- Complex filtering, pagination, or searching within the timestamps list.

### Estimation

**5 Points**: Large task (**~2-3 days**).

---

## User Story 11: Fetch Stock Data

> As a **logged-in User**, I want to retrieve the latest data or data for a specific timestamp, so that I can view the time-specific stock data.

### Acceptance Criteria

- User can trigger "Get latest data" to fetch the most recent stock data snapshot.
- User can provide a specific timestamp and trigger "Get data" to fetch historical stock data.
- System successfully fetches and returns the requested `FundPosition` data in both scenarios.

### Out of Scope

- Advanced data visualization (e.g., charts or graphs).

### Estimation

**3 Points**: Medium task (**~1 day**).

---

## User Story 12: Unit Testing - Authentication

> As the **team**, we want to implement unit tests for user registration, login, and session logic, so that we can ensure our security flows are correct.

### Acceptance Criteria

- A dedicated unit test project is added to the solution.
- Unit tests cover user registration logic (e.g., duplicate checks, field validation).
- Unit tests cover login credential validation.
- Unit tests cover session termination (logout) logic.

### Estimation

**2 Points**: Small task (**~0.5 day**).

---

## User Story 13: Unit Testing - Data Validation

> As the **team**, we want to implement unit tests for `FundPosition` constraints, so that we prevent malformed data from entering our system.

### Acceptance Criteria

- Unit tests ensure `FundPosition` records meet formatting rules (e.g., Ticker format).
- Unit tests ensure numeric constraints (e.g., non-negative Shares).
- Unit tests verify date range validations.

### Estimation

**2 Points**: Small task (**~0.5 day**).

---

## User Story 14: Unit Testing - Ingestion Logic

> As the **team**, we want to implement unit tests for the ingestion and audit logic, so that we accurately track data origins.

### Acceptance Criteria

- Unit tests verify that Cron-triggered saves result in a `NULL` audit field.
- Unit tests verify that Admin-triggered saves store the correct Admin ID.

### Out of scope

- Unit tests ensure graceful handling of rate limits during manual ingestion.

### Estimation

**2 Points**: Small task (**~0.5 day**).

---

## User Story 15: Unit Testing - Data Retrieval

> As the **team**, we want to implement unit tests for the data retrieval logic, so that users always see the correct stock snapshots.

### Acceptance Criteria

- Unit tests verify "Get Latest" logic returns the most recent record.
- Unit tests verify "Get by Timestamp" logic correctly filters by the provided date.
- **Comparison Logic Tests**: Unit tests ensuring correctly calculated differences (New, Increased, Reduced) and percentage changes between snapshots.
- Unit tests ensure correct sorting of multiple records.

### Estimation

**2 Points**: Small task (**~0.5 day**).

---

## User Story 16: C# End-to-End (E2E) Testing Infrastructure

> As a **team**, we want to configure and run E2E tests for the complete user flows, so that we ensure the entire system works together before release.
 
### Acceptance Criteria

- A dedicated E2E test project (e.g., `ProjectName.E2ETests`) is added.
- A C#-compatible E2E framework (e.g., Playwright for .NET) is installed.
- Smoke tests and critical path tests (Login -> Ingest -> View Data) are implemented.
- The CI pipeline (US6) is updated to run these E2E tests in a headless environment.

### Out of Scope

- Setting up external production test databases.

### Estimation

**5 Points**: Large task (**~2-3 days**).

---

## User Story 17: Continuous Deployment (CD)

> As the **team**, we want to automate the deployment of our application, so that successful builds from the main branch are automatically shipped to their destination.

### Acceptance Criteria

- The CI pipeline is extended to include a **CD (Continuous Deployment)** stage triggered after successful completion of all tests (US12-16).
- The application is automatically deployed to its target environment (e.g., a staging server or cloud host).
- Deployment secrets (DB connection strings, API keys) are managed securely within the pipeline.
- The pipeline performs a basic "health check" after deployment to ensure the app is running.

### Out of Scope

- Multi-environment deployment (e.g., QA -> Staging -> Prod) with manual approvals.
- High-availability or zero-downtime deployment strategies.

### Estimation

**5 Points**: Large task (**~2-3 days**).