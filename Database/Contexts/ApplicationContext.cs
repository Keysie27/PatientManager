using Microsoft.EntityFrameworkCore;
using SGP.Core.Domain.Entities;

namespace SGP.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<PruebaLab> PruebasLab { get; set; }
        public DbSet<ResultadoLab> ResultadosLab { get; set; }
        public DbSet<Cita> Citas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
            #region Tables
                modelBuilder.Entity<Usuario>().ToTable("Usuarios");
                modelBuilder.Entity<Paciente>().ToTable("Pacientes");
                modelBuilder.Entity<Medico>().ToTable("Medicos");
                modelBuilder.Entity<PruebaLab>().ToTable("PruebasLab");
                modelBuilder.Entity<ResultadoLab>().ToTable("ResultadosLab");
                modelBuilder.Entity<Cita>().ToTable("Citas");
            #endregion

            #region "Primary Keys"
                modelBuilder.Entity<Usuario>().HasKey(usuario => usuario.IdUsuario);
                modelBuilder.Entity<Paciente>().HasKey(paciente => paciente.IdPaciente);
                modelBuilder.Entity<Medico>().HasKey(medico => medico.IdMedico);
                modelBuilder.Entity<PruebaLab>().HasKey(pruebaLab => pruebaLab.IdPruebaLab);
                modelBuilder.Entity<ResultadoLab>().HasKey(resultadoLab => resultadoLab.IdResultadoLab);
                modelBuilder.Entity<Cita>().HasKey(cita => cita.IdCita);
            #endregion

            #region relationships
                #region Paciente/ResultadoLab
                    modelBuilder.Entity<Paciente>()
                        .HasMany<ResultadoLab>(paciente => paciente.ResultadosLab)
                        .WithOne(resultadoLab => resultadoLab.Paciente)
                        .HasForeignKey(resultadoLab => resultadoLab.IdPaciente)
                        .OnDelete(DeleteBehavior.Cascade);
                #endregion
                
                #region PruebaLab/ResultadoLab
                    modelBuilder.Entity<PruebaLab>()
                        .HasMany<ResultadoLab>(pruebaLab => pruebaLab.ResultadosLab)
                        .WithOne(resultadoLab => resultadoLab.PruebaLab)
                        .HasForeignKey(resultadoLab => resultadoLab.IdPruebaLab)
                        .OnDelete(DeleteBehavior.Cascade);
                #endregion

                #region Paciente/Cita
                    modelBuilder.Entity<Paciente>()
                        .HasMany<Cita>(paciente => paciente.Citas)
                        .WithOne(cita => cita.Paciente)
                        .HasForeignKey(cita => cita.IdPaciente)
                        .OnDelete(DeleteBehavior.Cascade);
                #endregion
            
                #region Medico/Cita
                    modelBuilder.Entity<Medico>()
                        .HasMany<Cita>(medico => medico.Citas)
                        .WithOne(cita => cita.Medico)
                        .HasForeignKey(cita => cita.IdMedico)
                        .OnDelete(DeleteBehavior.Cascade);
                #endregion
            #endregion

            #region "Propertys configurations"
                #region Usuario
                    modelBuilder.Entity<Usuario>().Property(usuario => usuario.Nombre)
                        .IsRequired()
                        .HasMaxLength(50);
                    modelBuilder.Entity<Usuario>().Property(usuario => usuario.Apellido)
                        .IsRequired()
                        .HasMaxLength(100);
                    modelBuilder.Entity<Usuario>().Property(usuario => usuario.NombreUsuario)
                        .IsRequired()
                        .HasMaxLength(30);
                    modelBuilder.Entity<Usuario>().Property(usuario => usuario.Correo)
                        .IsRequired();
                    modelBuilder.Entity<Usuario>().Property(usuario => usuario.Contraseña)
                        .IsRequired();
                    modelBuilder.Entity<Usuario>().Property(usuario => usuario.TipoUsuario)
                        .IsRequired();

                    modelBuilder.Entity<Usuario>().HasIndex(usuario => usuario.NombreUsuario)
                        .IsUnique();
                    modelBuilder.Entity<Usuario>().HasIndex(usuario => usuario.Correo)
                        .IsUnique();
                #endregion

                #region Paciente
                    modelBuilder.Entity<Paciente>().Property(paciente => paciente.Nombre)
                        .IsRequired()
                        .HasMaxLength(50);
                    modelBuilder.Entity<Paciente>().Property(paciente => paciente.Apellido)
                        .IsRequired()
                        .HasMaxLength(100);
                    modelBuilder.Entity<Paciente>().Property(paciente => paciente.Telefono)
                        .IsRequired()
                        .HasMaxLength(20);
                    modelBuilder.Entity<Paciente>().Property(paciente => paciente.Direccion)
                        .IsRequired()
                        .HasMaxLength(150);
                    modelBuilder.Entity<Paciente>().Property(paciente => paciente.FechaNacimiento)
                        .IsRequired();
                    modelBuilder.Entity<Paciente>().Property(paciente => paciente.Cedula)
                        .IsRequired()
                        .HasMaxLength(15);
                    modelBuilder.Entity<Paciente>().Property(paciente => paciente.FotoUrl)
                        .IsRequired(false);
                    modelBuilder.Entity<Paciente>().Property(paciente => paciente.Fuma)
                        .IsRequired();
                    modelBuilder.Entity<Paciente>().Property(paciente => paciente.Alergias)
                        .IsRequired();

                    modelBuilder.Entity<Paciente>().HasIndex(paciente => paciente.Cedula)
                        .IsUnique();
                #endregion

                #region Medico
                    modelBuilder.Entity<Medico>().Property(medico => medico.Nombre)
                        .IsRequired()
                        .HasMaxLength(50);
                    modelBuilder.Entity<Medico>().Property(medico => medico.Apellido)
                        .IsRequired()
                        .HasMaxLength(100);
                    modelBuilder.Entity<Medico>().Property(medico => medico.Telefono)
                        .IsRequired()
                        .HasMaxLength(15);
                    modelBuilder.Entity<Medico>().Property(medico => medico.Correo)
                        .IsRequired();
                    modelBuilder.Entity<Medico>().Property(medico => medico.Cedula)
                        .IsRequired()
                        .HasMaxLength(15);
                    modelBuilder.Entity<Medico>().Property(medico => medico.FotoUrl)
                        .IsRequired(false);

                    modelBuilder.Entity<Medico>().HasIndex(medico => medico.Correo)
                        .IsUnique();
                    modelBuilder.Entity<Medico>().HasIndex(medico => medico.Cedula)
                        .IsUnique();
                #endregion

                #region PruebaLab
                    modelBuilder.Entity<PruebaLab>().Property(pruebaLab => pruebaLab.Nombre)
                        .IsRequired()
                        .HasMaxLength(100);
                #endregion
            
                #region ResultadoLab
                    modelBuilder.Entity<ResultadoLab>().Property(resultadoLab => resultadoLab.Estado)
                        .HasMaxLength(15)
                        .IsRequired(false);
                    modelBuilder.Entity<ResultadoLab>().Property(resultadoLab => resultadoLab.Resultado)
                        .HasMaxLength(100)
                        .IsRequired(false);
                    modelBuilder.Entity<ResultadoLab>().Property(resultadoLab => resultadoLab.IdPaciente)
                        .IsRequired();
                    modelBuilder.Entity<ResultadoLab>().Property(resultadoLab => resultadoLab.IdPruebaLab)
                        .IsRequired();
                #endregion

                #region Cita
                    modelBuilder.Entity<Cita>().Property(cita => cita.Estado)
                        .HasMaxLength(30)
                        .IsRequired(false);
                    modelBuilder.Entity<Cita>().Property(cita => cita.Fecha)
                        .IsRequired();
                    modelBuilder.Entity<Cita>().Property(cita => cita.Hora)
                        .IsRequired();
                    modelBuilder.Entity<Cita>().Property(cita => cita.Causa)
                        .IsRequired()
                        .HasMaxLength(200);
                    modelBuilder.Entity<Cita>().Property(cita => cita.IdPaciente)
                        .IsRequired();
                    modelBuilder.Entity<Cita>().Property(cita => cita.IdMedico)
                        .IsRequired();
                #endregion
            #endregion
        }
    }
}
