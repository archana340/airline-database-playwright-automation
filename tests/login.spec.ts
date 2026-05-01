import { test, expect } from '@playwright/test';
import { AdminLoginPage } from '../pages/AdminLoginPage';
import { HomePage } from '../pages/HomePage';

test('Admin should login successfully', async ({ page }) => {
  const loginPage = new AdminLoginPage(page);
  const homePage = new HomePage(page);

  await loginPage.navigate();
  await loginPage.login('archana', 'Admin123');

  await expect(page).toHaveURL(/\/$/);
  await homePage.verifyHomePageLoaded();
});