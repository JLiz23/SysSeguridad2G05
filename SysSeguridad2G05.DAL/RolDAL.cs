using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SysSeguridad2G05.EN;

namespace SysSeguridad2G05.DAL
{
    public class RolDAL
    {
        public static async Task<int> CrearAsync(Rol pRol)
        {
            int result = 0;
            using (var dbContext = new DBContexto())
            {
                dbContext.Rol.Add(pRol);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Rol pRol)
        {
            int result = 0;
            using (var dbContext = new DBContexto())
            {
                var rol = await dbContext.Rol.FirstOrDefaultAsync(s => s.IdRol == pRol.IdRol);//Select
                rol.Nombre = pRol.Nombre;
                dbContext.Rol.Update(rol);
                result = await dbContext.SaveChangesAsync();

            }
            return result;
        }

        public static async Task<int> EliminarAsync(Rol pIdRol)
        {
            int result = 0;
            using (var dbContext = new DBContexto())
            {
                var rol = await dbContext.Rol.FirstOrDefaultAsync(s => s.IdRol == pIdRol.IdRol);
                dbContext.Rol.Remove(rol);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
