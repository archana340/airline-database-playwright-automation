# Airline Database System – End-to-End UI Automation Framework using Playwright & TypeScript
This project demonstrates an end-to-end UI automation framework built using **Playwright with TypeScript** to test an **ASP.NET Airline Database Management System**.

The framework automates critical user workflows such as **Admin login and customer management**, ensuring the application behaves correctly across multiple browsers.

The project also integrates **GitHub Actions CI/CD** to automatically build the application and run Playwright tests on every push.

# Tech Stack

- Playwright
- TypeScript
- Node.js
- ASP.NET Core
- GitHub Actions (CI/CD)

# Key Features
- End-to-end UI automation using Playwright
- Page Object Model (POM) architecture
- Cross-browser testing
- Chromium
- Firefox
- WebKit
- Automated CI pipeline with GitHub Actions
- Automatic screenshots, videos, and traces on test failures
- Dynamic test data generation for reliable execution
  
## Project Structure
airline-database-playwright-automation

    ├── pages
        ├── AdminLoginPage.ts
        ├── CustomersPage.ts
        └── HomePage.ts


    ├── tests
        ├── login.spec.ts
        └── customers.spec.ts
    
    ├── AirlinedatabaseSystem-master
        └── ASP.NET Airline Database Application

    ├── playwright.config.ts
    ├── package.json
    └── .github/workflows/playwright.yml

# Test Scenarios Automated
## Admin Authentication
- Navigate to login page
- Enter credentials
- Verify successful login
- Customer Management
- Navigate to Customers page
- Create a new customer
- Validate the customer appears in the customer list

# Running Tests Locally
- Install dependencies
  npm install
- Install Playwright browsers
  npx playwright install
- Run tests
  npx playwright test
- View HTML test report
  npx playwright show-report
  
# Continuous Integration
This project uses GitHub Actions to automatically:
1. Build the ASP.NET application
2. Start the web server
3. Install Playwright
4. Execute cross-browser tests
5. Upload test artifacts
6. The pipeline runs on every push to the repository.

# Test Evidence
On failure, Playwright automatically captures:
- Screenshots
- Videos
- Trace files
These artifacts help diagnose issues quickly

# Why This Project
This framework demonstrates:
- Modern UI automation with Playwright
- Real application testing
- Cross-browser automation
- CI/CD integration
- Maintainable test architecture using Page Object Model

## Author
**Archana Puppireddy**

Automation Engineer | Playwright | Selenium | API Testing | CI/CD

Project Repository:  
https://github.com/archana340/airline-database-playwright-automation
