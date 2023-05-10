using BikeRepairShop.BL.Domain;
using BikeRepairShop.BL.DTO;
using BikeRepairShop.BL.Interfaces;
using BikeRepairShop.BL.Managers;
using BikeRepairShop.DL.Exceptions;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace BikeRepairShop.DL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private string connectionString;

        public CustomerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddBike(Bike bike)
        {
            try
            {
                string sql = "INSERT INTO Bike(biketype,purchasecost,description,customerid,status) output  INSERTED.ID VALUES(@biketype,@purchasecost,@description,@customerid,@status)";
                using(SqlConnection connection= new SqlConnection(connectionString))
                using(SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@biketype",bike.BikeType.ToString());
                    command.Parameters.AddWithValue("@purchasecost", bike.PurchaseCost);
                    if (bike.Description!= null)
                        command.Parameters.AddWithValue("@description", bike.Description);
                    else
                        command.Parameters.AddWithValue("@description",DBNull.Value);
                    command.Parameters.AddWithValue("@customerid", bike.Customer.ID);
                    command.Parameters.AddWithValue("@status", 1);
                    int bid=(int)command.ExecuteScalar();
                    bike.SetId(bid);
                }
            }
            catch(Exception ex)
            {
                throw new CustomerRepositoryException("AddBike", ex);
            }
        }

        public List<BikeInfo> GetBikesInfo()
        {
            try
            {
                List<BikeInfo> bikes=new List<BikeInfo>();
                string sql = "select t1.*,t2.name,t2.email from bike t1 inner join customer t2 on t1.CustomerId=t2.Id where t1.Status=1 and t2.Status=1";
                using(SqlConnection connection = new SqlConnection(connectionString))
                using(SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = sql;
                    IDataReader reader= command.ExecuteReader();
                    while(reader.Read())
                    {
                        //string d=reader.IsDBNull(reader.GetOrdinal("description")) ? null : (string)reader["description"];
                        //Enum.Parse(typeof(BikeType), (string)reader["biketype"], true);
                        bikes.Add(new BikeInfo((int)reader["id"], reader.IsDBNull(reader.GetOrdinal("description")) ? null : (string)reader["description"],(BikeType) Enum.Parse(typeof(BikeType), (string)reader["biketype"], true), (double)reader["purchasecost"], (int)reader["customerid"], (string)reader["name"]+" (" + (string)reader["email"]+")"));
                    }
                    reader.Close();
                }                
                return bikes;
            }
            catch(Exception ex)
            {
                throw new CustomerRepositoryException("GetBikesInfo", ex);
            }
        }

        public Customer GetCustomer(int? id) {
            try {
                Customer c = new Customer();
                string sql = "SELECT Id, Name, Email, Adress FROM Customer WHERE Id = @id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand()) {
                    connection.Open();
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@id", id);

                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read()) {
                        c.SetId((int)reader["Id"]);
                        c.SetName((string)reader["Name"]);
                        c.SetEmail((string)reader["Email"]);
                        c.Setadress((string)reader["Adress"]);
                        return c;
                    }
                    reader.Close();
                    return c;
                    }
            } catch (Exception ex) {
                throw new CustomerRepositoryException("GetCustomer", ex);
            }
        }


        public int GetLastBikeId() {
            try {
                int i = 0;
                string sql = "SELECT max(Id) as MaxId FROM Bike";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand()) {
                    connection.Open();
                    command.CommandText = sql;
                    using (IDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            i = (int)reader["MaxId"];
                        }
                    }
                }
                i++;
                return i;
            } catch (Exception ex) {
                throw new CustomerRepositoryException("GetLastBikeId", ex);
            }
        }

        public int GetLastCustomerId() {
            try {
                int i = 0;
                string sql = "SELECT max(Id) as MaxId FROM Customer";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand()) {
                    connection.Open();
                    command.CommandText = sql;
                    using (IDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            i = (int)reader["MaxId"];
                        }
                    }
                }
                return i;
            } catch (Exception ex) {
                throw new CustomerRepositoryException("GetLastCustomerId", ex);
            }
        }
        public void DeleteBike(int? bikeId) {
            try {
                string sql = "UPDATE Bike SET Status = 0 WHERE Id = @id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand command = connection.CreateCommand()) {
                    connection.Open();
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@id", bikeId);
                    command.ExecuteNonQuery();
                }
            } catch (Exception ex) {
                throw new CustomerRepositoryException("DeleteBike", ex);
            }
        }

        public void ChangeBike(BikeInfo bikeInfo) {
            string sql = "UPDATE Bike SET BikeType = @biketype, PurchaseCost = @purchasecost, Description = @description WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand()) {
                connection.Open();
                command.CommandText = sql;
                command.Parameters.AddWithValue("@biketype", bikeInfo.BikeType.ToString());
                command.Parameters.AddWithValue("@purchasecost", bikeInfo.PurchaseCost);
                if (bikeInfo.Description != null)
                    command.Parameters.AddWithValue("@description", bikeInfo.Description);
                else
                    command.Parameters.AddWithValue("@description", DBNull.Value);
                command.Parameters.AddWithValue("@id", bikeInfo.Id);
                command.ExecuteNonQuery();
            }
        }
        public List<CustomerInfo> GetCustomersInfo() {
            try {
                List<CustomerInfo> customers = new List<CustomerInfo>();
                string sql = "select id, Name, Adress, Email from Customer where Status=1";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand()) {
                    connection.Open();
                    command.CommandText = sql;
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read()) {
                        //string d=reader.IsDBNull(reader.GetOrdinal("description")) ? null : (string)reader["description"];
                        //Enum.Parse(typeof(BikeType), (string)reader["biketype"], true);
                        customers.Add(new CustomerInfo((int)reader["id"], (string)reader["Name"], (string)reader["Adress"], (string)reader["Email"]));
                    }
                    reader.Close();
                }
                return customers;
            } catch (Exception ex) {
                throw new CustomerRepositoryException("GetBikesInfo", ex);
            }
        }

        public void AddCustomer(Customer customer) {
            try {
                string sql = "INSERT INTO Customer(Name, Adress, Email,status) output  INSERTED.ID VALUES(@Name,@Adress,@Email,@status)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand()) {
                    connection.Open();
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@Name", customer.Name);
                    command.Parameters.AddWithValue("@Adress", customer.adress);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@status", 1);
                    int bid = (int)command.ExecuteScalar();
                    customer.SetId(bid);
                }
            } catch (Exception ex) {
                throw new CustomerRepositoryException("AddBike", ex);
            }
        }

        public void ChangeCustomer(CustomerInfo customerInfo) {
            string sql = "UPDATE Customer SET Name = @name, Adress = @adress, Email = @email WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand()) {
                connection.Open();
                command.CommandText = sql;
                command.Parameters.AddWithValue("@name", customerInfo.Name);
                command.Parameters.AddWithValue("@adress", customerInfo.adress);
                command.Parameters.AddWithValue("@email", customerInfo.Email);
                command.Parameters.AddWithValue("@id", customerInfo.ID);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(int? customerID) {
            string sql = "UPDATE Customer SET Status = 0 WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand()) {
                connection.Open();
                command.CommandText = sql;
                command.Parameters.AddWithValue("@id", customerID);
                command.ExecuteNonQuery();
            }
        }
        public CustomerInfo GetCustomerInfoByID(int? id) {
            try {
                string sql = "select id, Name, Adress, Email, status from Customer where Id = @id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand()) {
                    connection.Open();
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@id", id);
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read()) {
                        return new CustomerInfo((int)reader["id"], (string)reader["Name"], (string)reader["Adress"], (string)reader["Email"]);
                    }
                    reader.Close();
                    return new CustomerInfo(0, "", "", "");
                }
            } catch (Exception ex) {
                throw new CustomerRepositoryException("GetBikesInfo", ex);
            }
        }
    }
}
