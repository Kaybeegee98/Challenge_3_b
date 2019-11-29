using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ComputersController : ApiController
    {
        private civapiEntities2 db = new civapiEntities2();

        // GET: api/Computers
        public IEnumerable<CusComputer> GetComputers()
        {
            SqlConnection connection = new SqlConnection("Server=tcp:civapi.database.windows.net,1433;Initial Catalog=civapi;User ID=civ_user;Password=Monday1330;");

            string query = "Select Number, AssembledYear, Building, RoomNo from Computer";
            SqlCommand command = new SqlCommand(query, connection);


            connection.Open();
            SqlDataReader result = command.ExecuteReader();


            List<CusComputer> sqlComputer = new List<CusComputer>();
            while (result.Read())
            {
                sqlComputer.Add(new CusComputer(Int32.Parse(result[0].ToString()), Int32.Parse(result[1].ToString()), result[2].ToString(), Int32.Parse(result[3].ToString())));
            }
            connection.Close();


            return sqlComputer;
        }

        // GET: api/Computers/5
        [ResponseType(typeof(Computer))]
        public IHttpActionResult GetComputer(int id)
        {
            Computer computer = db.Computers.Find(id);
            if (computer == null)
            {
                return NotFound();
            }

            return Ok(computer);
        }

        // PUT: api/Computers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComputer(int id, Computer computer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != computer.Number)
            {
                return BadRequest();
            }

            db.Entry(computer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Computers
        [ResponseType(typeof(Computer))]
        public IHttpActionResult PostComputer(Computer computer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Computers.Add(computer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ComputerExists(computer.Number))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = computer.Number }, computer);
        }

        // DELETE: api/Computers/5
        [ResponseType(typeof(Computer))]
        public IHttpActionResult DeleteComputer(int id)
        {
            Computer computer = db.Computers.Find(id);
            if (computer == null)
            {
                return NotFound();
            }

            db.Computers.Remove(computer);
            db.SaveChanges();

            return Ok(computer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComputerExists(int id)
        {
            return db.Computers.Count(e => e.Number == id) > 0;
        }
    }
}