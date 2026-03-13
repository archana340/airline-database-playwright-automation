import { Page, Locator, expect } from '@playwright/test';

export class HomePage {

  readonly page: Page;
  readonly welcomeText: Locator;
  readonly customersLink: Locator;

  constructor(page: Page) {
    this.page = page;
    this.welcomeText = page.locator('h1.display-4');
    this.customersLink = page.locator('a[href="/Customers/Customers"]');
  }

  async verifyHomePageLoaded() {
    await expect(this.welcomeText).toHaveText('Welcome');
  }

  async goToCustomers() {
    await this.customersLink.click();
  }
}