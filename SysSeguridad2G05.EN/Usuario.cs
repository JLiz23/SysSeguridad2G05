using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SysSeguridad2G05.EN
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        [ForeignKey("Rol")]
        [Required(ErrorMessage ="Rol es obligatorio")]
        [Display(Name ="Rol")]
        public int IdRol { get; set; }
        [Required(ErrorMessage = "Nombre de usuario es obligatorio")]
        [StringLength(40,ErrorMessage = "Maximo 40 caracteres")]
        [Display(Name ="Nombre Usuario")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Apellido de usuario es obligatorio")]
        [StringLength(40, ErrorMessage = "Maximo 40 caracteres")]
        [Display(Name = "Apellido Usuario")]
        public string Apellido{ get; set; }
        [Required(ErrorMessage = "Login de Usuario obligatorio")]
        [StringLength(200, ErrorMessage = "Maximo 200 caracteres")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password obligatorio")]
        [StringLength(40, ErrorMessage = "Maximo 40 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Estado es obligatorio")]
        public byte status { get; set; }
        [Display(Name ="Fecha Registro")]
        public DateTime FechaRegistro { get; set; }
        public Rol Rol { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Confirmar Password es obligatorio")]
        [StringLength(40, ErrorMessage = "Maximo 40 caracteres")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password y confirmar password debe ser iguales")]
        [Display(Name = "Confirmar Password")]
        public string ConfirmarPassword_aux { get; set; }

    }
     public enum statusUsuario
    {
        Activo = 1,
        Inactivo = 2
       
    }
}
