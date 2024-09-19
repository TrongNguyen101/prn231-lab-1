using BusinessObjects.DataContext;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.CustomerDataAccess
{
    public class CustomerDataAccess
    {
        #region Customer context
        private readonly CustomerContext context;
        #endregion

        #region CustomerDataAccess constructor
        public CustomerDataAccess() => context = new CustomerContext();
        #endregion
        
        #region Singleton disign pattern
        private static volatile CustomerDataAccess instance;
        private static readonly object lockInstance = new object();

        public static CustomerDataAccess GetInstance()
        {
            if (instance == null)
            {
                lock (lockInstance)
                {
                    if(instance == null)
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
                listCustomers = await context.Customers.ToListAsync();
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
            return listCustomers;
        }
        #endregion
    }
}