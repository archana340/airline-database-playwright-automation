import { Page, Locator, expect } from '@playwright/test';

export class CustomersPage {
  readonly page: Page;
  readonly newCustomerButton: Locator;
  readonly firstNameInput: Locator;
  readonly lastNameInput: Locator;
  readonly emailInput: Locator;
  readonly phoneInput: Locator;
  readonly submitButton: Locator;
  readonly successAlert: Locator;
  readonly customersTable: Locator;

  constructor(page: Page) {
    this.page = page;
    this.newCustomerButton = page.getByRole('link', { name: 'New Customer' });
    this.firstNameInput = page.getByLabel('First Name');
    this.lastNameInput = page.getByLabel('Last Name');
    this.emailInput = page.getByLabel('Email');
    this.phoneInput = page.getByLabel('Phone Number');
    this.submitButton = page.getByRole('button', { name: 'Submit' });
    this.successAlert = page.locator('.alert-success');
    this.customersTable = page.locator('table');
  }

  async openCustomersPage() {
    await this.page.goto('/Customers/Customers');
    await expect(this.page).toHaveURL(/Customers\/Customers/);
  }

  async clickNewCustomer() {
    await this.newCustomerButton.click();
    await expect(this.page).toHaveURL(/Customers\/Create/);
  }

  async addCustomer(firstName: string, lastName: string, email: string, phone: string) {
    await this.firstNameInput.fill(firstName);
    await this.lastNameInput.fill(lastName);
    await this.emailInput.fill(email);
    await this.phoneInput.fill(phone);

    await this.submitButton.click();

    // Wait for the app to confirm save completed
    await expect(this.successAlert).toBeVisible();
  }

  async verifyCustomerListed(email: string) {
    await this.page.goto('/Customers/Customers');
    await expect(this.customersTable).toBeVisible();
    await expect(this.customersTable).toContainText(email);
  }
}