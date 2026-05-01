import { test } from '@playwright/test';
import { CustomersPage } from '../pages/CustomersPage';
import { AdminLoginPage } from '../pages/AdminLoginPage';

test('Admin should add customer successfully', async ({ page }) => {
  const loginPage = new AdminLoginPage(page);
  const customersPage = new CustomersPage(page);

  const uniqueEmail = `archana${Date.now()}@test.com`;

  await loginPage.navigate();
  await loginPage.login('archana', 'Admin123');

  await customersPage.openCustomersPage();
  await customersPage.clickNewCustomer();

  await customersPage.addCustomer(
    'Laxmi',
    'Reddy',
    uniqueEmail,
    '9876543210'
  );

  // Verify customer is created
  await customersPage.verifyCustomerListed(uniqueEmail);
});