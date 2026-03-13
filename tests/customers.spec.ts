import { test } from '@playwright/test';
import { AdminLoginPage } from '../pages/AdminLoginPage';
import { CustomersPage } from '../pages/CustomersPage';

test('Admin can add a customer', async ({ page }) => {
  const loginPage = new AdminLoginPage(page);
  const customersPage = new CustomersPage(page);

  // Generate short email based on today's date
  const today = new Date().toISOString().slice(0,10).replace(/-/g, '');
  const email = `archana${today}@gmail.com`;

  await loginPage.navigate();
  await loginPage.login('archana', 'Admin123');

  await customersPage.openCustomersPage();
  await customersPage.clickNewCustomer();

  await customersPage.addCustomer(
    'Archana',
    'Reddy',
    email,
    '9876543210'
  );

  await customersPage.verifyCustomerListed(email);
});