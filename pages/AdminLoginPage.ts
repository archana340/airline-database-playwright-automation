import { Page, Locator, expect } from '@playwright/test';

export class AdminLoginPage {
  readonly page: Page;
  readonly usernameInput: Locator;
  readonly passwordInput: Locator;
  readonly loginButton: Locator;

  constructor(page: Page) {
    this.page = page;
    this.usernameInput = page.locator('#Username');
    this.passwordInput = page.locator('#Password');
    this.loginButton = page.getByRole('button', { name: 'Login' });
  }

  async navigate() {
    await this.page.goto('/Admin/AdminLogin');
  }

  async login(username: string, password: string) {
    await this.usernameInput.fill(username);
    await this.passwordInput.fill(password);

    await expect(this.loginButton).toBeVisible();
    await expect(this.loginButton).toBeEnabled();

    await Promise.all([
      this.page.waitForURL('**/', { waitUntil: 'domcontentloaded' }),
      this.loginButton.click()
    ]);
  }
}