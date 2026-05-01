import { Page, Locator, expect } from '@playwright/test';

export class AdminLoginPage {
  readonly page: Page;
  readonly usernameInput: Locator;
  readonly passwordInput: Locator;
  readonly loginButton: Locator;
  readonly errorMessage: Locator;

  constructor(page: Page) {
    this.page = page;

    this.usernameInput = page.locator('#Username');
    this.passwordInput = page.locator('#Password');
    this.loginButton = page.getByRole('button', { name: 'Login' });
    this.errorMessage = page.locator('.validation-summary-errors, .text-danger');
  }

  async navigate() {
    await this.page.goto('/Admin/AdminLogin');
    await expect(this.usernameInput).toBeVisible(); 
  }

  async login(username: string, password: string) {
    await this.usernameInput.fill(username);
    await this.passwordInput.fill(password);

    await expect(this.loginButton).toBeEnabled();
    await this.loginButton.click();
  }

  async expectLoginSuccess() {
    await expect(this.page).toHaveURL('/'); 
  }

  async expectLoginError() {
    await expect(this.errorMessage).toBeVisible();
  }

  async getErrorText() {
    return await this.errorMessage.textContent();
  }
}