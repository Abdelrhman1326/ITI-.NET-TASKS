using BankApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace BankApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        // --- Event Handler for Form Load (Optional) ---
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
        }

        // --- Event Handler for Customer Registration (e.g., Create Account Button) ---
        private void button1_Click_1(object sender, EventArgs e)
        {
            string userInput = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                MessageBox.Show("Owner name can't be empty!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] names = userInput.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            string firstName = names.Length > 0 ? names[0] : userInput;
            string lastName = names.Length > 1 ? names[1] : string.Empty;

            using (var context = new DotNetBankDbContext())
            {
                var newCustomer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                };

                try
                {
                    var newAccount = new Bankaccount
                    {
                        Balance = 0m,
                        Customer = newCustomer
                    };

                    context.Customers.Add(newCustomer);
                    context.Bankaccounts.Add(newAccount);

                    int rowsAffected = context.SaveChanges();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Customer '{userInput}' successfully registered! Customer ID: {newCustomer.CustomerId}");
                        textBox1.Clear();
                        LoadCustomerData();
                    }
                    else
                    {
                        MessageBox.Show("Registration failed, no changes were made to the database.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while saving: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // --- Event Handler for Deposit (button2_Click) ---
        private void button2_Click(object sender, EventArgs e)
        {
            decimal amountDecimal = numericUpDown1.Value;

            if (amountDecimal <= 0)
            {
                MessageBox.Show("Please enter a positive amount to deposit.", "Value Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dataGridView1.SelectedRows.Count == 0 || dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("Select only one account for deposit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            if (!int.TryParse(selectedRow.Cells["ID"].Value.ToString(), out int customerId))
            {
                MessageBox.Show("Could not determine the selected Customer ID.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new DotNetBankDbContext())
            {
                try
                {
                    var accountToUpdate = context.Bankaccounts
                        .FirstOrDefault(ba => ba.CustomerId == customerId);

                    if (accountToUpdate == null)
                    {
                        MessageBox.Show($"No bank account found for Customer ID: {customerId}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    accountToUpdate.Balance += amountDecimal;

                    int rowsAffected = context.SaveChanges();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Successfully deposited {amountDecimal:C2} into account for Customer ID {customerId}. New balance: {accountToUpdate.Balance:C2}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        numericUpDown1.Value = 0;
                        LoadCustomerData();
                    }
                    else
                    {
                        MessageBox.Show("Deposit failed, no changes were made to the database.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred during deposit: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 1. Get Withdrawal Amount and Perform Initial Validation
            decimal amountDecimal = numericUpDown1.Value;

            if (amountDecimal <= 0)
            {
                MessageBox.Show("Please enter a positive amount to withdraw.", "Value Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Perform DataGridView Selection Validation
            if (dataGridView1.SelectedRows.Count == 0 || dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("Select only one account for withdrawal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the selected row
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            // 3. Extract Customer ID
            if (!int.TryParse(selectedRow.Cells["ID"].Value.ToString(), out int customerId))
            {
                MessageBox.Show("Could not determine the selected Customer ID.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Perform the Database Update (Withdrawal)
            using (var context = new DotNetBankDbContext())
            {
                try
                {
                    var accountToUpdate = context.Bankaccounts
                        .FirstOrDefault(ba => ba.CustomerId == customerId);

                    if (accountToUpdate == null)
                    {
                        MessageBox.Show($"No bank account found for Customer ID: {customerId}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 5. CRITICAL: Check for sufficient funds
                    if (accountToUpdate.Balance < amountDecimal)
                    {
                        MessageBox.Show($"Insufficient funds. Current balance: {accountToUpdate.Balance:C2}. Cannot withdraw {amountDecimal:C2}.", "Withdrawal Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Update the Balance (Subtract the amount)
                    accountToUpdate.Balance -= amountDecimal;

                    int rowsAffected = context.SaveChanges();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Successfully withdrew {amountDecimal:C2} from account for Customer ID {customerId}. New balance: {accountToUpdate.Balance:C2}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        numericUpDown1.Value = 0;
                        LoadCustomerData();
                    }
                    else
                    {
                        MessageBox.Show("Withdrawal failed, no changes were made to the database.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred during withdrawal: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            // 1. Validate Selection
            var selectedRows = dataGridView1.SelectedRows;
            if (selectedRows.Count == 0 || selectedRows.Count > 1)
            {
                MessageBox.Show("Select one account to delete!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the selected row
            DataGridViewRow selectedRow = selectedRows[0];

            // 2. Extract Customer ID
            if (!int.TryParse(selectedRow.Cells["ID"].Value.ToString(), out int customerId))
            {
                MessageBox.Show("Could not retrieve a valid ID from the selected row.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. CRITICAL: Get User Confirmation
            var confirmResult = MessageBox.Show(
                "Are you sure you want to permanently delete this customer and their bank account?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmResult == DialogResult.No)
            {
                return;
            }

            using (var context = new DotNetBankDbContext())
            {
                try
                {
                    var accountToDelete = context.Bankaccounts.FirstOrDefault(atd => atd.CustomerId == customerId);

                    var customerToDelete = context.Customers.FirstOrDefault(c => c.CustomerId == customerId);

                    if (customerToDelete == null)
                    {
                        MessageBox.Show("Customer not found in the database. Deletion aborted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (accountToDelete != null)
                    {
                        context.Bankaccounts.Remove(accountToDelete);
                    }

                    context.Customers.Remove(customerToDelete);

                    int rowsAffected = context.SaveChanges();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Customer ID {customerId} and associated data successfully deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCustomerData();
                    }
                    else
                    {
                        MessageBox.Show("Deletion failed, no changes were made.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred during deletion: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadCustomerData()
        {
            using (var context = new DotNetBankDbContext())
            {
                try
                {
                    var customerData = context.Customers
                        .Include(c => c.Bankaccounts)
                        .Select(c => new
                        {
                            ID = c.CustomerId,
                            Name = c.FirstName + " " + c.LastName,
                            Balance = c.Bankaccounts.Any() ?
                              c.Bankaccounts.FirstOrDefault().Balance :
                              0m
                        })
                        .ToList();

                    dataGridView1.DataSource = customerData;

                    if (dataGridView1.Columns["Balance"] != null)
                    {
                        dataGridView1.Columns["Balance"].DefaultCellStyle.Format = "C2";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not load customer data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) { /* Empty */ }
        private void textBox1_TextChanged(object sender, EventArgs e) { /* Empty */ }
        private void label1_Click(object sender, EventArgs e) { /* Empty */ }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { /* Empty */ }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { /* Empty */ }
    }
}