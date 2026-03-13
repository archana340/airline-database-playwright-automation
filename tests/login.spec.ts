import { test, expect } from '@playwright/test';
import { AdminLoginPage } from '../pages/AdminLoginPage';
import { HomePage } from '../pages/HomePage';

test('Admin should login successfully', async ({ page }) => {

  const loginPage = new AdminLoginPage(page);
  const homePage = new HomePage(page);

  // Go to login page
  await loginPage.navigate();

  // Perform login
  await loginPage.login('archana', 'Admin123');

  // Verify homepage loaded
  await expect(page).toHaveURL('http://127.0.0.1:7291/');
  await homePage.verifyHomePageLoaded();

});