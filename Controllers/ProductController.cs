using CRUDoperations.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CRUDoperations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly string connectionString;
        public ProductController(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings:SqlserverDB"] ?? "";
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductDto productDto)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO products" +
                        "(name,category,salary) VALUES" +
                        "(@name,@category,@salary) ";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", productDto.Name);
                        command.Parameters.AddWithValue("@category", productDto.Category);
                        command.Parameters.AddWithValue("@salary", productDto.Salary);
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Product", "sorry we have an exception " +ex);
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    string sql = "SELECT * FROM products";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product();
                                product.ID = reader.GetInt32(0);
                                product.Name = reader.GetString(1);
                                product.Category = reader.GetString(2);
                                product.Salary = reader.GetDecimal(3);
                                product.CreatedAt = reader.GetDateTime(4);
                                products.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Product", "sorry we have an exception " + ex);
                return BadRequest(ModelState);

            }
            return Ok(products);
            }
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            Product product = new Product();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    string sql = "SELECT * FROM products WHERE id=@id";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                product.ID = reader.GetInt32(0);
                                product.Name = reader.GetString(1);
                                product.Category = reader.GetString(2);
                                product.Salary = reader.GetDecimal(3);
                                product.CreatedAt = reader.GetDateTime(4);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Product", "sorry we have an exception " + ex);
                return BadRequest(ModelState);

            }
            return Ok(product);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id,ProductDto productDto)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    string sql = "UPDATE products SET name=@name, category=@category," + "salary=@salary WHERE id=@id";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", productDto.Name);
                        command.Parameters.AddWithValue("@category", productDto.Category);
                        command.Parameters.AddWithValue("@salary", productDto.Salary);
                        command.Parameters.AddWithValue("@id",id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Product", "sorry we have an exception " + ex);
                return BadRequest(ModelState);

            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    string sql = "DELETE FROM products WHERE id=@id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                       
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Product", "sorry we have an exception " + ex);
                return BadRequest(ModelState);

            }
            return Ok();
        }
    }
}
    
