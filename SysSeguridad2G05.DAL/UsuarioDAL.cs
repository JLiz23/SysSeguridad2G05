using SysSeguridad2G05.EN;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SysSeguridad2G05.DAL
{
    public class UsuarioDAL
    {
        private static void EncriptarMD5(Usuario pUsuario)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(
                   pUsuario.Password));
                var strEncriptar = "";
                for (int i = 0; i < result.Length; i++)
                    strEncriptar += result[i].ToString("x2").ToLower();
                pUsuario.Password = strEncriptar;

            }
        }
        private static async Task<bool> ExisteLogin(Usuario pUsuario, DBContexto pDContexto)
        {
            bool result = false;
            var LoginUsuarioExiste = await pDContexto.Usuario.FirstOrDefaultAsync(u => u.Login == pUsuario.Login && u.IdUsuario != pUsuario.IdUsuario);
            if (LoginUsuarioExiste != null && LoginUsuarioExiste.IdUsuario > 0 && LoginUsuarioExiste.Login == pUsuario.Login)
                result = true;
            {
                return result;
            }
        }

        #region "CRUD"

        public static async Task<int> GuardarAsync(Usuario pUsuario)
        {
            int result = 0;
            try
            {
                using (var DContexto = new DBContexto())
                {

                    bool existeLogin = await ExisteLogin(pUsuario, DContexto);
                    if (existeLogin == false)
                    {
                        EncriptarMD5(pUsuario);
                        DContexto.Add(pUsuario);
                        result = await DContexto.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("El Login ya existe");
                    }
                }


            }
            catch (Exception ex)
            {
                result = 0;
                throw new Exception(ex.Message);
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Usuario pUsuario)
        {
            int result = 0;
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    bool existeLogin = await ExisteLogin(pUsuario, dbContexto);
                    if (existeLogin == false)
                    {
                        var usuario = await dbContexto.Usuario.FirstOrDefaultAsync(u => u.IdUsuario == pUsuario.IdUsuario);
                        usuario.IdRol = pUsuario.IdRol;
                        usuario.Nombre = pUsuario.Nombre;
                        usuario.Apellido = pUsuario.Apellido;
                        usuario.Login = pUsuario.Login;
                        usuario.status = pUsuario.status;
                        dbContexto.Update(usuario);
                        result = await dbContexto.SaveChangesAsync();
                    }
                    else
                        throw new Exception("El Login ya existe");
                }
            }
            catch (Exception ex)
            {
                result = 0;
                throw new Exception(ex.Message);
            }
            return result;
        }
        #endregion
    }
}

