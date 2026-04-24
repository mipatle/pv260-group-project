# Contributing Guide

This document defines the workflow and conventions for contributing to the project.

## Branch Naming

Branches must follow this format:

`us<NUMBER>-short-description`

Examples:
- us5-project-documentation
- us14-unit-testing-ingestion

---

## Commit Message Format

Commit messages must follow:

`<type>(us<NUMBER>): message`

Allowed types:
- feat
- fix
- refactor
- docs
- style
- test
- chore
- ci
- build
- perf
- revert

Example:
feat(us10): add CI workflow

---

## Workflow

1. Create a branch from `milestone-X` or `main`
2. Implement changes
3. Write/update tests
4. Run tests locally
5. Create a Pull Request

---

## Pull Requests

Requirements:
- At least 1 approval
- All tests must pass
- Clear description of changes

PR should include:
- What was implemented
- Why it was implemented
- Any limitations

---

## Code Style

- Follow the existing project structure
- Keep methods small and readable
- Use meaningful names
- Avoid unnecessary complexity

---

## Testing

- Every new feature should include tests
- Tests must pass before pushing
- Use xUnit framework

Run tests:

dotnet test

---

## Pre-push Checks

Before pushing:
- Tests must pass
- Code should compile without warnings

---

## Notes

- Do not commit secrets or sensitive data
- Keep changes focused and minimal
- Documentation updates are part of the contribution