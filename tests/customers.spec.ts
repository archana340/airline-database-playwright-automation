import { test } from '@playwright/test';
import { CustomersPage } from '../pages/CustomersPage';
import { AdminLoginPage } from '../pages/AdminLoginPage';

test('Admin should add customer successfully', async ({ page }) => {
  const loginPage = new AdminLoginPage(page);
  const customersPage = new CustomersPage(page);

  const uniqueEmail = `archana${Date.now()}@test.com`;

  await page.goto('/');
  await loginPage.login('archana', 'Admin123');

  await customersPage.openCustomersPage();
  await customersPage.clickNewCustomer();
  await customersPage.addCustomer(
    'Archana',
    'reddy',
    uniqueEmail,
    '9876543210'
  );

  await customersPage.verifyCustomerListed(uniqueEmail);
});