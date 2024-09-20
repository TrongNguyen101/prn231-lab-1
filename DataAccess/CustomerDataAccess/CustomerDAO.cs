using BusinessObjects.DataContext;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DataAccess.CustomerDataAccess
{
    public class CustomerDataAccess
    {
        #region Singleton design pattern
        private static volatile CustomerDataAccess instance;
        private static readonly object lockInstance = new object();

        public static CustomerDataAccess GetInstance()
        {
            if (instance == null)
            {
                lock (lockInstance)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDataAccess();
                    }
                }
            }
            return instance;
        }
        #endregion

        #region Get customers
        public async Task<List<Customer>> GetCustomers()
        {
            var listCustomers = new List<Customer>();
            try
            {
                using var context = new CustomerContext();
                listCustomers = await context.Customers.ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return listCustomers;
        }
        #endregion

        #region Get customer by Id
        public async Task<Customer> GetCustomerById(int id)
        {
            Customer customer;
            try
            {
                using var context = new CustomerContext();
                customer = await context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return customer;
        }
        #endregion

        #region Get customer by username
        public async Task<Customer> GetCustomerByUsername(string username)
        {
            Customer customer;
            try
            {
                using var context = new CustomerContext();
                customer = await context.Customers.SingleOrDefaultAsync(c => c.Username == username);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return customer;
        }
        #endregion

        #region Add customer
        public async Task AddCustomer(Customer customer)
        {
            try
            {
                using var context = new CustomerContext();
                await context.Customers.AddAsync(customer);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Update customer
        public async Task UpdateCustomer(Customer customer)
        {
            try
            {
                using var context = new CustomerContext();
                context.Update(customer);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region  Delete customer
        public async Task DeleteCustomer(Customer customer)
        {
             try
            {
                using var context = new CustomerContext();
                context.Remove(customer);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}