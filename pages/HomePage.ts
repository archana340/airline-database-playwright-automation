import { Page, Locator, expect } from '@playwright/test';

export class HomePage {
  readonly page: Page;
  readonly welcomeText: Locator;
  readonly customersLink: Locator;

  constructor(page: Page) {
    this.page = page;
    this.welcomeText = page.getByRole('heading', { name: 'Welcome', exact: true });
    this.customersLink = page.getByRole('link', { name: 'Customers' });
  }

  async verifyHomePageLoaded() {
    await expect(this.welcomeText).toBeVisible();
    await expect(this.welcomeText).toHaveText('Welcome');
  }

  async goToCustomers() {
    await expect(this.customersLink).toBeVisible();
    await this.customersLink.click();
  }
}