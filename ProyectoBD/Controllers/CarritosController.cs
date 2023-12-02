﻿using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProyectoBD.Models;
using System.Collections.Generic;

namespace ProyectoBD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class CarritosController : Controller
    {
        MySqlConnection db;
        public CarritosController(MySqlConnection _db)
        {
            db = _db;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> postCarritoJuego([FromBody] Carritos.postCarritoJuego model)
        {

            await db.QueryFirstOrDefaultAsync("Call CarritosInsertarJuego(@idUsuario, @idJuego);", model);
            return Ok(true);

        }
        

        [HttpPost("[action]")]
        public async Task<IActionResult> postVerJuegosEnCarrito([FromBody] Carritos.postCarritoJuego model)
        {

            IEnumerable < Juegos.Juego > juegos = await db.QueryAsync<Juegos.Juego>("Call CarritosObtenerJuegos(@id);", model);
            return Ok(juegos);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> postEliminarCarritoJuego([FromBody] Carritos.postEliminarCarritoJuego model)
        {

            await db.QueryFirstOrDefaultAsync("Call CarritosEliminarJuego(@idUsuario, @idJuego);", model);
            return Ok();
        }
    }
}