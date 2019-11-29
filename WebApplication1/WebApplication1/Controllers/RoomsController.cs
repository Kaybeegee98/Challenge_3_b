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
    public class RoomsController : ApiController
    {
        private civapiEntities2 db = new civapiEntities2();

        // GET: api/Rooms
        public IEnumerable<CusRoom> GetRooms()
        {
            SqlConnection connection = new SqlConnection("Server=tcp:civapi.database.windows.net,1433;Initial Catalog=civapi;User ID=civ_user;Password=Monday1330;");

            string query = "Select Building, RoomNo, Capacity from Room";
            SqlCommand command = new SqlCommand(query, connection);


            connection.Open();
            SqlDataReader result = command.ExecuteReader();


            List<CusRoom> sqlRoom = new List<CusRoom>();
            while (result.Read())
            {
                sqlRoom.Add(new CusRoom(result[0].ToString(), Int32.Parse(result[1].ToString()), Int32.Parse(result[2].ToString())));
            }
            connection.Close();


            return sqlRoom;
        }


        // GET: api/Rooms/Unused        
        [HttpGet]
        [ActionName("Unused")]
        [Route("api/Rooms/Unused")]
        public IEnumerable<CusRoom> GetUnused()
        {
            SqlConnection connection = new SqlConnection("Server=tcp:civapi.database.windows.net,1433;Initial Catalog=civapi;User ID=civ_user;Password=Monday1330;");

            string query = "Select * from Room where Concat(Building, RoomNo) Not IN (SELECT Distinct(Concat(Building, RoomNo)) from Class)";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader result = command.ExecuteReader();


            List<CusRoom> sqlRoom = new List<CusRoom>();
            while (result.Read())
            {
                sqlRoom.Add(new CusRoom(result[0].ToString(), Int32.Parse(result[1].ToString()), Int32.Parse(result[2].ToString())));
            }
            connection.Close();


            return sqlRoom;

        }

        // GET: api/Rooms/Used       
        [HttpGet]
        [ActionName("Used")]
        [Route("api/Rooms/Used")]
        public IEnumerable<CusRoom> GetUsed()
        {
            SqlConnection connection = new SqlConnection("Server=tcp:civapi.database.windows.net,1433;Initial Catalog=civapi;User ID=civ_user;Password=Monday1330;");

            string query = "Select * from Room where Concat(Building, RoomNo) IN (SELECT Distinct(Concat(Building, RoomNo)) from Room)";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader result = command.ExecuteReader();


            List<CusRoom> sqlRoom = new List<CusRoom>();
            while (result.Read())
            {
                    sqlRoom.Add(new CusRoom(result[0].ToString(), Int32.Parse(result[1].ToString()), Int32.Parse(result[2].ToString())));
            }
            connection.Close();


            return sqlRoom;

        }

        // GET: api/Rooms/Used       
        [HttpGet]
        [ActionName("Computers")]
        [Route("api/Rooms/Computers")]
        public IEnumerable<CusRoom> GetComputers()
        {
            SqlConnection connection = new SqlConnection("Server=tcp:civapi.database.windows.net,1433;Initial Catalog=civapi;User ID=civ_user;Password=Monday1330;");

            string query = "Select * from Room where Concat(Building, RoomNo) IN (SELECT Distinct(Concat(Building, RoomNo)) from Computer)";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader result = command.ExecuteReader();


            List<CusRoom> sqlRoom = new List<CusRoom>();
            while (result.Read())
            {
                sqlRoom.Add(new CusRoom(result[0].ToString(), Int32.Parse(result[1].ToString()), Int32.Parse(result[2].ToString())));
            }
            connection.Close();


            return sqlRoom;

        }

        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoom(string id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.Building)
            {
                return BadRequest();
            }

            db.Entry(room).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // POST: api/Rooms
        [ResponseType(typeof(Room))]
        public IHttpActionResult PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rooms.Add(room);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RoomExists(room.Building))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = room.Building }, room);
        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult DeleteRoom(string id)
        {
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            db.Rooms.Remove(room);
            db.SaveChanges();

            return Ok(room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(string id)
        {
            return db.Rooms.Count(e => e.Building == id) > 0;
        }
    }
}